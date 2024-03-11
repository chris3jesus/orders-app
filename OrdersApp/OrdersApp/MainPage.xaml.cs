using OrdersApp.Vistas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OrdersApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            CodigoEntry.Text = "999";
            ClaveEntry.Text = "prueba";
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            string codigo = CodigoEntry.Text;
            string clave = ClaveEntry.Text;

            if (codigo == "999" && clave == "prueba")
            {
                await Navigation.PushAsync(new MenuPage(codigo));
            }
            else
            {
                await DisplayAlert("Error", "Datos incorrectos", "Aceptar");
            }
        }
    }
}
