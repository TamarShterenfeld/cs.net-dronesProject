using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;



namespace BO
{
    /// <summary>
    /// the class CustomerInParcel contains all the customerInParcel's needed details
    /// </summary>
    public class CustomerInParcel
    {
        private string id;
        private string name;
        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                if (value.Length != 9)
                {
                    throw new BLStringIdException(value);
                }
                foreach (char digit in value)
                {
                    if (!Char.IsDigit(digit))
                    {
                        throw new BLStringIdException(value);

                    }
                }
                id = value;
            }
        }
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

        /// <summary>
        /// a constructor with parameters
        /// </summary>
        /// <param name="id">CustomerInShipment's id</param>
        /// <param name="name">CustomerInShipment's name</param>
        public CustomerInParcel(string id, string name)
        {
            this.id = id; this.name = name;
            Id = id; Name = name; 
        }

        // default constructor
        public CustomerInParcel() { }

        /// <summary>
        /// override ToString function.
        /// </summary>
        /// <returns>description of CustomerInShipment objectreturns>
        public override string ToString()
        {
            return $"id: {Id} \n" +
                    $"name: {Name}";
        }
    }
    
}
