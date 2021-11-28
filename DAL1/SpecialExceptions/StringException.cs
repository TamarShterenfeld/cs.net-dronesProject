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
            public string Id { get; set; }
            public StringException() : base() { }
            public StringException(string message, Exception inner) : base(message, inner) { }
            protected StringException(SerializationInfo info, StreamingContext context) : base(info, context) { }
            public StringException(string str) { Id = str; }
            override public string ToString()
            {
                return "String id Exceptioin : DAL logic level throws a string id exception " + Message;
            }

        }
    }
}