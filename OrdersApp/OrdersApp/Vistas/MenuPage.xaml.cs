﻿using OrdersApp.Modelos;
using OrdersApp.ViewModels;
using OrdersApp.Vistas.Avances;
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
            var menuPageViewModel = (MenuPageViewModel)BindingContext;
            var vendedor = menuPageViewModel.Vendedor;
            await Navigation.PushAsync(new PedidosMainPage(vendedor));
        }

        private async void OnClientesButtonClicked(object sender, EventArgs e)
        {
            var menuPageViewModel = (MenuPageViewModel)BindingContext;
            var vendedor = menuPageViewModel.Vendedor;
            await Navigation.PushAsync(new ClientesListPage(vendedor));
        }

        private async void OnProductosButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProductosListPage());
        }

        private async void OnAvancesButtonClicked(object sender, EventArgs e)
        {
            var menuPageViewModel = (MenuPageViewModel)BindingContext;
            var vendedor = menuPageViewModel.Vendedor;

            if (vendedor.Supervisor)
            {
                await Navigation.PushAsync(new SuperPage(vendedor));
            }
            else
            {
                await Navigation.PushAsync(new AvancePage(vendedor));
            }
        }
    }
}