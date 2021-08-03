using System;
using System.IO;
using System.Linq;
using IdSort.FileVars;

namespace IdSort.Restructure
{
    public static class Restructurer
    {
        public static void Restructure(Workspace workspace, RestructureSettings settings, out RestructureResults results)
        {
            results = new RestructureResults();

            foreach (GenericFile file in workspace.Files)
                ReformatFile(file, workspace, settings, results);
        }

        public static void ReformatFile(GenericFile file, Workspace workspace, RestructureSettings settings, RestructureResults results = null)
        {
            if (file is not AudioFile audioFile) return; // only audio files can be restructured for now

            bool resolved = false;
            foreach (string formatString in settings.FormatStrings)
            {
                resolved = true;
                string output = workspace.RootPath + Path.DirectorySeparatorChar + formatString;
                foreach (IFileVar var in FileVarHelper.Vars)
                {
                    if (!output.Contains("{" + var.Name + "}")) continue; // Var not used - skip

                    string value = var.GetValue(audioFile, settings);

                    if (value == null)
                    {
                        resolved = false;
                        break; // Var cannot resolve - try next format string
                    }
                    
                    output = output.Replace("{" + var.Name + "}", value, StringComparison.InvariantCultureIgnoreCase);
                }

                if (resolved)
                {
                    // Successfully resolved! Don't try any more format strings.
                    if (output != file.Path) // (we only set the staging path if it's different to path it's already at)
                    {
                        file.StagingPath = DeconflictPath(output, workspace);
                        if (results != null) results.ReformattedFiles++;
                    }
                    break;
                }
            }

            if (!resolved)
            {
                // File completely unable to be resolved
                if (results != null) results.UnresolvableFiles++;

                if (settings.ClearStagingPathForUnresolvableFiles)
                    file.StagingPath = null; // clear staging path
            }
        }

        private static string DeconflictPath(string path, Workspace workspace)
        {
            string safePath = path;
            int i = 0;
            while (workspace.Files.Any(f => f.StagingOrRealPath == safePath))
            {
                safePath = Path.GetDirectoryName(path) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(path) +
                           " (" + ++i + ")" + Path.GetExtension(path);
            }

            return safePath;
        }

        public class RestructureResults
        {
            public int ReformattedFiles;
            public int UnresolvableFiles;
        }

        public class RestructureSettings
        {
            public string[] FormatStrings;
            public bool NeverUseVariousArtists;
            public bool ClearStagingPathForUnresolvableFiles = true;

            public RestructureSettings(string[] formatStrings)
            {
                FormatStrings = formatStrings;
            }
        }
    }
}
