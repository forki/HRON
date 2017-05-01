using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.Activities.XamlIntegration;
using System.IO;
using HRONLib;

namespace HRONWorkflowService.Activities
{

    public sealed class loadDynamicActivity : CodeActivity
    {
        public InArgument<byte[]> XAML_WF { get; set; }
        public InArgument<Guid> WFID { get; set; }
        public InArgument<Employee> employee { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            MemoryStream mem = new MemoryStream(XAML_WF.Get(context));
            DynamicActivity da = ActivityXamlServices.Load(mem, new ActivityXamlServicesSettings() { CompileExpressions = true }) as DynamicActivity;
            WorkflowInvoker.Invoke(da, new Dictionary<string, object>() { {"employee", employee.Get(context) }, { "test", "Hallo Test" } });
        }
    }
}
