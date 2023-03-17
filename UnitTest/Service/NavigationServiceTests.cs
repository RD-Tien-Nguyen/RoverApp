using MarsRover.Interface;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace MarsRover.UnitTest.Service;

public class NavigationServiceTests : IClassFixture<DependencySetupFixture>
{
    private readonly INavigationService _navigationService;

    public NavigationServiceTests(DependencySetupFixture fixture)
    {
        _navigationService = fixture.ServiceProvider.GetRequiredService<INavigationService>();
    }

    [Fact]
    public void NavigateRover_Ok_Test()
    {
        // Arrange
        string fileName = "NavServicePassTest.txt";
        string folderPath = Utils.GetFilePath(fileName);
        var parsedData = Utils.ParseNavigationData(folderPath);
        
        // Act
        var result = _navigationService.NavigateRovers(parsedData);
        
        // Assert
        Assert.True(result.Collisions.Count < 1);
        Assert.Equal(result.FormatFinalRoverPositions[0], "1 3 N");
        Assert.Equal(result.FormatFinalRoverPositions[1], "5 1 E");

    }
    
    [Fact]
    public void NavigateRover_WhenRoverColliedWithAnotherRover_ThenNavigationServiceReturnCollionMessage()
    {
        // Arrange
        string fileName = "NavServiceCollisionTest.txt";
        string folderPath = Utils.GetFilePath(fileName);
        var parsedData = Utils.ParseNavigationData(folderPath);
        
        // Act
        var result = _navigationService.NavigateRovers(parsedData);
        
        // Assert
        Assert.True(result.Collisions.Count == 2);
    }
    
    [Fact]
    public void NavigateRover_WhenRoverMoveOutOfBounds_ThenRoverSkipMovement()
    {
        // Arrange
        string fileName = "NavServiceOutBoundTest.txt";
        string folderPath = Utils.GetFilePath(fileName);
        var parsedData = Utils.ParseNavigationData(folderPath);
        
        // Act
        var result = _navigationService.NavigateRovers(parsedData);
        
        // Assert
        Assert.True(result.Collisions.Count < 1);
        Assert.Equal(result.FormatFinalRoverPositions[0], "2 2 E");
    }
}