using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    sealed partial class BL
    {
        public void InvokeSimulator<T>(int droneId, Action<T> refreshDisplay, Func<bool> checkStopping)
        {
            Simulator<T> simulator = new(this , droneId, refreshDisplay, checkStopping);
        }
    }
}
