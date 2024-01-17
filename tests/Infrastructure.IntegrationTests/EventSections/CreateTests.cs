using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.EventSections;
using Application.Contracts.EventSections.Dto;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IntegrationTests.EventSections;

public class CreateTests : BaseTestFixture
{
    [Test]
    public async Task CreateAsync_CorrectArguments_CreatesAndReturnsEventSection()
    {
        var name = "Test";
        var description = "TestDescription";

        var result = await CreateAsync(name, description);

        result.Should().NotBeNull();
        result.Id.Should().BeGreaterThan(0);
        result.Name.Should().Be(name);
        result.Order.Should().Be(0);
        result.Description.Should().Be(description);
    }

    private static Task<EventSectionDto> CreateAsync(
        string name,
        string? description = null,
        CancellationToken cancellationToken = default)
    {
        using var scope = ScopeFactory.CreateScope();

        var eventSectionService = scope.ServiceProvider.GetRequiredService<IEventSectionService>();

        return eventSectionService.CreateAsync(name, description, cancellationToken);
    }
}
