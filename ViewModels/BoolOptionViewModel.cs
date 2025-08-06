using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundEventEditor.ViewModels
{
    public class BoolOptionViewModel : SoundEventOptionViewModel
    {
        private bool _value;
        public override object? Value
        {
            get => _value;
            set => _value = value is bool b ? b : _value;
        }

        public override byte GetByte() => (byte)((bool)Value ^ InvertValue ? 1 : 0);

        private bool InvertValue;

        public BoolOptionViewModel(string label, byte value, bool shouldInvert = false)
        {
            LabelKey = label;
            Value = (value == 1 ^ shouldInvert) ? true : false;
            InvertValue = shouldInvert;
        }
    }
}
