using OrdersApp.Modelos;
using OrdersApp.Servicios;
using OrdersApp.Vistas;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace OrdersApp.ViewModels
{
    public class ProductosListViewModel : INotifyPropertyChanged
    {
        public string TextoBusqueda { get; set; }
        public ObservableCollection<ProductoModel> Productos { get; set; }
        private ProductosService _productosService;

        public ICommand BuscarCommand { get; set; }
        public ICommand SeleccionarProductoCommand { get; private set; }

        public ProductosListViewModel()
        {
            // Aquí normalmente cargarías los datos de productos desde tu servicio o repositorio
            //Productos = new ObservableCollection<ProductoModel>
            //{
            //    new ProductoModel { Codigo = 2725648, Descripcion = "ELECTROLIGHT MORA", Marca = "Medifarma", Presentacion = "FCOX475ML", Precio = 2.2034m },
            //    new ProductoModel { Codigo = 2725645, Descripcion = "ABRILAR EA 575 35MG/5ML JARABE", Marca = "Megalabs", Presentacion = "FCOX100ML", Precio = 49.6600m },
            //    new ProductoModel { Codigo = 2726073, Descripcion = "REDOXON TOTAL COMPRIMIDOS RECUBIERTO", Marca = "Bayer", Presentacion = "CJAX10", Precio = 8.7000m },
            //};

            _productosService = new ProductosService();
            CargarProductos();

            Productos = new ObservableCollection<ProductoModel>();
            BuscarCommand = new Command(async () => await BuscarProductos());

            SeleccionarProductoCommand = new Command<ProductoModel>(async (producto) => await SeleccionarProducto(producto));
        }

        private async void CargarProductos()
        {
            var productos = await _productosService.ObtenerProductos();
            if (productos != null)
            {
                Productos = new ObservableCollection<ProductoModel>(productos);
                OnPropertyChanged(nameof(Productos));
            }
        }

        private async Task BuscarProductos()
        {
            var productos = await _productosService.BuscarProductos(TextoBusqueda);
            if (productos != null)
            {
                Productos.Clear();
                foreach (var producto in productos)
                {
                    Productos.Add(producto);
                }
            }
        }

        private async Task SeleccionarProducto(ProductoModel producto)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ProductoPage(producto));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
