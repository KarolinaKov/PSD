using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokus
{
    internal interface I_Publisher
    {
        void AddSubscriber(I_Observer observer);
        void RemoveSubscriber(I_Observer observer);
        void NotifySubscribers();
    }
}
