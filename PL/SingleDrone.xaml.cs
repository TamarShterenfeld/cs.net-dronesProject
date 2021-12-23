using BO;
using IBL;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Globalization;


namespace PL
{
    /// <summary>
    /// Interaction logic for Drone.xaml
    /// </summary>
    public partial class SingleDrone : Window
    {
        private BLApi.IBL bl;
        Action action;

        /// <summary>
        /// a constructor that gets the object bl + the delegate action.
        /// </summary>
        /// <param name="bl">the request object to the BL logic level</param>
        /// <param name="action">a delegate</param>
        public SingleDrone(BLApi.IBL bl, Action action)
        {
            id.DataContext = model.DataContext = weight.DataContext = station.DataContext = "True";
            button3.DataContext = button4.DataContext = "Collapsed";
            status.DataContext = typeof(DroneStatuses).GetEnumValues();
            weight.DataContext = typeof(BO.WeightCategories).GetEnumValues();
            List<string> stationsId = new List<string>();
            foreach (var item in bl.GetBOBaseStationsList())
            {
                stationsId.Add(item.Id.ToString());
            }
            station.DataContext = stationsId;
        }

        /// <summary>
        /// another constructor with parameters
        /// </summary>
        /// <param name="droneForList"></param>
        /// <param name="bl">the request object to the BL level</param>
        /// <param name="action">a delegate</param>
        public SingleDrone(DroneForList droneForList, BLApi.IBL bl, Action action)
            : this(bl, action)
        {
            station.DataContext = id.DataContext = "False";
            if (droneForList != null)
            {
                BO.Drone drone = bl.GetBLDrone(droneForList.Id);
                id.Text = drone.Id.ToString();
                model.Text = drone.Model;
                weight.IsEnabled = false;
                weight.SelectedItem = drone.MaxWeight;
                battery.Text = drone.Battery.ToString() + "%";
                status.SelectedItem = drone.Status;
                delivery.Text = drone.Parcel.Id.ToString();
                longitude.Text = drone.Location.CoorLongitude.ToString();
                latitude.Text = drone.Location.CoorLatitude.ToString();
                station.Visibility = Visibility.Collapsed;
                Lstation.Visibility = Visibility.Collapsed;
                string[] parcelOptions = { "Associate Parcel", "Pick Up Parcel", "Supply Parcel" };
                string[] chargeDroneOptions = { "Charging", "Stop Charging" };
                button3.DataContext = chargeDroneOptions;
                button4.DataContext = parcelOptions;
                button3.Visibility = button4.Visibility = Visibility.Visible;
                button2.Content = "Update";
            }
        }

        /// <summary>
        /// the function treats the event of clicking on the button 'Cancel'.
        /// </summary>
        /// <param name="sender">the invoking object</param>
        /// <param name="e">the event</param>
        private void Button_ClickCancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// the function treats the event of clicking on the button 'Add'.
        /// </summary>
        private void Button_ClickAdd()
        {
            MessageBoxResult m = new();
            if (model.Text == "" || weight.Text == "" || id.Text == "")
            {
                MessageBox.Show("Id, Model and Max-Weight must have value!");
            }
            else
            {
                BO.Drone drone = new();
                drone.Id = InputIntValue(id.Text);
                drone.Model = model.Text;
                drone.MaxWeight = InputWeightCategory(weight.Text);
                drone.Location = bl.GetBLBaseStation(InputIntValue(station.Text)).Location;
                try
                {
                    bl.Add(drone, InputIntValue(station.Text));
                    MessageBox.Show("drone was added successfully!");
                    action?.Invoke();
                    this.Close();
                }

                catch (BLIntIdException exe)
                {
                    m = MessageBox.Show("drone's id: " + exe.Id + " isn't valid!");

                }
                catch (IntIdException exe)
                {
                    m = MessageBox.Show("drone's id: " + exe.Id + " isn't valid!");
                }
            }
        }


        /// <summary>
        /// the function treats the event of clicking on the button 'UpDate'.
        /// </summary>
        private void Button_ClickUpdate()
        {
            if (model.Text == "")
            {
                MessageBox.Show("Model must have value!");
            }
            else
            {
                bl.UpdateDrone(InputIntValue(id.Text), model.Text);
                action?.Invoke();
            }
        }

        /// <summary>
        /// the function invoking the appropriate function according to the button's content.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickAddOrUpdate(object sender, RoutedEventArgs e)
        {
            if (button2.Content.ToString() == "Add")
            { Button_ClickAdd(); }
            else
            { Button_ClickUpdate(); }
        }

