using Newtonsoft.Json;
using OrdersApp.Modelos;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OrdersApp.Servicios
{
    public class ProductosService
    {
        private const string Url = "http://10.0.2.2:5077/api/productos";

        public async Task<List<ProductoModel>> ObtenerProductos()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(Url);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<List<ProductoModel>>(content);
                    }
                    else
                    {
                        // Manejar errores de la respuesta
                        Console.WriteLine("Error al obtener los productos: " + response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar errores de la solicitud
                Console.WriteLine("Error en la solicitud HTTP: " + ex.Message);
            }
            return null;
        }

        public async Task<List<ProductoModel>> BuscarProductos(string textoBusqueda)
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
                        return JsonConvert.DeserializeObject<List<ProductoModel>>(content);
                    }
                    else
                    {
                        // Manejar errores de la respuesta
                        Console.WriteLine("Error al buscar productos: " + response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar errores de la solicitud
                Console.WriteLine("Error en la solicitud HTTP: " + ex.Message);
            }
            return null;
        }
    }
}
