using System.Collections.ObjectModel;
using System.Diagnostics;
using MobileMaui.Contracts.EventSections;
using MobileMaui.Contracts.EventSections.Dto;
using MobileMaui.Pages;

namespace MobileMaui;

public partial class MainPage : ContentPage
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IEventSectionService _eventSectionService;

    public ObservableCollection<EventSectionDto> EventSections { get; set; }

    public MainPage(IServiceProvider serviceProvider, IEventSectionService eventSectionService)
    {
        InitializeComponent();

        _serviceProvider = serviceProvider;
        _eventSectionService = eventSectionService;

        EventSections = new ObservableCollection<EventSectionDto>();
        BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await InitializeAsync();
    }

    private async Task InitializeAsync()
    {
        EventSections.Clear();

        try
        {
            var sections = await _eventSectionService.GetListAsync();

            foreach (var section in sections)
            {
                EventSections.Add(section);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error during an initialization: {ex.Message}");
            await DisplayAlert("Ошибка", ex.Message, "OK");
        }
    }

    private async void NavigateToSectionCategories(object sender, EventArgs e)
    {
        var button = sender as Button;

        if (button?.CommandParameter is long sectionId)
        {
            var eventCategoriesPage = _serviceProvider.GetRequiredService<EventCategoriesPage>();
            await eventCategoriesPage.InitializeAsync(sectionId);
            await Navigation.PushAsync(eventCategoriesPage);
        }
    }
}