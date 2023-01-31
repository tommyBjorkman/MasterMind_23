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
    White,
    Black,
    Blue,
    Yellow,
    Green,
    Red,
    Purple,
    Orange
}