using RecupDonnees;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class UnitTest
    {
        public UnitTest(){
            ApiHelper.InitializeClient();
        }
        [Fact]
        public async Task GetPokemonNameTest()
        {
            var url = "https://pokeapi.co/api/v2/pokemon/pikachu";
            var response = await ApiHelper.client.GetAsync(url);
            response.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task GetPokemonIdTest()
        {
            var url = "https://pokeapi.co/api/v2/pokemon/596";
            var response = await ApiHelper.client.GetAsync(url);
            response.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task GetListeTest()
        {
            var url = "https://pokeapi.co/api/v2/pokemon?limit=10";
            var response = await ApiHelper.client.GetAsync(url);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetSpeciesTest()
        {
            var url = "https://pokeapi.co/api/v2/pokemon-species/6";
            var response = await ApiHelper.client.GetAsync(url);
            response.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task GetEvolutionTest()
        {
            var url = "https://pokeapi.co/api/v2/evolution-chain/2";
            var response = await ApiHelper.client.GetAsync(url);
            response.EnsureSuccessStatusCode();
        }
    }
}
