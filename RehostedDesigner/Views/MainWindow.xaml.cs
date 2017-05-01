using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Activities;
using System.Activities.Presentation.Toolbox;
using System.Reflection;
using System.IO;
using System.Activities.XamlIntegration;
using Microsoft.Win32;
using RehostedWorkflowDesigner.Helpers;
using System.ComponentModel;
using System.Timers;
using Twilio;
using System.Activities.Presentation;
using System.Runtime.Versioning;
using System.Activities.Statements;
using System.ServiceModel.Activities;
using System.ServiceModel.Activities.Presentation.Factories;
using System.Activities.Core.Presentation;
using HRONWorkflowService.Activities;
using HRONWorkflowService.Activities.Approve;
using HRONLib;

namespace RehostedWorkflowDesigner.Views
{

    public partial class MainWindow : INotifyPropertyChanged
    {
        private WorkflowApplication _wfApp;
        private ToolboxControl _wfToolbox;
        private CustomTrackingParticipant _executionLog;
        private List<Type> activitiesInTbx = new List<Type>();

        private string _currentWorkflowFile = string.Empty;
        private Timer _timer;


        public MainWindow()
        {
            InitializeComponent();
            _timer = new Timer(1000);
            _timer.Enabled = false;
            _timer.Elapsed += TrackingDataRefresh;

            //load all available workflow activities from loaded assemblies 
            InitializeActivitiesToolbox();

            //initialize designer
            WfDesignerBorder.Child = CustomWfDesigner.Instance.View;
            WfPropertyBorder.Child = CustomWfDesigner.Instance.PropertyInspectorView;

        }


        public string ExecutionLog
        {
            get
            {
                if (_executionLog != null)
                    return _executionLog.TrackData;
                else
                    return string.Empty;
            }
            set { _executionLog.TrackData = value; NotifyPropertyChanged("ExecutionLog"); }
        }


        private void TrackingDataRefresh(Object source, ElapsedEventArgs e)
        {
            NotifyPropertyChanged("ExecutionLog");
        }


        private void consoleExecutionLog_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            consoleExecutionLog.ScrollToEnd();
        }


        /// <summary>
        /// show execution log in ui
        /// </summary>
        private void UpdateTrackingData()
        {
            //retrieve & display execution log
            //consoleExecutionLog.Dispatcher.Invoke(
            //    System.Windows.Threading.DispatcherPriority.Normal,
            //    new Action(
            //        delegate ()
            //        {
            //            //consoleExecutionLog.Text = _executionLog.TrackData;
            NotifyPropertyChanged("ExecutionLog");
            //        }
            //));
        }


        private ToolboxControl CreateTbx()
        {
            Dictionary<string, List<Type>> activitiesToInclude = new Dictionary<string, List<Type>>();
            activitiesToInclude.Add("Control Flow", new List<Type>
            {
                typeof(DoWhile),
                typeof(ForEach<>),
                typeof(If),
                typeof(Parallel),
                typeof(ParallelForEach<>),
                typeof(Pick),
                typeof(PickBranch),
                typeof(Sequence),
                typeof (Switch<>),
                typeof(While),
            });
            activitiesToInclude.Add("Flowchart", new List<Type>
            {
                typeof(Flowchart),
                typeof(FlowDecision),
                typeof(FlowSwitch<>)
            });
            activitiesToInclude.Add("State Machine", new List<Type>
            {
                typeof (StateMachine),
                typeof (State),
                typeof (FinalState)
            });
            activitiesToInclude.Add("Messaging", new List<Type>
            {
                typeof(CorrelationScope),
                typeof(InitializeCorrelation),
                typeof(Receive),
                typeof(ReceiveAndSendReplyFactory),
                typeof(Send),
                typeof(SendAndReceiveReplyFactory),
                typeof(TransactedReceiveScope)
            });
            activitiesToInclude.Add("Runtime", new List<Type>
            {
                typeof(Persist),
                typeof(TerminateWorkflow),
                typeof(NoPersistScope)
            });
            activitiesToInclude.Add("Primitives", new List<Type>
            {
                typeof(Assign),
                typeof(Delay),
                typeof(InvokeDelegate),
                typeof(InvokeMethod),
                typeof(WriteLine)
            });
            activitiesToInclude.Add("Transaction", new List<Type>
            {
                typeof(CancellationScope),
                typeof(CompensableActivity),
                typeof(Compensate),
                typeof(Confirm),
                typeof(TransactionScope)
            });
            activitiesToInclude.Add("Collection", new List<Type>
            {
                typeof(AddToCollection<>),
                typeof(ClearCollection<>),
                typeof(ExistsInCollection<>),
                typeof(RemoveFromCollection<>),
            });
            activitiesToInclude.Add("Error Handling", new List<Type>
            {
                typeof(Rethrow),
                typeof(Throw),
                typeof(TryCatch)
            });
            activitiesToInclude.Add("HR Workflow Service", new List<Type>
            {
                typeof(StartApproval),
                typeof(createAppointment),
                typeof(SendEmail)
            });

            ToolboxControl tb = new ToolboxControl();
            foreach (var category in activitiesToInclude)
            {
                ToolboxCategory cat = CreateTbxCat(category.Key, category.Value, true);
                tb.Categories.Add(cat);
            }
            return tb;
        }

