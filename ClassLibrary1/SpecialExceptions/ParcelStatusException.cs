using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace IBL
{
    namespace BO
    {
        [Serializable]
        public class ParcelStatusException : Exception
        {
            public ParcelStatusException() : base() { }
            public ParcelStatusException(string message) : base(message) { }
            public ParcelStatusException(string message, Exception inner) : base(message, inner) { }
            protected ParcelStatusException(SerializationInfo info, StreamingContext context) : base(info, context) { }

            public ParcelStatusException(ParcelStatuses statuse) { }
            override public string ToString()
            {
                return "OverloadCapacityException: DAL capacity of " + " overloaded\n" + Message;
            }


        }
    }
}
