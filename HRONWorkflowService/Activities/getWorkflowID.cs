using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace HRONWorkflowService.Activities
{

    public sealed class getWorkflowID : CodeActivity
    {
        // Aktivitätseingabeargument vom Typ "string" definieren
        public OutArgument<Guid> WFID { get; set; }
        public OutArgument<String> ActivityInstanceID { get; set; }

        // Wenn durch die Aktivität ein Wert zurückgegeben wird, erfolgt eine Ableitung von CodeActivity<TResult>
        // und der Wert von der Ausführmethode zurückgegeben.
        protected override void Execute(CodeActivityContext context)
        {
            WFID.Set(context, context.WorkflowInstanceId);
            ActivityInstanceID.Set(context, context.ActivityInstanceId);
        }
    }
}
