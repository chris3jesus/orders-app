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
    public class SunatService
    {
        private static readonly string Host = (string)Application.Current.Resources["BaseUrl"];
        private static readonly string Url = Host + "/api/sunat";

        public async Task<SunatModel> ConsultarRuc(string ruc)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{Url}/ruc/{ruc}";
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<SunatModel>(content);
                    }
                    else
                    {
                        Console.WriteLine("Error al consultar SUNAT: " + response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en la solicitud HTTP: " + ex.Message);
            }
            return null;
        }

        public async Task<SunatModel> ConsultarDni(string dni)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{Url}/dni/{dni}";
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<SunatModel>(content);
                    }
                    else
                    {
                        Console.WriteLine("Error al consultar SUNAT: " + response.StatusCode);
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
