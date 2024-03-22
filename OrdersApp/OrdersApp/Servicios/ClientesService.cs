using Newtonsoft.Json;
using OrdersApp.Modelos;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OrdersApp.Servicios
{
    public class ClientesService
    {
        private const string Host = "http://10.0.2.2:5077";
        private const string Url = Host + "/api/clientes";

        public async Task<List<ClienteModel>> BuscarClientes(string textoBusqueda)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{Url}/{textoBusqueda}";
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<List<ClienteModel>>(content);
                    }
                    else
                    {
                        Console.WriteLine("Error al obtener los clientes: " + response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en la solicitud HTTP: " + ex.Message);
            }
            return null;
        }
    }
}
