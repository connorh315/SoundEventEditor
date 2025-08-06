using Avalonia.Remote.Protocol;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundEventEditor.ViewModels
{
    public class SelectionOptionViewModel : SoundEventOptionViewModel
    {
        private string _value = "";
        public override object? Value
        {
            get => _value;
            set => _value = value?.ToString() ?? "";
        }

        public override byte GetByte()
        {
            string val = (string)Value;

            foreach (var choice in choiceTranslation)
            {
                if (choice.Value == val)
                    return choice.Key;
            }

            throw new Exception($"Unknown value on {Label}");
        }

        public ObservableCollection<string> Choices { get; }

        private Dictionary<byte, string> choiceTranslation;

        public SelectionOptionViewModel(string label, Dictionary<byte, string> choices, string initialValue)
        {
            LabelKey = label;
            Choices = new ObservableCollection<string>(choices.Values);
            choiceTranslation = choices;

            for (int i = 0; i < Choices.Count(); i++)
            {
                Choices[i] = Localiser.GetString(Choices[i]);
            }

            foreach (var id in choiceTranslation.Keys)
            {
                choiceTranslation[id] = Localiser.GetString(choiceTranslation[id]);
            }

            initialValue = Localiser.GetString(initialValue);

            _value = initialValue ?? Choices.FirstOrDefault() ?? string.Empty;
        }
    }
}
