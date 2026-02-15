using Avalonia;
using P2CAM.Core;
using System;
using System.Diagnostics;
using System.IO;
using Tomlyn;

namespace P2CAM.UI.Avalonia
{
    public class OptionsLoader
    {
        private bool loadFailed = false;
        public Options options = new Options();

        public OptionsLoader()
        {
            loadFailed = false;
        }

        public void Save()
        {
            if (loadFailed)
            {
                /*DialogResult result = MessageBox.Show("Do you want to save options? There was a problem loading them previously and this could overwrite the old options.", "Confirm save", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    return;
                }*/
            }
            string tomlString = Toml.FromModel(this);
            File.WriteAllText("appsettings.toml", tomlString);
        }
        public void Load()
        {
            if (!File.Exists("appsettings.toml"))
            {
                // No settings found, generate stock settings and save
                options = new Options();
                Save();
                return;
            }

            // Read appsettings.toml from root of solution
            try
            {
                var optionsText = File.ReadAllText("appsettings.toml");
                var modelOptions = new TomlModelOptions { IgnoreMissingProperties = true };
                options = Toml.Parse(optionsText).ToModel<Options>(modelOptions);
            }
            catch (Exception e)
            {
                loadFailed = true;
                //MessageBox.Show("A problem occurred while loading options: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }


    internal sealed class Program
    {
        public static OptionsLoader optionsLoader = new OptionsLoader();
        public static Options options = optionsLoader.options;
        public static AssetManager assetManager = new AssetManager(options);

        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args)
        {
            optionsLoader.Load();
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
            => AppBuilder.Configure<App>(() => new App(assetManager))
                .UsePlatformDetect()
                .WithInterFont()
                .LogToTrace();
    }
}
