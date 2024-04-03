using OrdersApp.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace OrdersApp.ViewModels
{
    public class PedidoPageViewModel : INotifyPropertyChanged
    {
        private PedidosModel _pedido;

        public PedidosModel Pedido
        {
            get { return _pedido; }
            set { _pedido = value; OnPropertyChanged(nameof(Pedido)); }
        }

        public PedidoPageViewModel(PedidosModel pedido)
        {
            Pedido = pedido;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
