using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IDal.DO.BaseStation;
using IDal.DO;
using IDal;
using static DalObject.DalObject;
using static IBL.BL;


namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// the struct BaseStation contains the following details: id, name, longitude, latitude,  number of chargeSlots.
        /// </summary>

        public class BaseStation
        {
            private int id;
            public int Id
            {
                get
                {
                    return id;
                }
                set
                {
                    if (value < 0)
                    {
                        throw new OverloadException("Id must contain a positive number");
                    }
                    id = value;
                }
            }
            private string name;
            public string Name
            {
                get
                {
                    return name;
                }
                set
                {
                    foreach (char letter in value)
                    {
                        if (letter != ' ')
                        {
                            if (!Char.IsLetter(letter))
                            {
                                throw new OverloadException("Name can contain only letters.");
                            }
                        }
                    }
                    name = value;
                }
            }

            public Location MyLocation { get; set; }

            private int chargeSlots;

            public int ChargeSlots
            {
                get
                {
                    return chargeSlots;
                }
                set
                {
                    if (value < 0)
                    {
                        throw (new OverloadException("Not valid number of chargeSlots"));
                    }

                    chargeSlots = value;
                }
            }
            public List<DroneInCharging> DroneCharging { get; set; }


            ///// <summary>
            ///// constructor
            ///// </summary>
            ///// <param name="id"> BaseStation's id </param>
            ///// <param name="name"> BaseStation's name </param>
            ///// <param name="location"> BaseStation's location </param>
            ///// <param name="chargeSlots"> BaseStation's number of chargeSlots </param>
            ///// <param name="droneCharging"> BaseStation's droneInCharging </param>
            //public BaseStation(int id, string name, Location location, int chargeSlots, List<DroneInCharging> droneCharging)
            //{
            //    this.id = id; this.name = name; this.chargeSlots = chargeSlots;
            //    Id = id; Name = name; MyLocation = location; ChargeSlots = chargeSlots; DroneCharging = droneCharging;
            //}

            // default constructor
            public BaseStation() { }

            public BaseStation(IDal.DO.BaseStation baseStation)
            {
                this.Id = baseStation.Id;
                this.Name = baseStation.Name;
                this.MyLocation = new Location(this.MyLocation.CoorLongitude, this.MyLocation.CoorLatitude);
                this.ChargeSlots = baseStation.ChargeSlots - AvailableChargingSlots(this.Id);
                this.DroneCharging = (List<DroneInCharging>)GetDronesInMe(this.Id);
            }
            /// <summary>
            /// collect the details about the drones in charging
            /// </summary>
            /// <returns> the details about the drones in charging </returns>
            private string droneInChargingDetails()
            {
                string dronesDetails = "";
                foreach (DroneInCharging drone in DroneCharging)
                {
                    dronesDetails += drone.ToString();
                }
                return dronesDetails;
            }


            /// <summary>
            /// override ToString function.
            /// </summary>
            /// <returns>description of the BaseStation object</returns>
            public override string ToString()
            {
                return $"id: {Id} \n" +
                        $"name: {Name} \n" +
                        $"location: { MyLocation }\n" +
                        $"number of charge slots: {ChargeSlots}\n"
                        + $"drones in charging: {droneInChargingDetails()}\n";
            }


        }
    }
}
