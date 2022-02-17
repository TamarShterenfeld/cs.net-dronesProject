using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.PO
{
    public class CustomerInParcel : INotifyPropertyChanged
    {
        #region PrivateFields
        private string id;
        private string name;
        #endregion

        #region Properties

        public event PropertyChangedEventHandler PropertyChanged;

        public string Id
        {
            get => id;
            set
            {
                id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(id)));
            }
        }
        public string Name
        {
            get => name;
            set
            {
                name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(name)));
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// a constructor with parameters
        /// </summary>
        /// <param name="id">CustomerInShipment's id</param>
        /// <param name="name">CustomerInShipment's name</param>
        public CustomerInParcel(string id, string name)
        {
            this.id = id; this.name = name;
            Id = id; Name = name;
        }

        // default constructor
        public CustomerInParcel() { }
        #endregion


        #region ToString
        /// <summary>
        /// override ToString function.
        /// </summary>
        /// <returns>description of CustomerInShipment objectreturns>
        public override string ToString()
        {
            return $"id: {Id}, " +
                    $"name: {Name}";
        }
        #endregion


    }
}
