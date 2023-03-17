using MarsRover.Interface;
using MarsRover.Models;

namespace MarsRover.Controllers;

public class NavigationController
{
    private readonly INavigationService _navigationService;

    public NavigationController(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }
    
    public NavigationResultDto RunInstructions(NavigationInstructionsDto instructions)
    {
        return _navigationService.NavigateRovers(instructions);
    }
}