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
        public class IntIndexException : Exception
        {
            public IntIndexException() : base() { }
            public IntIndexException(string message) : base(message) { }
            public IntIndexException(string message, Exception inner) : base(message, inner) { }
            protected IntIndexException(SerializationInfo info, StreamingContext context) : base(info, context) { }
            public IntIndexException(int index) { }
            override public string ToString()
            {
                return "Int Id Exceptioin : DAL logic level throws an int id exception " + Message;
            }

        }
    }
}