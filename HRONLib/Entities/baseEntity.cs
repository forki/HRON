using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;

namespace HRONLib
{
    public abstract class baseEntity
    {
        public baseEntity()
        {
            dateCreated = DateTime.Now;
            userCreated = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        }

        public abstract int[] getKey();

        public DateTime dateCreated { get; set; }

        public string userCreated { get; set; }

        public virtual T UnProxy<T>(DbContext context) where T : class
        {
            var proxyCreationEnabled = context.Configuration.ProxyCreationEnabled;
            try
            {
                context.Configuration.ProxyCreationEnabled = false;
                T poco = context.Entry(this).CurrentValues.ToObject() as T;
                return poco;
            }
            finally
            {
                context.Configuration.ProxyCreationEnabled = proxyCreationEnabled;
            }
        }
    }
}