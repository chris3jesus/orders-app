using OrdersApp.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace OrdersApp.ViewModels
{
    public class PedidosMainPageViewModel : INotifyPropertyChanged
    {
        private VendedorModel _vendedor;

        public VendedorModel Vendedor
        {
            get { return _vendedor; }
            set { _vendedor = value; OnPropertyChanged(nameof(Vendedor)); }
        }

        public PedidosMainPageViewModel(VendedorModel vendedor)
        {
            Vendedor = vendedor;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
