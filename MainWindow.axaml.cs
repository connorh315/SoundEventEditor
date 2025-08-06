using Avalonia.Controls;
using Avalonia.Metadata;
using Avalonia.OpenGL.Surfaces;
using Avalonia.Platform.Storage;
using SoundEventEditor.SoundEvents;
using SoundEventEditor.ViewModels;
using SoundEventEditor.ViewModels.SoundEvents;
using System;
using System.ComponentModel;
using static SoundEventEditor.ViewModels.SoundEventViewModel;

namespace SoundEventEditor
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private static readonly SoundEventType[] PossibleRootTypes = 
            { SoundEventType.Event, SoundEventType.Sample, SoundEventType.Stream, SoundEventType.Group, SoundEventType.Sequence, SoundEventType.Conversation, SoundEventType.Speech, SoundEventType.StreamingSpeech };

        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            Title = AppSettings.DisplayName;
            
            DataContext = new MainWindowViewModel();

            var flyout = new MenuFlyout();

            foreach (SoundEventType typ in PossibleRootTypes)
            {
                var item = new MenuItem { Header = typ.ToString() };

                item.Click += (_, _) => CreateRootNode(typ);

                flyout.Items.Add(item);
            }

            CreateRootButton.Flyout = flyout;
        }

        private void CreateRootNode(SoundEventType type)
        {
            if (DataContext is not MainWindowViewModel vm)
                return;

            vm.SetRootSoundEvent(type);
        }

        private async void MenuItem_Open(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var files = await StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = "Open SoundEvent File",
                AllowMultiple = false,
                FileTypeFilter = new[]
                {
                    new FilePickerFileType("DAT files") { Patterns = new[] { "*.SOUND_EVENT" } }
                }
            });

            if (files.Count > 0)
            {
                string filePath = files[0].Path.LocalPath;

                if (DataContext is not MainWindowViewModel vm)
                    return;

                vm.OpenFile(filePath);
            }
        }

        private async void MenuItem_Save(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (DataContext is not MainWindowViewModel vm) return;

            if (vm.FileLocation == null || vm.FileLocation == "")
            {
                MenuItem_SaveAs(sender, e);
                return;
            }

            vm.SaveFile();
        }

        private async void MenuItem_SaveAs(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (DataContext is not MainWindowViewModel vm) return;

            var files = await StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
            {
                Title = "Open SoundEvent File",
                DefaultExtension = "SOUND_EVENT"
            });

            if (files == null) return;

            vm.FileLocation = files.Path.LocalPath;

            vm.SaveFile();
        }
    }
}