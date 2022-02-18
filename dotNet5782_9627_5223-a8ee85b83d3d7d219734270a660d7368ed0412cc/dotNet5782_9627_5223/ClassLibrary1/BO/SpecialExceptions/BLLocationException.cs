using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// a class that treats in exceptions of Location type.
    /// </summary>
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
            return "Location Exception is thrown from BL logic level because: " + Location + " isn't a valid value";
        }

    }
}