using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mara.Modules.DataAccess;

namespace Mara.Modules.Tests.DataAccess
{
    internal class TestRepo: RepositoryBase
    {
        public TestRepo(string connectionStringName, string configFilePath) : base(connectionStringName, configFilePath)
        {
        }

        public void M()
        {
            executor.Execute("SELECT * FROM Employees");
            executor.Execute("", "", "", "");
        }
    }
}
