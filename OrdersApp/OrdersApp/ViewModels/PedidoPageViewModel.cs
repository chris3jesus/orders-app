using OrdersApp.Modelos;
using OrdersApp.Servicios;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace OrdersApp.ViewModels
{
    public class PedidoPageViewModel : INotifyPropertyChanged
    {
        private PedidosModel _pedido;
        private ClienteModel _cliente;

        private ProductosService _productosService;
        private ClientesService _clientesService;
        public ObservableCollection<DetallePedidoModel> Detalles { get; set; }

        private string _direccion;
        private DateTime _fecha;

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

        public string Direccion
        {
            get { return _direccion; }
            set { _direccion = value; OnPropertyChanged(nameof(Direccion)); }
        }

        public DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; OnPropertyChanged(nameof(Fecha)); }
        }

        public PedidoPageViewModel(PedidosModel pedido)
        {
            Pedido = pedido;
            _productosService = new ProductosService();
            Detalles = new ObservableCollection<DetallePedidoModel>();
            _clientesService = new ClientesService();
            Cliente = new ClienteModel();
            CargarDetallePedidoAsync();
            CargarDatosClienteAsync();
            CargarDireccionAsync();
            CargarFechaAsync();
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
                    NomPres = producto[0].Descripcion + " - " + producto[0].Presentacion,
                    PscDsc = producto[0].Precio * (1 - detalle.Dscto1) * (1 - detalle.Dscto2)
                };
                Detalles.Add(detalleRes);
            }
        }

        private async void CargarDatosClienteAsync()
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

        private async void CargarDireccionAsync()
        {
            await Task.Delay(100);
            foreach (var direccion in Cliente.Direcciones)
            {
                if (direccion.Codigo == Pedido.IdDirCli)
                {
                    Direccion = direccion.Direccion;
                }
            }
        }

        private async void CargarFechaAsync()
        {
            await Task.Delay(100);
            if (Pedido.FechaProc != null)
            {
                Fecha = (DateTime)Pedido.FechaProc;
            }
            if (Pedido.FechaEdit != null)
            {
                Fecha = (DateTime)Pedido.FechaEdit;
            }
            else
            {
                Fecha = Pedido.FechaReg;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
