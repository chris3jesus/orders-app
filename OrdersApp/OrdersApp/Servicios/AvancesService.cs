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
    public class AvancesService
    {
        private static readonly string Host = (string)Application.Current.Resources["BaseUrl"];
        private static readonly string Url = Host + "/api/avances";

        public async Task<List<AvanceModel>> ReportarAvanceGeneral(string fechaInico, string fechaFin)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{Url}/general/?fechaInicio={fechaInico}&fechaFin={fechaFin}";
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<List<AvanceModel>>(content);
                    }
                    else
                    {
                        Console.WriteLine("Error al obtener los avances: " + response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en la solicitud HTTP: " + ex.Message);
            }
            return null;
        }

        public async Task<List<AvanceVendedorModel>> ReportarAvanceGeneralVendedor(string fechaInico, string fechaFin, string id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{Url}/general/{id}?fechaInicio={fechaInico}&fechaFin={fechaFin}";
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<List<AvanceVendedorModel>>(content);
                    }
                    else
                    {
                        Console.WriteLine("Error al obtener los avances: " + response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en la solicitud HTTP: " + ex.Message);
            }
            return null;
        }

        public async Task<List<AvanceLineasModel>> ReportarAvanceLineas(string fechaInico, string fechaFin, string id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{Url}/lineas/{id}?fechaInicio={fechaInico}&fechaFin={fechaFin}";
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<List<AvanceLineasModel>>(content);
                    }
                    else
                    {
                        Console.WriteLine("Error al obtener los avances: " + response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en la solicitud HTTP: " + ex.Message);
            }
            return null;
        }

        public async Task<List<AvanceClientesModel>> ReportarAvanceClientes(string fechaInico, string fechaFin, string id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{Url}/clientes/{id}?fechaInicio={fechaInico}&fechaFin={fechaFin}";
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<List<AvanceClientesModel>>(content);
                    }
                    else
                    {
                        Console.WriteLine("Error al obtener los avances: " + response.StatusCode);
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
