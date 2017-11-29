/**************************************************************************************************
*  Author: Mads Mikkel Rasmussen (mara@aspit.dk), github: https://github.com/Mara-AspIT/          *
*  Solution: .NET version: 4.7.1, C# version: 7.1                                                 *
*  Visual Studio version: Visual Studio Enterprise 2017, version 15.4.5                           *
*  Repository: https://github.com/Mara-AspIT/Mara.Modules.git                                     *
**************************************************************************************************/
using System;
using System.Runtime.Serialization;

namespace Mara.Modules.DataAccess
{
    [Serializable]
    internal class DataAccessException: Exception
    {
        public DataAccessException()
        {
        }

        public DataAccessException(string message) : base(message)
        {
        }

        public DataAccessException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DataAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}