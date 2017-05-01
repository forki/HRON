using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using HRONLib;

namespace HRONWorkflowService.Activities.Approve
{

    public sealed class getInstances : CodeActivity
    {
        public InArgument<string> mail { get; set; }

        public OutArgument<List<WFApprovals>> approvalRequests { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            HRONEntities dbcontext = new HRONEntities();
            string m = mail.Get(context);
            List<WFApprovals> ret = dbcontext.WFApprovals.Where(w => (!w.endtime.HasValue && w.mail == m)).ToList();
            approvalRequests.Set(context, ret);
        }
    }
}
