using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// a class that treats in exceptions of string type.
    /// </summary>
    [Serializable]
    public class BLStringException : Exception
    {
        public string Str { get; set; }
        public BLStringException() : base() { }
        public BLStringException(string message, Exception inner) : base(message, inner) { }
        protected BLStringException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public BLStringException(string str) { Str = str; }
        override public string ToString()
        {
            return "String Exception is thrown from BL logic level because: " + Str + " isn't a valid value";
        }

    }
}