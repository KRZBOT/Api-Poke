using Donnees;
using RecupDonnees;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Runtime.Caching;

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
            DefaultMessage();

            var commands = new Dictionary<string, Action<string>>
            {
                ["liste"] = ListeCommand,
                ["pokemon"] = PokemonCommand,
            };

            UserEntry(commands);
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
            Console.Write("\n/liste n");
            Console.ResetColor();
            Console.WriteLine(" : affiche la liste des pokemons par pages de n éléments. (Exemple : /liste 10)");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("/pokemon id");
            Console.ResetColor();
            Console.WriteLine(" : affiche les détails du pokemon n° [id] dans le pokedex. (Exemple : /pokemon 25)");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("/pokemon name");
            Console.ResetColor();
            Console.WriteLine(" : affiche les détails du pokemon dont le nom anglais est [name] dans le pokedex. (Exemple : /pokemon charizard)");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("exit");
            Console.ResetColor();
            Console.WriteLine(" : Quitter le pokédex");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("help");
            Console.ResetColor();
            Console.WriteLine(" : affiche la liste des commandes disponibles et leurs descriptions\n");
        }

        public static void ListeCommand(string arg)
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
                            if (page < pageMax)
                            {
                                page++;
                                id += limit;
                                t = Task.Run(async () => { liste = await ApiCalls.GetNextList(liste); });
                                t.Wait();
                                liste.Afficher(id);
                                ListeFooter(page, pageMax);
                            }
                            break;
                        case ConsoleKey.Backspace:
                            //page précédente
                            if (page > 1)
                            {
                                page--;
                                id -= limit;
                                t = Task.Run(async () => { liste = await ApiCalls.GetPreviousList(liste); });
                                t.Wait();
                                liste.Afficher(id);
                                ListeFooter(page, pageMax);
                                
                            }
                            break;
                        case ConsoleKey.Escape:
                            Console.Clear();
                            DefaultMessage();
                            running = false;
                            break;
                        case ConsoleKey.Divide:
                            var args = Console.ReadLine().Split(' ');
                            Console.WriteLine(args[0] + " et " + args[1]);
                            if (args[0].StartsWith("pokemon"))
                            {
                                PokemonCommand(args[1]);
                            }
                            else
                            {
                                Console.WriteLine("Cette commande n'existe pas ou n'est pas disponible");
                            }
                            ListeFooter(page, pageMax);
                            break;
                        default:
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
                var species = await ApiCalls.GetSpecies(pokemon.id);
                var evolutionChain = await ApiCalls.GetEvolution(species.evolution_chain.url);
                              
                pokemon.Afficher(species, evolutionChain);
            }
            catch
            {
                Console.WriteLine("Le pokemon " + arg + " n'existe pas.");
            } 
        }

        public static void DefaultMessage()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Centrer("Bienvenue sur le Pokedex");
            Centrer("Voici les commandes dont vous aurez besoin pour l'utiliser\n");
            HelpCommand();
        }

        public static void ListeFooter(int page, int pageMax)
        {
            Console.WriteLine("...Page " + page + "/" + pageMax);
            Console.WriteLine("\nTouches :");
            if (page < pageMax) { Console.WriteLine("- Page suivante : [Espace]"); }
            if (page > 1) { Console.WriteLine("- Page précédente : [Retour arrière]"); }
            Console.WriteLine("- Quitter : [Echap]");
        }

        public static void UserEntry(Dictionary<string, Action<string>> commands)
        {
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
    }
}
