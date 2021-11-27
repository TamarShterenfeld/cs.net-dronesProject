using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
using static ConsoleUI_BL.Program;
using static IBL.BO.WeightCategories;
using IBL;

namespace ConsoleUI_BL
{
    /// <summary>
    /// Navigates the add choice appropriate to the user's choice.
    /// </summary>
    public class AddOption : ISubNavigate
    {
        /// <inheritdoc />
        public void Options(ref BL bl)
        {
            Console.WriteLine("Please enter : \n1- For Base Station \n2- For Drone\n3- For Customer\n4- For Parcel ");
            if (int.TryParse(Console.ReadLine(), out int innerChoice))
            {
                switch (innerChoice)
                {
                    case (int)AddOptions.BaseStation:
                        {
                            BaseStation baseStation = new();
                           (baseStation.Id, baseStation.Name, baseStation.Location, baseStation.ChargeSlots) = InputBaseStationDetails();
                            bl.Add(baseStation);
                            break;
                        }

                    case (int)AddOptions.Drone:
                        {
                            int baseStationId;
                            Drone drone = new();
                            (drone.Id, drone.Model, drone.MaxWeight) = InputDroneDetails();
                            baseStationId = InputIntValue();
                            bl.Add(drone, baseStationId);
                            break;
                        }
                    case (int)AddOptions.Customer:
                        {          
                            Customer customer = new();
                            (customer.Id, customer.Name, customer.Phone, customer.Location) = InputCustomerDetails();
                            bl.Add(customer);
                            break;
                        }
                    case (int)AddOptions.Parcel:
                        {
                            Parcel parcel = new();
                            CustomerInParcel sender = new();
                            CustomerInParcel target = new();
                            (sender.Id, target.Id, parcel.Weight, parcel.Priority)=InputParcelDetails();
                            bl.Add(parcel);
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
