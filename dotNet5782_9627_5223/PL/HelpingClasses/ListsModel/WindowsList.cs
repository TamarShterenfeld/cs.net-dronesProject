using BO;
using Singleton;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using static PL.PO.POConverter;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace PL
{
    static class RefreshAllWindows
    {
        public static event EventHandler RefreshStation;

        public static event EventHandler RefreshDrone;

        public static event EventHandler RefreshCustomer;

        public static event EventHandler RefreshParcel;

    }
}
