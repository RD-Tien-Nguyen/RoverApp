using MarsRover.Models;

namespace MarsRover.Interface;

public interface INavigationService
{
    public NavigationResultDto NavigateRovers(NavigationInstructionsDto navigationInstructionsDto);
}