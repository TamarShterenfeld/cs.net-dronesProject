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

namespace PL
{
    /// <summary>
    /// Interaction logic for Drone.xaml
    /// </summary>
    public partial class SingleDrone : Window
    {
        private IBL.IBL bl;
        public SingleDrone(IBL.IBL bl)
        {
            this.bl = bl;
            InitializeComponent();
            this.DataContext = "true";
            status.DataContext = typeof(DroneStatuses).GetEnumValues();
            weight.DataContext = typeof(WeightCategories).GetEnumValues();
            station.DataContext = "True";
        }

        public SingleDrone(DroneForList droneForList, IBL.IBL bl)
        {
            this.bl = bl;
            InitializeComponent();
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
            station.DataContext = Lstation.DataContext = button2.DataContext = "Collapsed";
        }

        private void Button_ClickCancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_ClickAdd(object sender, RoutedEventArgs e)
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
                this.Close();
            }
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
