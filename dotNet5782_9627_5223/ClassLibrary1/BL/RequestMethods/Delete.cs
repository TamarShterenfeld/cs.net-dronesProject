using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    public partial class BL
    {
        public void Delete(BO.BaseStation station)
        {
            station.IsDeleted = true;
            dal.UpDate(ConvertBaseStationBOtODO(station), station.Id);
        }
        //public void Delete(BO.Drone drone)
        //{
        //    drone.IsDeleted = true;
        //    dal.UpDate(ConvertBoToDoDrone(drone), drone.Id);
        //}
        
    }
}
