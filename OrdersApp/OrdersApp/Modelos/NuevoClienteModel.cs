using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersApp.Modelos
{
    public class NuevoClienteModel
    {
        public bool Creado { get; set; } = false;
        public int CodVen { get; set; }
        public string NroDoc { get; set; }
        public string Nombre { get; set; }
        public string Comercial { get; set; }
        public string Direccion { get; set; }
    }
}
