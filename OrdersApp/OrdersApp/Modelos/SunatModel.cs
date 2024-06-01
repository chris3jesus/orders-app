using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersApp.Modelos
{
    public class SunatModel
    {
        public string Documento { get; set; }
        public string Comercial { get; set; }
        public string Estado { get; set; }
        public string Domicilio { get; set; }
        public DateTime FechaConsulta { get; set; }
    }
}
