using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace HRON
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.Exit += App_Exit;
        }

        private void App_Exit(object sender, ExitEventArgs e)
        {
            try
            {
                string basePath = System.IO.Path.GetTempPath() + "\\HR";
                if (Directory.Exists(basePath))
                    Directory.Delete(basePath, true);

                Database.SetInitializer(new MigrateDatabaseToLatestVersion<HRONLib.HRONEntities, HRONLib.Migrations.Configuration>());
            }
            catch (Exception) { }
        }
    }

}
