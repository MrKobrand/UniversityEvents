using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using WebBlazor.Contracts.Events;
using WebBlazor.Contracts.Events.Dto;
using WebBlazor.Contracts.Navigation;

namespace WebBlazor.Pages;

public partial class Events
{
    private bool _isLoadingFinish;
    private List<DetailedEventDto> _events = default!;

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
    /// Сервис для работы с мероприятиями.
    /// </summary>
    [Inject]
    public IEventService EventService { get; set; } = default!;

    /// <summary>
    /// Менеджер навигации по мероприятиям.
    /// </summary>
    [Inject]
    public IEventNavigationManager EventNavigationManager { get; set; } = default!;

    /// <inheritdoc/>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        _events = await EventService.GetListAsync(categoryId: CategoryId);
        _isLoadingFinish = true;
    }

    private void NavigateToEvent(long eventId)
    {
        EventNavigationManager.NavigateToEventPage(SectionId, CategoryId, eventId);
    }
}
