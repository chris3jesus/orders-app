using OrdersApp.Modelos;
using OrdersApp.ViewModels;
using OrdersApp.Vistas.Pedidos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OrdersApp.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        public MenuPage(VendedorModel vendedor)
        {
            InitializeComponent();
            BindingContext = new MenuPageViewModel(vendedor);
        }

        private async void OnPedidosButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PedidosMainPage());
        }

        private async void OnClientesButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ClientesListPage());
        }

        private async void OnProductosButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProductosListPage());
        }
    }
}