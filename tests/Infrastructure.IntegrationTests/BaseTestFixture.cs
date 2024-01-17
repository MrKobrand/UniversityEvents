using System.Threading.Tasks;

namespace Infrastructure.IntegrationTests;

[TestFixture]
public abstract class BaseTestFixture
{
    [SetUp]
    public async Task TestSetUpAsync()
    {
        await ResetState();
    }
}
