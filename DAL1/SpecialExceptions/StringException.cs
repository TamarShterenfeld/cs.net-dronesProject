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
        public class StringException : Exception
        {
            public StringException() : base() { }

          //  public StringIdExceptioin(string message) : base(message) { }
            public StringException(string message, Exception inner) : base(message, inner) { }
            protected StringException(SerializationInfo info, StreamingContext context) : base(info, context) { }
            public StringException(string str) { }
            override public string ToString()
            {
                return "String id Exceptioin : DAL logic level throws a string id exception " + Message;
            }

        }
    }
}