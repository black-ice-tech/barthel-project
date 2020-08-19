using Barthel.Project.Cli.Domain;
using System;
using System.Collections.Generic;
using System.IO;

namespace Barthel.Project.Cli.Abstractions
{
    public interface IHistoryRepository
    {
        void AddActivity(IActivity activity);

        List<IActivity> GetHistory();
    }
}
