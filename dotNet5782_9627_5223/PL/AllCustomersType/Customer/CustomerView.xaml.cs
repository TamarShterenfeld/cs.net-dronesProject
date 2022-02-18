﻿using PL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
    /// Interaction logic for StationView.xaml
    /// </summary>
    public partial class CustomerView : Window
    {
        public CustomerView(CustomerViewModel customerViewModel)
        {
            InitializeComponent();
            this.DataContext = customerViewModel;
        }
        private void NotClosing_Click(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
