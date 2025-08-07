using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using SoundEventEditor.ViewModels;
using System.ComponentModel;
using static SoundEventEditor.ViewModels.SoundEventViewModel;

namespace SoundEventEditor
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private static readonly SoundEventType[] _possibleRootTypes =
        [
            SoundEventType.Event,
            SoundEventType.Sample,
            SoundEventType.Stream,
            SoundEventType.Group,
            SoundEventType.Sequence,
            SoundEventType.Conversation,
            SoundEventType.Speech,
            SoundEventType.StreamingSpeech
        ];

        public MainWindow()
        {
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Title                 = AppSettings.DisplayName;
            DataContext           = new MainWindowViewModel();

            MenuFlyout menuFlyout = new();

            foreach (SoundEventType soundEventType in _possibleRootTypes)
            {
                MenuItem menuItem = new() { Header = soundEventType.ToString() };

                menuItem.Click += (_, _) => CreateRootNode(soundEventType);

                menuFlyout.Items.Add(menuItem);
            }

            CreateRootButton.Flyout = menuFlyout;
        }

        private void CreateRootNode(SoundEventType type)
        {
            if (DataContext is not MainWindowViewModel viewModel)
            {
                return;
            }

            viewModel.SetRootSoundEvent(type);
        }

        private async void MenuItem_Open(object sender, RoutedEventArgs e)
        {
            var files = await StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title          = "Open SoundEvent File",
                AllowMultiple  = false,
                FileTypeFilter =
                [
                    new FilePickerFileType("SoundEvent files") { Patterns = ["*.SOUND_EVENT"] }
                ]
            });

            if (files.Count > 0)
            {
                if (DataContext is not MainWindowViewModel viewModel)
                {
                    return;
                }

                viewModel.OpenFile(files[0].Path.LocalPath);
            }
        }

        private void MenuItem_Save(object sender, RoutedEventArgs e)
        {
            if (DataContext is not MainWindowViewModel viewModel)
            {
                return;
            }

            if (viewModel.FilePath == null || viewModel.FilePath == "")
            {
                MenuItem_SaveAs(sender, e);

                return;
            }

            viewModel.SaveFile();
        }

        private async void MenuItem_SaveAs(object sender, RoutedEventArgs e)
        {
            if (DataContext is not MainWindowViewModel viewModel)
            {
                return;
            }

            var files = await StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
            {
                Title            = "Open SoundEvent File",
                DefaultExtension = "SOUND_EVENT"
            });

            if (files == null)
            {
                return;
            }

            viewModel.FilePath = files.Path.LocalPath;

            viewModel.SaveFile();
        }

        private void MenuItem_Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}