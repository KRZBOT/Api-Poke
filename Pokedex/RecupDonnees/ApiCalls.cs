using Donnees;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Caching;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RecupDonnees
{
    public static class ApiCalls
    {
        public static ObjectCache cache = MemoryCache.Default;
        public static async Task<Pokemon> GetPokemon(String ID)
        {
            if (cache.Contains("Pokemon"+ID)){
                
                Pokemon pokemon = (Pokemon) cache.Get("Pokemon"+ID);
                return pokemon;
            }
            else
            {
                string url = ApiHelper.URL + "pokemon/" + ID;

                using HttpResponseMessage response = await ApiHelper.client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    Pokemon pokemon = await JsonSerializer.DeserializeAsync<Pokemon>(await response.Content.ReadAsStreamAsync());
                    cache.Add("Pokemon"+pokemon.name, pokemon, null);
                    cache.Add("Pokemon"+pokemon.id.ToString(), pokemon, null);
                    return pokemon;

                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }   
        }

        public static async Task<Species> GetDescription(int ID) {

            string url = ApiHelper.URL + "pokemon-species/" + ID;

            if (cache.Contains(url))
            {
                Species species = (Species) cache.Get(url);
                return species;
            }
            else
            {
                using HttpResponseMessage response = await ApiHelper.client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    Species species = await JsonSerializer.DeserializeAsync<Species>(await response.Content.ReadAsStreamAsync());
                    cache.Add(url, species, null);
                    return species;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<Liste> GetList(int limit)
        {
            string url = ApiHelper.URL + "pokemon?limit=" + limit;

            if (cache.Contains(url))
            {
                Liste liste = (Liste) cache.Get(url);
                return liste;
            }
            else
            {
                using HttpResponseMessage response = await ApiHelper.client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    Liste liste = await JsonSerializer.DeserializeAsync<Liste>(await response.Content.ReadAsStreamAsync());
                    cache.Add(url, liste, null);
                    return liste;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        public static async Task<Liste> GetNextList(Liste liste)
        {
            string url = liste.next;
            if (cache.Contains(url))
            {
                Liste nextListe = (Liste) cache.Get(url);
                return nextListe;
            }
            else
            {
                using HttpResponseMessage response = await ApiHelper.client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    Liste nextListe = await JsonSerializer.DeserializeAsync<Liste>(await response.Content.ReadAsStreamAsync());
                    cache.Add(url, nextListe, null);
                    return nextListe;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        public static async Task<Liste> GetPreviousList(Liste liste)
        {
            string url = liste.previous;
            if (cache.Contains(url))
            {
                Liste previousListe = (Liste) cache.Get(url);
                return previousListe;
            }
            else
            {
                using HttpResponseMessage response = await ApiHelper.client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    Liste previousListe = await JsonSerializer.DeserializeAsync<Liste>(await response.Content.ReadAsStreamAsync());
                    cache.Add(url, previousListe, null);
                    return previousListe;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<EvolutionChain> GetEvolution(string url)
        {
            if (cache.Contains(url))
            {
                EvolutionChain evolutionChain = (EvolutionChain)cache.Get(url);
                return evolutionChain;
            }
            else
            {
                using HttpResponseMessage response = await ApiHelper.client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    EvolutionChain evolutionChain = await JsonSerializer.DeserializeAsync<EvolutionChain>(await response.Content.ReadAsStreamAsync());
                    cache.Add(url, evolutionChain, null);
                    return evolutionChain;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
