using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI_BL
{
    /// <summary>
    /// Navigates the add choice appropriate to the user's choice.
    /// </summary>
    public class AddOption : ISubNavigate
    {
        int innerChoice;
        int id, droneId, baseStationId;
        string name, customerId, phone, model, senderId, targetId;
        double battery, lati, longi;
        int chrgeSlots;
        //all the enum type litteral are entered as string type
        //and then checked if they contain an enum name.
        string maxWeight = "", weight = "", priority = "";
        public void options(ref IBL.BL bl)
        {

            Console.WriteLine("Please enter : \n1- For Base Station \n2- For Drone\n3- For Customer\n4- For Parcel ");
            if (int.TryParse(Console.ReadLine(), out innerChoice))
            {
                switch (innerChoice)
                {
                    case (int)AddOptions.BaseStation:
                        {
                            CheckBaseStationDetails(out id, out name, out location, out chrgeSlots);
                            bl.BaseStation baseStation = new BO.BaseStation(id, name, location, chrgeSlots, null);
                            bl.Add(baseStation);
                            break;
                        }

                    case (int)AddOptions.Drone:
                        {
                            CheckDroneDetails(out id, out model, out maxWeight, out baseStationId);
                            bl.AddDrone(id, maxWeight, baseStationId);
                            break;
                        }
                    case (int)AddOptions.Customer:
                        {
                            CheckCustomerDetails(out customerId, out name, out phone, out longitude, out latitude);
                            AddCustomer(customerId, name, phone, longitude, latitude);
                            break;
                        }
                    case (int)AddOptions.Parcel:
                        {
                            CheckParcelDetails(out id, out senderId, out targetId, out weight, out priority);
                            AddParcel(id, senderId, targetId, droneId, weight, priority);
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("ERROR! \nan unknown option");
                            break;
                        }
                }
            }
            else Console.WriteLine("The add option must hold a numeric value!");
        }
    }
}
