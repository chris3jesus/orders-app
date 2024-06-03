using OrdersApp.Modelos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OrdersApp.Vistas.Avances
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AvancesGeneralPage : ContentPage
    {
        public ObservableCollection<AvanceVendedorModel> Avances { get; set; }
        public ICommand CloseCommand { get; set; }

        public string Fecha { get; set; }
        public string Reporte { get; set; }
        public string Persona { get; set; }

        public AvancesGeneralPage(ObservableCollection<AvanceVendedorModel> avances, string fecha, string reporte, string persona)
        {
            InitializeComponent();
            Avances = avances;
            AvancesListView.ItemsSource = Avances;

            Fecha = fecha;
            Reporte = reporte;
            Persona = persona;

            CloseCommand = new Command(async () => await Navigation.PopModalAsync());
            BindingContext = this;
        }
    }
}