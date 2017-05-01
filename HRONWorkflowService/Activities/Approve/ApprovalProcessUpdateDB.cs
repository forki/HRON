using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using HRONLib;

namespace HRONWorkflowService.Activities.Approve
{

    public sealed class ApprovalProcessUpdateDB : CodeActivity
    {
        // Aktivitätseingabeargument vom Typ "string" definieren
        public InArgument<Guid> WFId { get; set; }
        public InArgument<string> ActivityId { get; set; }
        public InArgument<string> Note { get; set; }
        public InArgument<Boolean> approved { get; set; }

        public OutArgument<Boolean> result { get; set; }

        // Wenn durch die Aktivität ein Wert zurückgegeben wird, erfolgt eine Ableitung von CodeActivity<TResult>
        // und der Wert von der Ausführmethode zurückgegeben.
        protected override void Execute(CodeActivityContext context)
        {
            HRONEntities dbcontext = new HRONEntities();

            WFApprovals approval = dbcontext.WFApprovals.Find(new object[] { WFId.Get(context), ActivityId.Get(context) });
            if(approval!=null)
            {
                approval.note = Note.Get(context);
                approval.approved = approved.Get(context);
                approval.endtime = DateTime.Now;

                dbcontext.SaveChanges();

                result.Set(context, true);
                return;
            }
            result.Set(context, false);
        }
    }
}
