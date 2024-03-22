using OrdersApp.Modelos;
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
    public partial class ClientePage : ContentPage
    {
        public ClientePage(ClienteModel cliente)
        {
            InitializeComponent();
            BindingContext = cliente;
        }
    }
}