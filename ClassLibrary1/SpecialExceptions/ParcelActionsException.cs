using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// a class that treats in exceptions of ParcelActions type.
        /// </summary>
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
                return "ParcdlActions Exception is thrown from BL logic level because: " + Action + " isn't a valid value";
            }


        }
    }
}
