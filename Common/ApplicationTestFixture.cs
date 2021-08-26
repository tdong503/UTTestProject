using System;
using System.Threading.Tasks;

namespace Common
{
    public class ApplicationTestFixture : IAsyncDisposable
    {
        public ValueTask DisposeAsync()
        {
            //var database = SqlDatabaseHandler.Create();
            throw new NotImplementedException();
        }
    }
}