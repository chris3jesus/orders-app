using OrdersApp.Modelos;
using OrdersApp.Servicios;
using OrdersApp.Vistas.Clientes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace OrdersApp.ViewModels
{
    public class ClientesListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string TextoBusqueda { get; set; }
        public ObservableCollection<ClienteModel> Clientes { get; set; }
        private ClientesService _clientesService;

        public ICommand BuscarCommand { get; set; }
        public ICommand SeleccionarClienteCommand { get; private set; }

        public ClientesListViewModel()
        {
            _clientesService = new ClientesService();
            Clientes = new ObservableCollection<ClienteModel>();
            BuscarCommand = new Command(async () => await BuscarClientes());
            SeleccionarClienteCommand = new Command<ClienteModel>(async (cliente) => await SeleccionarCliente(cliente));
        }

        private VendedorModel _vendedor;

        public VendedorModel Vendedor
        {
            get { return _vendedor; }
            set { _vendedor = value; OnPropertyChanged(nameof(Vendedor)); }
        }

        public ClientesListViewModel(VendedorModel vendedor) : this()
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
        }

        private async Task SeleccionarCliente(ClienteModel cliente)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ClientePage(cliente));
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
