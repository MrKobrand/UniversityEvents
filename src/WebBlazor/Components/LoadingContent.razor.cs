using Microsoft.AspNetCore.Components;

namespace WebBlazor.Components;

public partial class LoadingContent
{
    /// <summary>
    /// Закончена ли загрузка.
    /// </summary>
    [Parameter]
    [EditorRequired]
    public bool IsLoadingFinish { get; set; }

    /// <summary>
    /// Вложенное содержимое.
    /// </summary>
    [Parameter]
    [EditorRequired]
    public RenderFragment? ChildContent { get; set; }
}
