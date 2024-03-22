using OrdersApp.Modelos;
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
    public partial class ClientesListPage : ContentPage
    {
        public ClientesListPage()
        {
            InitializeComponent();
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            var viewModel = BindingContext as ClientesListViewModel;
            var selectedCliente = e.SelectedItem as ClienteModel;
            viewModel?.SeleccionarClienteCommand.Execute(selectedCliente);
            ((ListView)sender).SelectedItem = null;
        }
    }
}