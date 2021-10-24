using System;
using System.Collections.Generic;
using System.Text;
using static DalObject.DataSource;
using static DalObject.DataSource.Config;
using static IDAL.DO.IDAL;
using IDAL.DO;
using System.Linq;

namespace DalObject
{
    public partial class DalObject
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
                throw new Exception("You try to add a parcel which is already exists!");
            //check if the other ids really exist in the appropriate lists.
            CustomersList.First(item => item.Id == senderId);
            CustomersList.First(item => item.Id == targetId);
            DronesList.First(item => item.Id == droneId);
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
