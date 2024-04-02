using OrdersApp.Modelos;
using OrdersApp.Servicios;
using OrdersApp.Vistas.Pedidos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace OrdersApp.ViewModels
{
    public class PedidosListViewModel : INotifyPropertyChanged
    {
        private VendedorModel _vendedor;

        public VendedorModel Vendedor
        {
            get { return _vendedor; }
            set { _vendedor = value; OnPropertyChanged(nameof(Vendedor)); }
        }

        private PedidosService _pedidosService;
        public ObservableCollection<PedidosModel> Pedidos { get; set; }

        private int _nPedidos;
        private decimal _total;

        public int NPedidos
        {
            get { return _nPedidos; }
            set { _nPedidos = value; OnPropertyChanged(nameof(NPedidos)); }
        }

        public decimal Total
        {
            get { return _total; }
            set { _total = value; OnPropertyChanged(nameof(Total)); }
        }

        public ICommand SeleccionarPedidoCommand { get; private set; }

        public PedidosListViewModel(VendedorModel vendedor)
        {
            Vendedor = vendedor;
            Pedidos = new ObservableCollection<PedidosModel>();
            _pedidosService = new PedidosService();
            CargarPedidos(Int32.Parse(vendedor.Codigo));
            SeleccionarPedidoCommand = new Command<PedidosModel>(async (pedido) => await SeleccionarPedido(pedido));
        }

        private async Task CargarPedidos(int vendedor)
        {
            var pedidos = await _pedidosService.ObtenerPedidos(vendedor);
            if (pedidos != null)
            {
                Pedidos = new ObservableCollection<PedidosModel>(pedidos);
                OnPropertyChanged(nameof(Pedidos));
            }
            CargarDatos(Pedidos);
        }

        private void CargarDatos(ObservableCollection<PedidosModel> Pedidos)
        {
            NPedidos = Pedidos.Count;
            decimal sumaTotales = 0;
            foreach (var pedido in Pedidos)
            {
                sumaTotales += pedido.Total;
            }
            Total = sumaTotales;

            Console.WriteLine($"Total de objetos: {NPedidos}");
            Console.WriteLine($"Suma de totales: {sumaTotales}");
        }

        private async Task SeleccionarPedido(PedidosModel pedidos)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new PedidoPage(pedidos));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
