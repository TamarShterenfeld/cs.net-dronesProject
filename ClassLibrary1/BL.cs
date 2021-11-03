using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IDAL.IDal;
using static IBL.BO.DataSource;

namespace IBL
{
    namespace BO
    {
        public partial class BL : IBL
        {

            List<ListDrone> dronesMaintenance;

            public List<ListDrone> DronesMaintenance { get; set; }

            public BL()
            {
                IDAL.IDal dalObject;
                for (int i = 0; i < DronesMaintenance.Count; i++)
                {
                   if(ParcelsList.FindIndex(item=> item.Id == DronesMaintenance[i].ParcelId) != -1)
                    {
                        Parcel parcel = ParcelsList.First(item => item.Id == DronesMaintenance[i].ParcelId);
                      //the associated parcel wasn't supplied.
                        if(parcel.Association == new DateTime(01 / 01 / 0001))
                        {
                            ListDrone listDrone = DronesMaintenance[i];
                            listDrone.Status = DroneStatuses.Shipment;

                            if(parcel.PickingUp != new DateTime(01/01/0001)&& parcel.Supplied == new DateTime(01 / 01 / 0001))
                            {
                                Customer customer = CustomersList.First(item => item.Id == parcel.SenderId);
                                listDrone.Location = customer.Location;
                            }
                        }
                    }
                }
            }
        }
    }

}
