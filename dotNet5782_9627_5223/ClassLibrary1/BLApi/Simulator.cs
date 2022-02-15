using System;
using BO;
using System.Threading;
using DalApi;
using System.Linq;
using static System.Math;
using DO;

namespace IBL
{
    internal class Simulator
    {
        enum Maintenance { Starting, Going, Charging }
        enum BatteryUsage { Available, Light, Medium, Heavy, Charging }
        private const double VELOCITY = 1.0;
        private const int DELAY = 500;
        private const double TIME_STEP = DELAY / 1000.0;
        private const double STEP = VELOCITY / TIME_STEP;

        public Simulator(BLApi.IBL Bl, int droneId, Action updateDrone, Func<bool> checkStop)
        {
            BL bl = Bl as BL;
            var dal = bl.dal;
            var drone = bl.GetDroneForList(droneId);
            int? parcelId = null;
            int? baseStationId = null;
            BO.BaseStation station = null;
            double distance = 0.0;
            int batteryUsage = 0;
            DO.Parcel? parcel = null;
            bool pickedUp = false;
            BO.Customer customer = null;
            Maintenance maintenance = Maintenance.Starting;

            void initDelivery(int id)
            {
                parcel = dal.GetParcel(id);
                batteryUsage = (int)Enum.Parse(typeof(BatteryUsage), parcel?.Weight.ToString());
                pickedUp = parcel?.PickUpDate is not null;
                customer = bl.GetBLCustomer((pickedUp ? parcel?.TargetId : parcel?.SenderId));
            }

            do
            {
                //(var next, var id) = drone.nextAction(bl);

                switch (drone)
                {
                    case DroneForList { Status: DroneStatuses.Available }:
                        if (!sleepDelayTime()) break;

                        lock (bl)
                        {
                            try 
                            { 
                                lock(bl)
                                {
                                    bl.AssociateParcel(droneId);
                                    parcelId = bl.GetBLDrone(droneId).Parcel?.Id;
                                    switch (parcelId, drone.Battery)
                                    {
                                        case (null, 1.0):
                                            break;

                                        case (null, _):
                                            if (baseStationId != null)
                                            {
                                                maintenance = Maintenance.Starting;
                                            }
                                            break;
                                    }
                                }
                            } catch (ParcelActionsException ex){ } catch(DroneStatusException ex){} catch(Exception ex){}
                        }
                        break;

                    case DroneForList { Status: DroneStatuses.Maintenance }:
                        switch (maintenance)
                        {
                            case Maintenance.Starting:
                                lock (bl)
                                {
                                    try { station = bl.GetBLBaseStation(dal.GetDroneChargeBaseStationId(droneId));  }
                                    catch (IntIdException ex) { throw new IntIdException("Internal error base station", ex); }
                                    distance = drone.Distance(station);
                                    maintenance = Maintenance.Going;
                                }
                                break;

                            case Maintenance.Going:
                                if (distance < 0.01)
                                    lock (bl)
                                    {
                                        drone.Location = station.Location;
                                        maintenance = Maintenance.Charging;
                                    }
                                else
                                {
                                    if (!sleepDelayTime()) break;
                                    lock (bl)
                                    {
                                        double delta = distance < STEP ? distance : STEP;
                                        distance -= delta;
                                        drone.Battery = Max(0.0, drone.Battery - delta * bl.BatteryUsages[BL.DRONE_FREE]);
                                    }
                                }
                                break;

                            case Maintenance.Charging:
                                if (drone.Battery == 1.0)
                                    lock (bl)
                                    {
                                        drone.Status = DroneStatuses.Available;
                                        lock(dal)
                                        {
                                            dal.ReleaseDroneFromRecharge(droneId);
                                        }
                                    }
                                else
                                {
                                    if (!sleepDelayTime()) break;
                                    lock (bl) drone.Battery = Min(1.0, drone.Battery + bl.BatteryUsages[BL.DRONE_CHARGE] * TIME_STEP);
                                }
                                break;
                            default:
                                throw new Exception("Internal error: wrong maintenance substate");
                        }
                        break;

                    case DroneForList { Status: DroneStatuses.Shipment }:
                        lock (bl)
                        {
                            try { if (parcelId == null) initDelivery(drone.ParcelId); }
                            catch (IntIdException ex) { throw new IntIdException("Internal error getting parcel", ex); }
                            distance = drone.Distance(customer);
                        }

                        if (distance < 0.01 || drone.Battery == 0.0)
                            lock (bl)
                            {
                                drone.Location = customer.Location;
                                if (pickedUp)
                                {
                                    bl.SupplyParcel((int)parcel?.Id);
                                    drone.Status = DroneStatuses.Available;
                                }
                                else
                                {
                                    bl.PickUpParcel((int)parcel?.Id);
                                    customer = bl.GetBLCustomer(parcel?.TargetId);
                                    pickedUp = true;
                                }
                            }
                        else
                        {
                            if (!sleepDelayTime()) break;
                            lock (bl)
                            {
                                double delta = distance < STEP ? distance : STEP;
                                double proportion = delta / distance;
                                drone.Battery = Max(0.0, drone.Battery - delta * bl.BatteryUsages[pickedUp ? batteryUsage : BL.DRONE_FREE]);
                                double lat = drone.Location.CoorLatitude.InputCoorValue + (customer.Location.CoorLatitude.InputCoorValue - drone.Location.CoorLatitude.InputCoorValue) * proportion;
                                double lon = drone.Location.CoorLongitude.InputCoorValue + (customer.Location.CoorLongitude.InputCoorValue - drone.Location.CoorLongitude.InputCoorValue) * proportion;
                                drone.Location = new(new BO.Coordinate(lon,BO.Locations.Longitude),new BO.Coordinate(lat,BO.Locations.Latitude));
                            }
                        }
                        break;

                    default:
                        throw new Exception("Internal error: not available after Delivery...");

                }
                bl.UpdateDronesForSimulator(drone);
                updateDrone();
            } while (!checkStop());
        }

        private static bool sleepDelayTime()
        {
            try { Thread.Sleep(DELAY); } catch (ThreadInterruptedException) { return false; }
            return true;
        }
    }
}

