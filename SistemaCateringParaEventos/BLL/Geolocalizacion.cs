using BE;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BLL
{
    /// <summary>
    /// Proporciona métodos para obtener información de geolocalización utilizando la API de Google Maps.
    /// </summary>
    public class Geolocalizacion
    {
        /// <summary>
        /// Obtiene la geolocalización (latitud y longitud) de una dirección específica utilizando la API de Google Maps.
        /// </summary>
        /// <param name="calle">Nombre de la calle.</param>
        /// <param name="altura">Altura o número de la calle.</param>
        /// <param name="ciudad">Nombre de la ciudad.</param>
        /// <param name="provincia">Nombre de la provincia.</param>
        /// <returns>
        /// Un objeto <see cref="BE.Geolocalizacion"/> con la latitud y longitud de la dirección especificada,
        /// o <c>null</c> si no se pudo obtener la información.
        /// </returns>
        public async Task<BE.Geolocalizacion> ObtenerGeolocalizacion(string calle, int altura, string ciudad, string provincia)
        {
            try
            {
                // Construye la dirección completa a partir de los parámetros recibidos.
                string direccion = $"{calle} {altura}, {ciudad}, {provincia}, Argentina";
                // Obtiene la clave de API de Google Maps desde la configuración de la aplicación.
                string apiKey = System.Configuration.ConfigurationManager.AppSettings["ApiKeyGoogleMaps"];
                // Construye la URL de la solicitud a la API de Google Maps.
                string url = $"https://maps.googleapis.com/maps/api/geocode/json?address={Uri.EscapeDataString(direccion)}&key={apiKey}";

                using (HttpClient client = new HttpClient())
                {
                    // Realiza la solicitud HTTP GET a la API.
                    var response = await client.GetAsync(url);

                    // Si la respuesta no es exitosa, retorna null.
                    if (!response.IsSuccessStatusCode)
                        return null;

                    // Lee el contenido de la respuesta como una cadena JSON.
                    var json = await response.Content.ReadAsStringAsync();
                    // Deserializa el JSON dinámicamente.
                    dynamic resultado = JsonConvert.DeserializeObject(json);

                    // Verifica si el estado de la respuesta es "OK".
                    if (resultado.status != "OK")
                        return null;

                    // Obtiene la ubicación (latitud y longitud) del primer resultado.
                    var location = resultado.results[0].geometry.location;

                    // Retorna un objeto Geolocalizacion con los datos obtenidos.
                    return new BE.Geolocalizacion
                    {
                        Latitud = (decimal)location.lat,
                        Longitud = (decimal)location.lng
                    };
                }
            }
            catch
            {
                // En caso de excepción, retorna null.
                return null;
            }
        }
    }
}
