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
    }
}