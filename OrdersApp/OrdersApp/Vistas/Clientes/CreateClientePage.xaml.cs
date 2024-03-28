using OrdersApp.Modelos;
using OrdersApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OrdersApp.Vistas.Clientes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateClientePage : ContentPage
    {
        public CreateClientePage(VendedorModel vendedor)
        {
            InitializeComponent();
            BindingContext = new CreateClienteViewModel(vendedor);
        }
    }
}