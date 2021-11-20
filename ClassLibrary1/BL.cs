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
        private IDal.IDal dal;
        private List<DroneToList> dronesToList;

        public List<DroneToList> Drones { get; set; }

        public BL()
        {
            dal = new DalObject.DalObject();
            dronesToList = new List<DroneToList>();
            var dalDrones = dal.GetDronesList();
            var parcelsList = dal.GetParcelsList();
            var CustomersList = dal.GetCustomersList();

            foreach (var dalDrone in dalDrones)
            {
                var drone = new DroneToList();
                drone.Id = dalDrone.Id;

                drones.Add(drone);
            }
            for (int i = 0; i < Drones.Count; i++)
            {
                if (dalDrones.Any(item => item.Id == Drones[i].ParcelId))
                {
                    //Parcel parcel = parcelsList.First(item => item.Id == Drones[i].ParcelId);
                    ////the associated parcel wasn't supplied.
                    //if (parcel.AssociationDate == new DateTime(01 / 01 / 0001))
                    //{
                    //    DroneToList listDrone = Drones[i];
                    //    listDrone.Status = DroneStatuses.Shipment;

                    //    //if (parcel.PickUpDate != new DateTime(01 / 01 / 0001) && parcel.SupplyDate == new DateTime(01 / 01 / 0001))
                    //    //{
                    //    //    Customer customer = CustomersList.First(item => item.Id == parcel.Sender);
                    //    //    listDrone.Location = customer.Location;
                    //    //}
                    //}
                }
            }
        }

    }


}
