using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SoundEventEditor.ViewModels
{
    public class StringListOptionViewModel : SoundEventOptionViewModel
    {
        public override object Value
        {
            get => Values;
            set => throw new Exception($"Should not be setting .Value on a StringListOptionViewModel ({Label})");
        }
        
        public ObservableCollection<StringItemViewModel> Values { get; private set; } = [];

        public List<string> StringValues
        {
            get
            {
                var list = new List<string>();

                foreach (var item in Values)
                {
                    list.Add(item.Value);
                }

                return list;
            }
        }

        public void AddItem() => Values.Add(new StringItemViewModel(string.Empty));

        public ICommand RemoveCommand { get; }

        public void RemoveItem(object parameter)
        {
            if (parameter is StringItemViewModel item)
            {
                Values.Remove(item);
            }
        }

        public StringListOptionViewModel(string label, IEnumerable<string> values)
        {
            RemoveCommand = new DelegateCommand(RemoveItem);

            LabelKey = label;
            if (values != null)
            {
                foreach (var item in values)
                {
                    Values.Add(new StringItemViewModel(item));
                }
            }
        }

        public override byte GetByte() => throw new Exception($"Getting byte value from a list of strings: {Label}");
    }
}
