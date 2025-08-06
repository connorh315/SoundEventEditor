using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundEventEditor
{
    public static class AppSettings
    {
        public const string AppName = "SoundEvent Editor";
        public const string Version = "1.0.0";

        public static string DisplayName => $"{AppName} - {Version}";
    }
}
