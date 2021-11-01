using System;
using System.Collections.Generic;
using System.Text;
using static DalObject.DataSource;
using static DalObject.DataSource.Config;
using static IDAL.DO.IDAL;
using IDAL.DO;
using System.Linq;
using static IDAL.DO.OverloadException;

namespace DalObject
{
    public partial class DalObject : IDAL.IDAL
    {

        /// <summary>
        /// The function checks the ids of a parcel
        /// </summary>
        /// <param name="id">parcel's id</param>
        /// <param name="senderId">parcel's sender id</param>
        /// <param name="targetId">parcel target id</param>
        /// <param name="droneId">parcel drone id</param>
        private static void chackingIdentitiesOfParcel(int id, string senderId, string targetId, int droneId)
        {
            if (ParcelsList.FindIndex(item => item.Id == id) != -1)
                throw new OverloadException("You try to add a parcel which is already exists!");
            //check if the other ids really exist in the appropriate lists.
            if (CustomersList.FindIndex(item => item.Id == senderId) == -1 || CustomersList.FindIndex(item => item.Id == targetId) == -1)
                throw new OverloadException("sender's id or target's id don't exist in the customers' list.");
            if (DronesList.FindIndex(item => item.Id == droneId) == -1)
                throw new OverloadException("drone's id doesn't exist in the drones' List.");
        }

        /// <summary>
        /// The function checks if the variable is int type.
        /// </summary>
        /// <param name="id">a int type variable</param>
        private static void inputIntValue(ref int id)
        {
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Id can contain only digits, Please try again!");
            }
        }

    }
}
