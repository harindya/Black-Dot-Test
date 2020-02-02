using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Bds.Tech.Test.Model
{
    [Serializable]
    public class BlackDotTestException : ApplicationException
    {
        public BlackDotTestException() 
        { }

        public BlackDotTestException(string message) : base(message) 
        { }

        public BlackDotTestException(string message, Exception inner) : base(message, inner) 
        { }

        protected BlackDotTestException(SerializationInfo info, StreamingContext context) : base(info, context) 
        { }
    }
}
