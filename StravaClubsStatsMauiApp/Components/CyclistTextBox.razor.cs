using Microsoft.AspNetCore.Components;

namespace StravaClubsStatsMauiApp.Components;

public partial class CyclistTextBox
{
    [Parameter]
    public string? Cyclist { get; set; }

    [Parameter]
    public EventCallback<string?> CyclistChanged { get; set; }
}
