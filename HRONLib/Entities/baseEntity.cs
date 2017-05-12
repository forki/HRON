using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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

        [SkipTracking]
        public DateTime dateCreated { get; set; }

        [SkipTracking]
        public string userCreated { get; set; }

        [SkipTracking]
        public DateTime dateModified { get; set; }

        [SkipTracking]
        public string userModified { get; set; }

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

        public bool Equals(baseEntity e)
        {
            int[] keys = getKey();
            int[] keys2 = e.getKey();
            if (keys.Length != keys2.Length)
                return false;
            for (int i = 0; i < keys.Length; i++)
                if (keys[i] != keys2[i])
                    return false;
            return true;
        }

        public void onUpdate()
        {
            dateModified = DateTime.Now;
            userModified = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        }
        public void onInsert()
        {
            dateCreated = DateTime.Now;
            userCreated = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            onUpdate();
        }
    }
}