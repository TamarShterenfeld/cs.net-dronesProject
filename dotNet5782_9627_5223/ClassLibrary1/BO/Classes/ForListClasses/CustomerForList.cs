using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{

    /// <summary>
    /// the class CustomerForList contains all the baseStation's details
    /// that we want to show to the client.
    /// </summary>
    public class CustomerForList
    {

        private string id;
        private string name;
        private string phone;
        int amountOfSendAndSuppliedParcels;
        int amountOfSendAndNotSuppliedParcels;
        int amountOfGetParcels;
        int amountOfInPassingParcels;

        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                if (value.Length != 9)
                {
                    throw new BLStringIdException(value);
                }
                foreach (char digit in value)
                {
                    if (!Char.IsDigit(digit))
                    {
                        throw new BLStringIdException(value);

                    }
                }
                id = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                foreach (char letter in value)
                {
                    if (letter != ' ')
                    {
                        if (!Char.IsLetter(letter))
                        {
                            throw new BLStringException(value);
                        }
                    }
                }
                name = value;
            }
        }

        public string Phone
        {
            get
            {
                return phone;
            }
            set
            {
                if (value[0] != '0')
                    throw new BLPhoneException(value);
                foreach (char digit in value)
                {
                    if (!Char.IsDigit(digit))
                    {
                        throw new BLPhoneException(value);
                    }
                }
                phone = value;
            }
        }

        public int AmountOfSendAndSuppliedParcels
        {
            set
            {
                if (value < 0)
                    throw new AmountOfParcelsException(value);
                amountOfSendAndSuppliedParcels = value;
            }
            get
            {
                return amountOfSendAndSuppliedParcels;
            }
        }
        public int AmountOfGetParcels
        {
            set
            {
                if (value < 0)
                    throw new AmountOfParcelsException(value);
                amountOfGetParcels = value;
            }
            get
            {
                return amountOfGetParcels;
            }
        }
        public int AmountOfSendAndNotSuppliedParcels
        {
            set
            {
                if (value < 0)
                    throw new AmountOfParcelsException(value);
                amountOfSendAndNotSuppliedParcels = value;
            }
            get
            {
                return amountOfSendAndNotSuppliedParcels;
            }
        }
        public int AmountOfInPassingParcels
        {
            set
            {
                if (value < 0)
                    throw new AmountOfParcelsException(value);
                amountOfInPassingParcels = value;
            }
            get
            {
                return amountOfInPassingParcels;
            }
        }
 

        /// <summary>
        /// default constructor
        /// </summary>
        public CustomerForList() { }

        /// <summary>
        /// a constructor with parameters
        /// </summary>
        /// <param name="id">customer's id</param>
        /// <param name="name">customer's name</param>
        /// <param name="phone">customer's phone</param>
        /// <param name="amountOfSendAndSuppliedParcels">customer's amountOfSendAndSuppliedParcels</param>
        /// <param name="amountOfSendAndNotSuppliedParcels">customer's amountOfSendAndNotSuppliedParcels</param>
        /// <param name="amountOfGetParcels">customer's amountOfGetParcels</param>
        /// <param name="amountOfInPassingParcels">customer's amountOfInPassingParcels</param>
        public CustomerForList(string id, string name, string phone, int amountOfSendAndSuppliedParcels, int amountOfSendAndNotSuppliedParcels, int amountOfGetParcels, int amountOfInPassingParcels)
        {
            Id = id; Name = name; Phone = phone; AmountOfSendAndNotSuppliedParcels = amountOfSendAndSuppliedParcels;
            AmountOfSendAndNotSuppliedParcels = amountOfSendAndNotSuppliedParcels; AmountOfGetParcels = amountOfGetParcels;
            AmountOfInPassingParcels = amountOfInPassingParcels;
        }

        /// <summary>
        /// override ToString function.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"id: {Id} \n" +
                    $"name: {Name} \n" +
                    $"phone:  {Phone}\n" +
                    $"amount of 'send & supplied' parcels: {AmountOfSendAndSuppliedParcels}\n" +
                    $"amount of 'send & not supplied' parcels: {AmountOfSendAndNotSuppliedParcels}\n" +
                    $"amount of 'get' parcels: { AmountOfGetParcels}\n" +
                    $"amount of 'In Passing' parcels: {AmountOfInPassingParcels}";
        }

    }
    
}
