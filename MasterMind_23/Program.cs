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
            Game game = new Game();
            game.CreateCode();

            foreach(Pin pin in game.Code)
            {
                WritePin(pin.Color);
            }
        }
       //display right colors
        public static void WritePin(Color color)
        {
            switch(color)
            {
                case color.Blue:
                    Console.ForegroundColor = ConsoleColor.Blue; 
                    break;
                case color.Red:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case color.Yellow:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case color.Black:
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case color.Magenta:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case color.Orange:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case color.White:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case color.Green:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
            }
            Console.Write("⬤ ");
            Console.ForegroundColor = ConsoleColor.Gray;
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
    // Random color generator. Get the 4 colors to guess on. 
    public void CreateCode()
    {
        Random random = new Random();

        for(int i = 0; i < 4; i++)
        {
            Color randomColor = (Color)random.Next(1, 8);
            Code.Add(new Pin(i, randomColor));
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
// The colors in the game
public enum Color
{
    White = 1,
    Black = 2,
    Blue = 3,
    Yellow = 4,
    Green = 5,
    Red = 6,
    Magenta = 7,
    Orange = 8
}