namespace MarsRover.Models;

public class NavigationInstructionsDto
{
    public string PlateauSize { get; set; }
    public List<string[]> RoverState { get; set; }
}