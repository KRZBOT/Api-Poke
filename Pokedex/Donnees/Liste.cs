using System;
using System.Collections.Generic;

namespace Donnees
{
    public class Result
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Liste
    {
        public int count { get; set; }
        public string next { get; set; }
        public object previous { get; set; }
        public IList<Result> results { get; set; }

        public void Afficher(int id)
        {
            Console.Clear();
            Console.WriteLine("-------Liste des Pokemons-------");
            for (int i = 0; i < results.Count; i++)
            {
                Console.WriteLine(id+1 + " - " + results[i].name);
                id++;
            }
        }
    }
}
