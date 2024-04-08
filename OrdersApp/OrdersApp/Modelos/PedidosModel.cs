using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersApp.Modelos
{
    public class PedidosModel
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public int CodCli { get; set; }
        public int CodVen { get; set; }
        public string CondPago { get; set; }
        public int DiasCred { get; set; }
        public string TipoDoc { get; set; }
        public DateTime FechaReg { get; set; }
        public DateTime? FechaProc { get; set; }
        public string Estado { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Igv { get; set; }
        public decimal Total { get; set; }
        public int IdDirCli { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public bool Flete { get; set; }
        public string Observ { get; set; }
        public DateTime? FechaEdit { get; set; }
        public string UsuarioProc { get; set; }
        public List<DetallesPedidoModel> DetPedidos { get; set; }
    }

    public class DetallesPedidoModel
    {
        public int Id { get; set; }
        public int IdPed { get; set; }
        public int Item { get; set; }
        public string IdProd { get; set; }
        public int Cantidad { get; set; }
        public decimal Valor { get; set; }
        public decimal Precio { get; set; }
        public decimal Dscto1 { get; set; }
        public decimal Dscto2 { get; set; }
        public decimal PrecDscto { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Igv { get; set; }
        public decimal Total { get; set; }
    }
}
