using IBL.BO;
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
        private IBL.IBL bl;
        Action action;
        public SingleDrone(IBL.IBL bl,Action action)
        {
            this.bl = bl;
            this.action = action;
            InitializeComponent();
            id.DataContext = model.DataContext = weight.DataContext = station.DataContext = "true";
            button3.DataContext = button4.DataContext = "Collapsed";
            status.DataContext = typeof(DroneStatuses).GetEnumValues();
            weight.DataContext = typeof(WeightCategories).GetEnumValues();
            List <string> str = new List<string>();
            foreach (var item in bl.GetBOBaseStationsList())
            {
                str.Add(item.Id.ToString());
            }
            station.DataContext = str;
        }

        public SingleDrone(DroneForList droneForList, IBL.IBL bl,Action action)
            :this(bl,action)
        {
            this.DataContext = "False";
            station.DataContext = "False";
            Drone drone = bl.GetBLDrone(droneForList.Id);
            id.Text = drone.Id.ToString();
            model.Text = drone.Model;
            weight.SelectedItem = drone.MaxWeight;
            battery.Text = drone.Battery.ToString()+"%";
            status.SelectedItem = drone.Status;
            delivery.Text = drone.Parcel.Id.ToString();
            longitude.Text = drone.Location.CoorLongitude.ToString();
            latitude.Text = drone.Location.CoorLatitude.ToString();
            station.DataContext = Lstation.DataContext =  "Collapsed";
            button3.DataContext = button4.DataContext= "Visible";
            button2.Content = "Update";

        }

        private void Button_ClickCancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_ClickAdd()
        {
            if (model.Text == "" || weight.Text == "")
            {
                MessageBox.Show("Model and Max Weight must have value!");
            }
            else
            {
                Drone drone = new Drone();
                drone.Id = InputIntValue(id.Text);
                drone.Model = model.Text;
                drone.MaxWeight = InputWeightCategory(weight.Text);
                drone.Location = bl.GetBLBaseStation(InputIntValue(station.Text)).Location;
                bl.Add(drone, InputIntValue(station.Text));
                MessageBox.Show("drone was added successfully!");
                action();
                this.Close();
            }
        }

        private void Button_ClickUpdate()
        {
            if (model.Text == "")
            {
                MessageBox.Show("Model must have value!");
            }
            else
            {
                bl.UpdateDrone(InputIntValue(id.Text), model.Text);
                action();
            }
        }

        private void Button_ClickAddOrUpdate(object sender, RoutedEventArgs e)
        {
            if(button2.Content.ToString() == "Add")
            { Button_ClickAdd(); }
            else 
            { Button_ClickUpdate(); }  
        }

        private void Button_ClickCharging(object sender, RoutedEventArgs e)
        {
            if (status.Text.ToString() == DroneStatuses.Maintenance.ToString())
            {
                hlong.DataContext = "Collapsed";
                int timeCharge = InputIntValue(howLong.Text);
                bl.ReleaseDroneFromRecharge(InputIntValue(id.Text), timeCharge);
                MessageBox.Show("drone stops charging!");
            }
            else if(delivery.Text.ToString() == "0")
            {
                bl.SendDroneForCharge(InputIntValue(id.Text));
                MessageBox.Show("drone starts charging!");
            }
            else
            {
                MessageBox.Show("drone can not start or stop charging!");
            }
            action();
        }

        

        private void Button_ClickToParcel(object sender, RoutedEventArgs e)
        {
            if (delivery.Text.ToString() == "0")
            {
                bl.AssociateParcel(InputIntValue(id.Text));
            }
            else if (bl.GetBLParcel(InputIntValue(delivery.Text)).PickUpDate == null )
            {
                bl.PickUpParcel(InputIntValue(id.Text));
            }
            else if(bl.GetBLParcel(InputIntValue(delivery.Text)).PickUpDate != null && bl.GetBLParcel(InputIntValue(delivery.Text)).SupplyDate == null)
            {
                bl.SupplyParcel(InputIntValue(delivery.Text));
            }
            else
            {
                MessageBox.Show("drone can not treat a parcel!");
            }
            action();
        }
        private int InputIntValue(string str)
        {
            int numericalValue = 0;
            if (!int.TryParse(str, out numericalValue))
            {
                Console.WriteLine("a field which supposed to contain numerical value does not contain!");
            }
            return numericalValue;
        }

        private WeightCategories InputWeightCategory(string str)
        {
            bool isExist1 = false;
            string currentEnum;
            WeightCategories weight = WeightCategories.Average;
            //checking if the inputed category (string) exists in WeightCategories enum
            while (isExist1 == false)
            {
                for (int i = 1; i <= Enum.GetNames(typeof(WeightCategories)).Length; i++)
                {
                    currentEnum = (string)Enum.GetNames(typeof(WeightCategories)).GetValue(i - 1);
                    if (currentEnum == str || currentEnum.ToLower() == str)
                    {
                        weight = (WeightCategories)i;
                        isExist1 = true;
                        break;
                    }
                }
                if (isExist1 == false)
                {
                    Console.WriteLine("The entered weight category doesn't exist\nPlease enter another weight category");
                }
            }
            return weight;
        }
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
    
}


