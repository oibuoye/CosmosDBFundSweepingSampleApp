using System;
using System.Collections.Generic;
using System.Text;

namespace CosmosDBFundSweepingSampleApp.Core.Exceptions
{
    public class AccountDetailsAlreadyExistException : Exception
    {
        public AccountDetailsAlreadyExistException(string message) : base(message)
        {
        }

        public AccountDetailsAlreadyExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

    }
}
