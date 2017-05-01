using HRONLib;
using System;

namespace RehostedWorkflowDesigner.Views
{
    public class EventArgsSave : EventArgs
    {
        public string FileName;
        public baWorkflows baWorkflows;

        public EventArgsSave(string f, baWorkflows w)
        {
            FileName = f;
            baWorkflows = w;
        }
    }
}