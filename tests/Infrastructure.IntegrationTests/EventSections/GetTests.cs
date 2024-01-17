using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.EventSections;
using Application.Contracts.EventSections.Dto;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IntegrationTests.EventSections;

public class GetTests : BaseTestFixture
{
    [Test]
    public async Task GetAsync_EventSectionDoesNotExist_ReturnsNull()
    {
        var eventSectionDto = await GetAsync(0);

        eventSectionDto.Should().BeNull();
    }

    [Test]
    public async Task GetAsync_EventSectionExists_ReturnsEventSection()
    {
        var eventSection = new EventSection
        {
            Name = "Test",
            Order = 0,
            Description = "TestDescription",
        };

        await AddAsync(eventSection);

        var eventSectionDto = await GetAsync(eventSection.Id);

        eventSectionDto.Should().NotBeNull();
        eventSectionDto!.Id.Should().Be(eventSection.Id);
        eventSectionDto!.Name.Should().Be(eventSection.Name);
        eventSectionDto!.Order.Should().Be(eventSection.Order);
        eventSectionDto!.Description.Should().Be(eventSection.Description);
    }

    private static Task<EventSectionDto?> GetAsync(long id, CancellationToken cancellationToken = default)
    {
        using var scope = ScopeFactory.CreateScope();

        var eventSectionService = scope.ServiceProvider.GetRequiredService<IEventSectionService>();

        return eventSectionService.GetAsync(id, cancellationToken);
    }
}
