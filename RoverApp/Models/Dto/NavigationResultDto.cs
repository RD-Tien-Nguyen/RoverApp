namespace MarsRover.Models;

public class NavigationResultDto
{
    public List<string> Collisions { get; set; }
    public List<string> FormatFinalRoverPositions { get; set; }

    public NavigationResultDto()
    {
        Collisions = new List<string>();
        FormatFinalRoverPositions = new List<string>();
    }
}