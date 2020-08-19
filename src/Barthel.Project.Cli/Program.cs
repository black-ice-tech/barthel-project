using Barthel.Project.Cli.Abstractions;
using Barthel.Project.Cli.Domain;
using Barthel.Project.Cli.Repositories;
using System;
using System.Collections.Generic;

namespace Barthel.Project.Cli
{
    class Program
    {
        public static void Main(string[] args)
        {
            IActivityRepository gameRepository = new ActivityTextFileRepository("games.txt");
            IActivityRepository movieRepository = new ActivityTextFileRepository("movies.txt");
            IHistoryRepository historyRepository = new HistoryTextFileRepository("history.txt");

            string userInput = "";

            while (userInput != "quit")
            {
                Console.WriteLine("Do you want to play a game or watch a movie or view your history? Type 'quit' to exit");

                userInput = Console.ReadLine();

                List<IActivity> activities;
                switch (userInput.ToLower())
                {
                    case "movie":
                        activities = movieRepository.GetAllActivities<Movie>();
                        break;
                    case "game":
                        activities = gameRepository.GetAllActivities<Game>();
                        break;
                    case "history":
                        activities = historyRepository.GetHistory();

                        foreach (var activity in activities)
                        {
                            Console.WriteLine($"{activity.GetType().Name}: {activity.Name}");
                        }

                        continue;
                    case "quit":
                        return;
                    default:
                        continue;
                }

                Console.WriteLine($"Select a {userInput}:");

                for (int i = 0; i < activities.Count; i++)
                {
                    var currentActivity = activities[i];
                    Console.WriteLine($"{i + 1}) {currentActivity.Name}");
                }

                var selection = int.Parse(Console.ReadLine());
                var selectedActivity = activities[selection-1];
                historyRepository.AddActivity(selectedActivity);
                Console.WriteLine($"You selected \"{selectedActivity.Name}\": {selectedActivity.Description}");

                Console.WriteLine("Would you like to see more details?");

                var yesOrNo = Console.ReadLine();

                if (yesOrNo == "yes")
                {
                    Console.WriteLine($"Rating: {selectedActivity.Rating}");
                    Console.WriteLine($"Length: {selectedActivity.LengthInMinutes}m");
                }
            }
        }
    }
}
