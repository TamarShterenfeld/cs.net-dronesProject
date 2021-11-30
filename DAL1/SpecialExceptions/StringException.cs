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
        /// <summary>
        /// a class that treats in exceptions of String type.
        /// </summary>
        [Serializable]
        public class StringException : Exception
        {
            public string Str { get; set; }
            public StringException() : base() { }
            public StringException(string message, Exception inner) : base(message, inner) { }
            protected StringException(SerializationInfo info, StreamingContext context) : base(info, context) { }
            public StringException(string str) { Str = str; }
            override public string ToString()
            {
                return "String Exception is thrown from DAL logic level because: " + Str + " isn't a valid value";
            }

        }
    }
}