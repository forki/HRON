using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using HRONLib;

namespace HRON.Views
{
    /// <summary>
    /// Interaktionslogik für ImportPhoto.xaml
    /// </summary>
    public partial class ImportPhoto : UserControl
    {
        HRONEntities entities;
        public ImportPhoto()
        {
            InitializeComponent();
            entities = new HRONEntities();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var files = Directory.EnumerateFiles(FolderPath.Text);
            foreach(string f in files)
            {
                FileInfo fi = new FileInfo(f);
                if(fi.Extension == ".jpg" || fi.Extension == ".png")
                {
                    string name = fi.Name.Substring(0, fi.Name.Length - fi.Extension.Length);
                    var empl = entities.Employee.Where(em => em.emplSamAccountName == name).FirstOrDefault();
                    if(empl!=null)
                    {
                        if (empl.emplPhoto == null || empl.emplPhoto.Length == 0 || (@override.IsChecked.HasValue && @override.IsChecked.Value))
                            empl.emplPhoto = File.ReadAllBytes(fi.FullName);
                    }
                }
                entities.SaveChanges();
            }
        }
    }
}
