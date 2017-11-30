/**************************************************************************************************
*  Author: Mads Mikkel Rasmussen (mara@aspit.dk), github: https://github.com/Mara-AspIT/          *
*  Solution: .NET version: 4.7.1, C# version: 7.1                                                 *
*  Visual Studio version: Visual Studio Enterprise 2017, version 15.4.5                           *
*  Repository: https://github.com/Mara-AspIT/Mara.Modules.git                                     *
**************************************************************************************************/
using System;
using System.Data;
using System.Data.SqlClient;

#if DEBUG
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Mara.Modules.Tests.DataAccess")]
#endif
namespace Mara.Modules.DataAccess
{
    /// <summary>Executes provided queries against an SQL database.</summary>
    public class Executor
    {
        #region Constants
        /// <summary>The connection string.</summary>
        protected readonly string connectionString;
        #endregion


        #region Constructors
        /// <summary>Creates a new <see cref="Executor"/> object. The specified connection string is used to attempt to connect to a database, specified in the string.</summary>
        /// <param name="connectionString">The connection string used to connect to the specified database.</param>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="DataAccessException"/>
        internal Executor(string connectionString)
        {
            if(String.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Null, empty or white-space",
                    nameof(connectionString));
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                connection.Close();
            }
            catch(Exception e)
            {
                throw new DataAccessException("See inner exception for details", e);
            }
            this.connectionString = connectionString;
        }
        #endregion


        #region Methods
        /// <summary>Executes the provided SQL query and returns a <see cref="DataSet"/> containing any data returned from the database. Will also work for non returnning queries such as INSERT, UPDATE etc. The returned <see cref="DataSet"/> will be empty in this case.</summary>
        /// <param name="sqlQuery">The SQL query to execute.</param>
        /// <returns>A <see cref="DataSet"/> containing any data returned from the database.</returns>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="SqlException"/>
        /// <exception cref="InvalidOperationException"/>
        public virtual DataSet Execute(string sqlQuery)
        {
            if(String.IsNullOrWhiteSpace(sqlQuery))
                throw new ArgumentException("Null, empty or white-space", nameof(sqlQuery));
            try
            {
                return Process(new SqlCommand(sqlQuery));
            }
            catch(Exception e)
            {
                throw;
            }
        }

        /// <summary>Executes the stored procedure with any provided parameters, and returns any data if found.</summary>
        /// <param name="storedProcedureName">The name of the stored procedure to call.</param>
        /// <param name="storedProcedureParameters">Parameters to the stored procedure. Must have same formal positions as the storedprocedure itself.</param>
        /// <returns>A <see cref="DataSet"/> containing any data returned from the database.</returns>
        public virtual DataSet Execute(string storedProcedureName, params string[] storedProcedureParameters)
        {
            if(String.IsNullOrWhiteSpace(storedProcedureName))
                throw new ArgumentException("Null, empty or white-space", nameof(storedProcedureName));
            try
            {
                DataSet resultSet = new DataSet();
                using(SqlCommand command = new SqlCommand(storedProcedureName))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    if(storedProcedureParameters.Length > 0)    // params array is always not null.
                    {
                        command.Parameters.AddRange(storedProcedureParameters);
                    }
                    resultSet = Process(command);
                }
                return resultSet;
            }
            catch(Exception)
            {
                throw;
            }
        }

        /// <summary>Processes the provided <see cref="SqlCommand"/>.</summary>
        /// <param name="command">The <see cref="SqlCommand"/> to execute against the database.</param>
        /// <returns>A <see cref="DataSet"/> containing any data returned from the database.</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="SqlException"/>
        protected virtual DataSet Process(SqlCommand command)
        {
            if(command is null)
                throw new ArgumentNullException(nameof(command));
            if(command.Connection is null)
                command.Connection = new SqlConnection(connectionString);
            DataSet resultSet = new DataSet();
            using(SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(resultSet);
            }
            return resultSet;
        }
        #endregion
    }
}