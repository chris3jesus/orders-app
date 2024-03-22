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

        public MenuPageViewModel(VendedorModel vendedor)
        {
            WelcomeMessage = $"Bienvenido {vendedor.Nombre} ({vendedor.Codigo})";
        }
    }
}
