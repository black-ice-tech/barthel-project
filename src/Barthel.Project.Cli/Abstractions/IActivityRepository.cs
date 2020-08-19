using System.Collections.Generic;

namespace Barthel.Project.Cli.Abstractions
{
    public interface IActivityRepository
    {
        List<IActivity> GetAllActivities<T>() where T : IActivity, new();
    }
}