        private ToolboxCategory CreateTbxCat(string catName, List<Type> activities, Boolean isStandard)
        {
            ToolboxCategory tc = new ToolboxCategory(catName);
            foreach (var act in activities)
            {
                if (!activitiesInTbx.Contains(act))
                {
                    ToolboxItemWrapper tiw = new ToolboxItemWrapper(act.FullName, act.Assembly.FullName, null, act.Name);
                    tc.Add(tiw);
                    if (isStandard)
                        activitiesInTbx.Add(act);
                }
            }
            return tc;
        }

        /// <summary>
        /// Retrieves all Workflow Activities from the loaded assemblies and inserts them into a ToolboxControl 
        /// </summary>
        private void InitializeActivitiesToolbox()
        {
            try
            {
                _wfToolbox = CreateTbx();

                /*
                _wfToolbox = new ToolboxControl();
                //load dependency
                //AppDomain.CurrentDomain.Load("Twilio.Api");
                // load Custom Activity Libraries into current domain
                //AppDomain.CurrentDomain.Load("MeetupActivityLibrary");
                // load System Activity Libraries into current domain; uncomment more if libraries below available on your system
                //AppDomain.CurrentDomain.Load("System.Activities");
                //AppDomain.CurrentDomain.Load("System.ServiceModel.Activities");
                //AppDomain.CurrentDomain.Load("System.Activities.Core.Presentation");
                //AppDomain.CurrentDomain.Load("Microsoft.Workflow.Management");
                //AppDomain.CurrentDomain.Load("Microsoft.Activities.Extensions");
                //AppDomain.CurrentDomain.Load("Microsoft.Activities");
                //AppDomain.CurrentDomain.Load("Microsoft.Activities.Hosting");
                //AppDomain.CurrentDomain.Load("Microsoft.PowerShell.Utility.Activities");
                //AppDomain.CurrentDomain.Load("Microsoft.PowerShell.Security.Activities");
                //AppDomain.CurrentDomain.Load("Microsoft.PowerShell.Management.Activities");
                //AppDomain.CurrentDomain.Load("Microsoft.PowerShell.Diagnostics.Activities");
                //AppDomain.CurrentDomain.Load("Microsoft.Powershell.Core.Activities");
                //AppDomain.CurrentDomain.Load("Microsoft.PowerShell.Activities");

                // get all loaded assemblies
                IEnumerable<Assembly> appAssemblies = AppDomain.CurrentDomain.GetAssemblies().OrderBy(a => a.GetName().Name);

                // check if assemblies contain activities
                int activitiesCount = 0;
                foreach (Assembly activityLibrary in appAssemblies)
                {
                    var wfToolboxCategory = new ToolboxCategory(activityLibrary.GetName().Name);
                    var actvities = from
                                        activityType in activityLibrary.GetExportedTypes()
                                    where
                                        (activityType.IsSubclassOf(typeof(Activity))
                                        || activityType.IsSubclassOf(typeof(NativeActivity))
                                        || activityType.IsSubclassOf(typeof(DynamicActivity))
                                        || activityType.IsSubclassOf(typeof(ActivityWithResult))
                                        || activityType.IsSubclassOf(typeof(AsyncCodeActivity))
                                        || activityType.IsSubclassOf(typeof(CodeActivity))
                                        || activityType == typeof(System.Activities.Core.Presentation.Factories.ForEachWithBodyFactory<Type>)
                                        || activityType == typeof(System.Activities.Statements.FlowNode)
                                        || activityType == typeof(System.Activities.Statements.State)
                                        || activityType == typeof(System.Activities.Core.Presentation.FinalState)
                                        || activityType == typeof(System.Activities.Statements.FlowDecision)
                                        || activityType == typeof(System.Activities.Statements.FlowNode)
                                        || activityType == typeof(System.Activities.Statements.FlowStep)
                                        || activityType == typeof(System.Activities.Statements.FlowSwitch<Type>)
                                        || activityType == typeof(System.Activities.Statements.ForEach<Type>)
                                        || activityType == typeof(System.Activities.Statements.Switch<Type>)
                                        || activityType == typeof(System.Activities.Statements.TryCatch)
                                        || activityType == typeof(System.Activities.Statements.While))
                                        && activityType.IsVisible
                                        && activityType.IsPublic
                                        && !activityType.IsNested
                                        && !activityType.IsAbstract
                                        && (activityType.GetConstructor(Type.EmptyTypes) != null)
                                        //&& !activityType.Name.Contains('`')
                                    orderby
                                        activityType.Name
                                    select
                                        new ToolboxItemWrapper(activityType);

                    actvities.ToList().ForEach(wfToolboxCategory.Add);                    

                    if (wfToolboxCategory.Tools.Count > 0)
                    {
                        _wfToolbox.Categories.Add(wfToolboxCategory);
                        activitiesCount += wfToolboxCategory.Tools.Count;
                    }
                }
                //fixed ForEach
                _wfToolbox.Categories.Add(
                       new System.Activities.Presentation.Toolbox.ToolboxCategory
                       {
                           CategoryName = "CustomForEach",
                           Tools = {
                                new ToolboxItemWrapper(typeof(System.Activities.Core.Presentation.Factories.ForEachWithBodyFactory<>)),
                                new ToolboxItemWrapper(typeof(System.Activities.Core.Presentation.Factories.ParallelForEachWithBodyFactory<>))
                           }
                       }
                );
*/
                LabelStatusBar.Content = String.Format("Loaded Activities: {0}", activitiesInTbx.Count.ToString());
                WfToolboxBorder.Child = _wfToolbox;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// Retrieve Workflow Execution Logs and Workflow Execution Outputs
        /// </summary>
        private void WfExecutionCompleted(WorkflowApplicationCompletedEventArgs ev)
        {
            try
            {
                //retrieve & display execution log
                _timer.Stop();
                UpdateTrackingData();

                //retrieve & display execution output
                foreach (var item in ev.Outputs)
                {
                    consoleOutput.Dispatcher.Invoke(
                        System.Windows.Threading.DispatcherPriority.Normal,
                        new Action(
                            delegate()
                            {
                                consoleOutput.Text += String.Format("[{0}] {1}" + Environment.NewLine, item.Key, item.Value);
                            }
                    ));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        #region Commands Handlers - Executed - New, Open, Save, Run

        /// <summary>
        /// Creates a new Workflow Application instance and executes the Current Workflow
        /// </summary>
        private void CmdWorkflowRun(object sender, ExecutedRoutedEventArgs e)
        {
            //get workflow source from designer
            CustomWfDesigner.Instance.Flush();
            MemoryStream workflowStream = new MemoryStream(ASCIIEncoding.Default.GetBytes(CustomWfDesigner.Instance.Text));
            DynamicActivity activityExecute = ActivityXamlServices.Load(workflowStream) as DynamicActivity;

            //configure workflow application
            consoleExecutionLog.Text = String.Empty;
            consoleOutput.Text = String.Empty;
            _executionLog = new CustomTrackingParticipant();
            _wfApp = new WorkflowApplication(activityExecute);
            _wfApp.Extensions.Add(_executionLog);
            _wfApp.Completed = WfExecutionCompleted;

            //execute 
            _wfApp.Run();

            //enable timer for real-time logging
            _timer.Start();
        }

        /// <summary>
        /// Stops the Current Workflow
        /// </summary>
        private void CmdWorkflowStop(object sender, ExecutedRoutedEventArgs e)
        {
            //manual stop
            if (_wfApp != null)
            {
                _wfApp.Abort("Stopped by User");
                _timer.Stop();
                UpdateTrackingData();
            }

        }


        public event EventHandlerSave onSave;
        /// <summary>
        /// Save the current state of a Workflow
        /// </summary>
        private void CmdWorkflowSave(object sender, ExecutedRoutedEventArgs e)
        {
            if (_currentWorkflowFile == String.Empty)
            {
                var dialogSave = new SaveFileDialog();
                dialogSave.Title = "Save Workflow";
                dialogSave.Filter = "Workflows (.xaml)|*.xaml";

                if (dialogSave.ShowDialog() == true)
                {
                    CustomWfDesigner.Instance.Save(dialogSave.FileName);
                        _currentWorkflowFile = dialogSave.FileName;
                    if (onSave != null)
                        onSave(this, new EventArgsSave(dialogSave.FileName, actualWorkflow));
                }
            }
            else
            {
                CustomWfDesigner.Instance.Save(_currentWorkflowFile);
                if (onSave != null)
                    onSave(this, new EventArgsSave(_currentWorkflowFile, actualWorkflow));
            }
        }


        /// <summary>
        /// Creates a new Workflow Designer instance and loads the Default Workflow 
        /// </summary>
        private void CmdWorkflowNew(object sender, ExecutedRoutedEventArgs e)
        {
            _currentWorkflowFile = String.Empty;
            CustomWfDesigner.NewInstance();
            WfDesignerBorder.Child = CustomWfDesigner.Instance.View;
            WfPropertyBorder.Child = CustomWfDesigner.Instance.PropertyInspectorView;
        }


        /// <summary>
        /// Creates a new Workflow Designer instance and loads the Default Workflow with C# Expression Editor
        /// </summary>
        private void CmdWorkflowNewCSharp(object sender, ExecutedRoutedEventArgs e)
        {
            _currentWorkflowFile = String.Empty;
            CustomWfDesigner.NewInstanceCSharp();
            WfDesignerBorder.Child = CustomWfDesigner.Instance.View;
            WfPropertyBorder.Child = CustomWfDesigner.Instance.PropertyInspectorView;
        }


        /// <summary>
        /// Loads a Workflow into a new Workflow Designer instance
        /// </summary>
        private void CmdWorkflowOpen(object sender, ExecutedRoutedEventArgs e)
        {            
            var dialogOpen = new OpenFileDialog();
            dialogOpen.Title = "Open Workflow";
            dialogOpen.Filter = "Workflows (.xaml)|*.xaml";

            if (dialogOpen.ShowDialog() == true)
                openFile(dialogOpen.FileName);
        }

        baWorkflows actualWorkflow = null;
        public void openFile(baWorkflows wf)
        {
            actualWorkflow = wf;
            string fileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".xaml";
            if (File.Exists(fileName))
                File.Delete(fileName);

            File.WriteAllBytes(fileName, wf.WorkFlowXAML);

            openFile(fileName);
        }

        public baWorkflows getActualWorkflow()
        {
            return actualWorkflow;
        }

        public void openFile(string filename)
        {
            using (var file = new StreamReader(filename, true))
            {
                CustomWfDesigner.NewInstanceCSharp(filename);
                WfDesignerBorder.Child = CustomWfDesigner.Instance.View;
                WfPropertyBorder.Child = CustomWfDesigner.Instance.PropertyInspectorView;

                _currentWorkflowFile = filename;
            }
        }

        #endregion


        #region INotify
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
