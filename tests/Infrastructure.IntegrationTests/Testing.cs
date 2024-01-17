using System;
using System.Threading.Tasks;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IntegrationTests;

[SetUpFixture]
public partial class Testing
{
    private static ITestDatabase Database = default!;
    private static CustomWebApplicationFactory Factory = default!;
    public static IServiceScopeFactory ScopeFactory = default!;

    [OneTimeSetUp]
    public async Task RunBeforeAnyTests()
    {
        Database = await TestDatabaseFactory.CreateAsync();
        Factory = new CustomWebApplicationFactory(Database.GetConnection());

        ScopeFactory = Factory.Services.GetRequiredService<IServiceScopeFactory>();
    }

    [OneTimeTearDown]
    public async Task RunAfterAnyTests()
    {
        await Database.DisposeAsync();
        await Factory.DisposeAsync();
    }

    public Testing()
    {
    }

    public static async Task ResetState()
    {
        try
        {
            await Database.ResetAsync();
            ScopeFactory = Factory.Services.GetRequiredService<IServiceScopeFactory>();
        }
        catch (Exception)
        {
        }
    }

    public static async Task<TEntity?> FindAsync<TEntity>(params object[] keyValues)
        where TEntity : class
    {
        using var scope = ScopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<UniversityEventsDbContext>();

        return await context.FindAsync<TEntity>(keyValues);
    }

    public static async Task AddAsync<TEntity>(TEntity entity)
        where TEntity : class
    {
        using var scope = ScopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<UniversityEventsDbContext>();

        await context.AddAsync(entity);

        await context.SaveChangesAsync();
    }

    public static async Task<int> CountAsync<TEntity>() where TEntity : class
    {
        using var scope = ScopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<UniversityEventsDbContext>();

        return await context.Set<TEntity>().CountAsync();
    }
}
