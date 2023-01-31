using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MasterMind_23
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.CreateCode();

            string input = Console.ReadLine();
            while (!Regex.IsMatch(input, "^[WCBYGRMO]-[WCBYGRMO]-[WCBYGRMO]-[WCBYGRMO]$"))
            {
                Console.WriteLine("Invalid input");
                input = Console.ReadLine();
            }

            game.MakeGuess(input);

            foreach(Pin pin in game.Code)
            {
                WritePin(pin.Color);
            }
            Console.WriteLine();
        }
       //display right colors
        public static void WritePin(Color color)
        {
            switch(color)
            {
                case Color.Blue:
                    Console.ForegroundColor = ConsoleColor.Blue; 
                    break;
                case Color.Red:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case Color.Yellow:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case Color.Cyan:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case Color.Magenta:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case Color.Orange:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case Color.White:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case Color.Green:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
            }
            
            Console.OutputEncoding = Encoding.Unicode;
            Console.Write("\x25A0 ");
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
    public void MakeGuess(string guess)
    {
        List<string> inputs = guess.Split("-").ToList();
        List<Pin> guessCode = new List<Pin>();

        foreach(string guessColor in inputs)
        {
            switch(guessColor)
            {
                case "B":
                    guessCode.Add(new Pin(guessCode.Count, Color.Blue));
                    break;
                case "C":
                    guessCode.Add(new Pin(guessCode.Count, Color.Cyan));
                    break;
                case "W":
                    guessCode.Add(new Pin(guessCode.Count, Color.White));
                    break;
                case "Y":
                    guessCode.Add(new Pin(guessCode.Count, Color.Yellow));
                    break;
                case "G":
                    guessCode.Add(new Pin(guessCode.Count, Color.Green));
                    break;
                case "R":
                    guessCode.Add(new Pin(guessCode.Count, Color.Red));
                    break;
                case "M":
                    guessCode.Add(new Pin(guessCode.Count, Color.Magenta));
                    break;
                case "O":
                    guessCode.Add(new Pin(guessCode.Count, Color.Orange));
                    break;
            }
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
    Cyan = 2,
    Blue = 3,
    Yellow = 4,
    Green = 5,
    Red = 6,
    Magenta = 7,
    Orange = 8
}