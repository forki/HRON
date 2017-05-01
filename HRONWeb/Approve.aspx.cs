using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRONWeb
{
    public partial class Approve : System.Web.UI.Page
    {
        Guid WFID;
        string ActionID = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            WFID = Guid.Parse(Request.QueryString["WFID"]);
            ActionID = Request.QueryString["AID"];

            if (WFID == null || ActionID == null || ActionID == "")
                throw new Exception("URL not correct WFID / AID");

            ApprovalInstances.ApprovalServiceClient cli = new ApprovalInstances.ApprovalServiceClient();
            ApprovalInstances.WFApprovals[] aprovals = cli.getApprovalInstances(Default.getEmail());
            ApprovalInstances.WFApprovals w = null;
            foreach (ApprovalInstances.WFApprovals wf in aprovals)
                if (wf.approverWFID == WFID && wf.approverActivityID == ActionID)
                    w = wf;

            if(w==null)
                throw new Exception("WFID / AID not found");
            txtBody.Text=w.body;
            txtTitle.Text = w.subject;
        }

        protected void btnApprove_ServerClick(object sender, EventArgs e)
        {
            ApprovalWF.ApprovalServiceClient cli = new ApprovalWF.ApprovalServiceClient();
            cli.sendApprovalResponse(new ApprovalWF.WFIdentification() { ActivityID = ActionID, WFID = WFID }, true, txtNotes.InnerText);
        }

        protected void btnReject_ServerClick(object sender, EventArgs e)
        {
            if (txtNotes.InnerText.Trim() == "")
                message.Text = "Message cannot be empty";

            ApprovalWF.ApprovalServiceClient cli = new ApprovalWF.ApprovalServiceClient();
            cli.sendApprovalResponse(new ApprovalWF.WFIdentification() { ActivityID = ActionID, WFID = WFID }, false, txtNotes.InnerText );
        }
    }
}