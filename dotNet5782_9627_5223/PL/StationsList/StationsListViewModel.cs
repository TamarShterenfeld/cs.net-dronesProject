using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL.BaseStations
{
    class StationsListViewModel
    {
        public StationsListViewModel()
        {
            Cancel = new(Button_ClickCancel, null);
            Add = new(Button_ClickAdd, null);
        }
        public RelayCommand Cancel { get; set; }
        public RelayCommand Add { get; set; }

        //---------------------------------Stations's Methods------------------------------
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
        /// the function treats the event of clicking on the button 'Add'.
        /// </summary>
        private void Button_ClickAdd(object sender)
        {
        }

    }
}
