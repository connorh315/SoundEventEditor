using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundEventEditor.ViewModels
{
    public abstract class SoundEventOptionViewModel
    {
        public string Label { get => Localiser.GetString(LabelKey); }
        public string LabelKey { get; protected set; }
        public abstract object? Value { get; set; }

        public abstract byte GetByte();

        public virtual float GetFloat()
        {
            throw new Exception($"Attempted to get float from {Label}");
        }
    }
}
