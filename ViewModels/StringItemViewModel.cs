using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SoundEventEditor.ViewModels
{
    public class StringItemViewModel
    {
        private string _value;
        public string Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }

        public StringItemViewModel(string value) => _value = value;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
