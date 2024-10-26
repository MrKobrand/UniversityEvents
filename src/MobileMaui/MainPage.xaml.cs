using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using MobileMaui.Contracts.EventSections;
using MobileMaui.Contracts.EventSections.Dto;

namespace MobileMaui;

public partial class MainPage : ContentPage
{
    private readonly IEventSectionService _eventSectionService;

    public ObservableCollection<EventSectionDto> EventSections { get; set; }
    public ICommand NavigateToSectionCategoriesCommand { get; }

    public MainPage(IEventSectionService eventSectionService)
    {
        InitializeComponent();
        _eventSectionService = eventSectionService;

        EventSections = new ObservableCollection<EventSectionDto>();
        NavigateToSectionCategoriesCommand = new Command<int>(NavigateToSectionCategories);
        BindingContext = this;
    }

    private async Task InitializeAsync()
    {
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
            // Логируем ошибку
            Debug.WriteLine($"Error during an initialization: {ex.Message}");
            await DisplayAlert("Ошибка", ex.Message, "OK");
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await InitializeAsync();
    }

    private void NavigateToSectionCategories(int sectionId)
    {
        // Логика для перехода в категории раздела
    }
}