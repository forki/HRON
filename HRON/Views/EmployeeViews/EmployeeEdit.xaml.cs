using HRON.Views;
using HRONLib;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Globalization;
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

namespace HRON.Views.EmployeeViews
{
    /// <summary>
    /// Interaktionslogik für UserEdit.xaml
    /// </summary>
    public partial class EmployeeEdit : UserControl
    {
        protected HRONEntities entities = new HRONEntities();
        public ObservableCollection<baCarPolicy> carPolicy { get; set; }
        MainWindow mainWindow = null;
        Employee actualEmployee = null;
        EmplSalary actualSalaryRow = null;

        protected Visibility isInDesignerMode
        {
            get { return (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this)) ? Visibility.Collapsed : Visibility.Visible; }
        }

        public Visibility SalaryPlusVisibility
        {
            get
            {
                if (salaryExpander.IsExpanded)
                    return Visibility.Visible;
                return Visibility.Collapsed;
            }
        }

        public Boolean SaveEnabled 
        {
            get {
                return entities.ChangeTracker.HasChanges();
            }
        }

        public EmployeeEdit(MainWindow main, int EmployeeId)
        {
            InitializeComponent();
            this.entities = new HRONEntities();
            this.mainWindow = main;

            setActualEmployee(EmployeeId);
        }

        private void setActualEmployee(int EmployeeId)
        {
            Employee e = entities.Employee.Find(new object[] { EmployeeId });
            if (e == null)
                throw new Exception("Employee with id " + EmployeeId + " not found");
            this.actualEmployee = e;
        }

        public event EventHandler UserSaved;

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Laden Sie Ihre Daten nicht zur Entwurfszeit.
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["employeeViewSource"];
                myCollectionViewSource.Source = entities.Employee.Local;
                myCollectionViewSource.View.MoveCurrentToFirst();

                System.Windows.Data.CollectionViewSource myemployeeEmplSalaryCarPolicyViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["employeeEmplSalaryCarPolicyViewSource"];
                entities.baCarPolicy.Load();
                myemployeeEmplSalaryCarPolicyViewSource.Source = entities.baCarPolicy.Local;

                entities.baFringeBenefit.Load();

                System.Windows.Data.CollectionViewSource mybaDocumentationViewSourceViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["baDocumentationViewSource"];
                entities.baDocumentation.Load();
                mybaDocumentationViewSourceViewSource.Source = entities.baDocumentation.Local;
                
                /*
                System.Windows.Data.CollectionViewSource myemployeebaFringeBenefitViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["employeebaFringeBenefitViewSource"];
                myemployeebaFringeBenefitViewSource.Source = entities.baFringeBenefit.Local;
                */

                entities.baCarPolicy.Load();
                this.carPolicy = entities.baCarPolicy.Local;
                entities.baCDC.Load();
                DataGridComboBoxColumn cdcColumn = (DataGridComboBoxColumn)this.Resources["cdcColumn"];
                cdcColumn.ItemsSource = entities.baCDC.Local;

                this.DataContext = this;

                //outerGrid.DataContext = actualEmployee;

                
                //Todo: CDC muss eine NxM werden
                emplCdcIDComboBox.ItemsSource = entities.baCDC.ToList();
                emplCdcIDComboBox.DisplayMemberPath = "cdcValue";
                emplCdcIDComboBox.SelectedValuePath = "cdcID";

                emplBusinessUnitIDComboBox.ItemsSource = entities.baBusinessUnitID.ToList();
                emplBusinessUnitIDComboBox.DisplayMemberPath = "businessUnitDescription";
                emplBusinessUnitIDComboBox.SelectedValuePath = "businessUnitID";

                emplContractTypeIDComboBox.ItemsSource = entities.baContractType.ToList();
                emplContractTypeIDComboBox.DisplayMemberPath = "contractTypeDescription";
                emplContractTypeIDComboBox.SelectedValuePath = "contractTypeID";

