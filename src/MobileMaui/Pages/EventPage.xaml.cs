using System.Diagnostics;
using MobileMaui.Contracts.Events;

namespace MobileMaui.Pages;

public partial class EventPage : ContentPage
{
    private readonly IEventService _eventService;

    public EventPage(IEventService eventService)
    {
        InitializeComponent();

        _eventService = eventService;
    }

    public async Task InitializeAsync(long eventId)
    {
        try
        {
            var @event = await _eventService.GetAsync(eventId);

            BindingContext = @event;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error during initialization: {ex.Message}");
            await DisplayAlert("Ошибка", ex.Message, "OK");
        }
    }
}