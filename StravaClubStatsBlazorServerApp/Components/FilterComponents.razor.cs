using Microsoft.AspNetCore.Components;

namespace StravaClubStatsBlazorServerApp.Components;

public partial class FilterComponents
{
    [Parameter]
    public List<string> Cyclists { get; set; } = null!;

    [Parameter]
    public string? Cyclist { get; set; }

    [Parameter]
    public EventCallback<string?> CyclistChanged { get; set; }

    [Parameter]
    public bool IsSmall { get; set; }
}
