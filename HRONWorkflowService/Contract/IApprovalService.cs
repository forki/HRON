using HRONLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Web;
using System.Text;

namespace HRONWorkflowService.Contract
{
    [ServiceContract(Namespace="http://HRONLib.Contracts/2017/04")]
    public interface IApprovalService
    {
        [OperationContract]
        WFIdentification startApprovalProcess(string MailAddress, string MailBody, string MailSubject, TimeSpan WaitTime, TimeSpan SendFirstReminder, TimeSpan ResendReminderAfter, String ReminderHeader, String MailPrincipal);

        [OperationContract]
        bool sendApprovalResponse(WFIdentification wfIdentification, bool approved, string note);

        [OperationContract]
        WFApprovals[] getApprovalInstances(string MailAddress);

    }
}