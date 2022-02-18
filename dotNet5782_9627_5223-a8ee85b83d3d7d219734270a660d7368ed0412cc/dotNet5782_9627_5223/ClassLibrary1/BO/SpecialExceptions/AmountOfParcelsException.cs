using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace BO
{
    /// <summary>
    /// a class that treats in exceptions of AmountOfParcel type.
    /// </summary>
    [Serializable]
    public class AmountOfParcelsException : Exception
    {
        public int Amount { get; set; }
        public AmountOfParcelsException() : base() { }
        public AmountOfParcelsException(string message) : base(message) { }
        public AmountOfParcelsException(string message, Exception inner) : base(message, inner) { }
        protected AmountOfParcelsException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public AmountOfParcelsException(int amount) { Amount = amount; }
        override public string ToString()
        {
            return "AmountOfParcels Exception is thrown from BL logic level because: " + Amount + " isn't a valid value";
        }


    }
}
