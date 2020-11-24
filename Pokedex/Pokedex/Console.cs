using System;

namespace Pokedex
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Centrer("Bienvenue sur le Pokedex");
            Centrer("Voici les commandes dont vous aurez besoin pour l'utiliser\n");
            Help();
        }

        public static void Centrer(string texte)
        {
            int nbEspaces = (Console.WindowWidth - texte.Length) / 2;
            Console.SetCursorPosition(nbEspaces, Console.CursorTop);
            Console.WriteLine(texte);
        }

        public static void Help()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("/help");
            Console.ResetColor();
            Console.WriteLine(" : affiche la liste des commandes disponibles et leurs descriptions");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("/liste n");
            Console.ResetColor();
            Console.WriteLine(" : affiche la liste des pokemons par pages de n éléments. (Exemple : /liste 10)");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("/pokemon id");
            Console.ResetColor();
            Console.WriteLine(" : affiche les détails du pokemon n° id dans le pokedex. (Exemple : /pokemon 25)");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("/pokemon name");
            Console.ResetColor();
            Console.WriteLine(" : affiche les détails du pokemon dont le nom est name (nom anglais seulement) dans le pokedex. (Exemple : /pokemon pikachu)");

        }
    }
}
