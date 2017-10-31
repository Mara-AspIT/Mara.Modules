/**************************************************************************************************
*  Author: Mads Mikkel Rasmussen (mara@aspit.dk), github: https://github.com/Mara-AspIT/          *
*  Solution: .NET version: 4.7, C# version: 7.0                                                   *
*  Visual Studio version: Visual Studio Enterprise 2017, version 15.4.0                           *
*  Repository: Not yet available                                                                  *
**************************************************************************************************/
using System;
using System.Data;
using System.Data.SqlClient;

namespace Mara.Modules.DataAccess
{
    /// <summary>Executes provided queries against an SQL database.</summary>
    public class QueryExecutor
    {
        #region Constants
        /// <summary>The connection string.</summary>
        protected readonly string connectionString;
        #endregion


        #region Constructors
        /// <summary>Creates a new <see cref="QueryExecutor"/> object. The specified connection string is used to attempt to connect to a database, specified in the string.</summary>
        /// <param name="connectionString">The connection string used to connect to the specified database.</param>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="SqlException"/>
        /// <exception cref="InvalidOperationException"/>
        public QueryExecutor(string connectionString)
        {
            if(String.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Null, empty or white-space", nameof(connectionString));
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                connection.Close();
            }
            catch(Exception)
            {
                throw;
            }
        }
        #endregion


        #region Methods
        /// <summary>Executes the provided SQL query and returns a <see cref="DataSet"/> containg any data returned from the database. Will also work for non returnning queries such as INSERT, UPDATE etc. The returned <see cref="DataSet"/> will be empty in this case. </summary>
        /// <param name="sql">The SQL query to execute.</param>
        /// <returns>A <see cref="DataSet"/> containg any data returned from the database.</returns>
        /// /// <exception cref="ArgumentException"/>
        /// <exception cref="SqlException"/>
        /// <exception cref="InvalidOperationException"/>
        public virtual DataSet Execute(string sql)
        {
            if(String.IsNullOrWhiteSpace(sql))
                throw new ArgumentException("Null, empty or white-space", nameof(sql));
            try
            {
                DataSet resultSet = new DataSet();
                using(SqlDataAdapter adapter = new SqlDataAdapter(new SqlCommand(sql, new SqlConnection(connectionString))))
                {
                    adapter.Fill(resultSet);
                }
                return resultSet;
            }
            catch(Exception)
            {
                throw;
            }
        }
        #endregion
    }
}