using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;


namespace IBL
{
   
        public partial class BL : IBL
        {
            private IDAL.IDal dal;
            private List<DroneToList> drones;

            public List<DroneToList> Drones { get; set; }

            public BL()
            {
                dal = new DalObject.DalObject();
                drones = new List<DroneToList>();
                for (int i = 0; i < Drones.Count; i++)
                {
                   if(ParcelsList.FindIndex(item=> item.Id == Drones[i].ParcelId) != -1)
                    {
                        Parcel parcel = ParcelsList.First(item => item.Id == Drones[i].ParcelId);
                      //the associated parcel wasn't supplied.
                        if(parcel.Association == new DateTime(01 / 01 / 0001))
                        {
                            DroneToList listDrone = Drones[i];
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

        //
    }
    

}
