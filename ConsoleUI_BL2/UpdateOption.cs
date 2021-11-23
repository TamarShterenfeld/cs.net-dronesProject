using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleUI_BL.Program;
using IBL;

namespace ConsoleUI_BL
{/// <summary>
 /// Navigates the upDate choice appropriate to the customer's choice.
 /// </summary>
    public class UpDateOption : ISubNavigate
    {

        /// <inheritdoc />
        public void Options(ref BL bl)
        {
            int droneId = 0;
            Console.WriteLine("Please enter : \n1- For associating parcel\n2- For picking up parcel\n3- For supply parcel\n4- For charging drone\n5- For stop drone charging ");
            if (int.TryParse(Console.ReadLine(), out int innerChoice))
            {
                switch (innerChoice)
                {
                    case (int)UpDateOptions.AssociationParcel:
                        {
                            Console.WriteLine("Please enter the drone id");
                            droneId = InputIntValue( );
                            bl.AssociateParcel(droneId);
                            break;
                        }
                    case (int)UpDateOptions.PickUpParcel:
                        {
                            Console.WriteLine("Please enter the drone id");
                            droneId = InputIntValue( );
                            bl.PickUpParcel(droneId);
                            break;
                        }
                    case (int)UpDateOptions.SupplyParcel:
                        {
                            Console.WriteLine("Please enter the drone id");
                            droneId = InputIntValue( );
                            bl.SupplyParcel(droneId );
                            break;
                        }
                    case (int)UpDateOptions.ChargeDrone:
                        {
                            Console.WriteLine("Please enter droneId ");
                            droneId = InputIntValue();
                            bl.ChargeDrone(droneId);
                            break;
                        }
                    case (int)UpDateOptions.StopDroneCharge:
                        {
                            Console.WriteLine("Please enter droneId and baseStationId");
                            bl.ReleaseDroneFromRecharge(droneId);
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("ERROR! \nan unknown option");
                            break;
                        }
                }
            }
            else Console.WriteLine("The update option must hold a numeric value!");
        }
    }

}
