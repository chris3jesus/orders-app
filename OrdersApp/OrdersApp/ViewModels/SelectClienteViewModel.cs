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
    public class SelectClienteViewModel : INotifyPropertyChanged
    {
        private VendedorModel _vendedor;
        private ClienteModel _clienteSeleccionado;

        public string TextoBusqueda { get; set; }
        public ObservableCollection<ClienteModel> Clientes { get; set; }
        private ClientesService _clientesService;

        public ICommand BuscarCommand { get; set; }
        public ICommand SeleccionarClienteCommand { get; private set; }

        public VendedorModel Vendedor
        {
            get { return _vendedor; }
            set { _vendedor = value; OnPropertyChanged(nameof(Vendedor)); }
        }

        public ClienteModel ClienteSeleccionado
        {
            get { return _clienteSeleccionado; }
            set { _clienteSeleccionado = value; OnPropertyChanged(nameof(ClienteSeleccionado)); }
        }

        public SelectClienteViewModel()
        {
            _clientesService = new ClientesService();
            Clientes = new ObservableCollection<ClienteModel>();
            BuscarCommand = new Command(async () => await BuscarClientes());
            SeleccionarClienteCommand = new Command<ClienteModel>(async (cliente) => await SeleccionarCliente(cliente));
        }

        public SelectClienteViewModel(VendedorModel vendedor) : this()
        {
            Vendedor = vendedor;
        }

        private async Task BuscarClientes()
        {
            var clientes = await _clientesService.BuscarClientes(Int32.Parse(Vendedor.Codigo), TextoBusqueda);
            if (clientes != null)
            {
                Clientes.Clear();
                foreach (var cliente in clientes)
                {
                    Clientes.Add(cliente);
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Mensaje", "No se encontraron clientes del vendedor con el parámetro de búsqueda.", "Aceptar");
            }
        }

        private async Task SeleccionarCliente(ClienteModel cliente)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new SelectProductoPage(cliente, Vendedor));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
