using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        [Serializable]
        public class BLLocationException : Exception
        {
            public double Location { get; set; }
            public BLLocationException() : base() { }
            public BLLocationException(string message) : base(message) { }
            public BLLocationException(string message, Exception inner) : base(message, inner) { }
            protected BLLocationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
            public BLLocationException(double location) { Location = location; }
            override public string ToString()
            {
                return "Int Id Exceptioin : DAL logic level throws an int id exception " + Message;
            }

        }
    }
}