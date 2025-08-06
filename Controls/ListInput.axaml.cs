using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using SoundEventEditor.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SoundEventEditor;

[TemplatePart("PART_AddItemBtn", typeof(Button))]
public class ListInput : TemplatedControl
{

    /// <summary>
    /// Label StyledProperty definition
    /// </summary>
    public static readonly StyledProperty<string> LabelProperty =
        AvaloniaProperty.Register<ListInput, string>(nameof(Label));

    /// <summary>
    /// Gets or sets the Label property. This StyledProperty
    /// indicates ....
    /// </summary>
    public string Label
    {
        get => this.GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }


    /// <summary>
    /// Items StyledProperty definition
    /// </summary>
    public static readonly StyledProperty<ObservableCollection<StringItemViewModel>> ItemsProperty =
        AvaloniaProperty.Register<ListInput, ObservableCollection<StringItemViewModel>>(nameof(Items));

    /// <summary>
    /// Gets or sets the Items property. This StyledProperty
    /// indicates ....
    /// </summary>
    public ObservableCollection<StringItemViewModel> Items
    {
        get => this.GetValue(ItemsProperty);
        set => SetValue(ItemsProperty, value);
    }


    /// <summary>
    /// RemoveCommand StyledProperty definition
    /// </summary>
    public static readonly StyledProperty<ICommand> RemoveCommandProperty =
        AvaloniaProperty.Register<ListInput, ICommand>(nameof(RemoveCommand));

    /// <summary>
    /// Gets or sets the RemoveCommand property. This StyledProperty 
    /// indicates ....
    /// </summary>
    public ICommand RemoveCommand
    {
        get => this.GetValue(RemoveCommandProperty);
        set => SetValue(RemoveCommandProperty, value);
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        Button addItemButton = e.NameScope.Find<Button>("PART_AddItemBtn");
        if (addItemButton != null)
        {
            addItemButton.Click += AddItemButton_Click;
        }

        RemoveCommand = new DelegateCommand(RemoveItemButton_Click);
    }

    private void RemoveItemButton_Click(object? parameter)
    {
        if (parameter is string item && DataContext is StringListOptionViewModel vm)
        {
            vm.RemoveItem(item);
        }
    }

    private void AddItemButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (DataContext is not StringListOptionViewModel vm) return;

        vm.AddItem();
    }

    private void Button_Click_1(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
    }
}