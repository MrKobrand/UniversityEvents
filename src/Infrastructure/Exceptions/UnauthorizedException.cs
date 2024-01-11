using System;

namespace Infrastructure.Exceptions;

/// <summary>
/// Исключение, выбрасываемое при попытке запроса ресурсов неавторизованным пользователем.
/// </summary>
public class UnauthorizedException : Exception
{
}
