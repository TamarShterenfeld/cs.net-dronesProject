using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace ConsoleUI_BL
{
    public interface IShowList
    {
        /// <summary>
        /// prints all the objects in the collection which contains BaseStation variables.
        /// </summary>
        /// <param name="baseStations"> a collection which contains values of BaseStation type </param>
        static void ShowList(IEnumerable<BaseStation> baseStations) { }

        /// <summary>
        /// prints all the objects in the collection which contains Drone variables.
        /// </summary>
        /// <param name="baseStations"> a collection which contains values of Drone type </param>
        static void  ShowList(IEnumerable<Drone> drones) { }

        /// <summary>
        /// prints all the objects in the collection which contains Parcel variables.
        /// </summary>
        /// <param name="baseStations"> a collection which contains values of Parcel type </param>
        static void ShowList(IEnumerable<Parcel> parcels) { }

        /// <summary>
        /// prints all the objects in the collection which contains Customer variables.
        /// </summary>
        /// <param name="baseStations"> a collection which contains values of Customer type </param>
        static void ShowList(IEnumerable<Customer> customers) { }

    }
}
