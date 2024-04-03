using OrdersApp.Modelos;
using OrdersApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OrdersApp.Vistas.Pedidos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PedidoPage : ContentPage
    {
        public PedidoPage(PedidosModel pedidos)
        {
            InitializeComponent();
            BindingContext = new PedidoPageViewModel(pedidos);
        }

        private async void OnUpdateButtonClicked(object sender, EventArgs e)
        {
            var pedidoPageViewModel = (PedidoPageViewModel)BindingContext;
            var pedido = pedidoPageViewModel.Pedido;
            await Navigation.PushAsync(new UpdatePedidoPage(pedido));
        }
    }
}