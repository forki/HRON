using HRONLib;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
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

namespace HRON
{
    /// <summary>
    /// Interaktionslogik für UserList.xaml
    /// </summary>
    public partial class UserList : UserControl
    {
        protected HRONEntities entities;
        MainWindow mainWindow = null;
        private ListSortDirection _sortDirection;
        private GridViewColumn _sortColumn;

        public UserList(MainWindow main)
        {
            InitializeComponent();

            entities = new HRONEntities();

            this.mainWindow = main;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (grdUserListCard == null || grdUserListGrid == null)
                return;

            //MessageBox.Show("Count:" + lstGridView.Items.Count);

            ListBox lb = (ListBox)sender;
            if(lb.SelectedItem.Equals(LstBox_Card))
            {
                grdUserListCard.Visibility = Visibility.Visible;
                grdUserListGrid.Visibility = Visibility.Collapsed;
            }
            if (lb.SelectedItem.Equals(LstBox_Grid))
            {
                grdUserListCard.Visibility = Visibility.Collapsed;
                grdUserListGrid.Visibility = Visibility.Visible;
            }
        }

        private void lstGridView_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null && item is Employee)
            {
                Employee em = (Employee)item;
                UserEdit ue = new UserEdit(this.mainWindow, em.emplID);
                ue.UserSaved += Ue_UserSaved;
                this.mainWindow.addTab(ue, ((em.emplKey != null) ? em.emplKey + ":" : "") + em.emplName + "  " + em.emplLastName);
            }
        }

        private void Ue_UserSaved(object sender, EventArgs e)
        {
            if (!(sender is Employee))
                throw new Exception("Error in Ue_UserSaved: sender is not Employee");
            if (sender == null)
                throw new Exception("Error in Ue_UserSaved: sender is null");

            Employee em = (Employee)sender;
            Employee em2 = entities.Employee.Find(em.emplID);
            if (em2 != null)
            {
                entities.Entry(em2).State = EntityState.Detached;
                em2 = entities.Employee.Find(em2.emplID);

                orderGridBy(false);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            DbSet<Employee> setE = entities.Employee;
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["employeeViewSource"];
                setE.Load();
                myCollectionViewSource.Source = setE.Local;

                GridView gv = lstGridView.View as GridView;
                if (gv.Columns.Count > 0)
                    orderGridBy(gv.Columns[1]);

                setColumnOrder(gv);
                gv.Columns.CollectionChanged += Columns_CollectionChanged;
            }
        }

        private void setColumnOrder(GridView gv)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", true);
            key = key.CreateSubKey("HRON").CreateSubKey("HRON").CreateSubKey("EmployeeList");
            object o = key.GetValue("ColumnOrder");
            if(o!=null)
            {
                string[] cols = (o as string).Split(';');
                int col = 0;
                foreach(string c in cols)
                {
                    if (c != "")
                    {
                        int old = getColumnIndex(c, gv.Columns);
                        if (old >= 0)
                            gv.Columns.Move(old, col);
                    }
                    col++;
                }
            }
        }

        private int getColumnIndex(string BindingPath, GridViewColumnCollection col)
        {
            int x = 0;
            foreach (GridViewColumn c in col)
            {
                try
                {
                    string p = (c.DisplayMemberBinding as System.Windows.Data.Binding).Path.Path;
                    if (p == BindingPath)
                        return x;
                }
                catch { }
                x++;
            }
            return -1;
        }
        private void Columns_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if(e.NewStartingIndex!=e.OldStartingIndex)
            {
                GridViewColumnCollection col = (GridViewColumnCollection)sender;
                string cols = "";
                foreach(GridViewColumn c in col)
                {
                    try
                    {
                        cols += (c.DisplayMemberBinding as System.Windows.Data.Binding).Path.Path + ";";
                    }
                    catch { }
                }
                RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", true);
                key.CreateSubKey("HRON").CreateSubKey("HRON").CreateSubKey("EmployeeList").SetValue("ColumnOrder", cols);
            }
        }

        private void lstGridView_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = e.OriginalSource as GridViewColumnHeader;
            orderGridBy(column.Column);
        }

        private void orderGridBy(Boolean toggleOrder)
        {
            if (_sortColumn != null)
                orderGridBy(_sortColumn, toggleOrder);
        }
        private void orderGridBy()
        {
            orderGridBy(true);
        }

        private void orderGridBy(GridViewColumn column)
        {
            orderGridBy(column, true);
        }

        private void orderGridBy(GridViewColumn column, Boolean toggleOrder)
        {
            if (column == null)
            {
                return;
            }

            if (_sortColumn == column)
            {
                if(toggleOrder)
                {
                    // Toggle sorting direction 
                    _sortDirection = _sortDirection == ListSortDirection.Ascending ?
                                                       ListSortDirection.Descending :
                                                       ListSortDirection.Ascending;
                }
            }
            else
            {
                // Remove arrow from previously sorted header 
                if (_sortColumn != null)
                {
                    _sortColumn.HeaderTemplate = null;
                    _sortColumn.Width = _sortColumn.ActualWidth - 20;
                }

                _sortColumn = column;
                _sortDirection = ListSortDirection.Ascending;
                column.Width = column.ActualWidth + 20;
            }

            if (_sortDirection == ListSortDirection.Ascending)
            {
                column.HeaderTemplate = Resources["ArrowUp"] as DataTemplate;
            }
            else
            {
                column.HeaderTemplate = Resources["ArrowDown"] as DataTemplate;
            }

            string header = string.Empty;

            // if binding is used and property name doesn't match header content 
            Binding b = _sortColumn.DisplayMemberBinding as Binding;
            if (b != null)
            {
                header = b.Path.Path;
            }

            ICollectionView resultDataView = CollectionViewSource.GetDefaultView(lstGridView.ItemsSource);
            resultDataView.SortDescriptions.Clear();
            resultDataView.SortDescriptions.Add(new SortDescription(header, _sortDirection));
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            GridView gv = lstGridView.View as GridView;

            ICollectionView resultDataView = CollectionViewSource.GetDefaultView(lstGridView.ItemsSource);
            resultDataView.Filter = item => {
                Employee em = item as Employee;
                if (em == null)
                    return false;

                if (em.emplName != null && em.emplName.IndexOf(txtSearch.Text, 0, StringComparison.CurrentCultureIgnoreCase)!=-1) return true;
                if (em.emplLastName != null && em.emplLastName.IndexOf(txtSearch.Text, 0, StringComparison.CurrentCultureIgnoreCase) != -1) return true;
                if (em.emplFiscalCode != null && em.emplFiscalCode.IndexOf(txtSearch.Text, 0, StringComparison.CurrentCultureIgnoreCase) != -1) return true;

                if (em.emplID.ToString().IndexOf(txtSearch.Text, 0, StringComparison.CurrentCultureIgnoreCase) != -1) return true;
                if (em.emplLastName != null && em.emplLastName.IndexOf(txtSearch.Text, 0, StringComparison.CurrentCultureIgnoreCase) != -1) return true;
                if (em.emplKey != null && em.emplKey.IndexOf(txtSearch.Text, 0, StringComparison.CurrentCultureIgnoreCase) != -1) return true;
                if (em.baCDC!=null && em.baCDC.cdcDescription != null && em.baCDC.cdcDescription.IndexOf(txtSearch.Text, 0, StringComparison.CurrentCultureIgnoreCase) != -1) return true;
                if (em.baCountry != null && em.baCountry.countryISOCode != null && em.baCountry.countryISOCode.IndexOf(txtSearch.Text, 0, StringComparison.CurrentCultureIgnoreCase) != -1) return true;
                if (em.baWorkPlace != null && em.baWorkPlace.workPlaceCity != null && em.baWorkPlace.workPlaceCity.IndexOf(txtSearch.Text, 0, StringComparison.CurrentCultureIgnoreCase) != -1) return true;
                if (em.baBusinessUnitID != null && em.baBusinessUnitID.businessUnitDescription != null && em.baBusinessUnitID.businessUnitDescription.IndexOf(txtSearch.Text, 0, StringComparison.CurrentCultureIgnoreCase) != -1) return true;
                if (em.baJobTitle != null && em.baJobTitle.jobTitleDescription != null && em.baJobTitle.jobTitleDescription.IndexOf(txtSearch.Text, 0, StringComparison.CurrentCultureIgnoreCase) != -1) return true;
                if (em.baSpecialization != null && em.baSpecialization.specializationDescription != null && em.baSpecialization.specializationDescription.IndexOf(txtSearch.Text, 0, StringComparison.CurrentCultureIgnoreCase) != -1) return true;
                //if (em.emplManagerID != null && em.emplManagerID.IndexOf(txtSearch.Text, 0, StringComparison.CurrentCultureIgnoreCase) != -1) return true;
                if (em.baTimeType != null && em.baTimeType.timeTypeDescription != null && em.baTimeType.timeTypeDescription.IndexOf(txtSearch.Text, 0, StringComparison.CurrentCultureIgnoreCase) != -1) return true;
                if (em.emplGender != null && em.emplGender.IndexOf(txtSearch.Text, 0, StringComparison.CurrentCultureIgnoreCase) != -1) return true;
                if (em.baType != null && em.baType.typeDescription != null && em.baType.typeDescription.IndexOf(txtSearch.Text, 0, StringComparison.CurrentCultureIgnoreCase) != -1) return true;
                if (em.baLevel != null && em.baLevel.levelDescription != null && em.baLevel.levelDescription.IndexOf(txtSearch.Text, 0, StringComparison.CurrentCultureIgnoreCase) != -1) return true;
                if (em.baContractType != null && em.baContractType.contractTypeDescription != null && em.baContractType.contractTypeDescription.IndexOf(txtSearch.Text, 0, StringComparison.CurrentCultureIgnoreCase) != -1) return true;
                if (em.emplProbationaryEnd != null && em.emplProbationaryEnd.Value.ToString("dd/mm/YYYY").IndexOf(txtSearch.Text, 0, StringComparison.CurrentCultureIgnoreCase) != -1) return true;
                if (em.emplWorkEndDate != null && em.emplWorkEndDate.Value.ToString("dd/mm/YYYY").IndexOf(txtSearch.Text, 0, StringComparison.CurrentCultureIgnoreCase) != -1) return true;
                if (em.emplWorkStartDate != null && em.emplWorkStartDate.Value.ToString("dd/mm/YYYY").IndexOf(txtSearch.Text, 0, StringComparison.CurrentCultureIgnoreCase) != -1) return true;
                if (em.emplBirthPlace != null && em.emplBirthPlace.IndexOf(txtSearch.Text, 0, StringComparison.CurrentCultureIgnoreCase) != -1) return true;
                if (em.emplBirthDay != null && em.emplBirthDay.Value.ToString("dd/mm/YYYY").IndexOf(txtSearch.Text, 0, StringComparison.CurrentCultureIgnoreCase) != -1) return true;
                if (em.emplFiscalCode != null && em.emplFiscalCode.IndexOf(txtSearch.Text, 0, StringComparison.CurrentCultureIgnoreCase) != -1) return true;
                if (em.baNationality != null && em.baNationality.nationalityDescription != null && em.baNationality.nationalityDescription.IndexOf(txtSearch.Text, 0, StringComparison.CurrentCultureIgnoreCase) != -1) return true;
                if (em.baStudyTitle != null && em.baStudyTitle.studyTitleDescription != null && em.baStudyTitle.studyTitleDescription.IndexOf(txtSearch.Text, 0, StringComparison.CurrentCultureIgnoreCase) != -1) return true;
                if (em.emplFiscalCodePartner != null && em.emplFiscalCodePartner.IndexOf(txtSearch.Text, 0, StringComparison.CurrentCultureIgnoreCase) != -1) return true;
                if (em.emplLivingPlace != null && em.emplLivingPlace.IndexOf(txtSearch.Text, 0, StringComparison.CurrentCultureIgnoreCase) != -1) return true;
                if (em.emplMobileNumberPrivate != null && em.emplMobileNumberPrivate.IndexOf(txtSearch.Text, 0, StringComparison.CurrentCultureIgnoreCase) != -1) return true;
                if (em.emplName != null && em.emplName.IndexOf(txtSearch.Text, 0, StringComparison.CurrentCultureIgnoreCase) != -1) return true;
                if (em.emplTimePercentage != null && em.emplTimePercentage.ToString().IndexOf(txtSearch.Text, 0, StringComparison.CurrentCultureIgnoreCase) != -1) return true;
                return false;
            };
        }
    }
}
