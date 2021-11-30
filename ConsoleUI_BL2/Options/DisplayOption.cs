using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleUI_BL.Program;
using IBL.BO;
using IBL;
using DAL.DO;


namespace ConsoleUI_BL
{

    /// <summary>
    /// Navigates the display choice appropriate to the user's choice.
    /// </summary>
    public class DisplayOption : ISubNavigate
    {
        /// <inheritdoc />
        public void Options(ref BL bl)
        {
            int parcelId, droneId, baseStationId;
            string customerId;
            Console.WriteLine("Please enter: \n1- For a base station\n2- For a drone\n3- For a customer\n4- For a parcel");
            if (int.TryParse(Console.ReadLine(), out int innerChoice))
            {
                try
                {
                    switch (innerChoice)
                    {

                        case (int)DisplayOptions.BaseStation:
                            {
                                Console.WriteLine("Please enter baseStationId");
                                baseStationId = InputIntValue();
                                Console.WriteLine(bl.GetBLBaseStation(baseStationId));
                                break;
                            }
                        case (int)DisplayOptions.Drone:
                            {
                                Console.WriteLine("Please enter droneId");
                                droneId = InputIntValue();
                                Console.WriteLine(bl.GetBLDrone(droneId));
                                break;
                            }
                        case (int)DisplayOptions.Customer:
                            {
                                Console.WriteLine("Please enter customerId");
                                customerId = InputStringId();
                                Console.WriteLine(bl.GetBLCustomer(customerId));
                                break;
                            }
                        case (int)DisplayOptions.Parcel:
                            {
                                Console.WriteLine("Please enter parcelId");
                                parcelId = InputIntValue();
                                Console.WriteLine(bl.GetBLParcel(parcelId));
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("ERROR! \nan unknown option.");
                                break;
                            }
                    }
                }
                //while only getting data only IdExceptions may be thrown.
                catch (BLIntIdException exe)
                {
                    Console.WriteLine("the Id: " + exe.Id + " isn't valid in the BL logic level");

                }
                catch (BLStringIdException exe)
                {
                    Console.WriteLine("the Customer Id: " + exe.Id + " isn't valid in the BL logic level");
                }
                catch (IntIdException exe)
                {
                    Console.WriteLine("the Id: " + exe.Id + " isn't valid in the DAL logic level");
                }
                catch (StringIdException exe)
                {
                    Console.WriteLine("the Customer Id: " + exe.Id + " isn't valid in the DAL logic level");
                }
            }
            else Console.WriteLine("The display option must hold a numeric value!");
        }
    }

}
