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
    public partial class PedidosMainPage : ContentPage
    {
        public PedidosMainPage(VendedorModel vendedor)
        {
            InitializeComponent();
            BindingContext = new PedidosMainPageViewModel(vendedor);
        }

        private async void OnListaButtonClicked(object sender, EventArgs e)
        {
            var pedidosMainPageViewModel = (PedidosMainPageViewModel)BindingContext;
            var vendedor = pedidosMainPageViewModel.Vendedor;
            await Navigation.PushAsync(new PedidosListPage(vendedor));
        }

        private async void OnNuevoButtonClicked(object sender, EventArgs e)
        {
            var pedidosMainPageViewModel = (PedidosMainPageViewModel)BindingContext;
            var vendedor = pedidosMainPageViewModel.Vendedor;
            await Navigation.PushAsync(new SelectClientePage(vendedor));
        }
    }
}