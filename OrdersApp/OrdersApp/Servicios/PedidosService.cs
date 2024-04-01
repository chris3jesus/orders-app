using Newtonsoft.Json;
using OrdersApp.Modelos;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OrdersApp.Servicios
{
    public class PedidosService
    {
        private const string Host = "http://10.0.2.2:5077";
        private const string Url = Host + "/api/pedidos";

        public async Task<bool> EnviarPedido(PedidoModel pedido)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string pedidoJson = JsonConvert.SerializeObject(pedido);
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.PostAsync(Url, new StringContent(pedidoJson, Encoding.UTF8, "application/json"));

                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("La solicitud POST no fue exitosa. Estado: " + response.StatusCode);
                        Console.WriteLine(pedidoJson);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar el pedido: " + ex.Message);
                return false;
            }
        }

        public async Task<List<PedidosModel>> ObtenerPedidos(int vendedor)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{Url}/v/{vendedor}";
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<List<PedidosModel>>(content);
                    }
                    else
                    {
                        Console.WriteLine("Error al buscar pedidos: " + response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}
