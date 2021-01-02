using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace RecupDonnees
{
    public class ApiHelper
    {
        public static HttpClient client;
        public static string URL = "https://pokeapi.co/api/v2/";

        public static void InitializeClient()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        }       
    }
}
