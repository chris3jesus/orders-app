using OrdersApp.Modelos;
using OrdersApp.Servicios;
using OrdersApp.Vistas.Avances;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace OrdersApp.ViewModels
{
    public class AvancesViewModel : INotifyPropertyChanged
    {
        private VendedorModel _vendedor;
        private DateTime _fechaInicio;
        private DateTime _fechaFin;
        private string _codigo;

        private AvancesService _avancesService;
        private HelpersService _helpersService;

        public ICommand GeneralCommand { get; }
        public ICommand LineaCommand { get; }
        public ICommand ClienteCommand { get; }

        public AvancesViewModel()
        {
            FechaInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            FechaFin = DateTime.Now;

            _avancesService = new AvancesService();
            _helpersService = new HelpersService();

            GeneralCommand = new Command(OnGeneral);
            LineaCommand = new Command(OnLinea);
            ClienteCommand = new Command(OnCliente);
        }

        public VendedorModel Vendedor
        {
            get { return _vendedor; }
            set { _vendedor = value; OnPropertyChanged(nameof(Vendedor)); }
        }

        public DateTime FechaInicio
        {
            get { return _fechaInicio; }
            set { _fechaInicio = value; OnPropertyChanged(nameof(FechaInicio)); }
        }

        public DateTime FechaFin
        {
            get { return _fechaFin; }
            set { _fechaFin = value; OnPropertyChanged(nameof(FechaFin)); }
        }

        public string Codigo
        {
            get { return _codigo; }
            set { _codigo = value; OnPropertyChanged(nameof(Codigo)); }
        }

        public AvancesViewModel(VendedorModel vendedor) : this()
        {
            Vendedor = vendedor;
        }

        private async void OnGeneral()
        {
            if (Vendedor.Supervisor)
            {
                var avances = await _avancesService.ReportarAvanceGeneral(FechaInicio.ToString("yyyy-MM-dd"), FechaFin.ToString("yyyy-MM-dd"));
                if (avances != null)
                {
                    var fechas = await _helpersService.GetFecha();
                    var avancesCollection = new ObservableCollection<AvanceModel>(avances);

                    string fecha = fechas.Message;
                    string reporte = FechaInicio.ToString("dd/MM/yyyy") + " al " + FechaFin.ToString("dd/MM/yyyy");
                    string persona = Vendedor.Codigo + " - " + Vendedor.Nombre;

                    var dialogPage = new AvancesListPage(avancesCollection, fecha, reporte, persona);
                    await Application.Current.MainPage.Navigation.PushModalAsync(dialogPage);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "No se encontraron avances", "OK");
                }
            }
            else
            {
                var avances = await _avancesService.ReportarAvanceGeneralVendedor(FechaInicio.ToString("yyyy-MM-dd"), FechaFin.ToString("yyyy-MM-dd"), Vendedor.Codigo);
                if (avances != null)
                {
                    var fechas = await _helpersService.GetFecha();
                    var avancesCollection = new ObservableCollection<AvanceVendedorModel>(avances);

                    string fecha = fechas.Message;
                    string reporte = FechaInicio.ToString("dd/MM/yyyy") + " al " + FechaFin.ToString("dd/MM/yyyy");
                    string persona = Vendedor.Codigo + " - " + Vendedor.Nombre;

                    var dialogPage = new AvancesGeneralPage(avancesCollection, fecha, reporte, persona);
                    await Application.Current.MainPage.Navigation.PushModalAsync(dialogPage);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "No se encontraron avances", "OK");
                }
            }
        }

        private async void OnLinea()
        {
            if (Vendedor.Supervisor)
            {
                var avances = await _avancesService.ReportarAvanceLineas(FechaInicio.ToString("yyyy-MM-dd"), FechaFin.ToString("yyyy-MM-dd"), Codigo);
                if (avances != null)
                {
                    var fechas = await _helpersService.GetFecha();
                    var personas = await _helpersService.GetVendedor(Codigo);
                    var avancesCollection = new ObservableCollection<AvanceLineasModel>(avances);

                    string fecha = fechas.Message;
                    string reporte = FechaInicio.ToString("dd/MM/yyyy") + " al " + FechaFin.ToString("dd/MM/yyyy");
                    string persona = personas.Message;

                    var dialogPage = new AvancesLineasPage(avancesCollection, fecha, reporte, persona);
                    await Application.Current.MainPage.Navigation.PushModalAsync(dialogPage);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "No se encontraron avances", "OK");
                }
            }
            else
            {
                var avances = await _avancesService.ReportarAvanceLineas(FechaInicio.ToString("yyyy-MM-dd"), FechaFin.ToString("yyyy-MM-dd"), Vendedor.Codigo);
                if (avances != null)
                {
                    var fechas = await _helpersService.GetFecha();
                    var avancesCollection = new ObservableCollection<AvanceLineasModel>(avances);

                    string fecha = fechas.Message;
                    string reporte = FechaInicio.ToString("dd/MM/yyyy") + " al " + FechaFin.ToString("dd/MM/yyyy");
                    string persona = Vendedor.Codigo + " - " + Vendedor.Nombre;

                    var dialogPage = new AvancesLineasPage(avancesCollection, fecha, reporte, persona);
                    await Application.Current.MainPage.Navigation.PushModalAsync(dialogPage);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "No se encontraron avances", "OK");
                }
            }
        }

        private async void OnCliente()
        {
            var avances = await _avancesService.ReportarAvanceClientes(FechaInicio.ToString("yyyy-MM-dd"), FechaFin.ToString("yyyy-MM-dd"), Codigo);
            if (avances != null)
            {
                var fechas = await _helpersService.GetFecha();
                var personas = await _helpersService.GetCliente(Codigo);
                var avancesCollection = new ObservableCollection<AvanceClientesModel>(avances);

                string fecha = fechas.Message;
                string reporte = FechaInicio.ToString("dd/MM/yyyy") + " al " + FechaFin.ToString("dd/MM/yyyy");
                string persona = personas.Message;

                var dialogPage = new AvancesClientesPage(avancesCollection, fecha, reporte, persona);
                await Application.Current.MainPage.Navigation.PushModalAsync(dialogPage);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No se encontraron avances", "OK");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
