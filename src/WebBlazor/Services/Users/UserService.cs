using System.Threading;
using System.Threading.Tasks;
using WebBlazor.Contracts.Users;
using WebBlazor.Contracts.Users.Dto;
using WebBlazor.Data;

namespace WebBlazor.Services.Users;

/// <summary>
/// Сервис для работы с пользователями.
/// </summary>
public class UserService : IUserService
{
    private readonly IHttpRepository _httpRepository;

    private const string USER = "api/users/{0}";

    /// <summary>
    ///  Конструктор, подтягивающий зависимости через DI.
    /// </summary>
    /// <param name="httpRepository">Http-репозиторий.</param>
    public UserService(IHttpRepository httpRepository)
    {
        _httpRepository = httpRepository;
    }

    /// <inheritdoc/>
    public Task<DetailedUserDto?> GetAsync(long id, CancellationToken cancellationToken = default)
    {
        return _httpRepository.GetRequestAsync<DetailedUserDto>(
            string.Format(USER, id), cancellationToken: cancellationToken);
    }
}
