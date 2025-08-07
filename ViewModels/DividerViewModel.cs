using System;

namespace SoundEventEditor.ViewModels
{
    internal class DividerViewModel : SoundEventOptionViewModel
    {
        public override object Value
        {
            get => throw new Exception("Getting value of a divider");
            set => throw new Exception("Setting value of a divider");
        }

        public override byte GetByte() => throw new Exception($"Getting value of a divider: {Label}");

        public DividerViewModel(string label)
        {
            LabelKey = label;
        }
    }
}
