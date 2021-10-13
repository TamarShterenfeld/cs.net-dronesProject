using System;
using System.Collections.Generic;
using System.Text;
using static IDAL.DO.IDAL;
using IDAL.DO;

namespace DalObject
{

    public class DataSource
    {
        //const literals.
        const int DRONESAMOUNT = 10;
        const int BASESTATIONSAMOUNT = 5;
        const int CUSTOMERSAMOUNT = 100;
        const int PARCELAMOUNT = 1000;

        //internal arrs of different entities.
        internal static Drone[] DronesArr = new Drone[DRONESAMOUNT];
        internal static BaseStation[] BaseStationsArr = new BaseStation[BASESTATIONSAMOUNT];
        internal static Customer[] CustomersArr = new Customer[CUSTOMERSAMOUNT];
        internal static Parcel[] ParcelsArr = new Parcel[PARCELAMOUNT];
        public static Random rand = new Random();
        public static void Initialize()
        {
            int size;
            //initalize at least the two first item in BaseStationArr.
            size =  rand.Next(2, BASESTATIONSAMOUNT);
            for (int i = 0; i < size; i++)
            {
                BaseStationsArr[i] = new BaseStation();
            }

            //initalize at least the first five drones in DronesArr
            size = rand.Next(5, DRONESAMOUNT);
            int status;
           
            for (int i = 0; i<size; i++)
            {
                //initalize a status for each item in DronesArr.
                status = rand.Next(1, Enum.GetNames(typeof(DroneStatuses)).Length);
                DronesArr[i] = new Drone();
                DronesArr[i].Status = (DroneStatuses)Enum.GetNames(typeof(DroneStatuses)).GetValue(status);
            }

            //initalize at least the first tenth customers.
            size = rand.Next(10, CUSTOMERSAMOUNT);
            for(int i =0; i<size; i++)
            {
                CustomersArr[i] = new Customer();
            }

            //initalize at least the first tenth parcels.
            size = rand.Next(10, PARCELAMOUNT);
            int priority;
            for(int i =0;i<size; i++)
            {
                priority = rand.Next(1, Enum.GetNames(typeof(Priorities)).Length);
                ParcelsArr[i] = new Parcel();
                ParcelsArr[i].Priority = (Priorities)Enum.GetNames(typeof(Priorities)).GetValue(priority);
            }
        }

    }

    public class DalObject
    {

    }
}
