using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using HRONLib;
using Microsoft.Exchange.WebServices.Data;
using System.ComponentModel;

namespace HRONWorkflowService.Activities
{

    public sealed class SendEmail : AsyncCodeActivity
    {
        public InArgument<string> to { get; set; }
        public InArgument<string> cc { get; set; }
        public InArgument<string> subject { get; set; }
        public InArgument<string> body { get; set; }

        [DefaultValue(new string[0])]
        public InArgument<string[]> attachmentNames { get; set; }

        [DefaultValue(new byte[0])]
        public InArgument<byte[][]> attachmentBytes { get; set; }
        public OutArgument<string> result { get; set; }

        void _createEmail(string to, string subject, string body, string cc, string[] attachmentName, Byte[][] attachments)
        {
            ExchangeService exch = Exchange.create("***REMOVED***", "***REMOVED***");

            EmailMessage app = new EmailMessage(exch);
            app.ToRecipients.Add(to);
            if(cc!=null && cc!="")
                app.CcRecipients.Add(cc);
            app.Subject = subject;
            app.Body = new MessageBody(body);
            if (attachmentName != null && attachmentName.Length > 0 && attachments != null && attachments.Length == attachmentName.Length)
                for (int i = 0; i < attachmentName.Length; i++)
                    app.Attachments.AddFileAttachment(attachmentName[i], attachments[i]);

            app.Send();
        }

        protected override IAsyncResult BeginExecute(AsyncCodeActivityContext context, AsyncCallback callback, object state)
        {
            Action<string, string, string, string, string[], byte[][]> createEmailDelegate = new Action<string, string, string, string, string[], byte[][]>(_createEmail);
            context.UserState = createEmailDelegate;
            return createEmailDelegate.BeginInvoke(
                to.Get(context).ToString(),
                subject.Get(context).ToString(),
                body.Get(context),
                cc.Get(context),
                attachmentNames.Get(context),
                attachmentBytes.Get(context),
                callback,
                state);
        }

        protected override void EndExecute(AsyncCodeActivityContext context, IAsyncResult result)
        {
            Action<string, string, String> createEmailDelegate = (Action<string, string, string>)context.UserState;
            createEmailDelegate.EndInvoke(result);
        }
    }
}
