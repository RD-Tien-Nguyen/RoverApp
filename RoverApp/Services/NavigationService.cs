using System.Text;
using MarsRover.Interface;
using MarsRover.Models;

namespace MarsRover.Services;

public class NavigationService : INavigationService
{
    public NavigationResultDto NavigateRovers(NavigationInstructionsDto navigationInstructionsDto)
    {
        NavigationResultDto result = new NavigationResultDto();
        
        var maxCoords = navigationInstructionsDto.PlateauSize.Split(' ');
        Plateau plateau = new Plateau()
        {
            Max_X = int.Parse(maxCoords[0]),
            Max_Y = int.Parse(maxCoords[1])
        };

        for (int i = 0; i < navigationInstructionsDto.RoverState.Count; i++)
        {
            var position = navigationInstructionsDto.RoverState[i][0].Split(' ');
            int xPos = int.Parse(position[0]);
            int yPos = int.Parse(position[1]);
            char heading =  position[2][0];
            Rover rover = new Rover()
            {
                Position = new Position() { X = xPos, Y = yPos },
                Heading = heading,
                Instruction = navigationInstructionsDto.RoverState[i][1]
            };
            plateau.UnmovedRovers.AddLast(rover);
        }

        return ExecuteInstructions(plateau, result);
    }

    private NavigationResultDto ExecuteInstructions(Plateau plateau, NavigationResultDto result)
    {
        while (plateau.UnmovedRovers.Count > 0)
        {
            var rover = plateau.UnmovedRovers.First();
            plateau.UnmovedRovers.RemoveFirst();
            
            foreach (var instruction in rover.Instruction)
            {
                if (instruction == 'L') TurnLeft(rover);
                else if (instruction == 'R') TurnRight(rover);
                else if (instruction == 'M') Move(rover, plateau, result);
            }
            
            plateau.MovedRovers.Add(rover);
            result.FormatFinalRoverPositions.Add($"{rover.Position.X} {rover.Position.Y} {rover.Heading}");
        }
        
        return result;
    }
    
    private void TurnLeft(Rover rover)
    {
        var directions = "NESW";
        var index = directions.IndexOf(rover.Heading);
        rover.Heading = directions[(index + 3) % 4];
    }

    private void TurnRight(Rover rover)
    {
        var directions = "NESW";
        var index = directions.IndexOf(rover.Heading);
        rover.Heading = directions[(index + 1) % 4];
    }
    
    private void Move(Rover rover, Plateau plateau, NavigationResultDto result)
    {
        if (rover.Heading == 'N' && rover.Position.Y < plateau.Max_Y)
        {
            rover.Position.Y++;
            CheckForCollisions(rover, plateau, result);
        }
        else if (rover.Heading == 'E' && rover.Position.X < plateau.Max_X)
        {
            rover.Position.X++;
            CheckForCollisions(rover, plateau, result);
        }
        else if (rover.Heading == 'S' && rover.Position.Y > 0)
        {
            rover.Position.Y--;
            CheckForCollisions(rover, plateau, result);
        }
        else if (rover.Heading == 'W' && rover.Position.X > 0)
        {
            rover.Position.X--;
            CheckForCollisions(rover, plateau, result);
        }
    }

    private void CheckForCollisions(Rover rover, Plateau plateau, NavigationResultDto result)
    {
        if (plateau.MovedRovers.Count != 0)
        {
            int movedIndex = 1;
            foreach (var value in plateau.MovedRovers)
            {
                if (rover.Position.X == value.Position.X && rover.Position.Y == value.Position.Y)
                {
                    string errorMessage = $"Rover {plateau.MovedRovers.Count + 1} collide with Rover {movedIndex}";
                    result.Collisions.Add(errorMessage);
                }
                movedIndex++;
            }
        }

        if (plateau.UnmovedRovers.Count != 0)
        {
            int unmovedIndex = 1;
            foreach (var value in plateau.UnmovedRovers)
            {
                if (rover.Position.X == value.Position.X && rover.Position.Y == value.Position.Y)
                {
                    string errorMessage =
                        $"Rover {plateau.MovedRovers.Count + 1} collide with Rover {plateau.MovedRovers.Count + unmovedIndex + 1}";
                    result.Collisions.Add(errorMessage);
                }
                unmovedIndex++;
            }
        }
    }
}