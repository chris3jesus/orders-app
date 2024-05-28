using OrdersApp.Modelos;
using OrdersApp.Servicios;
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
    public class UpdatePedidoViewModel : INotifyPropertyChanged
    {
        private PedidosModel _pedido;
        private ClienteModel _cliente;

        public string TextoBusqueda { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;

        private double _latitud;
        private double _longitud;
        private string _ubicacion;

        public ObservableCollection<DetallePedidoModel> Detalles { get; set; }
        public ObservableCollection<ProductoModel> Productos { get; set; }

        private ClientesService _clientesService;
        private PedidosService _pedidosService;
        private ProductosService _productosService;

        public ICommand BuscarCommand { get; set; }
        public ICommand SeleccionarProductoCommand { get; private set; }
        public ICommand ActualizarPedidoCommand { get; }
        public ICommand EditarProductoCommand { get; private set; }
        public ICommand CalcularTotalCommand { get; private set; }

        public PedidosModel Pedido
        {
            get { return _pedido; }
            set { _pedido = value; OnPropertyChanged(nameof(Pedido)); }
        }

        public ClienteModel Cliente
        {
            get { return _cliente; }
            set { _cliente = value; OnPropertyChanged(nameof(Cliente)); }
        }

        public string Ubicacion
        {
            get { return _ubicacion; }
            set { _ubicacion = value; OnPropertyChanged(nameof(Ubicacion)); }
        }

        public UpdatePedidoViewModel(PedidosModel pedido)
        {
            Pedido = pedido;
            Detalles = new ObservableCollection<DetallePedidoModel>();
            Cliente = new ClienteModel();

            _clientesService = new ClientesService();
            _productosService = new ProductosService();
            _pedidosService = new PedidosService();

            Productos = new ObservableCollection<ProductoModel>();
            BuscarCommand = new Command(async () => await BuscarProductos());

            SeleccionarProductoCommand = new Command<ProductoModel>(async (producto) => await SeleccionarProducto(producto));
            ActualizarPedidoCommand = new Command(async () => await ActualizarPedido());
            EditarProductoCommand = new Command<DetallePedidoModel>(async (producto) => await SeleccionarItem(producto));
            CargarDetallePedidoAsync();
            CargarDatosClienteAsync();
            ObtenerUbicacion();

            CalcularTotalCommand = new Command(CalcularTotal);
        }

        private async Task CargarDetallePedidoAsync()
        {
            foreach (var detalle in Pedido.DetPedidos)
            {
                var producto = await _productosService.BuscarProductos(detalle.IdProd.ToString());
                var detalleRes = new DetallePedidoModel
                {
                    IdProd = producto[0].Codigo,
                    Cantidad = detalle.Cantidad,
                    Dscto1 = detalle.Dscto1,
                    Dscto2 = detalle.Dscto2,
                    Producto = producto[0],
                    SubtotalLb = Math.Round(producto[0].Precio * (1 - detalle.Dscto1 / 100m) * (1 - detalle.Dscto2 / 100m) * detalle.Cantidad, 2),
                    NomPres = producto[0].Descripcion + " - " + producto[0].Presentacion,
                    PscDsc = Math.Round(producto[0].Precio * (1 - detalle.Dscto1 / 100m) * (1 - detalle.Dscto2 / 100m), 2)
                };
                Detalles.Add(detalleRes);
            }
            CalcularTotal();
        }

        private async Task CargarDatosClienteAsync()
        {
            var cliente = await _clientesService.BuscarClientes(Pedido.CodCli, "");
            Cliente = new ClienteModel
            {
                Codigo = cliente[0].Codigo,
                Documento = cliente[0].Documento,
                Razon = cliente[0].Razon,
                Nombre = cliente[0].Nombre,
                Comercial = cliente[0].Comercial,
                Direcciones = cliente[0].Direcciones,
                Vendedor = cliente[0].Vendedor
            };
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

        private async Task ActualizarPedido()
        {
            if (Detalles == null || Detalles.Count == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe agregar productos", "OK");
                return;
            }

            var detallesPedidoModel = Detalles.Select(detalle => new DetallesPedidoModel
            {
                IdProd = detalle.IdProd,
                Cantidad = detalle.Cantidad,
                Dscto1 = detalle.Dscto1,
                Dscto2 = detalle.Dscto2,
            }).ToList();

            Pedido.DetPedidos.Clear();
            Pedido.DetPedidos = new List<DetallesPedidoModel>(detallesPedidoModel);
            bool enviado = await _pedidosService.EditarPedido(Pedido);

            if (enviado)
            {
                await Application.Current.MainPage.DisplayAlert("Éxito", "El pedido se actualizó correctamente", "OK");
                await Application.Current.MainPage.Navigation.PopAsync();
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
