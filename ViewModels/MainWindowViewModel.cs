using Avalonia.Controls;
using SoundEventEditor.SoundEvents;
using SoundEventEditor.ViewModels.SoundEvents;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static SoundEventEditor.ViewModels.SoundEventViewModel;

namespace SoundEventEditor.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<SoundEventViewModel> rootNode = new(); // This is not my fault. Silly UI Framework that I don't know how to use :)
        public ObservableCollection<SoundEventViewModel> RootNode
        {
            get => rootNode;
        }

        public string FileLocation;

        private bool showSoundEventPanel;
        public bool ShowSoundEventPanel
        {
            get => showSoundEventPanel;
            set
            {
                showSoundEventPanel = value;
                OnPropertyChanged();
            }
        }

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
            var evt = SoundEventViewModel.CreateSoundEvent(type);
            evt.OnDestroy = OnRootNodeDestroy;
            SetRootNode(evt);
        }

        public void OpenFile(string fileLocation)
        {
            FileLocation = fileLocation;
            SetRootNode(SoundEventViewModel.OpenFile(fileLocation, OnRootNodeDestroy));
        }

        public void SaveFile()
        {
            if (RootNode.Count == 0) return;

            SoundEvent root = RootNode[0].RebuildEvent();

            SoundEvent.SaveFile(FileLocation, root);
        }

        public MainWindowViewModel()
        {
            //string file = @"C:\Users\Connor\Desktop\soundevents\AUDIO\EVENTS\DX_WICKEDWITCH_ENTRANCE.SOUND_EVENT";

            //var root = SoundEvent.OpenFile(file);
            //SoundEvent.SaveFile(@"C:\Users\Connor\Desktop\DX_WICKEDWITCH_ENTRANCE222.SOUND_EVENT", root);

            //SetRootNode(SoundEventViewModel.OpenFile(@"C:\Users\Connor\Desktop\soundevents\AUDIO\EVENTS\DX_Aquaman_Exit.SOUND_EVENT", OnRootNodeDestroy));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
