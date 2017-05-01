using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HRON.Views.Helper
{
    public interface DetailControl
    {
        void LoadID(int d);
        void New();
    }
}
