using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mara.Modules.DataAccess;

namespace Mara.Modules.Tests.DataAccess
{
    internal class TestRepo: DataRepository
    {
        public TestRepo(string connectionString) : base(connectionString)
        {
        }
    }
}
