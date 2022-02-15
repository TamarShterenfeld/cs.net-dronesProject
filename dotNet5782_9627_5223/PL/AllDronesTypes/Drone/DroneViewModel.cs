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

namespace PL
{
    public class DroneViewModel : INotifyPropertyChanged
    {
        BLApi.IBL bl;
        object coorLon, coorLat;
        public PO.Drone Drone { get; set; }
        public bool EnableUpdate { get; set; }
        bool inSimulator;
        public bool InSimulator { get => inSimulator; set { inSimulator = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(InSimulator))); } }
        public RelayCommand Cancel { get; set; }
        public RelayCommand AddOrUpdate { get; set; }
        public RelayCommand Delete { get; set; }
        public RelayCommand LeftDoubleClick { get; set; }
        public RelayCommand Simulator { get; set; }
        public RelayCommand Regular { get; set; }
        public ListCollectionView DroneWeightsList { get; set; }
        public ListCollectionView Statuses { get; set; }
        public ListCollectionView StationsId { get; set; }
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
            //ParcelID = Drone.Parcel != null? Drone.Parcel.Id:null;
            //Parcel = ParcelInPassingBOTOPO(bl.GetParcelInPassing(Drone.ParcelId));
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="bl">BL object</param>
        public DroneViewModel(BLApi.IBL bl)
        {
            this.bl = bl;
            Drone = new();
            Cancel = new(Button_ClickCancel, null);
            AddOrUpdate = new(Button_ClickAdd, null);
            EnableUpdate = true;
            LeftDoubleClick = new(doubleClickParcel, null);
            DroneWeightsList = new ListCollectionView(Enum.GetValues(typeof(PO.POConverter.WeightCategories)).Cast<PO.POConverter.WeightCategories>().ToList());
            Statuses = new ListCollectionView(Enum.GetValues(typeof(PO.POConverter.DroneStatuses)).Cast<PO.POConverter.DroneStatuses>().ToList());
            if (Drone.Status == POConverter.DroneStatuses.None) { Drone.Status = POConverter.DroneStatuses.Available; }
            StationsId = new ListCollectionView(bl.GetAvailableChargeSlots().ToList());
        }

        public object CoorLon
        {
            get => coorLon;
            set
            {
                if (double.TryParse(value.ToString(), out double longitude))
                {
                    coorLon = value;
                    Drone.Location.CoorLongitude = new PO.Coordinate(longitude, POConverter.Locations.Longitude);
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
                    coorLat = value;
                    Drone.Location.CoorLatitude = new PO.Coordinate(latitude, POConverter.Locations.Latitude);
                }
            }
        }



        //public PO.ParcelInPassing Parcel { get; set; }

        //readonly Action refreshDroneList;
        object chargeDurationTime;
        string button3SelectedItem;
        string parcelOption;
        private string simulatorOrRegular;
        

        public event PropertyChangedEventHandler PropertyChanged;


        //properties



        //public string Button2Content { get; set; }

        //public List<string> IsOrNotCharging = new List<string>() { "Charging", "NotCharging" };

        //public List<string> ParcelOptions = new List<string>() { "Associate", "PickUp", "Supply" };
        //public string ParcelOption 
        //{ set
        //    {
        //        parcelOption = ParcelOption;
        //        ParcelsOptions();
        //    }
        //    get
        //    {
        //        return parcelOption;
        //    }
        //}

        //public TimeSpan Time { set; get; }

        //public TimeSpan TimeText { set; get; }
        //public string Button3SelectedItem
        //{
        //    set
        //    {
        //        button3SelectedItem = value;
        //        Button_ClickCharging();
        //    }
        //    get
        //    {
        //        return button3SelectedItem;
        //    }
        //}


        //public RelayCommand TimeDuration { set; get; }
        //public RelayCommand DisplayParcelCommand { set; get; }



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

        /// <summary>
        /// add a new customer.
        /// </summary>
        /// <param name="sender">the event</param>
        private void Button_ClickAdd(object sender)
        {
            if (sender as BaseStationForList == null)
            {
                MessageBox.Show("please select a station and try again.");
                return;
            }
            bl.Add(DronePOToBo(Drone), (sender as BaseStationForList).Id);
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
            Drone = DroneBOToPO(bl.GetBLDrone(Drone.Id),bl);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Drone)));
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
            new ParcelInPassingView(new ParcelInPassingViewModel(bl.GetParcelInPassing(Drone.Id), bl)).Show();
        }

        private void Simulator_Click(Object sender, EventArgs e)
        {
            worker = new() { WorkerReportsProgress = true, WorkerSupportsCancellation = true, };
            // worker.DoWork += bl.InvokeSimulator<BO.Drone>((sender as BO.Drone).Id, );
            // worker.ProgressChanged += func;
            
            //Auto = true;
            
            //worker.DoWork += (sender, args) => bl.StartDroneSimulator((int)args.Argument, updateDrone, checkStop);
            //worker.RunWorkerCompleted += (sender, args) => Auto = false;
            //worker.ProgressChanged += (sender, args) => updateDroneView();
            //worker.RunWorkerAsync(Drone.Id);
        }




        ///// <summary>
        ///// the function treats the event of clicking on the button 'Cancel'.
        ///// </summary>
        ///// <param name="sender">the invoking object</param>
        ///// <param name="e">the event</param>
        //private void Button_ClickCancel(object sender)
        //{
        //    (sender as Window).Close();
        //}

        ///// <summary>
        ///// the function treats the event of clicking on the button 'Add'.
        ///// </summary>
        //private void Button_ClickAdd(object sender)
        //{
        //    MessageBoxResult m = new();
        //    if (Drone.Model == "" || Drone.Weight.ToString() == "" || Drone.Id.ToString() == "")
        //    {
        //        MessageBox.Show("Id, Model and Max-Weight must have value!");
        //    }
        //    else
        //    {
        //        BO.Drone drone = new();
        //        drone.Id = InputIntValue(Drone.Id.ToString());
        //        drone.Model = Drone.Model;
        //        drone.MaxWeight = InputWeightCategory(Drone.Weight.ToString());
        //        drone.Location = Bl.GetBLBaseStation(InputIntValue(StationId.ToString())).Location;
        //        try
        //        {
        //            Bl.Add(drone, InputIntValue(StationsId.ToString()));
        //            MessageBox.Show("drone was added successfully!");
        //            //refreshDroneList?.Invoke();
        //            (sender as Window).Close();
        //        }

        //        catch (BLIntIdException exe)
        //        {
        //            m = MessageBox.Show("drone's id: " + exe.Id + " isn't valid!");

        //        }
        //        catch (IntIdException exe)
        //        {
        //            m = MessageBox.Show("drone's id: " + exe.Id + " isn't valid!");
        //        }
        //    }
        //}

        ///// <summary>
        ///// the function treats the event of clicking on the button 'UpDate'.
        ///// </summary>
        //private void Button_ClickUpdate()
        //{
        //    if (Drone.Model == "")
        //    {
        //        MessageBox.Show("Model must have value!");
        //    }
        //    else
        //    {
        //        Bl.UpdateDrone(InputIntValue(Drone.Id.ToString()), Drone.Model);
        //        //refreshDroneList();
        //    }
        //}

        ///// <summary>
        ///// the function invoking the appropriate function according to the button's content.
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void Button_ClickAddOrUpdate(object sender)
        //{
        //    if (Button2Content == "Add")
        //    { Button_ClickAdd(sender); }
        //    else
        //    { Button_ClickUpdate(); }
        //}

        ///// <summary>
        ///// the function treats the evdent of choosing  - 
        ///// 'Charging' / 'Stop Charging' from the ComboBox - 'button3'
        ///// </summary>
        ///// <param name="sender">the invoking object</param>
        ///// <param name="e">the event</param>
        //private void Button_ClickCharging()
        //{
        //    try
        //    {
        //        switch (Button3SelectedItem)
        //        {
        //            case "Stop Charging":
        //                {
        //                    Key key1 = new();
        //                    if (key1 == Key.None)
        //                    {
        //                        MessageBox.Show("Enter the charge Duration in the suitable field");
        //                        ((object)Time as Window).Visibility = Visibility.Visible;
        //                        ((object)TimeText as Window).Visibility = Visibility.Visible;
        //                        //---it begins charging the drone when the Time textbox field is filled.
        //                    }
        //                    break;
        //                }
        //            case "Charging":
        //                {
        //                    Bl.SendDroneForCharge(InputIntValue(Drone.Id.ToString()));
        //                    //status.SelectedIndex = 1;
        //                    MessageBox.Show("drone starts charging!");
        //                    //refreshDroneList();
        //                    break;
        //                }
        //        }
        //    }
        //    catch (DroneStatusException exe)
        //    {
        //        MessageBox.Show("the drone status: " + exe.Status + " isn't valid");
        //    }
        //    catch (ChargeSlotsException exe)
        //    {
        //        MessageBox.Show("the chargeSlots: " + exe.ChargeSlots + " isn't available!");
        //    }
        //    catch (BLChargeSlotsException exe)
        //    {
        //        MessageBox.Show("the chargeSlots: " + exe.ChargeSlots + " isn't available!");
        //    }
        //    catch (ParcelActionsException exe)
        //    {
        //        MessageBox.Show("the action of: " + exe.Action + " wasn't completed successfully!");
        //    }
        //    catch (BatteryException exe)
        //    {
        //        MessageBox.Show("the drone's battery: " + exe.Battery + " isn't enouph for reaching the  base station for charging!");
        //    }
        //   //refreshDroneList();
        //}

        ///// <summary>
        ///// the function treats the event of choosing an action (from button4 - a ComboBox)
        ///// from the update options of a parcel.
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void ParcelsOptions()
        //{
        //    try
        //    {
        //        switch (ParcelOption)
        //        {
        //            case "Associate Parcel":
        //                {
        //                    InvokeTimeField();
        //                    Bl.AssociateParcel(InputIntValue(Drone.Id.ToString()));
        //                    MessageBox.Show("The parcel was associated successfully!");
        //                    //refreshDroneList();
        //                    break;
        //                }
        //            case "Pick Up Parcel":
        //                {
        //                    InvokeTimeField();
        //                    Bl.PickUpParcel(InputIntValue(Drone.Id.ToString()));
        //                    MessageBox.Show("The parcel was picked up successfully!");
        //                    //refreshDroneList();
        //                    break;
        //                }
        //            case "Supply Parcel":
        //                {
        //                    InvokeTimeField();
        //                    Bl.SupplyParcel(InputIntValue(Drone.Id.ToString()));
        //                    MessageBox.Show("The parcel was supplied successfully!");
        //                    //refreshDroneList();
        //                    break;
        //                }
        //        }
        //    }
        //    catch (DroneStatusException exe)
        //    {
        //        MessageBox.Show("The action isn't valid, because the drone status: " + exe.Status + " isn't valid!");
        //    }
        //    catch (ParcelActionsException exe)
        //    {
        //        MessageBox.Show("The action " + exe.Action + " isn't valid!");
        //    }
        //    catch (ParcelStatusException exe)
        //    {
        //        MessageBox.Show("The action isn't valid, because the parcel status: " + exe.ParcelStatus + " isn't valid!");
        //    }
        //    //refreshDroneList();
        //}

        ///// <summary>
        ///// the function treats the event of inserting a value into the charge duration field.
        ///// this function is in charge of release drone drom chaging.
        ///// </summary>
        ///// <param name="sender">the invoking object</param>
        ///// <param name="e">the event</param>
        //public object ChargeDurationTime
        //{
        //    get => chargeDurationTime;

        //    set
        //    {
        //        chargeDurationTime = value;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ChargeDurationTime)));
        //        int timeCharge = InputIntValue(Time.ToString());
        //        Bl.ReleaseDroneFromRecharge(InputIntValue(Drone.Id.ToString()), timeCharge);
        //        //refreshDroneList();
        //        //status.SelectedIndex = 0;
        //        MessageBox.Show("drone stopps charging!");
        //        InvokeTimeField();
        //    }
        //}


        //public void DisplayParcle (object sender)
        //{
        //    var parcel = Bl.GetParcelInPassing(Parcel.Id);
        //    new ParcelInPassingView(new ParcelInPassingViewModel(parcel, Bl))
        //        .Show();
        //}

        ////-----------------------------------Helping Functions----------------------------------
        ///// <summary>
        ///// the function enforces the user to enter only a number that contains digits.
        ///// </summary>
        ///// <param name="sender">the invoking object</param>
        ///// <param name="e">the event</param>
        //private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        //{
        //    Regex regex = new Regex("[^0-9]+");
        //    e.Handled = regex.IsMatch(e.Text);
        //}

        ///// <summary>
        ///// The function shows the field 'Time' and the appropriate lable.
        ///// </summary>
        //private void InvokeTimeField()
        //{
        //    ((object)Time as Window).Visibility = Visibility.Collapsed;
        //    ((object)TimeText as Window).Visibility = Visibility.Collapsed;
        //}

        ///// <summary>
        ///// the function checks that the inputed value really contains a numerical value.
        ///// </summary>
        ///// <param name="str">a string value</param>
        ///// <returns></returns>
        //private int InputIntValue(string str)
        //{
        //    int numericalValue = 0;
        //    if (!int.TryParse(str, out numericalValue))
        //    {
        //        MessageBox.Show("a field which supposed to contain numerical value does not contain!");
        //    }
        //    return numericalValue;
        //}

        ///// <summary>
        ///// the function checks that the inputed value ( a string one )
        ///// is really contained in the enum 'WeightCategory'.
        ///// </summary>
        ///// <param name="str"></param>
        ///// <returns></returns>
        //private BO.WeightCategories InputWeightCategory(string str)
        //{
        //    bool isExist1 = false;
        //    string currentEnum;
        //    BO.WeightCategories weight = BO.WeightCategories.Average;
        //    //checking if the inputed category (string) exists in WeightCategories enum
        //    while (isExist1 == false)
        //    {
        //        for (int i = 1; i <= Enum.GetNames(typeof(BO.WeightCategories)).Length; i++)
        //        {
        //            currentEnum = (string)Enum.GetNames(typeof(BO.WeightCategories)).GetValue(i - 1);
        //            if (currentEnum == str || currentEnum.ToLower() == str)
        //            {
        //                weight = (BO.WeightCategories)i;
        //                isExist1 = true;
        //                break;
        //            }
        //        }
        //        if (isExist1 == false)
        //        {
        //            MessageBox.Show("The entered weight category doesn't exist\nPlease enter another weight category");
        //        }
        //    }
        //    return weight;
        //}

    }
}
