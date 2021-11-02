using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IDAL.IDal;

namespace IBL
{
    namespace BO
    {
        public partial class BL : IBL
        {

            List<Drone> dronesMaintenance;

            public List<Drone> DronesMaintenance { get; set; }

            public BL()
            {
                IDAL.IDal dalObject;
            }
        }
    }

}
