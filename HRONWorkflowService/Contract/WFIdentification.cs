using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRONWorkflowService.Contract
{
    public class WFIdentification
    {
        public Guid WFID { get; set; }
        public string ActivityID { get; set; }
    }
}