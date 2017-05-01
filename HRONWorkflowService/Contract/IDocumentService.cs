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
    public interface IDocumentService
    {
        [OperationContract]
        EmplFiles addDocument(int EmployeeId, int DocumentId);
    }
}