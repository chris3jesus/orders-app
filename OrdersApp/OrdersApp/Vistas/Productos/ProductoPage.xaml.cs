using OrdersApp.Modelos;
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
    public partial class ProductoPage : ContentPage
    {
        public ProductoPage(ProductoModel producto)
        {
            InitializeComponent();
            BindingContext = producto;
        }
    }
}