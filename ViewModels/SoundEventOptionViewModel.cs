using System;

namespace SoundEventEditor.ViewModels
{
    public abstract class SoundEventOptionViewModel
    {
        public string Label => LocaleManager.Instance[LabelKey];
        public string LabelKey { get; protected set; }
        public abstract object Value { get; set; }

        public abstract byte GetByte();

        public virtual float GetFloat()
        {
            throw new Exception($"Attempted to get float from {Label}");
        }
    }
}
