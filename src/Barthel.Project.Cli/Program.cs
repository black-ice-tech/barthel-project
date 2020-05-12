using System;
using System.Collections.Generic;
using System.IO;

namespace Barthel.Project.Cli
{
    public interface IActivity
    {
        string Choice();
        void Selection();
    }
    public class Movie : IActivity
    {
        public string input;
        public string choice;
        public int choiceInt;
        public List<string> movieName = new List<string>();
        public List<string> movieDesc = new List<string>();

        public string Choice()
        {
            string moviePath = @"C:\temp\movies.txt";
            File.OpenRead(moviePath);
            using (StreamReader sr = File.OpenText(moviePath))
            {
                string m;
                int i = 1;
                Console.WriteLine("Select a movie:");
                while ((m = sr.ReadLine()) != null)
                {
                    string[] movieLine = m.Split(": ");
                    string name = movieLine[0];
                    string desc = movieLine[1];
                    movieName.Add(name);
                    movieDesc.Add(desc);
                    Console.WriteLine(i + ". " + name);
                    i = i + 1;
                }
            }
            choice = Console.ReadLine();
            choiceInt = Convert.ToInt32(choice);
            if(choiceInt - 1 > movieName.Count)
            {
                Console.WriteLine("Please select a number between 1 - " + movieName.Count);
                choice = Console.ReadLine();
                choiceInt = Convert.ToInt32(choice);
            }
            string choiceName = movieName[choiceInt - 1];
            return choiceName;
        }
        public void Selection()
        {
            int c = choiceInt - 1;

            Console.WriteLine(movieName[c] + ": " + movieDesc[c]);
        }
    }
    public class Game : IActivity
    {
        public string input;
        public string choice;
        public int choiceInt;
        public List<string> gameName = new List<string>();
        public List<string> gameDesc = new List<string>();

        public string Choice()
        {
            string gamePath = @"C:\temp\games.txt";
            File.OpenRead(gamePath);
            using (StreamReader sr = File.OpenText(gamePath))
            {
                string g;
                int i = 1;
                Console.WriteLine("Select a game:");
                while ((g = sr.ReadLine()) != null)
                {
                    string[] gameList = g.Split(": ");
                    string name = gameList[0];
                    string desc = gameList[1];
                    gameName.Add(name);
                    gameDesc.Add(desc);
                    Console.WriteLine(i + ". " + name);
                    i = i + 1;
                }
            }
            choice = Console.ReadLine();
            choiceInt = Convert.ToInt32(choice);
            if(choiceInt - 1 > gameName.Count)
            {
                Console.WriteLine("Please chose a number between 1 - " + gameName.Count);
                choice = Console.ReadLine();
                choiceInt = Convert.ToInt32(choice);
            }
            string choiceName = gameName[choiceInt - 1];
            return choiceName;
        }
        public void Selection()
        {
            int c = choiceInt - 1;

            Console.WriteLine(gameName[c] + ": " + gameDesc[c]);
        }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            string input = "intialization";
            do
            {
                Console.WriteLine("Do you want to watch a movie or play a game or review your history? Please write 'exit' to exit.");
                input = Console.ReadLine();
                if (input == "movie")
                {
                    Movie testMovie = new Movie();
                    var choice = testMovie.Choice();
                    testMovie.Selection();
                    var change = ChangeCase.MakeUpper(input);
                    Hist.MakeHistory(change, choice);

                }
                else if (input == "game")
                {
                    Game testGame = new Game();
                    var choice = testGame.Choice();
                    testGame.Selection();
                    var change = ChangeCase.MakeUpper(input);
                    Hist.MakeHistory(change, choice);
                }
                else if (input == "history")
                {
                    Hist.History();
                }
                Console.WriteLine("-----------------");
            } while (input != "exit");
        }
    }

    public class Hist
    {
        public static void MakeHistory(string change, string choice)
        {
            string path = @"C:\temp\UserSelectionHistory.txt";
            string userResult = change + ": " + choice + "\n";
            File.AppendAllText(path, userResult);
        }
        public static void History()
        {
            string path = @"C:\temp\UserSelectionHistory.txt";
            foreach (string lst in File.ReadAllLines(path))
            {
                Console.WriteLine(lst);
            }
        }
    }
    public class ChangeCase
    {
        public static string MakeUpper(string input)
        {
            char[] change = input.ToCharArray();
            var newCharacter = char.ToUpper(change[0]);
            change[0] = newCharacter;
            return new string(change);
        }
    }
}
