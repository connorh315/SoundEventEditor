using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SoundEventEditor.ViewModels;
using SoundEventEditor.ViewModels.SoundEvents;
using System.Collections.ObjectModel;
using Tmds.DBus.Protocol;
using static SoundEventEditor.ViewModels.SoundEventViewModel;

namespace SoundEventEditor;

public partial class SoundEventPanel : UserControl
{
    public ObservableCollection<MenuItem> AddableChildItems { get; } = new();

    public SoundEventPanel()
    {
        InitializeComponent();

        DataContextChanged += (_, _) => PopulateAddChildButton();

    }

    private void AddChild(SoundEventType type)
    {
        if (DataContext is not SoundEventViewModel vm)
            return;

        vm.Children.Add(SoundEventViewModel.CreateSoundEvent(type));

        vm.Children[vm.Children.Count - 1].Parent = vm;

        ChildrenContainer.IsExpanded = true;
    }

    private void PopulateAddChildButton()
    {
        if (DataContext is not SoundEventViewModel vm)
            return;

        if (vm.SelectableChildren == null || vm.SelectableChildren.Count == 0)
        {
            ChildrenContainer.IsVisible = false;
            return;
        }

        var flyout = new MenuFlyout();

        foreach (var typ in vm.SelectableChildren)
        {
            var item = new MenuItem { Header = typ.ToString() };

            item.Click += (_, _) => AddChild(typ);

            flyout.Items.Add(item);
        }

        AddChildButton.Flyout = flyout;
    }

    private void DestroyThis(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (DataContext is not SoundEventViewModel vm) return;

        vm.Destroy();
    }

    private void AddConnection(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (DataContext is not SoundEventViewModel vm)
            return;

        vm.Connections.Add(new SoundEvent4() { Parent = vm });
        ConnectionsContainer.IsExpanded = true;
    }
}