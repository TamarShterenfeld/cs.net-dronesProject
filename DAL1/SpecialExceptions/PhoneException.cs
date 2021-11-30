using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    namespace DO
    {
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
                return "Int Id Exceptioin : DAL logic level throws an int id exception " + Message;
            }

        }
    }
}