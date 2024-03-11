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
    public partial class ProductosListPage : ContentPage
    {
        public ProductosListPage()
        {
            InitializeComponent();
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var viewModel = BindingContext as ProductosListViewModel;
            var selectedProducto = e.SelectedItem as ProductoModel;

            viewModel?.SeleccionarProductoCommand.Execute(selectedProducto);

            ((ListView)sender).SelectedItem = null;
        }
    }
}