using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pokus
{
    internal interface I_Observer
    {
        void Observe(List<List<int>> SeznamSeznamu);
      
    }
}
