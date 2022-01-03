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
using System.ComponentModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for ParcelView.xaml
    /// </summary>
    public partial class ParcelInPassingView : Window, INotifyPropertyChanged
    {
        public ParcelInPassingView(BLApi.IBL bl)
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void priority_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void id_Copy_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }

}


