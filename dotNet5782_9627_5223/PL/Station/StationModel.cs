using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.PO
{
    public class StationModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int id;
        Location location = new();
        public int Id
        {
            get=> id;
            set
            {
                id = value;
                PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(nameof(Id)));
            }
        }
        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
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
            get => chargeSlots;
            set
            {
                chargeSlots = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ChargeSlots)));
            }
        }
        public List<DroneInCharging> DroneCharging { get; set; }


        /// <summary>
        /// a constructor with parameters
        /// </summary>
        /// <param name="id"> BaseStation's id </param>
        /// <param name="name"> BaseStation's name </param>
        /// <param name="location"> BaseStation's location </param>
        /// <param name="chargeSlots"> BaseStation's number of chargeSlots </param>
        /// <param name="droneCharging"> BaseStation's droneInCharging </param>
        public StationModel(int id, string name, Location location, int chargeSlots, List<DroneInCharging> droneCharging)
        {
            Id = id; Name = name; Location = location; ChargeSlots = chargeSlots; DroneCharging = droneCharging;
        }

        // default constructor
        public StationModel() { }


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

    }
}
