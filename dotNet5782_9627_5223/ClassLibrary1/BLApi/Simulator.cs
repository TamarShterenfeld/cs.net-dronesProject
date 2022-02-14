using System;
using BO;
using System.Threading;
using static IBL.BL;
using System.Linq;
using System.ComponentModel;

namespace IBL
{
    internal class Simulator
    {
        const int timer = 1000;//a second is allocated for every pause stop.
        const double speed = 5000;//speed of 1,000 km per a second.
        public Simulator(IBL.BL bl, int droneId, Action refreshDisplay, Func<bool> checkStopping)
        {
                    
        }

        void func( object sender, EventArgs e)
        {

        }
    }
}
