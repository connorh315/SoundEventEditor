using SoundEventEditor.SoundEvents;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using static SoundEventEditor.ViewModels.SoundEventViewModel;

namespace SoundEventEditor.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly ObservableCollection<SoundEventViewModel> _rootNode = []; // This is not my fault. Silly UI Framework that I don't know how to use :)
        public ObservableCollection<SoundEventViewModel> RootNode
        {
            get => _rootNode;
        }

        private bool _showSoundEventPanel;
        public bool ShowSoundEventPanel
        {
            get => _showSoundEventPanel;
            set
            {
                _showSoundEventPanel = value;

                OnPropertyChanged();
            }
        }

        public string FilePath;

        public void OnRootNodeDestroy()
        {
            RootNode.Clear();

            ShowSoundEventPanel = false;
        }

        public void SetRootNode(SoundEventViewModel model)
        {
            RootNode.Clear();
            RootNode.Add(model);

            ShowSoundEventPanel = true;
        }

        public void SetRootSoundEvent(SoundEventType type)
        {
            var evt = CreateSoundEvent(type);
            evt.OnDestroy = OnRootNodeDestroy;

            SetRootNode(evt);
        }

        public void OpenFile(string filePath)
        {
            FilePath = filePath;

            SetRootNode(SoundEventViewModel.OpenFile(filePath, OnRootNodeDestroy));
        }

        public void SaveFile()
        {
            if (RootNode.Count == 0) return;

            SoundEvent root = RootNode[0].RebuildEvent();

            SoundEvent.SaveFile(FilePath, root);
        }

        public MainWindowViewModel()
        {
            //string file = @"C:\Users\Connor\Desktop\soundevents\AUDIO\EVENTS\DX_WICKEDWITCH_ENTRANCE.SOUND_EVENT";

            //var root = SoundEvent.OpenFile(file);
            //SoundEvent.SaveFile(@"C:\Users\Connor\Desktop\DX_WICKEDWITCH_ENTRANCE222.SOUND_EVENT", root);

            //SetRootNode(SoundEventViewModel.OpenFile(@"C:\Users\Connor\Desktop\soundevents\AUDIO\EVENTS\DX_Aquaman_Exit.SOUND_EVENT", OnRootNodeDestroy));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
