using HRONLib;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
using HRON.Views;

namespace HRON.Views.EmployeeViews
{
    /// <summary>
    /// Interaktionslogik für UserEditFiles.xaml
    /// </summary>
    public partial class EmployeeEditFiles : UserControl
    {
        HRONEntities entities;
        Employee employee;
        MainWindow mainWindow;

        public EmployeeEditFiles(MainWindow main, HRONEntities _entities, Employee e)
        {
            InitializeComponent();

            mainWindow = main;
            entities = _entities;
            employee = e;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            // Laden Sie Ihre Daten nicht zur Entwurfszeit.
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//Laden Sie hier Ihre Daten, und weisen Sie das Ergebnis der CollectionViewSource zu.
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }

            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                System.Windows.Data.CollectionViewSource employeeViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["employeeViewSource"];
                entities.Employee.Where(m => m.emplID == employee.emplID).Load();
                employeeViewSource.Source = entities.Employee.Local;
                employeeViewSource.View.MoveCurrentToFirst();
            }
        }

        private void uploadFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog newFile = new OpenFileDialog();
            newFile.CheckFileExists = true;
            newFile.DereferenceLinks = true;
            newFile.Multiselect = true;
            newFile.Title = "Please choose your files...";
            if(newFile.ShowDialog() == true)
            {
                loadFiles(newFile.FileNames);
            }
        }

        public void loadFiles( string[] files)
        {
            foreach(string f in files)
            {
                FileInfo fi = new FileInfo(f);

                EmplFiles file = new EmplFiles();
                file.EmplID = employee.emplID;
                file.Employee = employee;
                file.FileContent = File.ReadAllBytes(f);
                file.FileName = fi.Name;

                employee.EmplFiles.Add(file);
            }

        }
        private void emplFilesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null && item is EmplFiles)
            {
                EmplFiles em = (EmplFiles)item;

                string fileName = moveFile(em);

                Process prc = new Process();
                prc.StartInfo.FileName = fileName;
                prc.Start();
            }
        }

        private static string moveFile(EmplFiles em)
        {
            string basePath = System.IO.Path.GetTempPath() + "\\HR";
            if (!Directory.Exists(basePath))
                Directory.CreateDirectory(basePath);

            string fileName = basePath + "\\" + Guid.NewGuid();
            Directory.CreateDirectory(fileName);
            fileName = fileName + "\\" + em.FileName;
            File.WriteAllBytes(fileName, em.FileContent);
            return fileName;
        }

        private void PackIcon_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            MaterialDesignThemes.Wpf.PackIcon pi = (MaterialDesignThemes.Wpf.PackIcon)sender;

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (emplFilesListView.SelectedItem != null && emplFilesListView.SelectedItem is EmplFiles)
                {
                    EmplFiles ef = (EmplFiles)emplFilesListView.SelectedItem;

                    string file = moveFile(ef);

                    string[] array = { file };
                    var data = new DataObject(DataFormats.FileDrop, array);
                    DragDrop.DoDragDrop(pi, data, DragDropEffects.Move);
                    e.Handled = true;
                }
            }
        }
    }
}
