using System;
using System.Collections.Generic;
using System.Text;

namespace Barthel.Project.Cli.Abstractions
{
    public interface IActivity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Rating { get; set; }
        public string LengthInMinutes { get; set; }
    }
}
