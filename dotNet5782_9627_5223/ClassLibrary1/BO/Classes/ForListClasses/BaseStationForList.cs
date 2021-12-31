using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO
{
    /// <summary>
    /// the class BaseStationForList contains all the baseStation's details
    /// that we want to show to the client.
    /// </summary>
    public  class BaseStationForList
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

        private int availableChargeSlots;

        public int AvailableChargeSlots
        {
            get
            {
                return availableChargeSlots;
            }
            set
            {
                if (value < 0)
                {
                    throw (new BLChargeSlotsException(value));
                }

                availableChargeSlots = value;
            }
        }

        private int caughtChargeSlots;

        public int CaughtChargeSlots
        {
            get
            {
                return caughtChargeSlots;
            }
            set
            {
                if (value < 0)
                {
                    throw (new BLChargeSlotsException(value));
                }

                caughtChargeSlots = value;
            }
        }

        /// <summary>
        /// a constructor with parameters
        /// </summary>
        /// <param name="id">base station's id</param>
        /// <param name="name">base station's name</param>
        /// <param name="availableChargeSlots">available charge slots in the base station</param>
        /// <param name="caughtChargeSlots">caught charge slots in the base station</param>
        public BaseStationForList(int id, string name,int availableChargeSlots, int caughtChargeSlots)
        {
            this.id = id; this.name = name; this.availableChargeSlots = availableChargeSlots; this.caughtChargeSlots = caughtChargeSlots;
            Id = id; Name = name; AvailableChargeSlots = availableChargeSlots; CaughtChargeSlots = availableChargeSlots;
        }

        /// <summary>
        /// default constructor
        /// </summary>
        public BaseStationForList() { }

        /// <summary>
        /// override ToString function.
        /// </summary>
        /// <returns>description of the BaseStationForList object</returns>
        public override string ToString()
        {
            return $"id: {Id} \n" +
                   $"name: {Name} \n" +
                   $"number of free charge slots: {availableChargeSlots}\n"+
                   $"number of caught charge slots: {caughtChargeSlots}";
        }
    }
}
