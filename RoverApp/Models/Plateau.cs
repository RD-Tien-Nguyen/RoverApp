namespace MarsRover.Models;

public class Plateau
{
    public int Max_X { get; set; }
    public int Max_Y { get; set; }
    public List<Rover> MovedRovers { get; set; }
    public LinkedList<Rover> UnmovedRovers { get; set; }

    public Plateau()
    {
        MovedRovers = new List<Rover>();
        UnmovedRovers = new LinkedList<Rover>();
    }
}