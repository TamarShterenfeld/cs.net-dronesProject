using System;
using System.Collections.Generic;
using System.Text;
using static DalObject.DataSource;
using IDAL.DO;
using System.Linq;
using static IDAL.DO.OverloadException;

namespace IBL
{
    public partial class BL : IBL
    {

        /// <summary>
        /// The function checks the ids of a parcel
        /// </summary>
        /// <param name="id">parcel's id</param>
        /// <param name="senderId">parcel's sender id</param>
        /// <param name="targetId">parcel target id</param>
        /// <param name="droneId">parcel drone id</param>
        private void chackingIdentitiesOfParcel(int id, string senderId, string targetId, int droneId)
        {
            List<Parcel> parcelsList = (List<Parcel>)dal.GetParcelsList();
            List<Customer> customersList = (List<Customer>)dal.GetCustomersList();
            List<Drone> dronesList = (List<Drone>)dal.GetDronesList();
            throw new OverloadException("You try to add a parcel which is already exists!");
            //check if the other ids really exist in the appropriate lists.
            if (customersList.FindIndex(item => item.Id == senderId) == -1 || customersList.FindIndex(item => item.Id == targetId) == -1)
                throw new OverloadException("sender's id or target's id don't exist in the customers' list.");
            if (dronesList.FindIndex(item => item.Id == droneId) == -1)
                throw new OverloadException("drone's id doesn't exist in the drones' List.");
        }

        /// <summary>
        /// The function checks if the variable is int type.
        /// </summary>
        /// <param name="id">a int type variable</param>
        private void inputIntValue(ref int id)
        {
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Id can contain only digits, Please try again!");
            }
        }

    }
}
