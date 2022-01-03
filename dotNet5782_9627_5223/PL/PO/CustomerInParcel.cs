using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.PO
{
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
            return $"id: {Id}, " +
                    $"name: {Name}";
        }

    }
}
