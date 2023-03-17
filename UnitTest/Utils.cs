using System;
using System.Collections.Generic;
using System.IO;
using MarsRover.Models;

namespace MarsRover.UnitTest;

public static class Utils
{
    public static string GetFilePath(string fileName)
    {
        string curDirectory = Directory.GetCurrentDirectory();
        for (int i = 0; i < 3; i++)
        {
            curDirectory = Directory.GetParent(curDirectory).ToString();
        }

        return Path.Combine(curDirectory, "TestInputs", fileName);
    }

    public static NavigationInstructionsDto ParseNavigationData(string filePath)
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

}