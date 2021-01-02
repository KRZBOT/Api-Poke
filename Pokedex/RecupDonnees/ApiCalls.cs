using Donnees;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RecupDonnees
{
    public class ApiCalls
    {
        public static async Task<Pokemon> GetPokemon(String name)
        {
            string url = ApiHelper.URL + "pokemon/" + name;

            using HttpResponseMessage response = await ApiHelper.client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                Pokemon pokemon = await JsonSerializer.DeserializeAsync<Pokemon>(await response.Content.ReadAsStreamAsync());
                return pokemon;

            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        public static async Task<Pokemon> GetPokemon(int id)
        {
            string url = ApiHelper.URL + "pokemon/" + id;

            using HttpResponseMessage response = await ApiHelper.client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                Pokemon pokemon = await JsonSerializer.DeserializeAsync<Pokemon>(await response.Content.ReadAsStreamAsync());
                return pokemon;

            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        public static async Task<Species> GetDescription(int id) {
            string url = ApiHelper.URL + "pokemon-species/" + id;

            using HttpResponseMessage response = await ApiHelper.client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                Species species = await JsonSerializer.DeserializeAsync<Species>(await response.Content.ReadAsStreamAsync());

                return species;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
        public static async Task<Liste> GetList(int limit)
        {
            string url = ApiHelper.URL + "pokemon?limit=" + limit;

            using HttpResponseMessage response = await ApiHelper.client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                Liste liste = await JsonSerializer.DeserializeAsync<Liste>(await response.Content.ReadAsStreamAsync());

                return liste;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
        public static async Task<Liste> GetNextList(Liste liste)
        {
            string url = liste.next;
            using HttpResponseMessage response = await ApiHelper.client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                Liste nextListe = await JsonSerializer.DeserializeAsync<Liste>(await response.Content.ReadAsStreamAsync());

                return nextListe;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
        public static async Task<Liste> GetPreviousList(Liste liste)
        {
            string url = liste.next;
            using HttpResponseMessage response = await ApiHelper.client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                Liste previousListe = await JsonSerializer.DeserializeAsync<Liste>(await response.Content.ReadAsStreamAsync());

                return previousListe;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
    }
}
