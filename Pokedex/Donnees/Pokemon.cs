using Donnees;
using System;
using System.Collections.Generic;
using System.Text;

namespace Donnees
{
    public class Pokemon
    {
        public int id { get; set; }
        public string name { get; set; }
        //public IList<Type> types { get; set; }
        public IList<PokemonType> types { get; set; }
        public PokemonSpecies species { get; set; }

        public void Afficher(Species species, EvolutionChain evolutionChain)
        {
            Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("\nName : ");
            Console.ResetColor(); Console.WriteLine(name);
            Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("Id : ");
            Console.ResetColor(); Console.WriteLine(id);
            Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("Type(s) : ");
            Console.ResetColor(); Console.Write(types[0].type.name);
            try {
                Console.WriteLine(" - " + types[1].type.name);
            }catch { Console.WriteLine(); }
            Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("Description : ");
            Console.ResetColor(); Console.WriteLine(species.flavor_text_entries[1].flavor_text.Replace("\n"," ").Replace("\r"," "));
            Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("Evolution chain : ");
            Console.ResetColor(); Console.Write(evolutionChain.chain.species.name);
            try
            {
                Console.Write(" - " + evolutionChain.chain.evolves_to[0].species.name);
                try
                {
                    Console.Write(" - " + evolutionChain.chain.evolves_to[0].evolves_to[0].species.name);
                }catch { }
            }catch { }
            Console.WriteLine("\n");
        }
    }
    public class Type
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class PokemonType
    {
        public int slot { get; set; }
        public Type type { get; set; }
    }
    public class PokemonSpecies
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Species
    {
        public PokemonEvolutionChain evolution_chain { get; set; }
        public IList<FlavorTextEntry> flavor_text_entries { get; set; }
    }
    public class FlavorTextEntry
    {
        public string flavor_text { get; set; }
    }
    public class PokemonEvolutionChain
    {
        public string url { get; set; }
    }

    public class EvolutionChain
    {
        public Chain chain { get; set; }
    }
    public class EvolvesTo
    {
        public IList<EvolvesTo> evolves_to { get; set; }

        public PokemonSpecies species{ get; set; }
    }

    public class Chain
    {
        public IList<EvolvesTo> evolves_to { get; set; }

        public PokemonSpecies species{ get; set; }
    }
}
