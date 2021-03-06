using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BL;


namespace BO
{ 
        /// <summary>
        /// the struct BaseStation contains the following details: id, name, longitude, latitude,  number of chargeSlots.
        /// actaully, these are the needed details for creating a base station.
        /// </summary>

    public class BaseStation : ILocatable
    {
        private int id;
        Location location = new();
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
                    throw new BLIntIdException(value);
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
                            throw new BLStringException(value);
                        }
                    }
                }
                name = value;
            }
        }

        public Location Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
            }
        }

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
                    throw new BLChargeSlotsException(value);
                }

                chargeSlots = value;
            }
        }
        public List<DroneInCharging> DroneCharging { get; set; } = new();
        public bool IsDeleted { get; set; }

        /// <summary>
        /// a constructor with parameters
        /// </summary>
        /// <param name="id"> BaseStation's id </param>
        /// <param name="name"> BaseStation's name </param>
        /// <param name="location"> BaseStation's location </param>
        /// <param name="chargeSlots"> BaseStation's number of chargeSlots </param>
        /// <param name="droneCharging"> BaseStation's droneInCharging </param>
        public BaseStation(int id, string name, Location location, int chargeSlots, List<DroneInCharging> droneCharging)
        {
            Id = id; Name = name; Location = location; ChargeSlots = chargeSlots; DroneCharging = droneCharging;
        }

        // default constructor
        public BaseStation() { }

            
        /// <summary>
        /// collect the details about the drones in charging
        /// </summary>
        /// <returns> the details about the drones in charging </returns>
        private string DroneInChargingDetails()
        {
            string dronesDetails = "";
            if (DroneCharging != null)
            {
                foreach (DroneInCharging drone in DroneCharging)
                {
                    dronesDetails += drone.ToString();
                    dronesDetails += "\n";
                }                   
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
                    $"location: { Location }\n" +
                    $"number of charge slots: {ChargeSlots}\n"
                    + $"drones in charging:\n{DroneInChargingDetails()}";
        }

    }
    
}
