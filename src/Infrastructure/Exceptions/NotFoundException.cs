using System;

namespace Infrastructure.Exceptions;

/// <summary>
/// Исключение, выбрасываемое при отсутствии запрашиваемого ресурса.
/// </summary>
public class NotFoundException : Exception
{
}
