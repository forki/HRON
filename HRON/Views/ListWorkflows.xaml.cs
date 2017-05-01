using HRONLib;
using System;
using System.Collections.Generic;
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

namespace HRON.Views
{
    /// <summary>
    /// Interaktionslogik für ListWorkflows.xaml
    /// </summary>
    public partial class ListWorkflows : UserControl
    {
        MainWindow mainWindow;
        HRONEntities entities;
        Employee actualEmployee;

        public ListWorkflows(MainWindow main, Employee e)
        {
            mainWindow = main;
            actualEmployee = e;
            entities = new HRONEntities();    
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            // Laden Sie Ihre Daten nicht zur Entwurfszeit.
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                //Laden Sie hier Ihre Daten, und weisen Sie das Ergebnis der CollectionViewSource zu.
                System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["emplWorkflowsViewSource"];
                entities.EmplWorkflows.Where(em => em.wfEmplID == actualEmployee.emplID).Load();
                actualEmployee = entities.Employee.Where(em => em.emplID == actualEmployee.emplID).FirstOrDefault();
                myCollectionViewSource.Source = entities.EmplWorkflows.Local;

                System.Windows.Data.CollectionViewSource wfCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["wfViewSource"];
                entities.baWorkflows.Where(b => b.OnOnBoarding).Load();
                wfCollectionViewSource.Source = entities.baWorkflows.Local;

             }
        }
        private void startWorkflow_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int id = (int)button.Tag;
            baWorkflows workflow = entities.baWorkflows.Where(b => b.wfID==id).First();

            EmployeeOnBoarding.OnBoardingServiceClient client = new EmployeeOnBoarding.OnBoardingServiceClient();
            Guid wfid = client.StartProcess(actualEmployee.UnProxy<Employee>(entities), workflow.WorkFlowXAML );
            EmplWorkflows w = new EmplWorkflows();
            w.wfEmplID = actualEmployee.emplID;
            w.wfID = wfid.ToString();
            w.wfStartingDate = DateTime.Now;
            w.wfStartingDate = w.wfStartingDate.AddMilliseconds(-1 * w.wfStartingDate.Millisecond);
            w.wfType = workflow;

            entities.EmplWorkflows.Add(w);
            entities.SaveChanges();
        }

        private void LoadStatus_Click(object sender, RoutedEventArgs e)
        {
            EmplWorkflows ew = (EmplWorkflows)emplWorkflowsDataGrid.CurrentItem;
            Button b = (Button)sender;
            try
            {
                EmployeeOnBoarding.OnBoardingServiceClient cli = new EmployeeOnBoarding.OnBoardingServiceClient();
                string[] ret = cli.getWorkflowStatus(new Guid(ew.wfID));

                string t = "";
                foreach (string r in ret)
                    t += r + Environment.NewLine;
                ew.wfStatus = t;
                entities.SaveChanges();
            }
            catch(Exception ex)
            {
                b.Tag = ex.Message;
            }
            finally
            {
                b.Visibility = Visibility.Collapsed;
            }
        }
    }
}
