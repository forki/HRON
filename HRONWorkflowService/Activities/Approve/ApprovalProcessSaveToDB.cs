using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using HRONLib;

namespace HRONWorkflowService.Activities.Approve
{

    public sealed class ApprovalProcessSaveToDB : CodeActivity
    {
        // Aktivitätseingabeargument vom Typ "string" definieren
        public InArgument<string> to { get; set; }
        public InArgument<string> body { get; set; }
        public InArgument<string> subject { get; set; }
        public InArgument<TimeSpan> waittime { get; set; }

        public InArgument<Guid> WFId { get; set; }
        public InArgument<String> ActivityId { get; set; }

        public InArgument<Guid> parentWFId { get; set; }

        // Wenn durch die Aktivität ein Wert zurückgegeben wird, erfolgt eine Ableitung von CodeActivity<TResult>
        // und der Wert von der Ausführmethode zurückgegeben.
        protected override void Execute(CodeActivityContext context)
        {
            HRONEntities dbcontext = new HRONEntities();

            // Cerco parent WF
            EmplWorkflows ewf=null;
            if(parentWFId.Get(context)!=null)
               ewf = dbcontext.EmplWorkflows.Where(w => w.wfID == parentWFId.ToString()).FirstOrDefault();

            WFApprovals approval = new WFApprovals();
            approval.approverWFID = WFId.Get(context);
            approval.approverActivityID = ActivityId.Get(context);
            approval.body = body.Get(context);
            approval.mail = to.Get(context);
            approval.subject = subject.Get(context);
            approval.starttime = DateTime.Now;
            if (ewf != null)
                approval.Parent = ewf;

            dbcontext.WFApprovals.Add(approval);

            dbcontext.SaveChanges();
        }
    }
}
