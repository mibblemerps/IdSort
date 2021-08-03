using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdSort.Restructure;

namespace IdSort.FileVars
{
    public class DiscVar : IFileVar
    {
        public string Name => "disc";
        public string Description => "Disc #";

        public string GetValue(AudioFile file, Restructurer.RestructureSettings settings)
        {
            return file.Tags?.Disc != null ? file.Tags.Disc.ToString() : null;
        }
    }
}
