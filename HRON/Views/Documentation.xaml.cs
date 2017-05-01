using HRON.Views.Helper;
using HRONLib;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace HRON.Views
{
    /// <summary>
    /// Interaktionslogik für Documentation.xaml
    /// </summary>
    public partial class Documentation : UserControl, DetailControl
    {
        baDocumentation actualDoc = null;
        HRONEntities entities;

        public Documentation()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public void LoadID(int DocumentId)
        {
            entities = new HRONEntities();
            entities.baDocumentation.Where(d => d.documentationID == DocumentId).Load();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Laden Sie Ihre Daten nicht zur Entwurfszeit.
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                //Laden Sie hier Ihre Daten, und weisen Sie das Ergebnis der CollectionViewSource zu.
                System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["baDocumentationViewSource"];
                myCollectionViewSource.Source = entities.baDocumentation.Local;
                myCollectionViewSource.View.MoveCurrentToFirst();
             }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog newFile = new OpenFileDialog();
            newFile.CheckFileExists = true;
            newFile.DereferenceLinks = true;
            newFile.Multiselect = false;
            newFile.Title = "Please choose your files...";
            if (newFile.ShowDialog() == true)
            {
                FileInfo fi = new FileInfo(newFile.FileName);

                actualDoc.documentationDocument = File.ReadAllBytes(newFile.FileName);
                actualDoc.documentationDocumentName = fi.Name;
            }
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            entities.SaveChanges();
        }

        void DetailControl.New()
        {
            entities = new HRONEntities();

            entities.baDocumentation.Add(new baDocumentation());
        }

        private void PreviewTextNumeric(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);            
        }
        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }
    }
}
