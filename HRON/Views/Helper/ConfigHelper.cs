using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRON.Views.Helper
{
    public class ConfigHelper
    {
        static HRONLib.HRONEntities ent = new HRONLib.HRONEntities();

        public static String getString(String target, String key)
        {
            HRONLib.Config c = get(target, key);
            if (c != null)
                return c.value;
            return null;
        }
        public static bool? getBool(String target, String key)
        {
            HRONLib.Config c = get(target, key);
            if (c != null)
                return c.valueBool;
            return null;
        }
        public static decimal? getDecimal(String target, String key)
        {
            HRONLib.Config c = get(target, key);
            if (c != null)
                return c.valueDecimal;
            return null;
        }

        private static HRONLib.Config get(String target, string key)
        {
            HRONLib.Config c = ent.Config.Where(e => e.target == target && e.key == key).FirstOrDefault();
            if (c != null)
                return c;
            else
            {
                HRONLib.Config x = new HRONLib.Config();
                x.key = key;
                x.target = target;
                ent.Config.Add(x);

                ent.SaveChangesAsync();

                return null;
            }
        }

        private static void set(String target, String key, String vS, decimal? vD, bool? vB)
        {
            HRONLib.Config c = ent.Config.Where(e => e.target == target && e.key == key).FirstOrDefault();
            if (c != null)
            {
                c.value = vS;
                c.valueBool = vB;
                c.valueDecimal = vD;
            }
            else
            {
                HRONLib.Config x = new HRONLib.Config();
                x.key = key;
                x.target = target;
                ent.Config.Add(x);
                c.value = vS;
                c.valueBool = vB;
                c.valueDecimal = vD;
            }
            ent.SaveChangesAsync();

        }
        public static void set(String target, String key, String value)
        {
            set(target, key, value, null, null);
        }

        public static void set(String target, String key, bool value)
        {
            set(target, key, null, null, value);
        }
        public static void set(String target, String key, decimal value)
        {
            set(target, key, null, value, null);
        }
    }
}
