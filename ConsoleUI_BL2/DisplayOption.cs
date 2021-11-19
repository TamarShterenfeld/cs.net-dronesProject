using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleUI_BL.Program;

namespace ConsoleUI_BL
{
    /// <summary>
    /// Navigates the display choice appropriate to the customer's choice.
    /// </summary>
    public class DisplayOption : ISubNavigate
    {
        int innerChoice;
        int parcelId = 0, droneId = 0, baseStationId = 0;
        string customerId = "";
        public void options(ref IBL.BL bl)
        {
            Console.WriteLine("Please enter: \n1- For a base station\n2- For a drone\n3- For a customer\n4- For a parcel");
            if (int.TryParse(Console.ReadLine(), out innerChoice))
            {
                switch (innerChoice)
                {
                    case (int)DisplayOptions.BaseStation:
                        {
                            Console.WriteLine("Please enter baseStationId");
                            InputIntValue(ref baseStationId);
                            bl.DisplayBaseStation(baseStationId);
                            break;
                        }
                    case (int)DisplayOptions.Drone:
                        {
                            Console.WriteLine("Please enter droneId");
                            InputIntValue(ref droneId);
                            bl.DisplayDrone(droneId);
                            break;
                        }
                    case (int)DisplayOptions.Customer:
                        {
                            Console.WriteLine("Please enter customerId");
                            InputStringId(ref customerId);
                            bl.DisplayCustomer(customerId);
                            break;
                        }
                    case (int)DisplayOptions.Parcel:
                        {
                            Console.WriteLine("Please enter parcelId");
                            InputIntValue(ref parcelId);
                            bl.DisplayParcel(parcelId);
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("ERROR! \nan unknown option.");
                            break;
                        }
                }
            }
            else Console.WriteLine("The display option must hold a numeric value!");
        }
    }

}
