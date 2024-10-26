using MobileMaui.Contracts.EventSections;

namespace MobileMaui;

public partial class App : Application
{
    public App(IEventSectionService eventSectionService)
    {
        InitializeComponent();

        MainPage = new MainPage(eventSectionService);
        //MainPage = new AppShell();
    }
}
