using System.Collections.ObjectModel;
using System.Diagnostics;
using MobileMaui.Contracts.EventCategories;
using MobileMaui.Contracts.EventCategories.Dto;

namespace MobileMaui.Pages;

public partial class EventCategoriesPage : ContentPage
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IEventCategoryService _eventCategoryService;

    public ObservableCollection<EventCategoryDto> EventCategories { get; set; }

    public EventCategoriesPage(IServiceProvider serviceProvider, IEventCategoryService eventCategoryService)
    {
        InitializeComponent();

        _serviceProvider = serviceProvider;
        _eventCategoryService = eventCategoryService;

        EventCategories = new ObservableCollection<EventCategoryDto>();
        BindingContext = this;
    }

    public async Task InitializeAsync(long sectionId)
    {
        EventCategories.Clear();

        try
        {
            var categories = await _eventCategoryService.GetListAsync(sectionId: sectionId);

            foreach (var category in categories)
            {
                EventCategories.Add(category);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error during initialization: {ex.Message}");
            await DisplayAlert("Ошибка", ex.Message, "OK");
        }
    }

    private async void NavigateToCategoryEvents(object sender, EventArgs e)
    {
        var button = sender as Button;

        if (button?.CommandParameter is long categoryId)
        {
            var eventsPage = _serviceProvider.GetRequiredService<EventsPage>();
            await eventsPage.InitializeAsync(categoryId);
            await Navigation.PushAsync(eventsPage);
        }
    }
}