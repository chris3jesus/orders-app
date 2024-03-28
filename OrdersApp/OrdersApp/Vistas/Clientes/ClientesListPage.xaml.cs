using OrdersApp.Modelos;
using OrdersApp.ViewModels;
using OrdersApp.Vistas.Clientes;
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
    public partial class ClientesListPage : ContentPage
    {
        public ClientesListPage(VendedorModel vendedor)
        {
            InitializeComponent();
            BindingContext = new ClientesListViewModel(vendedor);
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            var viewModel = BindingContext as ClientesListViewModel;
            var selectedCliente = e.SelectedItem as ClienteModel;
            viewModel?.SeleccionarClienteCommand.Execute(selectedCliente);
            ((ListView)sender).SelectedItem = null;
        }

        private async void OnNuevoClienteButtonClicked(object sender, EventArgs e)
        {
            var clientesListViewModel = (ClientesListViewModel)BindingContext;
            var vendedor = clientesListViewModel.Vendedor;
            await Navigation.PushAsync(new CreateClientePage(vendedor));
        }
    }
}