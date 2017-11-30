/**************************************************************************************************
*  Author: Mads Mikkel Rasmussen (mara@aspit.dk), github: https://github.com/Mara-AspIT/          *
*  Solution: .NET version: 4.7.1, C# version: 7.1                                                 *
*  Visual Studio version: Visual Studio Enterprise 2017, version 15.4.5                           *
*  Repository: https://github.com/Mara-AspIT/Mara.Modules.git                                     *
**************************************************************************************************/
using System;
using System.Xml;

namespace Mara.Modules.DataAccess
{
    /// <summary>Abstract base class representing af repository of data.</summary>
    public abstract class RepositoryBase
    {
        #region Fields
        /// <summary>The aggregated instance of <see cref="Executor"/>.</summary>
        protected Executor executor;
        #endregion


        #region Constructors
        /// <summary>Creates a new <see cref="RepositoryBase"/> object when called from a deriving class. The specified connection string is passes directly to <see cref="Executor.Executor(String)"/> constructor.</summary>
        /// <param name="databaseName">The name of the database to conect to.</param>
        /// <param name="configurationFilePath">The path to the configuration file.</param>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="SqlException"/>
        /// <exception cref="InvalidOperationException"/>
        protected RepositoryBase(string connectionStringName, string configurationFilePath)
        {
            try
            {
                string connectionString = LoadConnectionString(connectionStringName, configurationFilePath);
                executor = new Executor(connectionString);
            }
            catch(Exception)
            {
                throw;
            }
        }
        #endregion


        #region Methods
        /// <summary>Loads the connection string from the specified configuration file.</summary>
        /// <param name="connectionStringName">The name of the connection string in the configuaration file.</param>
        /// <param name="configurationFilePath">The path to the configuration file. Can be both your app.config or aany external config file.</param>
        /// <returns>The connection string, belonging to the prvide connection string name.</returns>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="DataAccessException"/>
        protected virtual string LoadConnectionString(string connectionStringName, string configurationFilePath)
        {
            if(String.IsNullOrWhiteSpace(connectionStringName))
                throw new ArgumentException("Null, empty or white-space", nameof(connectionStringName));
            else if(String.IsNullOrWhiteSpace(configurationFilePath))
                throw new ArgumentException("Null, empty or white-space", nameof(configurationFilePath));
            string connectionString = String.Empty;
            try
            {
                // TODO: Måske smartere med System.Configuration, men så skal der linkes fra consumers project app.config fil til consumers *.config med connectionStrings. Måske bør consumer få valget om linkning.
                const string nodeXPath = "/Configuration/connectionStrings/add";
                XmlDocument configDoc = new XmlDocument();
                configDoc.Load(configurationFilePath);
                XmlNodeList connectionStringsNodes = configDoc.SelectNodes(nodeXPath);
                foreach(XmlNode node in connectionStringsNodes)
                {
                    XmlAttributeCollection attributes = node.Attributes;
                    XmlAttribute connectionStringNameAttribute = attributes["name"];
                    if(connectionStringNameAttribute.Value == connectionStringName)
                    {
                        connectionString = attributes["connectionString"].Value;
                        break;
                    }
                }
                if(connectionString != String.Empty)
                    return connectionString;
                else
                    throw new DataAccessException("Could not find connection string.");
            }
            catch(DataAccessException)
            {
                throw;
            }
            catch(Exception e)
            {
                throw new DataAccessException("See inner exception for details", e);
            }
        } 
        #endregion
    }
}