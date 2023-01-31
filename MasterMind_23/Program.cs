using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMind_23
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Pin purple = new Pin(1, Color.Purple);
            Console.WriteLine(purple.Color);
        }
    }
}
public class Game
{
    public List<Pin> Code { get; set; }

    public Game() 
    { 
        Code = new List<Pin>();
    }
    public void CreateCode()
    {
        Random random = new Random();

        for(int i = 0; i < 4; i++)
        {
            random.Next(1, 8);
        }
    }
}
public class Pin
{
    public int Position { get; set; }
    public Color Color { get; set; }

    public Pin(int position, Color color)
    {
        Position = position;
        Color = color;
    }
}
public enum Color
{
    White = 1,
    Grey = 2,
    Blue = 3,
    Yellow = 4,
    Green = 5,
    Red = 6,
    Purple = 7,
    Orange = 8
}