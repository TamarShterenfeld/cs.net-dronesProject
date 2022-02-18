using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace ConsoleUI_BL
{
   /// <summary>
   /// this interface declares of some functions which are implemented by ShowList class.
   /// </summary>
    public interface IShowList
    {
        /// <summary>
        /// prints all the objects in the collection which contains BaseStation variables.
        /// </summary>
        /// <param name="baseStations"> a collection which contains values of BaseStation type </param>
        void ShowList(IEnumerable<BaseStationForList> baseStations) { }

        /// <summary>
        /// prints all the objects in the collection which contains Drone variables.
        /// </summary>
        /// <param name="baseStations"> a collection which contains values of Drone type </param>
        void  ShowList(IEnumerable<DroneForList> drones) { }

        /// <summary>
        /// prints all the objects in the collection which contains Parcel variables.
        /// </summary>
        /// <param name="baseStations"> a collection which contains values of Parcel type </param>
        void ShowList(IEnumerable<ParcelForList> parcels) { }

        /// <summary>
        /// prints all the objects in the collection which contains Customer variables.
        /// </summary>
        /// <param name="baseStations"> a collection which contains values of Customer type </param>
        void ShowList(IEnumerable<CustomerForList> customers) { }

    }
}
