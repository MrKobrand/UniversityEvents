using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.EventSections;
using Application.Contracts.EventSections.Dto;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IntegrationTests.EventSections;

public class GetListTests : BaseTestFixture
{
    [Test]
    public async Task GetListAsync_EventSectionsDoNotExist_ReturnsEmptyList()
    {
        var eventSections = await GetListAsync();

        eventSections.Should().BeEmpty();
    }

    [Test]
    public async Task GetListAsync_EventSectionsExist_ReturnsEventSections()
    {
        var firstEventSection = new EventSection
        {
            Name = "FirstTest",
            Order = 0,
            Description = "FirstTestDescription",
        };

        var secondEventSection = new EventSection
        {
            Name = "SecondTest",
            Order = 1,
            Description = "SecondTestDescription",
        };

        await AddAsync(firstEventSection);
        await AddAsync(secondEventSection);

        var eventSections = await GetListAsync();

        eventSections.Should().HaveCount(2).And.SatisfyRespectively(
            firstItem =>
            {
                firstItem.Id.Should().Be(firstEventSection.Id);
                firstItem.Name.Should().Be(firstEventSection.Name);
                firstItem.Order.Should().Be(firstEventSection.Order);
                firstItem.Description.Should().Be(firstEventSection.Description);
            },
            secondItem =>
            {
                secondItem.Id.Should().Be(secondEventSection.Id);
                secondItem.Name.Should().Be(secondEventSection.Name);
                secondItem.Order.Should().Be(secondEventSection.Order);
                secondItem.Description.Should().Be(secondEventSection.Description);
            });
    }

    [Test]
    public async Task GetListAsync_LimitSetToOne_ReturnsOnlyOneEventSection()
    {
        var firstEventSection = new EventSection
        {
            Name = "FirstTest",
            Order = 0,
            Description = "FirstTestDescription",
        };

        var secondEventSection = new EventSection
        {
            Name = "SecondTest",
            Order = 1,
            Description = "SecondTestDescription",
        };

        await AddAsync(firstEventSection);
        await AddAsync(secondEventSection);

        var eventSections = await GetListAsync(1);

        eventSections.Should().HaveCount(1).And.SatisfyRespectively(
            firstItem =>
            {
                firstItem.Id.Should().Be(firstEventSection.Id);
                firstItem.Name.Should().Be(firstEventSection.Name);
                firstItem.Order.Should().Be(firstEventSection.Order);
                firstItem.Description.Should().Be(firstEventSection.Description);
            });
    }

    [Test]
    public async Task GetListAsync_SearchNameSet_ReturnsOnlyOneEventSection()
    {
        var firstEventSection = new EventSection
        {
            Name = "FirstTest",
            Order = 0,
            Description = "FirstTestDescription",
        };

        var secondEventSection = new EventSection
        {
            Name = "SecondTest",
            Order = 1,
            Description = "SecondTestDescription",
        };

        await AddAsync(firstEventSection);
        await AddAsync(secondEventSection);

        var eventSections = await GetListAsync(search: firstEventSection.Name);

        eventSections.Should().HaveCount(1).And.SatisfyRespectively(
            firstItem =>
            {
                firstItem.Id.Should().Be(firstEventSection.Id);
                firstItem.Name.Should().Be(firstEventSection.Name);
                firstItem.Order.Should().Be(firstEventSection.Order);
                firstItem.Description.Should().Be(firstEventSection.Description);
            });
    }

    private static Task<List<EventSectionDto>> GetListAsync(
        int? limit = null,
        string? search = null,
        CancellationToken cancellationToken = default)
    {
        using var scope = ScopeFactory.CreateScope();

        var eventSectionService = scope.ServiceProvider.GetRequiredService<IEventSectionService>();

        return eventSectionService.GetListAsync(limit, search, cancellationToken);
    }
}
