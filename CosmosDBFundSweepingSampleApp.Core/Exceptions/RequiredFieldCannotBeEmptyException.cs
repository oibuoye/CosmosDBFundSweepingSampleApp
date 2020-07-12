using System;
using System.Collections.Generic;
using System.Text;

namespace CosmosDBFundSweepingSampleApp.Core.Exceptions
{
    public class ModelIsNullException : Exception
    {
        public ModelIsNullException(string message) : base(message)
        {
        }

        public ModelIsNullException(string message, Exception innerException) : base(message, innerException)
        {
        }

    }
}
