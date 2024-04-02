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
    public partial class PedidosListPage : ContentPage
    {
        public PedidosListPage(VendedorModel vendedor)
        {
            InitializeComponent();
            BindingContext = new PedidosListViewModel(vendedor);
            Title = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            var viewModel = BindingContext as PedidosListViewModel;
            var selectedPedido = e.SelectedItem as PedidosModel;
            viewModel?.SeleccionarPedidoCommand.Execute(selectedPedido);
            ((ListView)sender).SelectedItem = null;
        }
    }
}