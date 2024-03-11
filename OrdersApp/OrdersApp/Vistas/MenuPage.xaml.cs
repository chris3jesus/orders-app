using OrdersApp.ViewModels;
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
        public MenuPage(string codigoVendedor)
        {
            InitializeComponent();
            BindingContext = new MenuPageViewModel(codigoVendedor);
        }

        private async void OnPedidosButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PedidosListPage());
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