using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRONWorkflowService
{
    public class Exchange
    {
        public static ExchangeService create(string user, string password)
        {
            ExchangeService exch = new ExchangeService();
            exch.UseDefaultCredentials = false;
            exch.Credentials = new WebCredentials(user, password);
            //exch.AutodiscoverUrl(user);
            exch.Url = new Uri("https://mail.***REMOVED***/EWS/Exchange.asmx");

            return exch;
        }
    }
}