                emplCountryIDComboBox.ItemsSource = entities.baCountry.ToList();
                emplCountryIDComboBox.DisplayMemberPath = "countryTitle";
                emplCountryIDComboBox.SelectedValuePath = "countryID";

                emplJobTitleIDComboBox.ItemsSource = entities.baJobTitle.ToList();
                emplJobTitleIDComboBox.DisplayMemberPath = "jobTitleDescription";
                emplJobTitleIDComboBox.SelectedValuePath = "jobTitleID";

                emplLevelIDComboBox.ItemsSource = entities.baLevel.ToList();
                emplLevelIDComboBox.DisplayMemberPath = "levelDescription";
                emplLevelIDComboBox.SelectedValuePath = "levelID";

                emplNationalityIDComboBox.ItemsSource = entities.baNationality.ToList();
                emplNationalityIDComboBox.DisplayMemberPath = "nationalityDescription";
                emplNationalityIDComboBox.SelectedValuePath = "nationalityID";

                emplSpecializationIDComboBox.ItemsSource = entities.baSpecialization.ToList();
                emplSpecializationIDComboBox.DisplayMemberPath = "specializationDescription";
                emplSpecializationIDComboBox.SelectedValuePath = "specializationID";

                emplStudyTitleIDComboBox.ItemsSource = entities.baStudyTitle.ToList();
                emplStudyTitleIDComboBox.DisplayMemberPath = "studyTitleDescription";
                emplStudyTitleIDComboBox.SelectedValuePath = "studyTitleID";

                emplTimeTypeIDComboBox.ItemsSource = entities.baTimeType.ToList();
                emplTimeTypeIDComboBox.DisplayMemberPath = "timeTypeDescription";
                emplTimeTypeIDComboBox.SelectedValuePath = "timeTypeID";

                emplTypeIDComboBox.ItemsSource = entities.baType.ToList();
                emplTypeIDComboBox.DisplayMemberPath = "typeDescription";
                emplTypeIDComboBox.SelectedValuePath = "typeID";

                emplWorkPlaceIDComboBox.ItemsSource = entities.baWorkPlace.ToList();
                emplWorkPlaceIDComboBox.DisplayMemberPath = "workPlaceName";
                emplWorkPlaceIDComboBox.SelectedValuePath = "workPlaceID";

                foreach (baFunctions f in entities.baFunctions)
                    if (actualEmployee.EmplFunctions.Where(x => x.emplFuncID == f.funcID).Count() == 0)
                        actualEmployee.EmplFunctions.Add(new EmplFunctions() { emplFuncID = f.funcID, value = false, Employee = actualEmployee, baFunctions = f });

