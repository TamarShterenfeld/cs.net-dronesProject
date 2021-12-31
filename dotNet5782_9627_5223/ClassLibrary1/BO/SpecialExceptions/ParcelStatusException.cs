using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace BO
{
    /// <summary>
    /// a class that treats in exceptions of ParcelStatuses type.
    /// </summary>
    [Serializable]
    public class ParcelStatusException : Exception
    {
        public ParcelStatuses ParcelStatus { get; set; }
        public ParcelStatusException() : base() { }
        public ParcelStatusException(string message) : base(message) { }
        public ParcelStatusException(string message, Exception inner) : base(message, inner) { }
        protected ParcelStatusException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public ParcelStatusException(ParcelStatuses status) { ParcelStatus = status; }
        override public string ToString()
        {
            return "ParcelStatus Exception is thrown from BL logic level because: " + ParcelStatus + " isn't a valid value";
        }


    }
}
