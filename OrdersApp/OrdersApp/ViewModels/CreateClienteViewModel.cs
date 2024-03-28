using OrdersApp.Modelos;
using OrdersApp.Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace OrdersApp.ViewModels
{
    public class CreateClienteViewModel : INotifyPropertyChanged
    {
        private VendedorModel _vendedor;

        private string _numeroDocumento;
        private string _nombre;
        private string _nombreComercial;
        private string _direccion;

        private ClientesService _clientesService;
        public ICommand RegistrarCommand { get; private set; }

        public VendedorModel Vendedor
        {
            get { return _vendedor; }
            set { _vendedor = value; OnPropertyChanged(nameof(Vendedor)); }
        }

        public string NumeroDocumento
        {
            get => _numeroDocumento;
            set { _numeroDocumento = value; OnPropertyChanged(nameof(NumeroDocumento)); }
        }

        public string Nombre
        {
            get => _nombre;
            set { _nombre = value; OnPropertyChanged(nameof(Nombre)); }
        }

        public string NombreComercial
        {
            get => _nombreComercial;
            set { _nombreComercial = value; OnPropertyChanged(nameof(NombreComercial)); }
        }

        public string Direccion
        {
            get => _direccion;
            set { _direccion = value; OnPropertyChanged(nameof(Direccion)); }
        }

        public CreateClienteViewModel(VendedorModel vendedor)
        {
            Vendedor = vendedor;

            _clientesService = new ClientesService();
            RegistrarCommand = new Command(Registrar);
        }

        private async void Registrar()
        {
            NuevoClienteModel nuevoCliente = new NuevoClienteModel();
            nuevoCliente.CodVen = Int32.Parse(Vendedor.Codigo);

            if (NumeroDocumento != "" && Nombre != "" && NombreComercial != "" && Direccion != "")
            {
                nuevoCliente.NroDoc = NumeroDocumento;
                nuevoCliente.Nombre = Nombre;
                nuevoCliente.Comercial = NombreComercial;
                nuevoCliente.Direccion = Direccion;

                bool registrado = await _clientesService.RegistrarCliente(nuevoCliente);
                if (registrado)
                {
                    await Application.Current.MainPage.DisplayAlert("Éxito", "Se envió solicitud de registro de cliente", "OK");
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Hubo un problema al enviar la solicitud de registro de cliente", "OK");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Ingresar todos los datos", "OK");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
