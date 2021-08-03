using System.IO;

namespace IdSort.Restructure
{
    public static class Planner
    {
        public static Plan Plan(this Workspace workspace)
        {
            var plan = new Plan(workspace.RootPath);

            foreach (GenericFile file in workspace.Files)
            {
                if (file.StagingPath == null) continue; // No staged path to move to

                plan.Operations.Add(Operation.Move(Path.GetRelativePath(workspace.RootPath, file.Path),
                                                       Path.GetRelativePath(workspace.RootPath, file.StagingPath)));
            }

            return plan;
        }
    }
}
