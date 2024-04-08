using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersApp.Modelos
{
    public class PedidoModel
    {
        public int CodCli { get; set; }
        public int CodVen { get; set; }
        public string CondPago { get; set; }
        public int DiasCred { get; set; } = 1;
        public string TipoDoc { get; set; }
        public DateTime FechaReg { get; } = DateTime.Now;
        public string Estado { get; set; } = "Nuevo pedido";
        public int IdDirCli { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public bool Flete { get; set; } = false;
        public string Observ { get; set; }
        public List<DetallePedidoModel> DetPedidos { get; set; } = new List<DetallePedidoModel>();
    }

    public class DetallePedidoModel
    {
        public string IdProd { get; set; }
        public int Cantidad { get; set; }
        public decimal Dscto1 { get; set; }
        public decimal Dscto2 { get; set; }
        public ProductoModel Producto { get; set; }
        public decimal SubtotalLb { get; set; }
        public decimal PscDsc { get; set; }
        public string NomPres { get; set; }
    }
}
