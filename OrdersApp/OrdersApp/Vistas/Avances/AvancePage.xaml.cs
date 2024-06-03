using OrdersApp.Modelos;
using OrdersApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OrdersApp.Vistas.Avances
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AvancePage : ContentPage
    {
        public AvancePage(VendedorModel vendedor)
        {
            InitializeComponent();
            BindingContext = new AvancesViewModel(vendedor);
        }
    }
}