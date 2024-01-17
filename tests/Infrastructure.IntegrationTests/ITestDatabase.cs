using System.Data.Common;
using System.Threading.Tasks;

namespace Infrastructure.IntegrationTests;

public interface ITestDatabase
{
    Task InitialiseAsync();

    DbConnection GetConnection();

    Task ResetAsync();

    Task DisposeAsync();
}
