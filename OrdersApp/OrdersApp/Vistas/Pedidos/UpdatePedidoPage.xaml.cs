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

            formaPagoPicker.SelectedIndex = 0;
            fleteCheckBox.IsChecked = pedido.Flete;

            if (pedido.TipoDoc == "F")
            {
                tipoDocPicker.SelectedIndex = 0;
            }
            if (pedido.TipoDoc == "B")
            {
                tipoDocPicker.SelectedIndex = 1;
            }

            SelectIndexBasedOnPedidoId();
        }

        private async Task SelectIndexBasedOnPedidoId()
        {
            var viewModel = (UpdatePedidoViewModel)BindingContext;
            var pedido = viewModel.Pedido;
            int idDireccionPedido = pedido.IdDirCli;

            int indiceSeleccionado = -1;
            await Task.Delay(300);

            for (int i = 0; i < viewModel.Cliente.Direcciones.Count; i++)
            {
                if (viewModel.Cliente.Direcciones[i].Codigo == idDireccionPedido)
                {
                    indiceSeleccionado = i;
                    break;
                }
            }
            direccionPicker.SelectedIndex = indiceSeleccionado;

        }

        private void FormaPagoPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            string selectedFormaPago = picker.SelectedItem.ToString();

            var viewModel = (UpdatePedidoViewModel)BindingContext;
            viewModel.Pedido.CondPago = selectedFormaPago;
        }

        private void TipoDocPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            string selectedTipoDoc = picker.SelectedItem.ToString();
            var viewModel = (UpdatePedidoViewModel)BindingContext;

            if (selectedTipoDoc == "Factura")
            {
                viewModel.Pedido.TipoDoc = "F";
            }

            if (selectedTipoDoc == "Boleta")
            {
                viewModel.Pedido.TipoDoc = "B";
            }
        }

        private void DireccionPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                var viewModel = (UpdatePedidoViewModel)BindingContext;
                var selectedDireccion = viewModel.Cliente.Direcciones[selectedIndex];
                viewModel.Pedido.IdDirCli = selectedDireccion.Codigo;
            }
        }

        private void FleteCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var checkBox = (CheckBox)sender;
            bool isChecked = checkBox.IsChecked;

            var viewModel = (UpdatePedidoViewModel)BindingContext;
            viewModel.Pedido.Flete = isChecked;
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