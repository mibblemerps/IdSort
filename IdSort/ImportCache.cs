using System;
using System.IO;

namespace IdSort
{
    /// <summary>
    /// Handles the caching of workspaces for quick loading of folders.
    /// </summary>
    public static class ImportCache
    {
        public static string CachePath = "cache";

        public static TimeSpan MaxCacheTime = TimeSpan.FromDays(14);

        static ImportCache()
        {
            // Create cache dir (if it doesn't exist)
            Directory.CreateDirectory(CachePath + Path.DirectorySeparatorChar + Workspace.FormatVersion);
        }

        /// <summary>
        /// Check if a cached workspace exists for a given workspace root.
        /// </summary>
        public static bool IsCachedWorkspaceAvailable(string workspaceRoot, out DateTime modifiedDate)
        {
            string cachedPath = GetCachedWorkspacePath(workspaceRoot);
            if (File.Exists(cachedPath))
            {
                modifiedDate = File.GetCreationTime(cachedPath);

                if (DateTime.Now - modifiedDate >= MaxCacheTime)
                {
                    // Past max cache time
                    File.Delete(cachedPath);
                    return false;
                }

                return true;
            }
            else
            {
                modifiedDate = DateTime.MinValue;
                return false;
            }
        }

        public static void CacheWorkspace(Workspace workspace)
        {
            workspace.Save(GetCachedWorkspacePath(workspace.RootPath));
        }

        /// <summary>
        /// Load the cached workspace for a workspace root.
        /// </summary>
        public static Workspace GetCachedWorkspace(string workspaceRoot)
        {
            if (!IsCachedWorkspaceAvailable(workspaceRoot, out _))
                return null; // cached workspace doesn't exist

            return Workspace.Load(GetCachedWorkspacePath(workspaceRoot));
        }
        
        public static void RemoveCachedWorkspace(string workspaceRoot)
        {
            File.Delete(GetCachedWorkspacePath(workspaceRoot));
        }

        /// <summary>
        /// Clear the entire workspace cache.
        /// </summary>
        public static void ClearCache()
        {
            // Delete cache directory, then recreate it
            Directory.Delete(CachePath, true);
            Directory.CreateDirectory(CachePath);
        }

        private static string GetCachedWorkspacePath(string workspaceRoot)
        {
            string rootHash = HashUtil.CreateMd5(Path.GetFullPath(workspaceRoot).Trim().ToLowerInvariant()).ToLowerInvariant();

            return CachePath + Path.DirectorySeparatorChar + Workspace.FormatVersion + Path.DirectorySeparatorChar + rootHash + ".json";
        }
    }
}
