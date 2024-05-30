using OrdersApp.Modelos;
using OrdersApp.Servicios;
using OrdersApp.Vistas;
using OrdersApp.Vistas.Productos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace OrdersApp.ViewModels
{
    public class SelectProductoViewModel : INotifyPropertyChanged
    {
        public string TextoBusqueda { get; set; }
        public ObservableCollection<ProductoModel> Productos { get; set; }
        private ProductosService _productosService;

        private PedidosService _pedidosService;
        public ICommand RegistrarPedidoCommand { get; }
        public ICommand CalcularTotalCommand { get; private set; }

        public ICommand BuscarCommand { get; set; }
        public ICommand SeleccionarProductoCommand { get; private set; }
        public ICommand EditarProductoCommand { get; private set; }

        private VendedorModel _vendedor;
        private ClienteModel _cliente;
        private PedidoModel _pedido;

        private string _observacion;

        private double _latitud;
        private double _longitud;

        private string _ubicacion;
        public string Ubicacion
        {
            get { return _ubicacion; }
            set { _ubicacion = value; OnPropertyChanged(nameof(Ubicacion)); }
        }

        public VendedorModel Vendedor
        {
            get { return _vendedor; }
            set { _vendedor = value; OnPropertyChanged(nameof(Vendedor)); }
        }

        public ClienteModel Cliente
        {
            get { return _cliente; }
            set { _cliente = value; OnPropertyChanged(nameof(Cliente)); }
        }

        public PedidoModel Pedido
        {
            get { return _pedido; }
            set { _pedido = value; OnPropertyChanged(nameof(Pedido)); }
        }

        public string Observacion
        {
            get { return _observacion; }
            set { _observacion = value; OnPropertyChanged(nameof(Observacion)); }
        }

        public ObservableCollection<DetallePedidoModel> Detalles { get; set; }

        public SelectProductoViewModel(VendedorModel vendedor, ClienteModel cliente)
        {
            Vendedor = vendedor;
            Cliente = cliente;
            Pedido = new PedidoModel();

            Detalles = new ObservableCollection<DetallePedidoModel>();
            _productosService = new ProductosService();

            _pedidosService = new PedidosService();
            RegistrarPedidoCommand = new Command(async () => await RegistrarPedido());

            Productos = new ObservableCollection<ProductoModel>();
            BuscarCommand = new Command(async () => await BuscarProductos());

            SeleccionarProductoCommand = new Command<ProductoModel>(async (producto) => await SeleccionarProducto(producto));
            EditarProductoCommand = new Command<DetallePedidoModel>(async (producto) => await SeleccionarItem(producto));

            ObtenerUbicacion();
            SetDetalles();
            CalcularTotalCommand = new Command(CalcularTotal);
        }

        private async Task BuscarProductos()
        {
            var productos = await _productosService.BuscarProductos(TextoBusqueda);
            if (productos != null)
            {
                Productos.Clear();
                foreach (var producto in productos)
                {
                    Productos.Add(producto);
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Mensaje", "No se encontraron productos con el parámetro de búsqueda.", "Aceptar");
            }
        }

        private async Task SeleccionarProducto(ProductoModel producto)
        {
            var dialog = new SeleccionProductoDialog(producto);

            dialog.DetallePedidoAgregado += (sender, detalle) =>
            {
                Detalles.Add(detalle);
                CalcularTotal();
            };

            await Application.Current.MainPage.Navigation.PushModalAsync(dialog);
        }

        private async Task SeleccionarItem(DetallePedidoModel producto)
        {
            var dialog = new UpdateProductoDialog(producto);
            dialog.DetallePedidoEditado += (sender, detalle) =>
            {
                int index = Detalles.IndexOf(producto);
                Detalles.RemoveAt(index);
                Detalles.Insert(index, detalle);
                CalcularTotal();
            };
            await Application.Current.MainPage.Navigation.PushModalAsync(dialog);
        }

        public async void ObtenerUbicacion()
        {
            try
            {
                var location = await Geolocation.GetLocationAsync();
                if (location != null)
                {
                    _latitud = location.Latitude;
                    _longitud = location.Longitude;
                    Pedido.Latitud = _latitud;
                    Pedido.Longitud = _longitud;

                    var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);
                    var placemark = placemarks?.FirstOrDefault();

                    if (placemark.SubLocality != null)
                    {
                        Ubicacion = placemark.SubLocality + " / " + placemark.Locality;
                    }
                    else
                    {
                        Ubicacion = placemark.Thoroughfare + " / " + placemark.Locality;
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "No se puede obtener ubicación", "Aceptar");
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Geolocalización no soportada", "Aceptar");
            }

            catch (PermissionException pEx)
            {

                await Application.Current.MainPage.DisplayAlert("Error", "Permiso denegado para acceder a la geolocalización", "Aceptar");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Error al obtener la ubicación", "Aceptar");
            }
        }

        private decimal _subtotal;
        public decimal Subtotal
        {
            get { return _subtotal; }
            set { _subtotal = value; OnPropertyChanged(nameof(Subtotal)); }
        }

        private decimal _igv;
        public decimal Igv
        {
            get { return _igv; }
            set { _igv = value; OnPropertyChanged(nameof(Igv)); }
        }

        private decimal _total;
        public decimal Total
        {
            get { return _total; }
            set { _total = value; OnPropertyChanged(nameof(Total)); }
        }

        private void CalcularTotal()
        {
            Total = Detalles.Sum(d => d.SubtotalLb);
            Igv = Total * 0.18m;
            Subtotal = Total - Igv;
        }

        private void SetDetalles()
        {
            Pedido.CodCli = Cliente.Codigo;
            Pedido.CodVen = Int32.Parse(Vendedor.Codigo);
        }

        private async Task RegistrarPedido()
        {
            if (Detalles == null || Detalles.Count == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe agregar productos", "OK");
                return;
            }

            if (Pedido.IdDirCli == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe seleccionar una dirección", "OK");
                return;
            }

            if (Observacion == null) { Observacion = ""; }
            Pedido.Observ = Observacion;

            Pedido.DetPedidos.Clear();
            Pedido.DetPedidos = new List<DetallePedidoModel>(Detalles);
            bool enviado = await _pedidosService.EnviarPedido(Pedido);

            if (enviado)
            {
                await Application.Current.MainPage.DisplayAlert("Éxito", "El pedido se registró correctamente", "OK");
                await Application.Current.MainPage.Navigation.PopAsync();
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Hubo un problema al enviar el pedido", "OK");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
