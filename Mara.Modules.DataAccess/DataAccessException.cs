/**************************************************************************************************
*  Author: Mads Mikkel Rasmussen (mara@aspit.dk), github: https://github.com/Mara-AspIT/          *
*  Solution: .NET version: 4.7.1, C# version: 7.1                                                 *
*  Visual Studio version: Visual Studio Enterprise 2017, version 15.4.5                           *
*  Repository: https://github.com/Mara-AspIT/Mara.Modules.git                                     *
**************************************************************************************************/
using System;

namespace Mara.Modules.DataAccess
{
    /// <summary>Thrown when a <see cref="System.Exception"/> or derivatives thereof, is generated. These exceptions can be found as the inner exception.</summary>
    [Serializable]
    internal class DataAccessException: Exception
    {

        /// <summary>Initializes a new instance of this type, with the provided message.</summary>
        /// <param name="message">The message explaining the cause of this exception.</param>
        public DataAccessException(string message) : base(message) { }

        /// <summary>Initializes a new instance of this type, with the provided message and inner exception.</summary>
        /// <param name="message">The message explaining the cause of this exception.</param>
        /// <param name="innerException">The inner exception wrapped in this exception.</param>
        public DataAccessException(string message, Exception innerException) : base(message, innerException) { }
    }
}