/**************************************************************************************************
*  Author: Mads Mikkel Rasmussen (mara@aspit.dk), github: https://github.com/Mara-AspIT/          *
*  Solution: .NET version: 4.7.1, C# version: 7.1                                                 *
*  Visual Studio version: Visual Studio Enterprise 2017, version 15.4.5                           *
*  Repository: https://github.com/Mara-AspIT/Mara.Modules.git                                     *
**************************************************************************************************/
using System;

namespace Mara.Modules.DataAccess
{
    /// <summary>Abstract base class representing af repository of data.</summary>
    public abstract class DataRepository
    {
        /// <summary>The agregated instance of <see cref="Executor"/>.</summary>
        protected Executor executor;

        /// <summary>Creates a new <see cref="DataRepository"/> object when called from a deriving class. The specified connection string is passes directly to <see cref="Executor.Executor(string)"/> constructor.</summary>
        /// <param name="connectionString">The connection string used to connect to the specified database.</param>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="SqlException"/>
        /// <exception cref="InvalidOperationException"/>
        public DataRepository(string connectionString)
        {
            try
            {
                executor = new Executor(connectionString);
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}