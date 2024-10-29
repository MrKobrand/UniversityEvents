using System.Collections.ObjectModel;
using System.Diagnostics;
using MobileMaui.Contracts.Events;
using MobileMaui.Contracts.Events.Dto;

namespace MobileMaui.Pages;

public partial class EventsPage : ContentPage
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IEventService _eventService;

    public ObservableCollection<DetailedEventDto> Events { get; set; }

    public EventsPage(IServiceProvider serviceProvider, IEventService eventService)
    {
        InitializeComponent();

        _serviceProvider = serviceProvider;
        _eventService = eventService;

        Events = new ObservableCollection<DetailedEventDto>();
        BindingContext = this;
    }

    public async Task InitializeAsync(long categoryId)
    {
        Events.Clear();

        try
        {
            var events = await _eventService.GetListAsync(categoryId: categoryId);

            foreach (var @event in events)
            {
                Events.Add(@event);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error during initialization: {ex.Message}");
            await DisplayAlert("Ошибка", ex.Message, "OK");
        }
    }

    private async void NavigateToEvent(object sender, EventArgs e)
    {
        var button = sender as Button;

        if (button?.CommandParameter is long eventId)
        {
            var eventPage = _serviceProvider.GetRequiredService<EventPage>();
            await eventPage.InitializeAsync(eventId);
            await Navigation.PushAsync(eventPage);
        }
    }
}