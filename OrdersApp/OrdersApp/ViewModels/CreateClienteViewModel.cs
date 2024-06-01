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

        private string _documento;
        private string _comercial;
        private string _domicilio;
        private string _estado;

        public int TextoBusqueda { get; set; }
        private SunatService _sunatService;
        public ICommand BuscarCommand { get; set; }

        public string Documento
        {
            get => _documento;
            set { _documento = value; OnPropertyChanged(nameof(Documento)); }
        }

        public string Comercial
        {
            get => _comercial;
            set { _comercial = value; OnPropertyChanged(nameof(Comercial)); }
        }

        public string Domicilio
        {
            get => _domicilio;
            set { _domicilio = value; OnPropertyChanged(nameof(Domicilio)); }
        }

        public string Estado
        {
            get => _estado;
            set { _estado = value; OnPropertyChanged(nameof(Estado)); }
        }

        public CreateClienteViewModel(VendedorModel vendedor)
        {
            Vendedor = vendedor;

            _clientesService = new ClientesService();
            RegistrarCommand = new Command(Registrar);

            _sunatService = new SunatService();
            BuscarCommand = new Command(async () => await BuscarDocumento());
        }

        private async Task BuscarDocumento()
        {
            if (NumeroDocumento.Length == 8)
            {
                var cliente = await _sunatService.ConsultarDni(NumeroDocumento);
                if (cliente != null)
                {
                    if (cliente.Documento == null)
                    {
                        await Application.Current.MainPage.DisplayAlert("Mensaje", "No se encontraron datos en la SUNAT.", "Aceptar");
                    }
                    Documento = cliente.Documento;
                    Comercial = cliente.Comercial;
                    Domicilio = cliente.Domicilio;
                    Estado = cliente.Estado;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Mensaje", "No se encontraron datos en la SUNAT.", "Aceptar");
                }
            }
            else
            {
                var cliente = await _sunatService.ConsultarRuc(NumeroDocumento);
                if (cliente != null)
                {
                    if (cliente.Documento == null)
                    {
                        await Application.Current.MainPage.DisplayAlert("Mensaje", "No se encontraron datos en la SUNAT.", "Aceptar");
                    }
                    Documento = cliente.Documento;
                    Comercial = cliente.Comercial;
                    Domicilio = cliente.Domicilio;
                    Estado = cliente.Estado;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Mensaje", "No se encontraron datos en la SUNAT.", "Aceptar");
                }
            }
        }

        private async void Registrar()
        {
            NuevoClienteModel nuevoCliente = new NuevoClienteModel();
            nuevoCliente.CodVen = Int32.Parse(Vendedor.Codigo);

            if (Documento != "" && Comercial != "" && NombreComercial != "" && Domicilio != "")
            {
                nuevoCliente.NroDoc = Documento;
                nuevoCliente.Nombre = Comercial;
                nuevoCliente.Comercial = NombreComercial;
                nuevoCliente.Direccion = Domicilio;

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
