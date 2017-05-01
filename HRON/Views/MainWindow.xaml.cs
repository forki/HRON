﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading;
using System.Reflection;
using HRON.Properties;
using HRONLib;
using System.IO;
using RehostedWorkflowDesigner.Views;
using HRON.Views.EmployeeViews;

namespace HRON.Views
{
    public partial class MainWindow : Window
    {
        protected HRONEntities entities = new HRONEntities();
        public Dictionary<String, String> masterDataSets { get; set; }

        public MainWindow()
        {
            masterDataSets= new Dictionary<string, String>();
            SetLanguageDictionary();
            setMasterdataSets();

            InitializeComponent();
            //FillAll(System.Security.Principal.WindowsIdentity.GetCurrent().Name);
            try
            {
                string usr = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                var user = entities.baUser.Where(u => u.userID == usr).First();

                lblUser.Content = user.userID;
                lblUserRight.Content = user.baUserGroup.userGroupDescription;
                String lang = Thread.CurrentThread.CurrentCulture.ToString();
                foreach (ComboBoxItem cbi in lblUserLocale.Items)
                    if (lang.StartsWith(cbi.Tag.ToString()))
                    {
                        lblUserLocale.SelectedItem = cbi;
                        break;
                    }

                System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["wfViewSource"];
                entities.baWorkflows.Load();
                myCollectionViewSource.Source = entities.baWorkflows.Local;
            }
            catch (Exception e)
            {
                MessageBox.Show("Not autorized to access - " + e.Message);
                //this.Close();
            }
        }

        private void setMasterdataSets(string name, string label)
        {
            masterDataSets.Add(name, this.Resources[label].ToString());
        }

        private void setMasterdataSets()
        {
            setMasterdataSets("baBusinessUnitID", "masterDatabaBusinessUnitID");
            setMasterdataSets("baCarPolicy", "masterDatababaCarPolicy");
            setMasterdataSets("baCDC", "masterDatabaCDC");
            setMasterdataSets("baCompanyRights", "masterDatababaCompanyRights");
            setMasterdataSets("baContractType", "masterDatabaContractType");
            setMasterdataSets("baCountry", "masterDatabaCountry");
            setMasterdataSets("baFringeBenefit", "masterDatababaFringeBenefit");
            setMasterdataSets("baFunctions", "masterDatababaFunctions");
            setMasterdataSets("baJobTitle", "masterDatabaJobTitle");
            setMasterdataSets("baLevel", "masterDatabaLevel");
            setMasterdataSets("baNationality", "masterDatabaNationality");
            setMasterdataSets("baSpecialization", "masterDatabaSpecialization");
            setMasterdataSets("baStudyTitle", "masterDatabaStudyTitle");
            setMasterdataSets("baTimeType", "masterDatabaTimeType");
            setMasterdataSets("baType", "masterDatabaType");
            setMasterdataSets("baWorkPlace", "masterDatabaWorkPlace");
            setMasterdataSets("baUser", "masterDatabaUser");
            setMasterdataSets("baUserGroup", "masterDatabaUserGroup");
            setMasterdataSets("baDocumentation", "masterDatabaDocumentation");
        }


        private void DataGrid_AutoGeneratedColumns(object sender, EventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            if(dg.Columns[0].IsReadOnly)
            {
                dg.Columns[0].CellStyle = new Style();
                dg.Columns[0].CellStyle.Setters.Add(new Setter(DataGridCell.BackgroundProperty, Brushes.LightGray));
            }

        }

        private void SetLanguageDictionary()
        {
            switch (Thread.CurrentThread.CurrentCulture.ToString())
            {
                case "de-AT":
                case "de-DE":
                    ChangeResources("de");
                    break;
                case "it-IT":
                    ChangeResources("it");
                    break;
                default:
                    ChangeResources("");
                    break;
            }
        }

        private void UIElement_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //until we had a StaysOpen glag to Drawer, this will help with scroll bars
            var dependencyObject = Mouse.Captured as DependencyObject;
            while (dependencyObject != null)
            {
                if (dependencyObject is ScrollBar) return;
                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
            }

            MenuToggleButton.IsChecked = false;
        }

        private void MasterdataListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            if (lb.SelectedItem == null)
                return;


            KeyValuePair<String, String> rg = (KeyValuePair<String, String>)lb.SelectedItem;

