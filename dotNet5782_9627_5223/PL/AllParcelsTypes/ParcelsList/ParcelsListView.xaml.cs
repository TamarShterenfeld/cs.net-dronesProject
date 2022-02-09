using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL
{
    /// <summary>
    /// Interaction logic for ParcelsListView.xaml
    /// </summary>
    public partial class ParcelsListView : Window
    {
        public ParcelsListView(ParcelsListViewModel parcelsListViewModel)
        {
            InitializeComponent();
            DataContext = parcelsListViewModel;
        }
    }
}
