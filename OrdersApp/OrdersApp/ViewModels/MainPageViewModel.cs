using OrdersApp.Modelos;
using OrdersApp.Servicios;
using OrdersApp.Vistas;
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
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private string _codigo;
        private string _clave;
        private VendedorModel _vendedor;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Codigo
        {
            get { return _codigo; }
            set { _codigo = value; OnPropertyChanged(); }
        }

        public string Clave
        {
            get { return _clave; }
            set { _clave = value; OnPropertyChanged(); }
        }

        public VendedorModel Vendedor
        {
            get { return _vendedor; }
            set { _vendedor = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; }

        public MainPageViewModel()
        {
            LoginCommand = new Command(async () => await LoginAsync());
        }

        private async Task LoginAsync()
        {
            try
            {
                var loginService = new LoginService();
                var vendedor = await loginService.LoginAsync(Codigo, Clave);
                Vendedor = vendedor;
                await Application.Current.MainPage.Navigation.PushAsync(new MenuPage(Vendedor));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Aceptar");
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
