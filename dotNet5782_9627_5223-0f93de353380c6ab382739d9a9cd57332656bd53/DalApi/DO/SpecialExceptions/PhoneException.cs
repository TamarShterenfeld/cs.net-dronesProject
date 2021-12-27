using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// a class that treats in exceptions of Phone type.
    /// </summary>
    [Serializable]
    public class PhoneException : Exception
    {
        public string Phone { get; set; }
        public PhoneException() : base() { }
        public PhoneException(string message, Exception inner) : base(message, inner) { }
        protected PhoneException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public PhoneException(string phone) { Phone = phone; }
        override public string ToString()
        {
            return "Phone Exception is thrown from DAL logic level because: " + Phone + " isn't a valid value";
        }

    }
}
