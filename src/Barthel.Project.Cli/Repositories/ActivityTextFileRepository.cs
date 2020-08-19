using Barthel.Project.Cli.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Barthel.Project.Cli.Repositories
{
    public class ActivityTextFileRepository : IActivityRepository
    {
        private readonly string _filePath;
        private readonly int _columnName;
        private readonly int _columnDesc;
        private readonly int _columnRating;
        private readonly int _columnLength;

        public ActivityTextFileRepository(string filePath)
        {
            _filePath = filePath;

            string[] fileContents = File.ReadAllLines(_filePath);

            var columnTitle = fileContents[0].Split("|");

            _columnName = Array.IndexOf(columnTitle, "Name");
            _columnDesc = Array.IndexOf(columnTitle, "Description");
            _columnRating = Array.IndexOf(columnTitle, "Rating");
            _columnLength = Array.IndexOf(columnTitle, "Length");
        }

        public List<IActivity> GetAllActivities<T>()
            where T : IActivity, new()
        {
            var fileContents = File.ReadAllLines(_filePath);

            var activities = new List<IActivity>();

            for (int i = 1; i < fileContents.Length; i++)
            {
                var line = fileContents[i];
                activities.Add(ConvertFrom<T>(line));
            }

            return activities;
        }

        private T ConvertFrom<T>(string activity)
            where T : IActivity, new()
        {
            var splitString = activity.Split("|").ToList();

            return new T
            {
                Name = splitString[_columnName],
                Description = splitString[_columnDesc],
                Rating = splitString[_columnRating],
                LengthInMinutes = splitString[_columnLength]
            };
        }
    }
}
