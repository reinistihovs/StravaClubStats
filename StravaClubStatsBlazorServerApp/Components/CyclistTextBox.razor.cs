using Microsoft.AspNetCore.Components;

namespace StravaClubStatsBlazorServerApp.Components;

public partial class CyclistTextBox
{
    [Parameter]
    public string? Cyclist { get; set; }

    [Parameter]
    public EventCallback<string?> CyclistChanged { get; set; }
}
