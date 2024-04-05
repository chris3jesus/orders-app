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
    public partial class UpdatePedidoPage : TabbedPage
    {
        public UpdatePedidoPage(PedidosModel pedido)
        {
            InitializeComponent();
            BindingContext = new UpdatePedidoViewModel(pedido);
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            var viewModel = BindingContext as UpdatePedidoViewModel;
            var selectedProducto = e.SelectedItem as ProductoModel;
            viewModel?.SeleccionarProductoCommand.Execute(selectedProducto);
            ((ListView)sender).SelectedItem = null;
        }

        private async void OnElementSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            var action = await DisplayActionSheet("Seleccionar acción", "Cancelar", null, "Editar", "Eliminar");

            if (action == "Editar")
            {
                if (e.SelectedItem is DetallePedidoModel detalle)
                {
                    if (BindingContext is UpdatePedidoViewModel viewModel)
                    {
                        viewModel.EditarProductoCommand.Execute(detalle);
                    }
                }
            }
            else if (action == "Eliminar")
            {
                if (BindingContext is UpdatePedidoViewModel viewModel)
                {
                    if (viewModel.Detalles.Contains((DetallePedidoModel)e.SelectedItem))
                    {
                        viewModel.Detalles.Remove((DetallePedidoModel)e.SelectedItem);
                        viewModel.CalcularTotalCommand.Execute(null);
                    }
                }
            }
            ((ListView)sender).SelectedItem = null;
        }
    }
}