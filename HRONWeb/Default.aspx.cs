using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRONWeb
{
    public partial class Default : System.Web.UI.Page
    {
        public ApprovalInstances.WFApprovals[] aprovals;
        protected void Page_Load(object sender, EventArgs e)
        {
            ApprovalInstances.ApprovalServiceClient cli = new ApprovalInstances.ApprovalServiceClient();
            aprovals = cli.getApprovalInstances(getEmail());

            repeater.DataSource = aprovals;
            repeater.DataBind();

            txtTitle.Text = "Approve Workflows";
            //aprovals[0].subject
        }

        public static string getEmail()
        {
            return getEmail(System.Web.HttpContext.Current.User.Identity.Name);
        }

        public static string getEmail(string account)
        {
            DirectorySearcher search = new DirectorySearcher();

            // specify the search filter
            string defaultDomain= ConfigurationManager.AppSettings["DefaultDomain"].ToString();
            search.Filter = "(&(objectClass=user)(sAMAccountName=" + account.ToLower().Replace(defaultDomain + "\\", "") + "))";

            // specify which property values to return in the search
            search.PropertiesToLoad.Add("givenName");   // first name
            search.PropertiesToLoad.Add("sn");          // last name
            search.PropertiesToLoad.Add("mail");        // smtp mail address

            // perform the search
            SearchResult result = search.FindOne();
            if(result==null)
                throw new Exception("Mail for User (" + account + ") not found.");

            return result.Properties["mail"][0].ToString();
        }
    }
}