using Avalonia.Controls;
using Avalonia.Interactivity;
using SoundEventEditor.ViewModels;
using SoundEventEditor.ViewModels.SoundEvents;
using System.Collections.ObjectModel;

namespace SoundEventEditor
{
    public partial class SoundEventPanel : UserControl
    {
        public ObservableCollection<MenuItem> AddableChildItems { get; } = [];

        public SoundEventPanel()
        {
            InitializeComponent();

            DataContextChanged += (_, _) => PopulateAddChildButton();
        }

        private void AddChild(SoundEventViewModel.SoundEventType type)
        {
            if (DataContext is not SoundEventViewModel soundEventViewModel)
            {
                return;
            }

            soundEventViewModel.Children.Add(SoundEventViewModel.CreateSoundEvent(type));

            soundEventViewModel.Children[^1].Parent = soundEventViewModel;

            ChildrenContainer.IsExpanded = true;
        }

        private void PopulateAddChildButton()
        {
            if (DataContext is not SoundEventViewModel soundEventViewModel)
            {
                return;
            }

            if (soundEventViewModel.SelectableChildren == null || soundEventViewModel.SelectableChildren.Count == 0)
            {
                ChildrenContainer.IsVisible = false;

                return;
            }

            MenuFlyout flyout = new();

            foreach (var soundEventType in soundEventViewModel.SelectableChildren)
            {
                MenuItem item = new() { Header = soundEventType.ToString() };

                item.Click += (_, _) => AddChild(soundEventType);

                flyout.Items.Add(item);
            }

            AddChildButton.Flyout = flyout;
        }

        private void DestroyThis(object sender, RoutedEventArgs e)
        {
            if (DataContext is not SoundEventViewModel soundEventViewModel)
            {
                return;
            }

            soundEventViewModel.Destroy();
        }

        private void AddConnection(object sender, RoutedEventArgs e)
        {
            if (DataContext is not SoundEventViewModel soundEventViewModel)
            {
                return;
            }

            soundEventViewModel.Connections.Add(new SoundEvent4ViewModel() { Parent = soundEventViewModel });

            ConnectionsContainer.IsExpanded = true;
        }
    }
}