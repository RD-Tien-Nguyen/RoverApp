using MarsRover.Interface;
using MarsRover.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MarsRover.UnitTest;

public class DependencySetupFixture
{
    public ServiceProvider ServiceProvider { get; private set; }
    
    public DependencySetupFixture()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddTransient<INavigationService, NavigationService>();

        ServiceProvider = serviceCollection.BuildServiceProvider();
    }
}