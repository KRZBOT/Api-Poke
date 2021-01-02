using Donnees;
using RecupDonnees;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Pokedex
{
    class Program
    {
        static void Main(string[] args)
        {
            ApiHelper.InitializeClient();

            InitializeConsole();
        }

        private static void InitializeConsole()
        {
            var commands = new Dictionary<string, Action<string>>
            {
                ["liste"] = ListeCommand,
                ["pokemon"] = PokemonCommand,
            };

            DefaultConsole();

            do
            {
                var arg = Console.ReadLine().Split(' ');
                if (arg[0].StartsWith("/"))
                {
                    if (commands.TryGetValue(arg[0].Substring(1), out var command))
                    {
                        command(arg[1]);
                    }
                    else
                    {
                        Console.WriteLine("Cette commande n'existe pas. Tapez help pour plus d'infos");
                    }
                }
                else
                {
                    string commande = String.Concat(arg);
                    switch (commande)
                    {
                        case "help":
                            HelpCommand();
                            break;
                        case "exit":
                            Environment.Exit(0);
                            break;
                        default:
                            if (commande != "")
                            {
                                Console.WriteLine("Nous n'avons pas compris cette commande. Tapez help pour plus d'infos");
                            }
                            break;
                    }
                }
            } while (true);
        }

        public static void Centrer(string texte)
        {
            int nbEspaces = (Console.WindowWidth - texte.Length) / 2;
            Console.SetCursorPosition(nbEspaces, Console.CursorTop);
            Console.WriteLine(texte);
        }

        public static void HelpCommand()
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

        public static async void ListeCommand(string arg)
        {
            if (arg == "")
            {
                arg = "20";
            }
            try
            {
                int limit = Int32.Parse(arg);
                int page = 1;
                int id = 0;
                //Appel de l'API
                Liste liste = null;
                Task t = Task.Run(async () => {liste = await ApiCalls.GetList(limit); });
                t.Wait();

                int nbTotal = 1118;
                if (limit <= 0 || limit > 1118)
                {
                    limit = nbTotal;
                }                        
                int pageMax = (int)Math.Ceiling((double)nbTotal / limit);
                bool running = true;

                liste.Afficher(id);
                ListeFooter(page, pageMax);
                do
                {
                    ConsoleKey ck = Console.ReadKey().Key;
                    switch (ck)
                    {
                        case ConsoleKey.Spacebar:
                            // page suivante
                            page++;
                            id += limit;
                            t = Task.Run(async () => { liste = await ApiCalls.GetNextList(liste);});
                            t.Wait();
                            if (page < pageMax)
                            {
                                liste.Afficher(id);
                                ListeFooter(page, pageMax);
                            }
                            else if(page == pageMax)
                            {
                                liste.Afficher(id);
                                ListeFooter(page, pageMax);
                            }
                            else
                            {
                                Console.Clear();
                                DefaultConsole();
                                running = false;
                            }
                            break;
                        case ConsoleKey.Escape:
                            Console.Clear();
                            DefaultConsole();
                            running = false;
                            break;
                        default:
                            Console.WriteLine("defaut");
                            break;
                    }
                } while (running);
            }
            catch
            {
                Console.WriteLine(arg + " n'est pas un nombre valide");
            }
        }
        public static async void PokemonCommand(string arg)
        {
            try
            {
                var pokemon = await ApiCalls.GetPokemon(arg);
                var species = await ApiCalls.GetDescription(pokemon.id);
                pokemon.Afficher(species);
            }
            catch
            {
                Console.WriteLine("Le pokemon " + arg + " n'existe pas.");
            } 
        }

        public static void DefaultConsole()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Centrer("Bienvenue sur le Pokedex");
            Centrer("Voici les commandes dont vous aurez besoin pour l'utiliser\n");
            HelpCommand();
        }

        public static void ListeFooter(int page, int pageMax)
        {
            Console.WriteLine("Page " + page + "/" + pageMax);
            Console.WriteLine("Appuyez sur espace pour passer à la page suivante et sur echap pour quitter");
        }
    }
}
