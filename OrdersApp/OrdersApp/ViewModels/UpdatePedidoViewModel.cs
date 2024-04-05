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
using Xamarin.Forms;

namespace OrdersApp.ViewModels
{
    public class UpdatePedidoViewModel : INotifyPropertyChanged
    {
        private PedidosModel _pedido;
        public string TextoBusqueda { get; set; }

        public ObservableCollection<DetallePedidoModel> Detalles { get; set; }
        public ObservableCollection<ProductoModel> Productos { get; set; }

        private PedidosService _pedidosService;
        private ProductosService _productosService;

        public ICommand BuscarCommand { get; set; }
        public ICommand SeleccionarProductoCommand { get; private set; }
        public ICommand ActualizarPedidoCommand { get; }
        public ICommand CalcularTotalCommand { get; private set; }

        public PedidosModel Pedido
        {
            get { return _pedido; }
            set { _pedido = value; OnPropertyChanged(nameof(Pedido)); }
        }

        public UpdatePedidoViewModel(PedidosModel pedido)
        {
            Pedido = pedido;
            Detalles = new ObservableCollection<DetallePedidoModel>();

            _productosService = new ProductosService();
            _pedidosService = new PedidosService();

            Productos = new ObservableCollection<ProductoModel>();
            BuscarCommand = new Command(async () => await BuscarProductos());

            SeleccionarProductoCommand = new Command<ProductoModel>(async (producto) => await SeleccionarProducto(producto));
            ActualizarPedidoCommand = new Command(async () => await ActualizarPedido());
            CargarDetallePedidoAsync();

            CalcularTotalCommand = new Command(CalcularTotal);
        }

        private async void CargarDetallePedidoAsync()
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
                    SubtotalLb = producto[0].Precio * (1 - detalle.Dscto1) * (1 - detalle.Dscto2) * detalle.Cantidad,
                };
                Detalles.Add(detalleRes);
                Console.WriteLine(detalleRes);
            }
            CalcularTotal();
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
            if (Detalles == null || Detalles.Count == 0) { return; }

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
                await Application.Current.MainPage.DisplayAlert("Éxito", "El pedido se registró correctamente", "OK");
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
