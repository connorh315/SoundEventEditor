using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SoundEventEditor.ViewModels
{
    public class StringOptionViewModel : SoundEventOptionViewModel
    {
        private string _value = "";
        public override object? Value
        {
            get => _value;
            set => _value = value?.ToString() ?? "";
        }

        public bool IsNumeric { get; set; } = false;

        public bool IsFloat { get; set; } = false;

        public override byte GetByte()
        {
            if (!byte.TryParse((string)Value, out byte val))
            {
                throw new Exception($"Could not convert string value to byte: {Label}");
            }

            return val;
        }

        public override float GetFloat()
        {
            if (!float.TryParse((string)Value, out float val))
            {
                throw new Exception($"Could not convert string value to float: {Label}");
            }

            return val;
        }

        public StringOptionViewModel(string label, string value, bool isNumeric = false, bool isFloat = false)
        {
            LabelKey = label;
            Value = value;
            IsNumeric = isNumeric;
            IsFloat = isFloat;
        }

        public StringOptionViewModel(string label, int value, bool isNumeric = true, bool isFloat = false)
            : this(label, value.ToString(), isNumeric, isFloat)
        { 
        }

        public StringOptionViewModel(string label, float value, bool isNumeric = true, bool isFloat = true)
        : this(label, value.ToString("0.#####"), isNumeric, isFloat)
        {
        }
    }
}
