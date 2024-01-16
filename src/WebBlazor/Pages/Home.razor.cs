using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using WebBlazor.Contracts.EventSections;
using WebBlazor.Contracts.EventSections.Dto;
using WebBlazor.Contracts.Navigation;

namespace WebBlazor.Pages;

public partial class Home
{
    private bool _isLoadingFinish;
    private List<EventSectionDto> _eventSections = default!;

    /// <summary>
    /// Сервис для работы с разделами мероприятий.
    /// </summary>
    [Inject]
    public IEventSectionService EventSectionService { get; set; } = default!;

    /// <summary>
    /// Менеджер навигации по категориям мероприятий.
    /// </summary>
    [Inject]
    public IEventCategoryNavigationManager EventCategoryNavigationManager { get; set; } = default!;

    /// <inheritdoc/>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        _eventSections = await EventSectionService.GetListAsync();
        _isLoadingFinish = true;
    }

    private void NavigateToSectionCategories(long sectionId)
    {
        EventCategoryNavigationManager.NavigateToCategoriesPage(sectionId);
    }
}
