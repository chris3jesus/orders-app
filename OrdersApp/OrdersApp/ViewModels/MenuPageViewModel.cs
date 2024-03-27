using OrdersApp.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace OrdersApp.ViewModels
{
    public class MenuPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _welcomeMessage;
        private VendedorModel _vendedor;

        public string WelcomeMessage
        {
            get { return _welcomeMessage; }
            set
            {
                if (_welcomeMessage != value)
                {
                    _welcomeMessage = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WelcomeMessage)));
                }
            }
        }

        public VendedorModel Vendedor
        {
            get { return _vendedor; }
            set
            {
                if (_vendedor != value)
                {
                    _vendedor = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Vendedor)));
                }
            }
        }

        public MenuPageViewModel(VendedorModel vendedor)
        {
            Vendedor = vendedor;
            WelcomeMessage = $"Bienvenido {vendedor.Nombre} ({vendedor.Codigo})";
        }
    }
}
