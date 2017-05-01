using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.Configuration;
using Microsoft.Exchange.WebServices.Data;

namespace HRONWorkflowService.Activities
{

    public sealed class createAppointment : AsyncCodeActivity<String>
    {
        // Aktivitätseingabeargument vom Typ "string" definieren
        public InArgument<string> Text { get; set; }
        public InArgument<string> to { get; set; }
        public InArgument<string> subject { get; set; }
        public InArgument<string> body { get; set; }
        public InArgument<DateTime> start { get; set; }
        public InArgument<DateTime> end { get; set; }
        public OutArgument<string> result { get; set; }

        protected override IAsyncResult BeginExecute(AsyncCodeActivityContext context, AsyncCallback callback, object state)
        {
            Func<string, string, DateTime, DateTime, String, String> createAppointmentDelegate = new Func<string, string, DateTime, DateTime, string, string>(_createAppointment);
            context.UserState = createAppointmentDelegate;
            return createAppointmentDelegate.BeginInvoke(
                to.Get(context).ToString(),
                subject.Get(context).ToString(),
                start.Get(context),
                end.Get(context),
                body.Get(context),
                callback, 
                state);

        }

        protected override string EndExecute(AsyncCodeActivityContext context, IAsyncResult result)
        {
            Func<string, string, DateTime, DateTime, String, String> createAppointmentDelegate = (Func<string, string, DateTime, DateTime, string, string>)context.UserState;
            return (string)createAppointmentDelegate.EndInvoke(result);
        }

        string _createAppointment(string to, string subject, DateTime start, DateTime end, string body)
        {
            string Username = ConfigurationManager.AppSettings["ExchangeUsername"].ToString();
            string Password = ConfigurationManager.AppSettings["ExchangePassword"].ToString();

            ExchangeService exch = Exchange.create(Username, Password);

            Appointment app = new Appointment(exch);
            app.RequiredAttendees.Add(to);
            app.Subject = subject;
            app.Start = start;
            app.End = end;
            app.Body = new MessageBody(body);

            app.Save(SendInvitationsMode.SendToNone);
            return app.Id.UniqueId;
        }
    }
}
