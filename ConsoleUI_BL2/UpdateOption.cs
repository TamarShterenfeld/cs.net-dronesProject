using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI_BL
{/// <summary>
 /// Navigates the upDate choice appropriate to the customer's choice.
 /// </summary>
    public class UpDateOption : ISubNavigate
    {
        int innerChoice;
        int parcelId = 0, droneId = 0, baseStationId = 0;
        string senderId = "", targetId = "";
        public void options(ref IBL.BL bl)
        {
            Console.WriteLine("Please enter : \n1- For associating parcel\n2- For picking up parcel\n3- For supply parcel\n4- For charging drone\n5- For stop drone charging ");
            if (int.TryParse(Console.ReadLine(), out innerChoice))
            {
                switch (innerChoice)
                {
                    case (int)UpDateOptions.AssociatingParcel:
                        {
                            Console.WriteLine("Please enter the parcel's id and the drone's id");
                            InputAssociatedParcelDetails(ref parcelId, ref droneId);
                            AssociateParcel(parcelId, droneId);
                            break;
                        }
                    case (int)UpDateOptions.PickingUpParcel:
                        {
                            Console.WriteLine("Please enter the parcel's id and the sender's id");
                            InputPickUpParcelDetails(ref parcelId, ref senderId);
                            PickUpParcel(parcelId, senderId);
                            break;
                        }
                    case (int)UpDateOptions.SupplyingParcel:
                        {
                            Console.WriteLine("Please enter the parcel's id and the target's id");
                            InputArrivalDetails(ref parcelId, ref targetId);
                            SupplyParcel(parcelId, targetId);
                            break;
                        }
                    case (int)UpDateOptions.ChargingDrone:
                        {
                            Console.WriteLine("Please enter droneId and baseStationId");
                            ChargingDrone(droneId, baseStationId);
                            break;
                        }
                    case (int)UpDateOptions.StopDroneCharging:
                        {
                            Console.WriteLine("Please enter droneId and baseStationId");
                            StopDroneCharging(droneId);
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
