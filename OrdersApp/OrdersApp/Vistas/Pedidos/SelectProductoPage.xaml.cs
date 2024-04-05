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
    public partial class SelectProductoPage : TabbedPage
    {
        public SelectProductoPage(ClienteModel cliente, VendedorModel vendedor)
        {
            InitializeComponent();
            BindingContext = new SelectProductoViewModel(vendedor, cliente);

            formaPagoPicker.SelectedIndex = 0;

            if (cliente.Documento.Length == 11)
            {
                tipoDocPicker.SelectedIndex = 0;
            }

            if (cliente.Documento.Length == 8)
            {
                tipoDocPicker.SelectedIndex = 1;
            }
        }

        private void FormaPagoPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            string selectedFormaPago = picker.SelectedItem.ToString();

            var viewModel = (SelectProductoViewModel)BindingContext;
            viewModel.Pedido.CondPago = selectedFormaPago;
        }

        private void TipoDocPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            string selectedTipoDoc = picker.SelectedItem.ToString();
            var viewModel = (SelectProductoViewModel)BindingContext;

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
                var viewModel = (SelectProductoViewModel)BindingContext;
                var selectedDireccion = viewModel.Cliente.Direcciones[selectedIndex];
                viewModel.Pedido.IdDirCli = selectedDireccion.Codigo;
            }
        }

        private void FleteCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var checkBox = (CheckBox)sender;
            bool isChecked = checkBox.IsChecked;

            var viewModel = (SelectProductoViewModel)BindingContext;
            viewModel.Pedido.Flete = isChecked;
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var viewModel = BindingContext as SelectProductoViewModel;
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
                    if (BindingContext is SelectProductoViewModel viewModel)
                    {
                        viewModel.EditarProductoCommand.Execute(detalle);
                    }
                }
            }
            else if (action == "Eliminar")
            {
                if (BindingContext is SelectProductoViewModel viewModel)
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