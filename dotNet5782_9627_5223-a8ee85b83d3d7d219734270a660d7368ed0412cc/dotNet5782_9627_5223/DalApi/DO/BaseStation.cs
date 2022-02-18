using System;


namespace DO
{

    /// <summary>
    /// the struct BaseStation contains the following details: id, name, longitude, latitude,  number of chargeSlots.
    /// actually, these are all the basic details for creating a baseStation.
    /// </summary>

    public struct BaseStation
    {
        private int id;

        private int chargeSlots;

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
                    throw new IntIdException(value);
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
                            throw new StringException(value);
                        }
                    }
                }
                name = value;
            }
        }

        public Coordinate Longitude { get; set; }
        public Coordinate Latitude { get; set; }

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
                    throw new ChargeSlotsException(value);
                }

                chargeSlots = value;
            }
        }  
        public bool IsDeleted { get; set; }
        /// <summary>
        /// override ToString function.
        /// </summary>
        /// <returns>description of the BaseStation  object</returns>
        public override string ToString()
        {
            return  $"id: {Id} \n" +
                    $"name: {Name} \n" +
                    $"longitude: {Longitude} \n" +
                    $"latitude: {Latitude} \n" +
                    $"number of charge slots: {ChargeSlots}\n";
        }

            
    }

}

