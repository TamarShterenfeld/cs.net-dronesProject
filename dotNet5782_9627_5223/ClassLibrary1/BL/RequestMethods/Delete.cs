using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace IBL
{
    public partial class BL
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Delete(BO.BaseStation station)
        {
            station.IsDeleted = true;
            dal.UpDate(ConvertBaseStationBOtODO(station), station.Id);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Delete(BO.Customer customer)
        {
            DO.Customer curCustomer = dal.GetCustomer(customer.Id);
            curCustomer.IsDeleted = true;
            dal.UpDate(curCustomer, customer.Id);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Delete(BO.Parcel parcel)
        {
            parcel.IsDeleted = true;
            dal.UpDate(ConvertBoToDoParcel(parcel), parcel.Id);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Delete(BO.Drone drone)
        {
            DO.Drone curDrone = dal.GetDrone(drone.Id);
            curDrone.IsDeleted = true;
            dal.UpDate(curDrone, drone.Id);
        }

    }
}
