using SoundEventEditor.Localisation;
using System.Collections.Generic;

namespace SoundEventEditor
{
    public class LocaleManager
    {
        public static LocaleManager Instance { get; } = new();

        private static Dictionary<string, string> _mainStrings;

        public LocaleManager()
        {
            _mainStrings = English.MainStrings;
        }

        public string this[string key]
        {
            get
            {
                if (_mainStrings.TryGetValue(key, out string value))
                {
                    return value;
                }

                return "MsgNotFound";
            }
        }
    }
}
