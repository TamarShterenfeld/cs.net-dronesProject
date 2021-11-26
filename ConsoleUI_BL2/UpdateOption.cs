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
            int id = 0;
            string name, num, customerId, model;
            Console.WriteLine("Please enter : \n1- For updating a drone \n2- For updating a basetation \n3- For updating a customer \n4- For charging drone \n5- For stop drone charging \n6- For associating parcel \n7- For picking up parcel \n8- For supply parcel \n");
            if (int.TryParse(Console.ReadLine(), out int innerChoice))
            {
                switch (innerChoice)
                {
                    case (int)UpDateOptions.UpdateDrone:
                        {
                            Console.WriteLine("Please enter droneId and a new model for the drone");
                            id = InputIntValue();
                            model = InputOptionalStringValue();
                            bl.UpdateDrone(id, model);
                            break;
                        }
                    case (int)UpDateOptions.UpdateBaseStation:
                        {
                            Console.WriteLine("Please enter baseStationId and one or more of the following details: name, number of charge slots");
                            id = InputIntValue();
                            name = InputOptionalStringValue();
                            num = InputOptionalIntValue();
                            bl.UpdateBaseStation(id, name, num);
                            break;
                        }
                    case (int)UpDateOptions.UpdateCustomer:
                        {
                            Console.WriteLine("Please enter customerId and one or more of the following details: name, phone");
                            customerId = InputStringId();
                            name = InputOptionalStringValue();
                            num = InputOptionalIntValue();
                            bl.UpdateCustomer(customerId, name,num);
                            break;
                        }
                   case (int)UpDateOptions.ChargeDrone:
                        {
                            Console.WriteLine("Please enter droneId ");
                            id = InputIntValue();
                            bl.ChargeDrone(id);
                            break;
                        }
                    case (int)UpDateOptions.StopDroneCharge:
                        {
                            Console.WriteLine("Please enter droneId and baseStationId");
                            bl.ReleaseDroneFromRecharge(id);
                            break;
                        }
                    case (int)UpDateOptions.AssociationParcel:
                        {
                            Console.WriteLine("Please enter the drone id");
                            id = InputIntValue( );
                            bl.AssociateParcel(id);
                            break;
                        }
                    case (int)UpDateOptions.PickUpParcel:
                        {
                            Console.WriteLine("Please enter the drone id");
                            id = InputIntValue( );
                            bl.PickUpParcel(id);
                            break;
                        }
                    case (int)UpDateOptions.SupplyParcel:
                        {
                            Console.WriteLine("Please enter the drone id");
                            id = InputIntValue( );
                            bl.SupplyParcel(id );
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
