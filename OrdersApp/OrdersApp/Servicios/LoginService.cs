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
    public class LoginService
    {
        private static readonly string Host = (string)Application.Current.Resources["BaseUrl"];
        private static readonly string Url = Host + "/api/login";

        public async Task<VendedorModel> LoginAsync(string codigo, string clave)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var loginRequest = new { Codigo = codigo, Clave = clave };
                    string jsonRequest = JsonConvert.SerializeObject(loginRequest);
                    var response = await httpClient.PostAsync(Url, new StringContent(jsonRequest, Encoding.UTF8, "application/json"));

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        var vendedor = JsonConvert.DeserializeObject<VendedorModel>(jsonResponse);
                        return vendedor;
                    }
                    else
                    {
                        throw new Exception("No se pudo iniciar sesión. Verifique sus credenciales.");
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
