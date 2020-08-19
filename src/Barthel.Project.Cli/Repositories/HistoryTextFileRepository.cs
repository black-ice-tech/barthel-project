using Barthel.Project.Cli.Abstractions;
using Barthel.Project.Cli.Domain;
using System;
using System.Collections.Generic;
using System.IO;

namespace Barthel.Project.Cli.Repositories
{
    public class HistoryTextFileRepository : IHistoryRepository
    {
        private readonly string _historyTextFile;

        public HistoryTextFileRepository(string historyTextFile)
        {
            _historyTextFile = historyTextFile;
        }

        public void AddActivity(IActivity activity)
        {
            using (StreamWriter w = File.AppendText(_historyTextFile))
            {
                w.WriteLine($"{activity.GetType().Name}: {activity.Name}");
            }
        }

        public List<IActivity> GetHistory()
        {
            var activityList = new List<IActivity>();

            var fileExists = File.Exists(_historyTextFile);

            if (!fileExists)
            {
                File.Create(_historyTextFile);
            }

            foreach (var line in File.ReadAllLines(_historyTextFile))
            {
                var activity = ConvertFrom(line);
                activityList.Add(activity);
            }

            return activityList;
        }

        private IActivity ConvertFrom(string historyItem)
        {
            var activityType = historyItem.Split(":")[0];
            var activityName = historyItem.Split(":")[1].Trim();

            IActivity activity;

            switch (activityType.ToLower())
            {
                case "movie":
                    activity = new Movie
                    {
                        Name = activityName
                    };
                    break;
                case "game":
                    activity = new Game
                    {
                        Name = activityName
                    };
                    break;
                default:
                    throw new Exception($"Text file is invalid, unknown type: {activityType}");
            }

            return activity;
        }
    }
}
