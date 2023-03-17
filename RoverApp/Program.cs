using System;
using System.Collections.Generic;
using MarsRover.Controllers;
using MarsRover.Interface;
using MarsRover.Models;
using MarsRover.Services;
using Microsoft.Extensions.DependencyInjection;


namespace MarsRover
{
    class Program
    {
 
        static void Main(string[] args)
        {
            // Set up the DI container
            var services = new ServiceCollection();
            ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();

            var navigationService = serviceProvider.GetRequiredService<INavigationService>();
            
            string folderPath = GetFilePath("Data1.txt");
            var parsedData = ParseNavigationData(folderPath);
            var result = navigationService.NavigateRovers(parsedData);
            
            foreach (var position in result.FormatFinalRoverPositions)
            {
                Console.WriteLine(position);
            }
        }
        
        private static void ConfigureServices(IServiceCollection services)
        {
            // Register services
            services.AddTransient<INavigationService, NavigationService>();
        }


        private static NavigationInstructionsDto ParseNavigationData(string filePath)
        {
            List<string> lines = new List<string>();

            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        lines.Add(line);
                    }
                }

                NavigationInstructionsDto NavInstructDto = new NavigationInstructionsDto()
                {
                    PlateauSize = lines[0],
                    RoverState = new List<string[]>()
                };

                int index = 1;
                while (index < lines.Count)
                {
                    NavInstructDto.RoverState.Add(new string[2] {lines[index], lines[index + 1]});
                    index = index + 2;
                }
                return NavInstructDto;
            }
            catch (IOException e)
            {
                Console.WriteLine("Error reading file:");
                Console.WriteLine(e.Message);
                return null;
            }
        }

        private static string GetFilePath(string fileName)
        {
            string curDirectory = Directory.GetCurrentDirectory();
            for (int i = 0; i < 3; i++)
            {
                curDirectory = Directory.GetParent(curDirectory).ToString();
            }

            return Path.Combine(curDirectory, "Data", fileName);
        }
    }
}
