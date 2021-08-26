using System;
using System.Threading.Tasks;
using Xunit;

namespace Common
{
    public class ApplicationTestBase : IAsyncLifetime, IAsyncDisposable
    {
        private readonly ApplicationTestFixture fixture;
        
        public Task InitializeAsync()
        {
            return Task.CompletedTask;
        }

        async Task IAsyncLifetime.DisposeAsync()
        {
            await DisposeAsync();
        }

        public virtual async ValueTask DisposeAsync()
        {
            await fixture.DisposeAsync();
            GC.SuppressFinalize(this);
        }
    }
}