        /// <summary>
        /// the function treats the evdent of choosing  - 
        /// 'Charging' / 'Stop Charging' from the ComboBox - 'button3'
        /// </summary>
        /// <param name="sender">the invoking object</param>
        /// <param name="e">the event</param>
        private void Button_ClickCharging(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (button3.SelectedItem)
                {
                    case "Stop Charging":
                        {
                            Key key1 = new();
                            if (key1 == Key.None)
                            {
                                MessageBox.Show("Enter the charge Duration in the suitable field");
                                TimeText.Visibility = Visibility.Visible;
                                Time.Visibility = Visibility.Visible;
                                //---it begins charging the drone when the Time textbox field is filled.
                            }
                            break;
                        }
                    case "Charging":
                        {
                            bl.SendDroneForCharge(InputIntValue(id.Text));
                            status.SelectedIndex = 1;
                            MessageBox.Show("drone starts charging!");
                            action();
                            break;
                        }
                }
            }
            catch (DroneStatusException exe)
            {
                MessageBox.Show("the drone status: " + exe.Status + " isn't valid");
            }
            catch (ChargeSlotsException exe)
            {
                MessageBox.Show("the chargeSlots: " + exe.ChargeSlots + " isn't available!");
            }
            catch (BLChargeSlotsException exe)
            {
                MessageBox.Show("the chargeSlots: " + exe.ChargeSlots + " isn't available!");
            }
            catch (ParcelActionsException exe)
            {
                MessageBox.Show("the action of: " + exe.Action + " wasn't completed successfully!");
            }
            catch (BatteryException exe)
            {
                MessageBox.Show("the drone's battery: " + exe.Battery + " isn't enouph for reaching the  base station for charging!");
            }
            action?.Invoke();
        }

        /// <summary>
        /// the function checks that the inputed value really contains a numerical value.
        /// </summary>
        /// <param name="str">a string value</param>
        /// <returns></returns>
        private int InputIntValue(string str)
        {
            int numericalValue = 0;
            if (!int.TryParse(str, out numericalValue))
            {
                MessageBox.Show("a field which supposed to contain numerical value does not contain!");
            }
            return numericalValue;
        }

        /// <summary>
        /// the function checks that the inputed value ( a string one )
        /// is really contained in the enum 'WeightCategory'.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private BO.WeightCategories InputWeightCategory(string str)
        {
            bool isExist1 = false;
            string currentEnum;
            BO.WeightCategories weight = BO.WeightCategories.Average;
            //checking if the inputed category (string) exists in WeightCategories enum
            while (isExist1 == false)
            {
                for (int i = 1; i <= Enum.GetNames(typeof(BO.WeightCategories)).Length; i++)
                {
                    currentEnum = (string)Enum.GetNames(typeof(BO.WeightCategories)).GetValue(i - 1);
                    if (currentEnum == str || currentEnum.ToLower() == str)
                    {
                        weight = (BO.WeightCategories)i;
                        isExist1 = true;
                        break;
                    }
                }
                if (isExist1 == false)
                {
                    MessageBox.Show("The entered weight category doesn't exist\nPlease enter another weight category");
                }
            }
            return weight;
       }

        /// <summary>
        /// the function enforces the user to enter only a number that contains digits.
        /// </summary>
        /// <param name="sender">the invoking object</param>
        /// <param name="e">the event</param>
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        /// <summary>
        /// the function treats the event of choosing an action (from button4 - a ComboBox)
        /// from the update options of a parcel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                switch (button4.SelectedItem)
                {
                    case "Associate Parcel":
                        {
                            Time.Visibility = Visibility.Collapsed;
                            TimeText.Visibility = Visibility.Collapsed;
                            bl.AssociateParcel(InputIntValue(id.Text));
                            MessageBox.Show("The parcel was associated successfully!");
                            action();
                            break;
                        }
                    case "Pick Up Parcel":
                        {
                            Time.Visibility = Visibility.Collapsed;
                            TimeText.Visibility = Visibility.Collapsed;
                            bl.PickUpParcel(InputIntValue(id.Text));
                            MessageBox.Show("The parcel was picked up successfully!");
                            action();
                            break;
                        }                   
                    case "Supply Parcel":
                        {
                            Time.Visibility = Visibility.Collapsed;
                            TimeText.Visibility = Visibility.Collapsed;
                            bl.SupplyParcel(InputIntValue(id.Text));
                            MessageBox.Show("The parcel was supplied successfully!");
                            action();
                            break;
                        }
                }
            }
            catch (DroneStatusException exe)
            {
                MessageBox.Show("The action isn't valid, because the drone status: " + exe.Status + " isn't valid!");
            }
            catch (ParcelActionsException exe)
            {
                MessageBox.Show("The action " + exe.Action + " isn't valid!");
            }
            catch (ParcelStatusException exe)
            {
                MessageBox.Show("The action isn't valid, because the parcel status: " + exe.ParcelStatus + " isn't valid!");
            }
            action?.Invoke();
        }

        /// <summary>
        /// the function treats the event of inserting a value into the charge duration field.
        /// this function is in charge of release drone drom chaging.
        /// </summary>
        /// <param name="sender">the invoking object</param>
        /// <param name="e">the event</param>
        private void TimeValue (object sender, TextChangedEventArgs e)
        {
            int timeCharge = InputIntValue(Time.Text);
            bl.ReleaseDroneFromRecharge(InputIntValue(id.Text), timeCharge);
            action();
            status.SelectedIndex = 0;
            MessageBox.Show("drone stopps charging!");
            TimeText.Visibility = Visibility.Collapsed;
            Time.Visibility = Visibility.Collapsed;
        }
    }

}


