using P2CAM.Core;
using System.Security.Principal;
using Tomlyn;

namespace P2CAM
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
                DialogResult result = MessageBox.Show("Do you want to save options? There was a problem loading them previously and this could overwrite the old options.", "Confirm save", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    return;
                }
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
                MessageBox.Show("A problem occurred while loading options: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }

    public class Program
    {
        public static OptionsLoader optionsLoader = new OptionsLoader();
        public static Options options = optionsLoader.options;
        public static AssetManager assetManager = new AssetManager(options);

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>

        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            optionsLoader.Load();
            if (string.IsNullOrWhiteSpace(options.Portal2_Dir))
            {
                options.Portal2_Dir = SteamUtils.FindPortal2Directory();
                if (options.Portal2_Dir == null)
                {
                    MessageBox.Show("Portal 2 directory not automatically found!");
                }
            }

            UI.WinForms.AssetBrowser window = new UI.WinForms.AssetBrowser(assetManager);
            window.Init();
            if (!window.IsDisposed)
            {
                Application.Run(window);
            }
        }
    }
}