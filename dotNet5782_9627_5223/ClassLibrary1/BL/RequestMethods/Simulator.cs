using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    sealed partial class BL
    {
        public void InvokeSimulator(int droneId, Action<object> refreshDisplay, Func<bool> checkStopping)
        {
            Simulator simulator = new(this , droneId, refreshDisplay, checkStopping);
        }
    }
}
