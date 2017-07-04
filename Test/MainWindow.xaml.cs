using HRONLib;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Test
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string activityID;
        Guid id;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            ServiceReference1.OnBoardingServiceClient cli = new ServiceReference1.OnBoardingServiceClient();
            FileInfo fi = new FileInfo(System.Reflection.Assembly.GetEntryAssembly().Location);
            String path = fi.Directory + @"\Activities\BlankOnboardActivity.xaml";
            byte[] by = File.ReadAllBytes(path);
            id = cli.StartProcess(new Employee() { emplName = "ABC", emplWorkStartDate = new DateTime(2017, 4, 18, 10, 0, 0), emplID = 995 }, by );
            MessageBox.Show(id.ToString());

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            ServiceReference1.OnBoardingServiceClient cli = new ServiceReference1.OnBoardingServiceClient();
            string[] resp = cli.getWorkflowStatus(id);
            foreach(string s in resp)
                MessageBox.Show(s);

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            ServiceReference2.ApprovalServiceClient cli = new ServiceReference2.ApprovalServiceClient();
            var ret = cli.startApprovalProcess("guenther.erb@gruber-logistics.com", "hallo Günther " + Environment.NewLine + "Wie gehts?", "New Approval Process", new TimeSpan(0, 30, 0));
            id = ret.WFID;
            activityID = ret.ActivityID;
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            ServiceReference2.ApprovalServiceClient cli = new ServiceReference2.ApprovalServiceClient();
            Boolean? b = cli.sendApprovalResponse(new ServiceReference2.WFIdentification() { WFID = id, ActivityID = activityID }, true, "Nota di Approvazione");
            if(b.Value)
                MessageBox.Show("Wow...");
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            ServiceReference3.ApprovalServiceClient cli = new ServiceReference3.ApprovalServiceClient();
            var x = cli.getApprovalInstances("guenther.erb@gruber-logistics.com");
            dataGrid.DataContext = x;
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            RehostedWorkflowDesigner.Views.MainWindow mw = new RehostedWorkflowDesigner.Views.MainWindow();
            FileInfo fi = new FileInfo(System.Reflection.Assembly.GetEntryAssembly().Location);
            String path = fi.Directory + @"\Activities\BlankOnboardActivity.xaml";
            mw.openFile(path);
            mw.onSave += Mw_onSave;
            mw.Show();
        }

        private void Mw_onSave(object sender, EventArgs e)
        {
            MessageBox.Show("Saved - " + sender.ToString());
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            documentService.DocumentServiceClient cli = new documentService.DocumentServiceClient();
            EmplFiles f = cli.addDocument(1, 1);
            MessageBox.Show(f.Progressive.ToString());
        }
    }
}
