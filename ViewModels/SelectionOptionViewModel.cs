using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SoundEventEditor.ViewModels
{
    public class SelectionOptionViewModel : SoundEventOptionViewModel
    {
        private string _value = "";
        public override object Value
        {
            get => _value;
            set => _value = value?.ToString() ?? "";
        }

        public override byte GetByte()
        {
            string val = (string)Value;

            foreach (var choice in _choiceTranslation)
            {
                if (choice.Value == val)
                {
                    return choice.Key;
                }
            }

            throw new Exception($"Unknown value on {Label}");
        }

        public ObservableCollection<string> Choices { get; }

        private readonly Dictionary<byte, string> _choiceTranslation;

        public SelectionOptionViewModel(string label, Dictionary<byte, string> choices, string initialValue)
        {
            _choiceTranslation = choices;
            LabelKey           = label;
            Choices            = new ObservableCollection<string>(choices.Values);

            for (int i = 0; i < Choices.Count; i++)
            {
                Choices[i] = LocaleManager.Instance[Choices[i]];
            }

            foreach (var id in _choiceTranslation.Keys)
            {
                _choiceTranslation[id] = LocaleManager.Instance[_choiceTranslation[id]];
            }

            initialValue = LocaleManager.Instance[initialValue];

            _value = initialValue ?? Choices.FirstOrDefault() ?? string.Empty;
        }
    }
}
