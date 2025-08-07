namespace SoundEventEditor.ViewModels
{
    public class BoolOptionViewModel : SoundEventOptionViewModel
    {
        private bool _value;
        public override object Value
        {
            get => _value;
            set => _value = value is bool b ? b : _value;
        }

        private readonly bool _invertValue;
        public override byte GetByte() => (byte)((bool)Value ^ _invertValue ? 1 : 0);

        public BoolOptionViewModel(string label, byte value, bool shouldInvert = false)
        {
            _invertValue = shouldInvert;
            LabelKey     = label;
            Value        = value == 1 ^ shouldInvert;
        }
    }
}
