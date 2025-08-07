using SoundEventEditor.SoundEvents;
using SoundEventEditor.ViewModels.SoundEvents;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SoundEventEditor.ViewModels
{
    public abstract class SoundEventViewModel : INotifyPropertyChanged
    {
        public enum SoundEventType
        {
            Event = 0,
            Sample,
            Stream,
            Group,
            BusConnection,
            Name = 6,
            Sequence,
            Conversation,
            Speech,
            StreamingSpeech,
            Character = 12
        }

        public static SoundEventViewModel CreateSoundEvent(SoundEventType type)
        {
            return type switch
            {
                SoundEventType.Event => new SoundEvent0ViewModel(),
                SoundEventType.Sample => new SoundEvent1ViewModel(),
                SoundEventType.Stream => new SoundEvent2ViewModel(),
                SoundEventType.Group => new SoundEvent3ViewModel(),
                SoundEventType.BusConnection => new SoundEvent4ViewModel(),
                SoundEventType.Name => new SoundEvent6ViewModel(),
                SoundEventType.Sequence => new SoundEvent7ViewModel(),
                SoundEventType.Conversation => new SoundEvent8ViewModel(),
                SoundEventType.Speech => new SoundEvent9ViewModel(),
                SoundEventType.StreamingSpeech => new SoundEvent10ViewModel(),
                SoundEventType.Character => new SoundEvent12ViewModel(),
                _ => null,
            };
        }

        public abstract string Title { get; }

        public abstract string BorderColour { get; }

        protected SoundEvent Model;

        public SoundEventViewModel Parent;

        public Action OnDestroy;

        protected virtual void BuildFromEvent(SoundEvent evt)
        {
            if (evt != null && evt.Children != null)
            {
                foreach (SoundEvent child in evt.Children)
                {
                    Children.Add(ConvertToViewModel(child));
                }
            }
        }

        public virtual SoundEvent RebuildEvent()
        {
            if (Model == null) throw new Exception("Event structure has collapsed");

            Model.Children = new(); // Some events may have been deleted, so it's important that everything is cleared from the model, so it can be rebuilt from new.
            foreach (var child in Children)
            {
                Model.Children.Add(child.RebuildEvent());
            }

            return Model;
        }

        private static SoundEventViewModel Convert(SoundEvent evt)
        {
            return evt switch
            {
                SoundEvent0 evt0 => new SoundEvent0ViewModel(evt0),
                SoundEvent1 evt1 => new SoundEvent1ViewModel(evt1),
                SoundEvent2 evt2 => new SoundEvent2ViewModel(evt2),
                SoundEvent3 evt3 => new SoundEvent3ViewModel(evt3),
                SoundEvent4 evt4 => new SoundEvent4ViewModel(evt4),
                SoundEvent6 evt6 => new SoundEvent6ViewModel(evt6),
                SoundEvent7 evt7 => new SoundEvent7ViewModel(evt7),
                SoundEvent8 evt8 => new SoundEvent8ViewModel(evt8),
                SoundEvent9 evt9 => new SoundEvent9ViewModel(evt9),
                SoundEvent10 evt10 => new SoundEvent10ViewModel(evt10),
                SoundEvent12 evt12 => new SoundEvent12ViewModel(evt12),
                _ => throw new NotImplementedException()
            };
        }

        public SoundEventViewModel ConvertToViewModel(SoundEvent evt)
        {
            var vm = Convert(evt);
            vm.Parent = this;
            return vm;
        }

        public static SoundEventViewModel OpenFile(string fileLocation, Action onRootNodeDestroy)
        {
            SoundEvent root = SoundEvent.OpenFile(fileLocation);

            var vm = Convert(root);
            vm.OnDestroy = onRootNodeDestroy;
            return vm;
        }

        public T GetValue<T>(string label)
        {
            foreach (var option in Options)
            {
                if (option.Label == label)
                {
                    return (T)(option.Value);
                }
            }

            return default;
        }

        public SoundEventOptionViewModel GetOption(string labelKey)
        {
            foreach (var option in Options)
            {
                if (option.LabelKey == labelKey)
                    return option;
            }

            return null;
        }

        protected string STR(byte val) => val.ToString();

        protected bool BOOL(byte val) => val != 0;

        public ObservableCollection<SoundEventViewModel> Connections { get; protected set; }
        public bool HasConnections
        {
            get => Connections != null;
            set => OnPropertyChanged();
        }

        public ObservableCollection<SoundEventOptionViewModel> Options { get; protected set; }

        public ObservableCollection<SoundEventType> SelectableChildren { get; protected set; }

        public ObservableCollection<SoundEventViewModel> Children { get; protected set; } = new();

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public void Destroy()
        {
            if (Parent != null)
            {
                if (Parent.Children.Contains(this))
                    Parent.Children.Remove(this);
                else if (Parent.Connections.Contains(this))
                    Parent.Connections.Remove(this);
            }
            else
            {
                if (OnDestroy != null)
                    OnDestroy();
            }
        }

        protected static readonly Dictionary<byte, string> FiletypeStrings = new()
        {
            { 0, "VAG" },
            { 1, "OGG" },
            { 3, "UNK3" },
            { 9, "CBX" },
        };
    }
}
