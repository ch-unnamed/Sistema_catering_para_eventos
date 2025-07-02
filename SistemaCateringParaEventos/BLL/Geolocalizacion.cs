using BE;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BLL
{
    public class Geolocalizacion
    {
        public async Task<BE.Geolocalizacion> ObtenerGeolocalizacion(string calle, int altura, string ciudad, string provincia)
        {
            try
            {
                string direccion = $"{calle} {altura}, {ciudad}, {provincia}, Argentina";
                string apiKey = System.Configuration.ConfigurationManager.AppSettings["ApiKeyGoogleMaps"];
                string url = $"https://maps.googleapis.com/maps/api/geocode/json?address={Uri.EscapeDataString(direccion)}&key={apiKey}";

                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(url);

                    if (!response.IsSuccessStatusCode)
                        return null;

                    var json = await response.Content.ReadAsStringAsync();
                    dynamic resultado = JsonConvert.DeserializeObject(json);

                    if (resultado.status != "OK")
                        return null;

                    var location = resultado.results[0].geometry.location;

                    return new BE.Geolocalizacion
                    {
                        Latitud = (decimal)location.lat,
                        Longitud = (decimal)location.lng
                    };
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
