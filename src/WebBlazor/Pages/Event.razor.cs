using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using WebBlazor.Contracts.Events;
using WebBlazor.Contracts.Events.Dto;

namespace WebBlazor.Pages;

public partial class Event
{
    private bool _isLoadingFinish;
    private DetailedEventDto? _event = default!;

    /// <summary>
    /// Уникальный идентификатор раздела мероприятия.
    /// </summary>
    [Parameter]
    public long SectionId { get; set; }

    /// <summary>
    /// Уникальный идентификатор категории мероприятия.
    /// </summary>
    [Parameter]
    public long CategoryId { get; set; }

    /// <summary>
    /// Уникальный идентификатор мероприятия.
    /// </summary>
    [Parameter]
    public long EventId { get; set; }

    /// <summary>
    /// Сервис для работы с мероприятиями.
    /// </summary>
    [Inject]
    public IEventService EventService { get; set; } = default!;

    /// <inheritdoc/>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        _event = await EventService.GetAsync(EventId);
        _isLoadingFinish = true;
    }
}
