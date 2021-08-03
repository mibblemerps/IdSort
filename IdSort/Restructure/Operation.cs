using System;
using System.IO;

namespace IdSort.Restructure
{
    public sealed class Operation
    {
        public readonly string FromPath;
        public readonly string ToPath;

        public Operation(string fromPath, string toPath)
        {
            FromPath = fromPath;
            ToPath = toPath;
        }

        public void Execute(string root)
        {
            string destination = root + Path.DirectorySeparatorChar + ToPath;
            string destinationDir = Path.GetDirectoryName(destination);

            if (!Directory.Exists(destinationDir))
                Directory.CreateDirectory(destinationDir);

            File.Move(root + Path.DirectorySeparatorChar + FromPath, destination);
        }

        /// <summary>
        /// Gets an operation that would effectively revert this operation.
        /// </summary>
        public Operation GetRevertOperation()
        {
            return new Operation(ToPath, FromPath);
        }

        /// <summary>
        /// Move a file from one location to another.
        /// </summary>
        /// <param name="from">Path of file to move</param>
        /// <param name="to">Destination</param>
        /// <returns></returns>
        public static Operation Move(string from, string to)
        {
            return new Operation(from, to);
        }

        /// <summary>
        /// Pseudo-delete a file by moving it into the deleted directory with a random file name.
        /// </summary>
        /// <param name="path">Path of file to delete</param>
        /// <returns></returns>
        public static Operation Delete(string path)
        {
            return new Operation(path, ".deleted" + Path.DirectorySeparatorChar + Guid.NewGuid().ToString().Replace("-", ""));
        }

        /// <summary>
        /// Converts this operation into a string. This can then be converted back into an <see cref="Operation"/> using <see cref="FromString"/>.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{FromPath} -> {ToPath}";
        }

        public static Operation FromString(string str)
        {
            string[] parts = str.Split("->");
            if (parts.Length != 2) return null;

            return new Operation(parts[0].Trim('"').Trim(), parts[1].Trim('"').Trim());
        }
    }
}
