using Microsoft.AspNetCore.Components;

namespace StravaClubsStatsMauiApp.Components;

public partial class CyclistDropdown
{
    [Parameter]
    public List<string> Cyclists { get; set; } = null!;

    [Parameter]
    public string? Cyclist { get; set; }

    [Parameter]
    public EventCallback<string?> CyclistChanged { get; set; }
}
