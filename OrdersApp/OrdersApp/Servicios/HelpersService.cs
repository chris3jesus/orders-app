using Newtonsoft.Json;
using OrdersApp.Modelos;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OrdersApp.Servicios
{
    public class HelpersService
    {
        private static readonly string Host = (string)Application.Current.Resources["BaseUrl"];
        private static readonly string Url = Host + "/api/helpers";

        public async Task<MessageModel> GetCliente(string id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{Url}/cliente/{id}";
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<MessageModel>(content);
                    }
                    else
                    {
                        Console.WriteLine("Error al obtener el cliente: " + response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en la solicitud HTTP: " + ex.Message);
            }
            return null;
        }

        public async Task<MessageModel> GetVendedor(string id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{Url}/vendedor/{id}";
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<MessageModel>(content);
                    }
                    else
                    {
                        Console.WriteLine("Error al obtener el cliente: " + response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en la solicitud HTTP: " + ex.Message);
            }
            return null;
        }

        public async Task<MessageModel> GetFecha()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{Url}/fecha";
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<MessageModel>(content);
                    }
                    else
                    {
                        Console.WriteLine("Error al obtener el cliente: " + response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en la solicitud HTTP: " + ex.Message);
            }
            return null;
        }

        public async Task<CreditModel> GetApp()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(Url);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<CreditModel>(content);
                    }
                    else
                    {
                        Console.WriteLine("Error al obtener los créditos: " + response.StatusCode);
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
