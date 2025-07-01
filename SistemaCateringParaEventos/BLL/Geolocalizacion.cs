using BE;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;


namespace BLL
{
    public class Geolocalizacion
    {

        public async Task<BE.Geolocalizacion> ObtenerGeolocalizacion(string calle, int altura, string ciudad, string provincia)
        {
            string direccion = $"{calle} {altura}, {ciudad}, {provincia}, Argentina";
            string apiKey = ConfigurationManager.AppSettings["GoogleMapsApiKey"]; ;
            string url = $"https://maps.googleapis.com/maps/api/geocode/json?address={WebUtility.UrlEncode(direccion)}&key={apiKey}";

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync(url);
                var json = JObject.Parse(response);

                var results = json["results"];
                if (results != null && results.HasValues)
                {
                    var location = results[0]["geometry"]["location"];

                    return new BE.Geolocalizacion
                    {
                        Latitud = location.Value<decimal>("lat"),
                        Longitud = location.Value<decimal>("lng")
                    };
                }
            }

            return null;
        }



    }
}
