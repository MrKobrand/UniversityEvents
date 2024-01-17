using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.EventSections;
using Application.Contracts.EventSections.Dto;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IntegrationTests.EventSections;

public class DeleteTests : BaseTestFixture
{
    [Test]
    public async Task DeleteAsync_EventSectionDoesNotExist_ReturnsNull()
    {
        var eventSectionDto = await DeleteAsync(0);

        eventSectionDto.Should().BeNull();
    }

    [Test]
    public async Task DeleteAsync_EventSectionExists_RemovesAndReturnsEventSection()
    {
        var eventSection = new EventSection
        {
            Name = "Test",
            Order = 0,
            Description = "TestDescription",
        };

        await AddAsync(eventSection);

        var eventSectionDto = await DeleteAsync(eventSection.Id);

        eventSectionDto.Should().NotBeNull();
        eventSectionDto!.Id.Should().Be(eventSection.Id);
        eventSectionDto!.Name.Should().Be(eventSection.Name);
        eventSectionDto!.Order.Should().Be(eventSection.Order);
        eventSectionDto!.Description.Should().Be(eventSection.Description);

        var eventSectionsCount = await CountAsync<EventSection>();
        eventSectionsCount.Should().Be(0);
    }

    private static Task<EventSectionDto?> DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        using var scope = ScopeFactory.CreateScope();

        var eventSectionService = scope.ServiceProvider.GetRequiredService<IEventSectionService>();

        return eventSectionService.DeleteAsync(id, cancellationToken);
    }
}
