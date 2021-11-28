using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace IBL
{
    namespace BO
    {
        [Serializable]
        public class ParcelActionsException : Exception
        {
            public ParcelActions Action { get; set; }
            public ParcelActionsException() : base() { }
            public ParcelActionsException(string message) : base(message) { }
            public ParcelActionsException(string message, Exception inner) : base(message, inner) { }
            protected ParcelActionsException(SerializationInfo info, StreamingContext context) : base(info, context) { }

            public ParcelActionsException(ParcelActions action) { Action = action; }
            override public string ToString()
            {
                return "OverloadCapacityException: DAL capacity of " + " overloaded\n" + Message;
            }


        }
    }
}
