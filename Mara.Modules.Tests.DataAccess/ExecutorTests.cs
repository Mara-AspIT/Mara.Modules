using System;
using Mara.Modules.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace Mara.Modules.Tests.DataAccess
{
    [TestClass]
    public class ExecutorTests
    {
        [TestMethod]
        public void RepositoryBaseInitializes()
        {
            // Arrange:
            string connectionStringName = "CompanyDB";
            string configFilePath = @"C:\dev\mara\projekter\Mara.Modules\Mara.Modules.Tests.DataAccess\test.config";
            
            try
            {
                // Act:
                TestRepo t = new TestRepo(connectionStringName, configFilePath);
            }
            catch(Exception e)
            {
                // Assert:
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void DataReturnedFromDB()
        {
            DataSet d = (new Executor(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CompanyDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")).Execute("SELECT * FROM Employees");
            Assert.AreNotEqual(0, d.Tables[0].Rows.Count);
        }
    }
}
