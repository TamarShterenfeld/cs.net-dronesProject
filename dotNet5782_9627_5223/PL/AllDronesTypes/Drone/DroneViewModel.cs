using BO;
using DO;
using PL.PO;
using static PL.PO.POConverter;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using System.Linq;
using System.ComponentModel;
using static PL.Validation;

namespace PL
{
    public class DroneViewModel : INotifyPropertyChanged
    {
        #region PrivateFields
        BLApi.IBL bl;
        PO.UserStage stage;
        public PO.UserStage Stage { get => stage; set { stage = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Stage))); } }
        object coorLon, coorLat;
        IList<string> nullString = new List<string>() { "" };
        IList<string> statuses = Enum.GetNames(typeof(POConverter.DroneStatuses));
        IList<string> weights = Enum.GetNames(typeof(POConverter.WeightCategories));
        IList<string> droneActions = Enum.GetNames(typeof(DroneActions));
        string selectedDroneAction, selectedStatus, selectedWeight, selectedModel;
        //double timeCharge;
        bool visibleTimeCharging, enableUpdate;
        bool inSimulator;
        PO.BaseStationForList station;
        BackgroundWorker worker;

        #endregion

        #region Properties
        //public bool VisibleTimeCharging
        //{
        //    get => visibleTimeCharging;
        //    set
        //    {
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VisibleTimeCharging)));
        //        visibleTimeCharging = value;
        //    }
        //}
        public PO.Drone Drone { get; set; }
        public bool EnableUpdate
        {
            get => enableUpdate;
            set
            {
                enableUpdate = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EnableUpdate)));
            }
        }
        public bool InSimulator
        {
            get => inSimulator;
            set
            {
                inSimulator = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(InSimulator)));
            }
        }
        public PO.BaseStationForList Station
        {
            get => station;
            set
            {
                station = value;
                initLocation();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Station)));
            }
        }
        void initLocation()
        {
            PO.Coordinate longitude = CoordinateBoToPo(bl.GetBLBaseStation(Station.Id).Location.CoorLongitude);
            PO.Coordinate latitude = CoordinateBoToPo(bl.GetBLBaseStation(Station.Id).Location.CoorLatitude);
            Drone.Location = new(new(longitude.InputCoorValue, longitude.MyLocation), new(latitude.InputCoorValue, latitude.MyLocation));
        }
        public object CoorLon
        {
            get => coorLon;
            set
            {
                if (IsValidDouble(coorLon + ""))
                {
                    if (!IsValidLocation(coorLon + ""))
                    {
                        MessageBox.Show("Location must be in range of -90º to 90º");
                        return;
                    }
                    coorLon = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CoorLon)));
                    Drone.Location.CoorLongitude = new PO.Coordinate((double)value, POConverter.Locations.Longitude);
                }
                else
                {
                    MessageBox.Show("Location must be a double value type");
                }
            }
        }
        public object CoorLat
        {
            get => coorLat;
            set
            {
                if (double.TryParse(value.ToString(), out double latitude))
                {
                    if (!IsValidLocation(latitude+""))
                    {
                        MessageBox.Show("Location must be in range of -90º to 90º");
                        return;
                    }
                    coorLat = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CoorLat)));
                    Drone.Location.CoorLatitude = new PO.Coordinate(latitude, POConverter.Locations.Latitude);
                }
                else
                {
                    MessageBox.Show("Location must be a double value type");
                }
            }
        }
        //public double TimeCharge
        //{
        //    get => timeCharge;
        //    set
        //    {
        //        timeCharge = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TimeCharge)));
        //        TimeDuration_Changed((object)value);
        //    }
        //}
        public string SelectedModel
        {
            get => selectedModel;
            set
            {
                selectedModel = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedModel)));
            }
        }
        public RelayCommand Cancel { get; set; }
        public RelayCommand AddOrUpdate { get; set; }
        public RelayCommand Delete { get; set; }
        public RelayCommand LeftDoubleClick { get; set; }
        public RelayCommand Simulator { get; set; }
        public RelayCommand Regular { get; set; }
        public ListCollectionView Weights { get; set; }
        public ListCollectionView Statuses { get; set; }
        public ListCollectionView MyDroneActions { get; set; }
        public ListCollectionView StationsId { get; set; }
        public string SelectedWeight { set { selectedWeight = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedWeight))); } get => selectedWeight; }
        public string SelectedStatus { set { selectedStatus = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedStatus))); } get => selectedStatus; }
        public string SelectedDroneAction
        {
            get => selectedDroneAction;
            set
            {
                selectedDroneAction = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(selectedDroneAction)));
                DroneAction_Selected(value);
            }

        }
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="bl">BL object</param>
        /// <param name="customer">CustomerForList object</param>
        public DroneViewModel(BLApi.IBL bl, PL.PO.DroneForList drone) : this(bl)
        {
            Drone = new PO.Drone(drone, bl);
            SelectedWeight = ((object)Drone.Weight).ToString();
            SelectedStatus = ((object)Drone.Status).ToString();
            SelectedModel = Drone.Model;
            AddOrUpdate = new(Button_ClickUpdate, null);
            Delete = new(Button_ClickDelete, null);
            Simulator = new(Button_Simulator, null);
            Regular = new(Button_Regular, null);
            EnableUpdate = false;
            //VisibleTimeCharging = false;
            coorLon = Drone.Location.CoorLongitude.ToString();
            coorLat = Drone.Location.CoorLatitude.ToString();
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="bl">BL object</param>
        public DroneViewModel(BLApi.IBL bl)
        {
            this.bl = bl;
            Weights = new ListCollectionView(nullString.Concat(weights).ToList());
            Statuses = new ListCollectionView(nullString.Concat(statuses).ToList());
            Drone = new();
            Drone.Status = POConverter.DroneStatuses.Available;
            Cancel = new(Button_ClickCancel, null);
            AddOrUpdate = new(Button_ClickAdd, null);
            EnableUpdate = true;
            //VisibleTimeCharging = false;
            SelectedStatus = nameof(POConverter.DroneStatuses.Available);
            LeftDoubleClick = new(doubleClickParcel, null);
            MyDroneActions = new ListCollectionView(nullString.Concat<string>(droneActions).ToList());
            StationsId = new ListCollectionView(ListOfStationForListBOToPO(bl.GetBaseStationList()).ToList()); 
            Station = (PO.BaseStationForList)StationsId.GetItemAt(0);
            initLocation();
        }

        #endregion

        #region Button_Events

        /// <summary>
        /// show full details of parcelInCustomer object
        /// </summary>
        /// <param name="sender">the event</param>
        private void doubleClickParcel(object sender)
        {
            new ParcelView(new ParcelViewModel(ParcelForListBOToPO(bl.GetParcelForList(Drone.Id)), bl)).Show();
        }

        /// <summary>
        /// the function treats the event of clicking on the button 'Cancel'.
        /// </summary>
        /// <param name="sender">the invoking object</param>
        /// <param name="e">the event</param>
        private void Button_ClickCancel(object sender)
        {
            (sender as Window).Close();
        }

        //private void TimeDuration_Changed(object sender)
        //{
        //    try
        //    {
        //        timeCharge = double.Parse(sender.ToString());
        //        BO.BaseStation baseStation = bl.ReleaseDroneFromRecharge(Drone.Id);
        //        SelectedStatus = ((object)Drone.Status).ToString();
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedStatus)));
        //        ListsModel.Instance.UpdateDrone(Drone.Id);
        //        ListsModel.Instance.UpdateStation(baseStation.Id);
        //        MessageBox.Show($"The drone now is released from charging...\nit's update battery is: {Drone.Battery}");
        //        VisibleTimeCharging = false;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VisibleTimeCharging)));
        //    }
        //    catch (DroneStatusException exe)
        //    {
        //        MessageBox.Show($"The status: {exe.Status} isn't correct for release the drone from charging.");
        //    }
        //}

        private void DroneAction_Selected(object sender)
        {
            try
            {
                if (!IsAllValid())
                {
                    MessageBox.Show("Not all the fields are filled with correct values\nThis action is invalid!");
                    return;
                }
                Drone.Weight = (POConverter.WeightCategories)Enum.Parse(typeof(POConverter.WeightCategories), SelectedWeight);
                Drone.Model = SelectedModel;
                Drone.Status = POConverter.DroneStatuses.Available;
                switch (sender.ToString())
                {

                    case nameof(DroneActions.Associate):
                        {
                            BO.Parcel parcel = bl.Associateparcel(bl.GetDroneForList(Drone.Id));
                            SelectedStatus = ((object)Drone.Status).ToString();
                            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedStatus)));
                            ListsModel.Instance.UpdateDrone(Drone.Id);
                            ListsModel.Instance.UpdateParcel(parcel.Id);
                            MessageBox.Show($"The drone succeeded in associating the parcel number: {parcel.Id} to it.");
                            break;
                        }
                    case nameof(DroneActions.PickUp):
                        {
                            bl.PickUpParcel(Drone.Id);
                            SelectedStatus = ((object)Drone.Status).ToString();
                            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedStatus)));
                            ListsModel.Instance.UpdateDrone(Drone.Id);
                            ListsModel.Instance.UpdateParcel(Drone.Parcel.Id);
                            MessageBox.Show("The drone succeeded in picking up the parcel.");
                            break;
                        }
                    case nameof(DroneActions.Supply):
                        {
                            bl.SupplyParcel(Drone.Id);
                            SelectedStatus = ((object)Drone.Status).ToString();
                            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedStatus)));
                            ListsModel.Instance.UpdateDrone(Drone.Id);
                            ListsModel.Instance.UpdateParcel(Drone.Parcel.Id);
                            MessageBox.Show("The drone succeeded in supplying the parcel to its destination.");
                            break;
                        }
                    case nameof(DroneActions.SendforRecharge):
                        {
                            BO.BaseStation baseStation = bl.SendDroneForCharge(Drone.Id);
                            SelectedStatus = ((object)Drone.Status).ToString();
                            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedStatus)));
                            ListsModel.Instance.UpdateDrone(Drone.Id);
                            ListsModel.Instance.UpdateStation(baseStation.Id);
                            MessageBox.Show("The drone now is in charging...");
                            break;
                        }
                    case nameof(DroneActions.ReleaseFromRecharge):
                        {
                            //MessageBox.Show("Please enter the time duration drone has been in charging.");
                            //VisibleTimeCharging = true;
                            bl.ReleaseDroneFromRecharge(Drone.Id);
                            Drone = new(DroneForListBOToPO( bl.GetDroneForList(Drone.Id)),bl);
                            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Drone)));
                            //invoking the method from the event "Lost Focus" of TimeCharge field.
                            break;
                        }
                }
            }
            catch (DroneStatusException exe)
            {
                MessageBox.Show($"The status: {exe.Status} isn't valid for the action: {sender}.");
            }
            catch (ParcelActionsException exe)
            {
                MessageBox.Show($"The action: {exe.Action} was failed to be completed\nTry another valid action.");
            }
            catch (ParcelStatusException exe)
            {
                MessageBox.Show($"The parcel status: {exe.ParcelStatus} isn't valid for the action: {sender}.");
            }

        }
        #endregion

        #region CRUD_Events
        /// <summary>
        /// delete a customer.
        /// </summary>
        /// <param name="sender">the event</param>
        private void Button_ClickDelete(object sender)
        {
            if (!IsAllValid())
            {
                MessageBox.Show("Not all the fields are filled with correct values\nThis action is invalid!");
                return;
            }
            Drone.Weight = (POConverter.WeightCategories)Enum.Parse(typeof(POConverter.WeightCategories), SelectedWeight);
            Drone.Model = SelectedModel;
            Drone.Status = POConverter.DroneStatuses.Available;
            if (Drone.Parcel != null)
            {
                MessageBox.Show("Can not delete this drone since he has a parcel\n finish with the parcel and try again.");
                return;
            }
            try
            {
                BO.Drone boDrone = DronePOToBo(Drone);
                boDrone.IsDeleted = true;
                bl.Delete(boDrone);
                ListsModel.Instance.DeleteDrone(Drone.Id);
                Button_ClickCancel(sender);
                MessageBox.Show("The drone has been deleted successfully!");
            }
            catch (IntIdException exe)
            {
                MessageBox.Show($"the chosen id: {exe.Id} doesn't exist in the database.");
            }
        }

        /// <summary>
        /// add a new drone
        /// </summary>
        /// <param name="sender">the event</param>
        private void Button_ClickAdd(object sender)
        {
            if (!IsAllValid())
            {
                MessageBox.Show("Not all the fields are filled with correct values\nThis action is invalid!");
                return;
            }
            Drone.Weight = (POConverter.WeightCategories)Enum.Parse(typeof(POConverter.WeightCategories), SelectedWeight);
            Drone.Model = SelectedModel;
            Drone.Status = POConverter.DroneStatuses.Available;
            if (sender as PO.BaseStationForList == null && StationsId.IsEmpty)
            {
                MessageBox.Show("please select a station and try again.");
                return;
            }
            if (sender as PO.BaseStationForList == null)
            {
                MessageBox.Show("please select a station and try again.");
                return;
            }
            try
            {
                bl.Add(DronePOToBo(Drone), (sender as PO.BaseStationForList).Id);
                ListsModel.Instance.AddDrone(Drone.Id);
                MessageBox.Show("The drone has been added successfully!\nPay attention - the last valid input is saved.");
            }
            catch (IntIdException exe)
            {
                MessageBox.Show($"the chosen id: {exe.Id} already exists in the database.");
            }
        }

        /// <summary>
        /// update details of a customer.
        /// </summary>
        /// <param name="sender">the event</param>
        private void Button_ClickUpdate(object sender)
        {
            Drone.Model = SelectedModel;
            if (!IsAllValid())
            {
                MessageBox.Show("Not all the fields are filled with correct values\nThis action is invalid!");
                return;
            }
            try
            {
                if (SelectedModel != Drone.Model)
                {
                    bl.UpdateDrone(Drone.Id, Drone.Model);
                    ListsModel.Instance.UpdateDrone(Drone.Id);
                    MessageBox.Show("The drone has been updated successfully!\nPay attention - the last valid input is saved.");
                }
                else
                {
                    MessageBox.Show("No changes have been done.\nThere's no what to update.");
                    return;
                }
            }
            catch (IntIdException exe)
            {
                MessageBox.Show($"the chosen id: {exe.Id} doesn't exists in the database");
                return;
            }
            catch (BLIntIdException exe)
            {
                MessageBox.Show($"the chosen id: {exe.Id} doesn't exists in the database");
                return;
            }
        }
        #endregion

        #region Simulator
        private void updateDrone(object userStage) => worker.ReportProgress(0, userStage);
        private bool checkStop() => worker.CancellationPending;
        private void updateDroneView(object userStage)
        {
            Stage = new PO.UserStage(userStage as BO.UserStage);
            ListsModel.Instance.UpdateDrone(Drone.Id);
            Drone = DroneBOToPO(bl.GetBLDrone(Drone.Id), bl);
            coorLon = Drone.Location.CoorLongitude.ToString();
            coorLat = Drone.Location.CoorLatitude.ToString();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Drone)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(coorLon)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(coorLat)));
        }

        /// <summary>
        /// Invoke the simulator
        /// </summary>
        /// <param name="sender">the sender</param>
        private void Button_Simulator(object sender)
        {
            InSimulator = true;
            worker = new() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
            worker.DoWork += (sender, args) => bl.InvokeSimulator((int)args.Argument, updateDrone, checkStop);
            worker.RunWorkerCompleted += (sender, args) => InSimulator = false;
            worker.ProgressChanged += (sender, args) => updateDroneView(args.UserState);
            worker.RunWorkerAsync(Drone.Id);
        }

        /// <summary>
        /// Stop the simulator
        /// </summary>
        /// <param name="sender">the sender</param>
        private void Button_Regular(object sender) => worker?.CancelAsync();
        #endregion

        #region Validation 
        public bool IsAllValid()
        {
            NotEmptyRule n1 = new NotEmptyRule();
            NumberRule n2 = new NumberRule();
            RealPositiveNumberRule n3 = new();
            PositiveDoubleRule n4 = new();
            DoubleRule n5 = new();
            RealPositiveDoubleRule n6 = new();
            return IsValid(Drone.Id, n1, n2, n3)
                && IsValid(SelectedModel, n1)
                && IsValid(Drone.Battery, n1, n5, n4)
                && IsValid(SelectedWeight, n1);
        }
        #endregion
    }
}
