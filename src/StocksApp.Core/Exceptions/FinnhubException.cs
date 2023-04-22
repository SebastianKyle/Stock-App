using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StocksApp.Core.Exceptions
{
    /// <summary>
    /// Represents a database connection or database updation failure
    /// </summary>
    public class FinnhubException : Exception
    {
        public FinnhubException()
        {

        } 

        public FinnhubException(string message) : base(message)
        {

        }

        public FinnhubException(string message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}