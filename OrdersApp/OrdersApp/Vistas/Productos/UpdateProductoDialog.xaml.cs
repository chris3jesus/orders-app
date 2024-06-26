﻿using OrdersApp.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OrdersApp.Vistas.Productos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateProductoDialog : ContentPage
    {
        private DetallePedidoModel _detallePedido;
        public event EventHandler<DetallePedidoModel> DetallePedidoEditado;

        public UpdateProductoDialog(DetallePedidoModel detalle)
        {
            InitializeComponent();
            BindingContext = detalle;
            _detallePedido = detalle;

            cantidadEntry.TextChanged += Entry_TextChanged;
            dscto1Entry.TextChanged += Entry_TextChanged;
            dscto2Entry.TextChanged += Entry_TextChanged;
            GetTotal();
        }

        private async void Aceptar_Clicked(object sender, EventArgs e)
        {
            if (cantidadEntry.Text == "")
            {
                await DisplayAlert("Error", "Ingrese una cantidad correcta", "Aceptar");
                return;
            }

            int cantidad = Convert.ToInt32(cantidadEntry.Text);

            if (cantidad == 0)
            {
                await DisplayAlert("Error", "La cantidad no debe ser cero", "Aceptar");
                return;
            }

            decimal descuento1 = Convert.ToDecimal(dscto1Entry.Text);

            if (descuento1 < 0 || descuento1 > 100)
            {
                await DisplayAlert("Error", "El rango de descuento permitido es de 0 a 100", "Aceptar");
                return;
            }

            if (cantidad > _detallePedido.Producto.Stock)
            {
                await DisplayAlert("Sin stock", "No hay suficiente stock disponible", "Aceptar");
                return;
            }

            _detallePedido.Cantidad = Convert.ToInt32(cantidadEntry.Text);
            _detallePedido.Dscto1 = Convert.ToDecimal(dscto1Entry.Text);
            _detallePedido.Dscto2 = Convert.ToDecimal(dscto2Entry.Text);

            _detallePedido.NomPres = _detallePedido.Producto.Descripcion + " - " + _detallePedido.Producto.Presentacion;
            _detallePedido.PscDsc = Math.Round(_detallePedido.Producto.Precio * (1 - _detallePedido.Dscto1 / 100m) * (1 - _detallePedido.Dscto2 / 100m), 2);

            DetallePedidoEditado?.Invoke(this, _detallePedido);
            await Navigation.PopModalAsync();
        }

        private async void Cancelar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cantidadEntry.Text) || string.IsNullOrWhiteSpace(dscto1Entry.Text) || string.IsNullOrWhiteSpace(dscto2Entry.Text)) return;

            int cantidad = Convert.ToInt32(cantidadEntry.Text);
            decimal descuento1 = Convert.ToDecimal(dscto1Entry.Text);
            decimal descuento2 = Convert.ToDecimal(dscto2Entry.Text);

            if (cantidad == 0)
            {
                DisplayAlert("Error", "La cantidad no debe ser cero", "Aceptar");
                return;
            }

            if (cantidad > _detallePedido.Producto.Stock)
            {
                DisplayAlert("Sin stock", "No hay suficiente stock disponible", "Aceptar");
                return;
            }

            decimal subtotal = Math.Round(_detallePedido.Producto.Precio, 4);

            decimal descuento1Amount = Math.Round(subtotal * (descuento1 / 100m), 4);
            decimal descuento2Amount = Math.Round(subtotal * (descuento2 / 100m), 4);

            subtotal -= descuento1Amount;
            subtotal -= descuento2Amount;

            decimal total = subtotal * cantidad;

            lblSubtotal.Text = subtotal.ToString("C");
            lblTotal.Text = total.ToString("C");
            _detallePedido.SubtotalLb = total;
        }

        private void GetTotal()
        {
            int cantidad = Convert.ToInt32(cantidadEntry.Text);
            decimal descuento1 = Convert.ToDecimal(dscto1Entry.Text);
            decimal descuento2 = Convert.ToDecimal(dscto2Entry.Text);

            decimal subtotal = Math.Round(_detallePedido.Producto.Precio, 4);
            decimal descuento1Amount = Math.Round(subtotal * (descuento1 / 100m), 4);
            decimal descuento2Amount = Math.Round(subtotal * (descuento2 / 100m), 4);

            subtotal -= descuento1Amount;
            subtotal -= descuento2Amount;

            decimal total = subtotal * cantidad;

            lblSubtotal.Text = subtotal.ToString("C");
            lblTotal.Text = total.ToString("C");
            _detallePedido.SubtotalLb = total;
        }
    }
}