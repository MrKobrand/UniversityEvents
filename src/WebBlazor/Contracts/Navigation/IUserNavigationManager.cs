namespace WebBlazor.Contracts.Navigation;

/// <summary>
/// Менеджер навигации по пользователям.
/// </summary>
public interface IUserNavigationManager
{
    /// <summary>
    /// Переводит на страницу пользователя.
    /// </summary>
    /// <param name="userId">Уникальный идентификатор пользователя.</param>
    void NavigateToUserPage(long userId);
}
