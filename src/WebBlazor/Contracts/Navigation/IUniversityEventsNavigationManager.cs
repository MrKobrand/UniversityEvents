namespace WebBlazor.Contracts.Navigation;

/// <summary>
/// Менеджер навигации по ключевым страницам UniversityEvents.
/// </summary>
public interface IUniversityEventsNavigationManager
{
    /// <summary>
    /// Переводит на страницу логина.
    /// </summary>
    void NavigateToLoginPage();

    /// <summary>
    /// Переводит на домашнюю страницу.
    /// </summary>
    void NavigateToHomePage();
}
