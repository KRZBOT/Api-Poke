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

        public void Afficher(Species species)
        {
            Console.WriteLine("name : " + name);
            Console.WriteLine("id : " + id);
            Console.Write("type(s) : ");
            Console.Write(types[0].type.name);
            try {
                Console.WriteLine(" - " + types[1].type.name);
            }catch(Exception ex) { Console.WriteLine(); };
            Console.Write("description : ");
            Console.WriteLine(species.flavor_text_entries[0].flavor_text);
            
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
        public IList<FlavorTextEntry> flavor_text_entries { get; set; }
    }
    public class FlavorTextEntry
    {
        public string flavor_text { get; set; }
    }
}
