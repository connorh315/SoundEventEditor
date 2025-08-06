using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Metadata;

[assembly: XmlnsDefinition("https://github.com/avaloniaui", "SoundEventEditor")]

namespace SoundEventEditor
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            Localiser.Initialise();

            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}