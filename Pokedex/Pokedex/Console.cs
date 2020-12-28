using System;
using System.Collections.Generic;

namespace Pokedex
{
    class Program
    {
        static void Main(string[] args)
        {
            Boolean running = true;
            var commands = new Dictionary<string, Action<string>>
            {
                ["liste"] = ListeCommand,
            };

            Console.ForegroundColor = ConsoleColor.Cyan;
            Centrer("Bienvenue sur le Pokedex");
            Centrer("Voici les commandes dont vous aurez besoin pour l'utiliser\n");
            HelpCommand("");

            do
            {
                var arg = Console.ReadLine().Split(' ');
                if (arg[0].StartsWith("/"))
                {
                    if (commands.TryGetValue(arg[0].Substring(1), out var command))
                    {
                        command(arg[0]);
                    }
                }
                else
                {
                    string commande = String.Concat(arg);
                    switch (commande)
                    {
                        case "help":
                            HelpCommand("");
                            break;
                        case "exit":
                            running = false;
                            break;
                        default :
                            if(commande != "")
                            {
                                Console.WriteLine("Nous n'avons pas compris cette commande. Tapez /help pour plus d'infos");
                            }
                            break;
                    }
                }
            } while(running);       
        }

        public static void Centrer(string texte)
        {
            int nbEspaces = (Console.WindowWidth - texte.Length) / 2;
            Console.SetCursorPosition(nbEspaces, Console.CursorTop);
            Console.WriteLine(texte);
        }

        public static void HelpCommand(string arg)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("\nhelp");
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
            Console.WriteLine(" : affiche les détails du pokemon dont le nom anglais est 'name' dans le pokedex. (Exemple : /pokemon charizard)");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("exit");
            Console.ResetColor();
            Console.WriteLine(" : Quitter le pokédex");
        }

        public static void ListeCommand(string arg)
        {
            Console.WriteLine("Voici la liste des pokemons : ");
            //Inserer requete api ici
            //
            //
            //
            /*-----------------------*/
            Console.WriteLine("Appuyez sur espace pour passer à la page suivante et sur echap pour quitter");
            bool running = true;
            do
            {
                switch (Console.ReadKey().Key)
                {
                    case (ConsoleKey.Spacebar):
                        Console.WriteLine("page suivante..");
                        // page suivante
                        break;
                    case (ConsoleKey.Escape):
                        Console.WriteLine("_Fermeture de la liste.");
                        running = false;
                        break;
                    default:
                        break;
                }
            } while (running);
        }

        public static void PokemonCommand(string arg)
        {

        }
    }
}
