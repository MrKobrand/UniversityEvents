using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using WebBlazor.Contracts.EventCategories;
using WebBlazor.Contracts.EventCategories.Dto;
using WebBlazor.Contracts.Navigation;

namespace WebBlazor.Pages;

public partial class EventCategories
{
    private bool _isLoadingFinish;
    private List<DetailedEventCategoryDto> _eventCategories = default!;

    /// <summary>
    /// Уникальный идентификатор раздела мероприятия.
    /// </summary>
    [Parameter]
    public long SectionId { get; set; }

    /// <summary>
    /// Сервис для работы с категориями мероприятий.
    /// </summary>
    [Inject]
    public IEventCategoryService EventCategoryService { get; set; } = default!;

    /// <summary>
    /// Менеджер навигации по мероприятиям.
    /// </summary>
    [Inject]
    public IEventNavigationManager EventNavigationManager { get; set; } = default!;

    /// <inheritdoc/>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        _eventCategories = await EventCategoryService.GetListAsync(sectionId: SectionId);
        _isLoadingFinish = true;
    }

    private void NavigateToCategoryEvents(long categoryId)
    {
        EventNavigationManager.NavigateToEventsPage(SectionId, categoryId);
    }
}
