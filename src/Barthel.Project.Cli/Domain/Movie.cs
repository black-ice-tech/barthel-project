using Barthel.Project.Cli.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Barthel.Project.Cli.Domain
{
    public class Movie : IActivity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Rating { get; set; }
        public string LengthInMinutes { get; set; }
    }
}
