using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersApp.Modelos
{
    public class ProductoModel
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Marca { get; set; }
        public string Presentacion { get; set; }
        public decimal Valor { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public int Comprometido { get; set; }
    }
}
