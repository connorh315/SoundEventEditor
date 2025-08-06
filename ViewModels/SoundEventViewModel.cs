using SoundEventEditor.SoundEvents;
using SoundEventEditor.ViewModels.SoundEvents;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
            switch (type)
            {
                case SoundEventType.Event:
                    return new SoundEvent0();
                case SoundEventType.Sample:
                    return new SoundEvent1();
                case SoundEventType.Stream:
                    return new SoundEvent2();
                case SoundEventType.Group:
                    return new SoundEvent3();
                case SoundEventType.BusConnection:
                    return new SoundEvent4();
                case SoundEventType.Name:
                    return new SoundEvent6();
                case SoundEventType.Sequence:
                    return new SoundEvent7();
                case SoundEventType.Conversation:
                    return new SoundEvent8();
                case SoundEventType.Speech:
                    return new SoundEvent9();
                case SoundEventType.StreamingSpeech:
                    return new SoundEvent10();
                case SoundEventType.Character:
                    return new SoundEvent12();
            }

            return null;
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
                SEVT_0 evt0 => new SoundEvent0(evt0),
                SEVT_1 evt1 => new SoundEvent1(evt1),
                SEVT_2 evt2 => new SoundEvent2(evt2),
                SEVT_3 evt3 => new SoundEvent3(evt3),
                SEVT_4 evt4 => new SoundEvent4(evt4),
                SEVT_6 evt6 => new SoundEvent6(evt6),
                SEVT_7 evt7 => new SoundEvent7(evt7),
                SEVT_8 evt8 => new SoundEvent8(evt8),
                SEVT_9 evt9 => new SoundEvent9(evt9),
                SEVT_10 evt10 => new SoundEvent10(evt10),
                SEVT_12 evt12 => new SoundEvent12(evt12)
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

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
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
