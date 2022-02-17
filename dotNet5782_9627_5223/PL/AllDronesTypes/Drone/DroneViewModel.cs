using BO;
using DO;
using PL.PO;
using static PL.PO.POConverter;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Data;
using System.Linq;
using System.ComponentModel;
using static PL.Validation;

namespace PL
{
    public class DroneViewModel : INotifyPropertyChanged
    {
        BLApi.IBL bl;
        object coorLon, coorLat;
        IList<string> nullString = new List<string>() { "" };
        IList<string> statuses = Enum.GetNames(typeof(POConverter.DroneStatuses));
        IList<string> weights = Enum.GetNames(typeof(POConverter.WeightCategories));
        IList<string> droneActions = Enum.GetNames(typeof(DroneActions));
        string selectedDroneAction, selectedStatus, selectedWeight;
        public PO.Drone Drone { get; set; }
        public bool EnableUpdate { get; set; }
        bool inSimulator;
        public bool InSimulator { get => inSimulator; set { inSimulator = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(InSimulator))); } }
        public object CoorLon
        {
            get => coorLon;
            set
            {
                if (double.TryParse(value.ToString(), out double longitude))
                {
                    if (!IsValidLocation(longitude))
                    {
                        MessageBox.Show("Location must be in range of -180º to 180º");
                        return;
                    }
                    coorLon = value;
                    Drone.Location.CoorLongitude = new PO.Coordinate(longitude, POConverter.Locations.Longitude);
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
                    if (!IsValidLocation(latitude))
                    {
                        MessageBox.Show("Location must be in range of -180º to 180º");
                        return;
                    }
                    coorLat = value;
                    Drone.Location.CoorLatitude = new PO.Coordinate(latitude, POConverter.Locations.Latitude);
                }
                else
                {
                    MessageBox.Show("Location must be a double value type");
                }
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
        public string SelectedDroneAction
        {
            get => selectedDroneAction;
            set
            {
                selectedDroneAction = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(selectedDroneAction)));
            }
        }

        BackgroundWorker worker;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="bl">BL object</param>
        /// <param name="customer">CustomerForList object</param>
        public DroneViewModel(BLApi.IBL bl, PL.PO.DroneForList drone) : this(bl)
        {
            Drone = new PO.Drone(drone, bl);
            AddOrUpdate = new(Button_ClickUpdate, null);
            Delete = new(Button_ClickDelete, null);
            Simulator = new(Button_Simulator, null);
            Regular = new(Button_Regular, null);
            EnableUpdate = false;
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
            LeftDoubleClick = new(doubleClickParcel, null);
            MyDroneActions = new ListCollectionView(nullString.Concat<string>(droneActions).ToList());
            StationsId = new ListCollectionView(bl.GetAvailableChargeSlots().ToList());
        }

        //public PO.ParcelInPassing Parcel { get; set; }

        //readonly Action refreshDroneList;
        private string simulatorOrRegular;


        public event PropertyChangedEventHandler PropertyChanged;


        //properties


        //---------------------------------Drone's Methods------------------------------
        /// <summary>
        /// the function treats the event of clicking on the button 'Cancel'.
        /// </summary>
        /// <param name="sender">the invoking object</param>
        /// <param name="e">the event</param>
        private void Button_ClickCancel(object sender)
        {
            (sender as Window).Close();
        }

        /// <summary>
        /// delete a customer.
        /// </summary>
        /// <param name="sender">the event</param>
        private void Button_ClickDelete(object sender)
        {
            if (Drone.Parcel != null)
            {
                MessageBox.Show("Can not delete this drone since he has a parcel\n finish with the parcel and try again.");
                return;
            }
            //Bl.Delete(CustomerPoToBo(Customer));///////////////////////////////////////////////////////////////////////////////////////
            ListsModel.Instance.DeleteDrone(Drone.Id);
            Button_ClickCancel(sender);
        }


        private void DroneAction_Selected(object sender)
        {
            switch (sender.ToString())
            {
                case nameof(DroneActions.Associate):
                    {
                        break;
                    }
                case nameof(DroneActions.PickUp):
                    {
                        break;
                    }
                case nameof(DroneActions.Supply):
                    {
                        break;
                    }
                case nameof(DroneActions.SendforRecharge):
                    {
                        break;
                    }
                case nameof(DroneActions.ReleaseFromRecharge):
                    {
                        break;
                    }
            }
        }
        /// <summary>
        /// add a new customer.
        /// </summary>
        /// <param name="sender">the event</param>
        private void Button_ClickAdd(object sender)
        {
            if (sender as PO.BaseStationForList == null)
            {
                MessageBox.Show("please select a station and try again.");
                return;
            }
            bl.Add(DronePOToBo(Drone), (sender as PO.BaseStationForList).Id);
            ListsModel.Instance.AddDrone(Drone.Id);
        }

        /// <summary>
        /// update details of a customer.
        /// </summary>
        /// <param name="sender">the event</param>
        private void Button_ClickUpdate(object sender)
        {
            bl.UpdateDrone(Drone.Id, Drone.Model);
            ListsModel.Instance.UpdateDrone(Drone.Id);
        }

        private void updateDrone() => worker.ReportProgress(0);
        private bool checkStop() => worker.CancellationPending;
        private void updateDroneView()
        {
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
            worker.ProgressChanged += (sender, args) => updateDroneView();
            worker.RunWorkerAsync(Drone.Id);
        }

        /// <summary>
        /// Stop the simulator
        /// </summary>
        /// <param name="sender">the sender</param>
        private void Button_Regular(object sender) => worker?.CancelAsync();

        /// <summary>
        /// show full details of parcelInCustomer object
        /// </summary>
        /// <param name="sender">the event</param>
        private void doubleClickParcel(object sender)
        {
            new ParcelView(new ParcelViewModel(bl.GetParcelForList(Drone.Id), bl)).Show();
        }    

    }
}
