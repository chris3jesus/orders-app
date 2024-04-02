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

        public async Task<List<ClienteModel>> BuscarClientes(int vendedor, string textoBusqueda)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{Url}/{vendedor}/{textoBusqueda}";
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

        public async Task<bool> RegistrarCliente(NuevoClienteModel nuevoCliente)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string clienteJson = JsonConvert.SerializeObject(nuevoCliente);
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.PostAsync(Url, new StringContent(clienteJson, Encoding.UTF8, "application/json"));

                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("La solicitud POST no fue exitosa. Estado: " + response.StatusCode);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al registrar el cliente: " + ex.Message);
                return false;
            }
        }
    }
}
