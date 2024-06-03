using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersApp.Modelos
{
    public class AvanceModel
    {
        public string Vendedor { get; set; }
        public decimal Cuota { get; set; }
        public decimal Avance { get; set; }
        public decimal Saldo { get; set; }
        public decimal Porcentaje { get; set; }
        public decimal Saldo95 { get; set; }
    }

    public class AvanceVendedorModel
    {
        public string Vendedor { get; set; }
        public decimal Cuota { get; set; }
        public decimal Avance { get; set; }
        public decimal Saldo { get; set; }
        public decimal Porcentaje { get; set; }
    }

    public class AvanceLineasModel
    {
        public string Linea { get; set; }
        public decimal Cuota { get; set; }
        public decimal Avance { get; set; }
        public decimal Saldo { get; set; }
        public decimal Porcentaje { get; set; }
        public decimal Saldo95 { get; set; }
    }

    public class AvanceClientesModel
    {
        public string Linea { get; set; }
        public decimal Cuota { get; set; }
        public decimal Avance { get; set; }
        public decimal Porcentaje { get; set; }
    }

    public class MessageModel
    {
        public string Message { get; set; }
    }

    public class CreditModel
    {
        public string ScrumMaster { get; set; }
        public string ProductOwner { get; set; }
    }
}
