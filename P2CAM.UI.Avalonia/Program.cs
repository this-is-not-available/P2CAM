using Avalonia;
using Avalonia.Controls;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using P2CAM.Core;
using P2CAM.UI.Avalonia.ViewModels;
using P2CAM.UI.Avalonia.Views;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Tomlyn;

namespace P2CAM.UI.Avalonia
{
    public enum Theme
    {
        System,
        Light,
        Dark
    }

    public class AppOptions : Options
    {
        public Theme AppTheme { get; set; } = Theme.System;
    }

    public class OptionsLoader
    {
        private bool loadFailed = false;
        public AppOptions options = new AppOptions();

        public OptionsLoader()
        {
            loadFailed = false;
        }

        public async void Save()
        {
            if (loadFailed)
            {
                var box = MessageBoxManager.GetMessageBoxStandard(
                    "Confirm save",
                    "Do you want to save options? There was a problem loading them previously and this could overwrite the old options.",
                    ButtonEnum.YesNo,
                    Icon.Question
                );

                var result = await box.ShowAsync();

                if (result == ButtonResult.No)
                {
                    return;
                }
            }
            string tomlString = Toml.FromModel(options);
            File.WriteAllText("appsettings.toml", tomlString);
        }

        public void Load()
        {
            if (!File.Exists("appsettings.toml"))
            {
                // No settings found, generate stock settings
                options = new AppOptions();
                return;
            }

            // Read appsettings.toml from root of solution
            try
            {
                var optionsText = File.ReadAllText("appsettings.toml");
                var modelOptions = new TomlModelOptions { IgnoreMissingProperties = true };
                options = Toml.Parse(optionsText).ToModel<AppOptions>(modelOptions);
            }
            catch (Exception e)
            {
                loadFailed = true;
                MessageBoxManager.GetMessageBoxStandard(
                    "Error",
                    "A problem occurred while loading options: " + e.Message,
                    ButtonEnum.Ok,
                    Icon.Error
                ).ShowAsync();
                return;
            }
        }
    }


    internal sealed class Program
    {
        public static OptionsLoader optionsLoader = new OptionsLoader();
        public static AppOptions? options;
        public static AssetManager? assetManager;

        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args)
        {
            optionsLoader.Load();
            options = optionsLoader.options;
            assetManager = new AssetManager(options);

            if (string.IsNullOrWhiteSpace(options.Portal2_Dir))
            {
                options.Portal2_Dir = SteamUtils.FindPortal2Directory();
                if (options.Portal2_Dir == null)
                {
                    Trace.WriteLine("Portal 2 directory not automatically found!");
                }
            }

            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
        {
            var app = AppBuilder.Configure<App>(() => new App(assetManager))
                .UsePlatformDetect()
                .WithInterFont()
                .LogToTrace();

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Reduces memory usage significantly
                app = app.With(new Win32PlatformOptions
                {
                    RenderingMode = [Win32RenderingMode.Software]
                });
            }

            return app;
        }
    }
}
