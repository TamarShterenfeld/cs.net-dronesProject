using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.PO
{
    public partial class POConverter
    {
        public static PO.Coordinate CoordinateBoToPo(BO.Coordinate coor)
        {
            return new PO.Coordinate() { InputCoorValue = coor.InputCoorValue, Degrees = coor.Degrees, Direction = (Directions)coor.Direction, MyLocation = (Locations)coor.MyLocation, Minutes = coor.Minutes, Seconds = coor.Seconds };
        }

        public static BO.Coordinate CoordinatePoToBo(PO.Coordinate coor)
        {
            return new BO.Coordinate() { InputCoorValue = coor.InputCoorValue, Degrees = coor.Degrees, Direction = (BO.Directions)coor.Direction, MyLocation = (BO.Locations)coor.MyLocation, Minutes = coor.Minutes, Seconds = coor.Seconds };
        }

        public static PO.DroneInCharging DroneInChargingBoToPo(BO.DroneInCharging drone)
        {
            return new PO.DroneInCharging() { Id = drone.Id, Battery = drone.Battery };
        }

        public static BO.DroneInCharging DroneInChargingPoToBo(PO.DroneInCharging drone)
        {
            return new BO.DroneInCharging() { Id = drone.Id, Battery = drone.Battery };
        }

        public static IEnumerable<PO.DroneInCharging> DroneInChargingListBoToPo(IEnumerable<BO.DroneInCharging> droneList)
        {
            List<PO.DroneInCharging> boDroneList = new();
            foreach (var drone in droneList)
            {
                boDroneList.Add(DroneInChargingBoToPo(drone));
            }
            return boDroneList;
        }

        public static BO.BaseStation StationPoToBo(PO.Station station)
        {
            return new BO.BaseStation()
            {
                Id = station.Id,
                Name = station.Name,
                Location = LocationPOTOBO(station.Location),
                ChargeSlots = station.ChargeSlots,
                DroneCharging = DroneInChargingListPoToBo(station.DroneCharging).ToList()
            };
        }
        public static IEnumerable<BO.DroneInCharging> DroneInChargingListPoToBo(IEnumerable<PO.DroneInCharging> droneList)
        {
            if (droneList == null)
                return Enumerable.Empty<BO.DroneInCharging>();
            else
                return droneList.Select(drone => DroneInChargingPoToBo(drone));
        }

        public static IEnumerable<PO.DroneInCharging> DroneInChargingBOToPO(IEnumerable<BO.DroneInCharging> inCharging)
        {
            var PoInCharging = from drone in inCharging
                               select new PO.DroneInCharging() { Battery = drone.Battery, Id = drone.Id };
            return PoInCharging;
        }

        public static PO.Location LocationBOTOPO(BO.Location location)
        {
            return new PO.Location(CoordinateBoToPo(location.CoorLongitude), CoordinateBoToPo(location.CoorLatitude));
        }

        public static BO.Location LocationPOTOBO(PO.Location location)
        {
            return new BO.Location(CoordinatePoToBo(location.CoorLongitude), CoordinatePoToBo(location.CoorLatitude));
        }

        public static PO.ParcelInPassing ParcelInPassingBOTOPO(BO.ParcelInPassing parcel)
        {
            string toDest = parcel.ToDestination ? "yes" : "no"; 
            return new PO.ParcelInPassing(parcel.Id, toDest, parcel.Priority,
                parcel.Weight, parcel.Sender, parcel.Target, parcel.Collect, parcel.Destination, parcel.Distatnce);
        }
        public static PO.CustomerInParcel CustomerInParcelBOTOPO(BO.CustomerInParcel customer)
        {
            return new PO.CustomerInParcel(customer.Id, customer.Name);
        }

        /// <summary>
        /// convert ParcelInCustomer object from PO to BO.
        /// </summary>
        /// <param name="parcel">BO.ParcelInCustomer</param>
        /// <returns>PO.ParcelInCustomer</returns>
        public static PO.ParcelInCustomer ParcelInCustomerBOToPO(BO.ParcelInCustomer parcel)
        {
            return new PO.ParcelInCustomer(parcel.Id, (PO.POConverter.WeightCategories)parcel.Weight, (PO.POConverter.Priorities)parcel.Priority, (PO.POConverter.ParcelStatuses)parcel.ParcelStatus, CustomerInParcelBOTOPO(parcel.SourceOrDest));
        }

        /// <summary>
        /// convert ParcelInCustomer IEnumerable from PO to BO.
        /// </summary>
        /// <param name="parcels">BO.ParcelInCustomer IEnumerable</param>
        /// <returns>PO.ParcelInCustomer IEnumerable</returns>
        public static IEnumerable<PO.ParcelInCustomer> ParcelInCustomerListBOToPO(IEnumerable<BO.ParcelInCustomer> parcels)
        {
            if (parcels == null)
                return Enumerable.Empty<PO.ParcelInCustomer>();
            else
                return parcels.Select(parcel => ParcelInCustomerBOToPO(parcel));
        }

    }
}