            switch(rg.Key)
            {
                case "baBusinessUnitID": addTabx<baBusinessUnitID>(rg.Key); break;
                case "baCarPolicy": addTabx<baCarPolicy>(rg.Key); break;
                case "baCDC": addTabx<baCDC>(rg.Key); break;
                case "baCompanyRights": addTabx<baCompanyRights>(rg.Key); break;
                case "baContractType": addTabx<baContractType>(rg.Key); break;
                case "baCountry": addTabx<baCountry>(rg.Key); break;
                case "baFringeBenefit": addTabx<baFringeBenefit>(rg.Key); break;
                case "baFunctions": addTabx<baFunctions>(rg.Key); break;
                case "baJobTitle": addTabx<baJobTitle>(rg.Key); break;
                case "baLevel": addTabx<baLevel>(rg.Key); break;
                case "baNationality": addTabx<baNationality>(rg.Key); break;
                case "baSpecialization": addTabx<baSpecialization>(rg.Key); break;
                case "baStudyTitle": addTabx<baStudyTitle>(rg.Key); break;
                case "baTimeType": addTabx<baTimeType>(rg.Key); break;
                case "baType": addTabx<baType>(rg.Key); break;
                case "baWorkPlace": addTabx<baWorkPlace>(rg.Key); break;
                case "baUser": addTabx<baUser>(rg.Key); break;
                case "baUserGroup": addTabx<baUserGroup>(rg.Key); break;
                case "baDocumentation": addTabx<baDocumentation>(rg.Key); break;
                default: throw new Exception("MasterdataTable not implemented (" + rg.Key + ")");
            }
            lb.UnselectAll();
        }

        private void lblUserLocale_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            ComboBoxItem cbi = ((ComboBoxItem)cb.SelectedItem);
            ChangeResources(cbi.Tag.ToString());
        }

        private void ChangeResources(string cbi)
        {
            ResourceDictionary dict = new ResourceDictionary();
            ResourceDictionary dictEn = new ResourceDictionary();

            if (cbi == "de")
                dict.Source = new Uri("..\\Resources\\StringResources.de-DE.xaml",
                                   UriKind.Relative);
            else if (cbi == "it")
                dict.Source = new Uri("..\\Resources\\StringResources.it-IT.xaml",
                                   UriKind.Relative);

            dictEn.Source = new Uri("..\\Resources\\StringResources.xaml",
                                  UriKind.Relative);
            this.Resources.MergedDictionaries.Add(dictEn);
            this.Resources.MergedDictionaries.Add(dict);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UserEdit ue = new UserEdit(this, 1);
            addTab(ue, "test 2");
        }

        private void btnEmploy_click(object sender, RoutedEventArgs e)
        {
            UserList ul = new UserList(this);
            addTab(ul, "Employee's");
        }

        public void closeTab(UserControl ctr)
        {
            if (tabControl.Items != null)
                foreach (object o in tabControl.Items)
                {
                    if (o is TabItem && ((TabItem)o).Content.Equals(ctr))
                    {
                        tabControl.Items.Remove(o);
                        return;
                    }
                }
        }
        public void addTabx<T>(string entity) where T : baseEntity
        {
            HRONEntities ent = new HRONEntities();
            PropertyInfo p = ent.GetType().GetProperty(entity);
            addTab(new MasterData<DbSet<T>, T>((DbSet<T>)p.GetValue(ent), ent), masterDataSets[entity]);
        }

        public void addTab(UserControl ctr, string header)
        {
            if(tabControl.Items!=null)
                foreach(object o in tabControl.Items)
                {
                    if(o is TabItem && ((TabItem)o).Header.ToString()==header)
                    {
                        tabControl.SelectedItem = o;
                        return;
                    }
                }

            TabItem ti = new TabItem();
            ti.Header = header;
            ti.Content = ctr;

            tabControl.SelectedIndex = tabControl.Items.Add(ti);
        }

        private void btnWfDesigner_Click(object sender, RoutedEventArgs e)
        {

        }

        private void WFListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            if (lb.SelectedItem == null)
                return;

            baWorkflows workflow = lb.SelectedItem as baWorkflows;

            RehostedWorkflowDesigner.Views.MainWindow designer = new RehostedWorkflowDesigner.Views.MainWindow();
            designer.onSave += Designer_onSave;
            designer.openFile(workflow);
            designer.Show();
        }

        private void Designer_onSave(object sender, EventArgsSave e)
        {
            RehostedWorkflowDesigner.Views.MainWindow designer = (RehostedWorkflowDesigner.Views.MainWindow)sender;

            baWorkflows workflow = e.baWorkflows;
            string fileName = e.FileName;
            if(File.Exists(fileName))
            {
                workflow.WorkFlowXAML = File.ReadAllBytes(fileName);
                entities.SaveChanges();
            }
        }

        private void _mainWindow_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}