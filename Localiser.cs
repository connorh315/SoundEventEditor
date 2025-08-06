using SoundEventEditor.Localisation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundEventEditor
{
    public class Localiser
    {
        public static Dictionary<string, string> MainStrings;

        public static Dictionary<string, string> InfoStrings;

        public static void Initialise()
        {
            MainStrings = English.MainStrings;
        }

        public static string GetString(string key)
        {
            if (MainStrings.TryGetValue(key, out string value))
            {
                return value;
            }

            return "MsgNotFound";
        }

        public static string GetInfo(string key)
        {
            return "";
        }
    }
}