                foreach (baCompanyRights f in entities.baCompanyRights)
                    if (actualEmployee.EmplCompanyRights.Where(x => x.rightID == f.compID).Count() == 0)
                        actualEmployee.EmplCompanyRights.Add(new EmplCompanyRights() { comprightID = f.compID, value = false, Employee = actualEmployee, baCompanyRights = f });

            }
        }

        private void save_click(object sender, RoutedEventArgs e)
        {
            try
            {
                entities.SaveChanges();
                mainWindow.closeTab(this);
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                {
                    // Get entry

                    DbEntityEntry entry = item.Entry;
                    string entityTypeName = entry.Entity.GetType().Name;

                    // Display or log error messages

                    foreach (DbValidationError subItem in item.ValidationErrors)
                    {
                        string message = string.Format("Error '{0}' occurred in {1} at {2}",
                                 subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
                        MessageBox.Show(message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in SaveChanges(): " + ex.Message);
            }

            if (UserSaved != null)
                UserSaved(actualEmployee, new EventArgs());
        }

        private void AddSalaryButton_Click(object sender, RoutedEventArgs e)
        {
            EmplSalary sal = actualEmployee.EmplSalary.OrderByDescending(s => s.salWriteDate).FirstOrDefault();

            EmplSalary newSal = new EmplSalary();
            newSal.salWriteDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            newSal.Employee = actualEmployee;
            newSal.salEmplID = actualEmployee.emplID;
            if (sal != null)
            {
                newSal.salStartingFrom = sal.salStartingFrom;
                newSal.salSuperMinimoAssorbibile = sal.salSuperMinimoAssorbibile;
                newSal.salOvertimeHours = sal.salOvertimeHours;
                newSal.salOvertimeForfait = sal.salOvertimeForfait;
                newSal.salNetSalaryMonth = sal.salNetSalaryMonth;
                newSal.salL68 = sal.salL68;
                newSal.salL104 = sal.salL104;
                newSal.salGrossSalaryYear = sal.salGrossSalaryYear;
                newSal.salGrossSalaryMonthFT = sal.salGrossSalaryYear;
                newSal.salGrossSalaryMonth = sal.salGrossSalaryMonth;
                newSal.salEmplID = sal.salEmplID;
                newSal.salCostYear = sal.salCostYear;
                newSal.salCostWithBonusYear = sal.salCostWithBonusYear;
                newSal.salCarPolicyClass = sal.salCarPolicyClass;
                newSal.salCar = sal.salCar;
                newSal.salBonus = sal.salBonus;
                foreach (EmplSalaryFringeBenefit bf in sal.EmplSalaryFringeBenefit)
                    newSal.EmplSalaryFringeBenefit.Add(new EmplSalaryFringeBenefit { benefitID = bf.benefitID, value=bf.value, baFringeBenefit=bf.baFringeBenefit, salEmplID=bf.salEmplID});
            }

            foreach (baFringeBenefit f in entities.baFringeBenefit)
                if (newSal.EmplSalaryFringeBenefit.Where(x => x.benefitID == f.benefitID).Count() == 0)
                    newSal.EmplSalaryFringeBenefit.Add(new EmplSalaryFringeBenefit() { benefitID = f.benefitID, value = false, EmplSalary = newSal, baFringeBenefit = f });

            actualEmployee.EmplSalary.Add(newSal);
            System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["employeeEmplSalaryViewSource"];
            myCollectionViewSource.View.MoveCurrentToFirst();
        }

        public Boolean SalaryRowWritable
        {
            get
            {
                System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["employeeEmplSalaryViewSource"];
                var o = myCollectionViewSource.View.CurrentItem;
                if (o is EmplSalary)
                {
                    if (!((EmplSalary)o).isActualLine) // Siamo nel futuro
                        return true;
                    if(actualSalaryRow==null)
                    {
                        actualSalaryRow = actualEmployee.EmplSalary.Where(e => e.isActualLine).OrderByDescending(e => e.salStartingFrom).OrderByDescending(e => e.salWriteDate).First();
                    }
                    if (((EmplSalary)o).salID == actualSalaryRow.salID) // Ultimo attuale
                        return true;
                }
                return false;
            }
        }
        private void emplSalaryDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            e.Cancel = !SalaryRowWritable;
        }

        private void Workflow_Click(object sender, RoutedEventArgs e)
        {
            ListWorkflows lw = new ListWorkflows(mainWindow, actualEmployee);
            DialogHost.Show(lw);
        }

        private void showFiles_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.Show(new EmployeeEditFiles(mainWindow, entities, actualEmployee));
        }

        private void DockPanel_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Note that you can have more than one file.
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                // Assuming you have one file that you care about, pass it off to whatever
                // handling code you have defined.
                EmployeeEditFiles uef = new EmployeeEditFiles(mainWindow, entities, actualEmployee);
                DialogHost.Show(uef);

                uef.loadFiles(files);
            }
        }

        private void showHistory_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.Show(new showHistory(mainWindow, entities, actualEmployee.GetType().BaseType, actualEmployee.emplID.ToString()));
        }

        private void FamiliyInfoButton_Click(object sender, RoutedEventArgs e)
        {
            if(emplFamilyDataGrid.SelectedItem is EmplFamily)
            {
                EmplFamily fam = (EmplFamily)emplFamilyDataGrid.SelectedItem;
                DialogHost.Show(new showHistory(mainWindow, entities, fam.GetType().BaseType, showHistory.GetPrimaryKeyValuesOf(new string[] { fam.emplID.ToString(), fam.famID.ToString() })));
            }
            if (emplFamilyDataGrid.SelectedItem is EmplSalary)
            {
                EmplSalary fam = (EmplSalary)emplFamilyDataGrid.SelectedItem;
                DialogHost.Show(new showHistory(mainWindow, entities, fam.GetType().BaseType, showHistory.GetPrimaryKeyValuesOf(new string[] { fam.salEmplID.ToString(), fam.salID.ToString() })));
            }
        }

        private void Files_Click(object sender, RoutedEventArgs e)
        {
            EmplDocumentation doc = emplDocumentationDataGrid.SelectedItem as EmplDocumentation;
            if(doc!=null)
            {
                EmployeeEditFiles uef = new EmployeeEditFiles(mainWindow, entities, actualEmployee);
                uef.addFilter(doc);
                DialogHost.Show(uef);
            }
        }

        private void AddCDCButton_Click(object sender, RoutedEventArgs e)
        {
            actualEmployee.EmplCDC.Add(new EmplCDC() { cdcStartingDate = DateTime.Now.Date });
        }

        private void emplCDCDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            createRestRow(dg);
        }

        private static void createRestRow(DataGrid dg)
        {
            if (dg.SelectedItem is EmplCDC)
            {
                EmplCDC ecdc = (EmplCDC)dg.SelectedItem;
                decimal perc = ecdc.EmplCDCDetail.Where(e => e.cdc != null).Sum(em => em.cdcPercentage);
                EmplCDCDetail d = ecdc.EmplCDCDetail.Where(e => e.cdc == null).FirstOrDefault();
                if (perc != 100)
                {
                    if (d != null)
                        d.cdcPercentage = 100 - perc;
                    else
                        ecdc.EmplCDCDetail.Add(new EmplCDCDetail() { cdcPercentage = 100 - perc, EmplCDC = ecdc });
                }
                else if (d != null)
                    ecdc.EmplCDCDetail.Remove(d);
            }
        }

        private void emplCDCDetailDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            createRestRow(emplCDCDataGrid);
            //emplCDCDataGrid.Items.Refresh();
        }

        /*
        private void emplCDCDetailDataGrid_LostFocus(object sender, RoutedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            if (dg.SelectedItem is EmplCDCDetail)
            {
                EmplCDCDetail ecdc = (EmplCDCDetail)dg.SelectedItem;
                if(ecdc.EmplCDC!=null && ecdc.EmplCDC.EmplCDCDetail!=null)
                {
                    decimal perc = ecdc.EmplCDC.EmplCDCDetail.Sum(em => em.cdcPercentage);
                    if (perc < 100)
                    {
                        ecdc.EmplCDC.EmplCDCDetail.Add(new EmplCDCDetail() { cdcPercentage = 100 - perc, EmplCDC = ecdc.EmplCDC });
                    }
                }
            }
        }

        private void emplCDCDetailDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            if (dg.SelectedItem is EmplCDCDetail)
            {
                EmplCDCDetail ecdc = (EmplCDCDetail)dg.SelectedItem;
                if (ecdc.EmplCDC != null && ecdc.EmplCDC.EmplCDCDetail != null)
                {
                    decimal perc = ecdc.EmplCDC.EmplCDCDetail.Sum(em => em.cdcPercentage);
                    EmplCDCDetail d = ecdc.EmplCDC.EmplCDCDetail.Where(w => w.cdc == null).First();
                    if (d != null)
                    {
                        d.cdcPercentage = 100 - perc;
                    }
                    else
                    {
                        if (perc < 100)
                            ecdc.EmplCDC.EmplCDCDetail.Add(new EmplCDCDetail() { cdcPercentage = 100 - perc, EmplCDC = ecdc.EmplCDC });
                        else
                        {
                            EmplCDCDetail oldRow = (EmplCDCDetail)e.OriginalSource;
                            oldRow.cdcPercentage = 100 - perc;
                        }
                    }
                }
            }
        }
        */
    }
    
}
