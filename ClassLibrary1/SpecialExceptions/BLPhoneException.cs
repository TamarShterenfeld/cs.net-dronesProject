using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// a class that treats in exceptions of phone type.
        /// </summary>
        [Serializable]
        public class BLPhoneException : Exception
        {
            public string Phone { set; get; }
            public BLPhoneException() : base() { }
            public BLPhoneException(string message, Exception inner) : base(message, inner) { }
            protected BLPhoneException(SerializationInfo info, StreamingContext context) : base(info, context) { }

            public BLPhoneException(string phone) { Phone = phone; }
            override public string ToString()
            {
                return "Phone Exception is thrown from BL logic level because: " + Phone + " isn't a valid value";
            }


        }
    }
}
