using Microsoft.AspNetCore.Mvc;
using Web.Configuration;
using Web.Infrastructure;

namespace Web.Controllers;

/// <summary>
/// Базовый API контроллер.
/// </summary>
[ApiController]
[KebabCaseNaming]
[ApiExplorerSettings(GroupName = ControllerSections.UNIVERSITY_EVENTS_API)]
[Route("api/[controller]")]
public class ApiControllerBase : ControllerBase
{
}
