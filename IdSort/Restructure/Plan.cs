using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IdSort.Restructure
{
    /// <summary>
    /// Represents a plan to move and soft-delete files.
    /// </summary>
    public class Plan
    {
        public const string RevertFileName = "revert.idsort";

        public string Root;
        public List<Operation> Operations;

        public Plan(string root, List<Operation> operations)
        {
            Root = root;
            Operations = operations;
        }

        public Plan(string root) : this(root, new List<Operation>()) {}

        public void Execute(Action<Operation> startingOperation, out Plan revertPlan)
        {
            // Clear cache for this path (it won't be accurate after this)
            ImportCache.RemoveCachedWorkspace(Root);

            var revertOps = new List<Operation>();

            // Loop through all planned operations and try to execute them
            foreach (var op in Operations)
            {
                try
                {
                    startingOperation?.Invoke(op);

                    op.Execute(Root);

                    // Add revert operation
                    revertOps.Add(op.GetRevertOperation());
                }
                catch (Exception e)
                {
                    // Failed to execute operation - throw exception with a plan to revert changes
                    revertPlan = new Plan(Root)
                    {
                        Operations = revertOps
                    };

                    throw new PlanException(revertPlan, op, "Restructure failed: " + e.Message, e);
                }
            }

            // Plan to revert this plan
            revertPlan = new Plan(Root)
            {
                Operations = revertOps
            };
        }

        public void Save(string path)
        {
            using var file = File.Open(path, FileMode.Create);

            // Write header and root path
            byte[] buffer = Encoding.UTF8.GetBytes("# This file allows IdSort to revert any changes it's made. You can delete it if you don't need to revert.\n" +
                                                   $"# Saved {DateTime.Now}\n\n# Root:\n" +
                                                   Root + "\n\n");
            file.Write(buffer, 0, buffer.Length);

            // Write operations
            foreach (Operation op in Operations)
            {
                buffer = Encoding.UTF8.GetBytes(op.ToString() + "\n");
                file.Write(buffer, 0, buffer.Length);
            }
        }

        public static Plan Load(string path)
        {
            using var file = File.Open(path, FileMode.Open);
            using var streamReader = new StreamReader(file, Encoding.UTF8);

            Plan plan = null;

            string line;
            while ((line = streamReader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                    continue; // blank or comment line

                if (plan == null)
                {
                    // Read root path and create plan
                    plan = new Plan(line.Trim());
                }
                else
                {
                    // Read operation
                    plan.Operations.Add(Operation.FromString(line.Trim()));
                }
            }

            return plan;
        }
    }
}
