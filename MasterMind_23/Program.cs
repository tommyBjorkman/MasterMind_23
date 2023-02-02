using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MasterMind_23
{
    class Program
    {
        public static void Main(string[] args)
        {
            //Introduction
            Console.Title = "Master Mind 2023";
            Console.WindowWidth = 110;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("Welcome to Master Mind! ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("■ ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("■ ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("■ ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("■ ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("■ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("■ ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("■ ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Beat the computer!");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Choose between White, Cyan, Blue, Yellow, Green, Red or Magenta. ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Use the first letter in each color and separate them with a -. Example W-C-B-Y. You have 10 tries!");
            Console.ForegroundColor = ConsoleColor.Gray;
            Game game = new Game();
            game.CreateCode();

            //how many guesses loop
            while (game.Guesses.Count < 10)
            {
                Console.WriteLine("Make your guess ->");
                string input = Console.ReadLine();
                while (!Regex.IsMatch(input, "^[WCBYGRMwcbygrm]-[WCBYGRMwcbygrm]-[WCBYGRMwcbygrm]-[WCBYGRMwcbygrm]$")) //make sure the input matches what is allowed
                {
                    Console.WriteLine("Invalid input"); //What to write if input is wrong
                    input = Console.ReadLine();
                }
                game.MakeGuess(input); //How to handle each guess
                foreach (var guess in game.Guesses)
                {
                    foreach (Pin pin in guess.Value)
                    {
                        WritePin(pin.Color);
                    }
                    List<ResultPin> results = game.Results[guess.Key]; //Write out the result of the guess
                    foreach (ResultPin result in results)
                    {
                        WriteResult(result.Type);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                List<ResultPin> lastResult = game.Results.Values.Last();
                if (lastResult.Count(x => x.Type == ResultType.Correct) == 4)  //Win condition, 4 correct colors in the correct place ends the game
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Congratulations! You have won!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Do you want to play again?");
                    Console.ReadLine();
                    break;
                }
            }
            if (game.Guesses.Count >= 10) //Lose condition, if 10 guesses and none is fully correct you lose
            {
                Console.WriteLine("You have lost");
                Console.WriteLine("Do you want to play again?");
                Console.ReadLine();
            }            
        }
        public static void WritePin(Color color) //display right colors. using switch to use different colors depending on guess
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
                case Color.White:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case Color.Green:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
            }
            Console.Write("\x25A0 "); //Symbol to display guesses
            Console.ForegroundColor = ConsoleColor.Gray; //reset color of console text back to default gray after each guess
        }
        public static void WriteResult(ResultType type) //How to display the different result types to the side of the guesses
        {
            switch (type)
            {
                case ResultType.Correct:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("V ");
                    break;
                case ResultType.Wrong:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("X ");
                    break;
                case ResultType.Malplaced:
                    Console.Write("  ");
                    break;
            }
            Console.ForegroundColor = ConsoleColor.Gray; //resets text color to default gray
        }
    }

    public class Game //create class Game
    {
        public List<Pin> Code { get; set; } 
        public Dictionary<int, List<Pin>> Guesses { get; set; }
        public Dictionary<int, List<ResultPin>> Results { get; set; }

        public Game()
        {
            Code = new List<Pin>(); //make list of pin class
            Guesses = new Dictionary<int, List<Pin>>(); //make dictionary for the guesses
            Results = new Dictionary<int, List<ResultPin>>(); //make a dictionary for the results
        }
        public void CheckCode(List<Pin> guessCode) //correct answer condition, how to return different types
        {
            List<ResultPin> resultPins = new List<ResultPin>();
            foreach (var pin in guessCode)
            {
                
                if (Code.FirstOrDefault(x => x.Color == pin.Color && x.Position == pin.Position) != null)
                {
                    resultPins.Add(new ResultPin(ResultType.Correct));
                }
                else if (Code.Any(x => x.Color == pin.Color))
                {
                    resultPins.Add(new ResultPin(ResultType.Wrong));
                }
                else
                {
                    resultPins.Add(new ResultPin(ResultType.Malplaced));
                }
            }
            Results.Add(Results.Count, resultPins);
        }
        public void CreateCode() // Random color generator. Get the 4 colors to guess on. 
        {
            Random random = new Random();

            for (int i = 0; i < 4; i++)
            {
                Color randomColor = (Color)random.Next(1, 7);
                while(Code.Any(x => x.Color == randomColor))
                {
                    randomColor = (Color)random.Next(1, 7);
                }
                Code.Add(new Pin(i, randomColor));
            }
        }
        public void MakeGuess(string guess)
        {
            List<string> inputs = guess.Split('-').ToList();
            List<Pin> guessCode = new List<Pin>();

            foreach (string guessColor in inputs)
            {
                switch (guessColor)
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
                    case "b":
                        guessCode.Add(new Pin(guessCode.Count, Color.Blue));
                        break;
                    case "c":
                        guessCode.Add(new Pin(guessCode.Count, Color.Cyan));
                        break;
                    case "w":
                        guessCode.Add(new Pin(guessCode.Count, Color.White));
                        break;
                    case "y":
                        guessCode.Add(new Pin(guessCode.Count, Color.Yellow));
                        break;
                    case "g":
                        guessCode.Add(new Pin(guessCode.Count, Color.Green));
                        break;
                    case "r":
                        guessCode.Add(new Pin(guessCode.Count, Color.Red));
                        break;
                    case "m":
                        guessCode.Add(new Pin(guessCode.Count, Color.Magenta));
                        break;
                }

            }
            Guesses.Add(Guesses.Count, guessCode);
            CheckCode(guessCode);
        }
    }

    public class ResultPin
    {
        public ResultType Type { get; set; }

        public ResultPin(ResultType type)
        {
            Type = type;
        }
    }
    
    public enum ResultType
    {
        Correct,
        Wrong,
        Malplaced
    }//The 3 different types of answers
    public class Pin //pins position and color
    {
        public int Position { get; set; }
        public Color Color { get; set; }

        public Pin(int position, Color color)
        {
            Position = position;
            Color = color;
        }
    }
    public enum Color // The colors in the game
    {
        White = 1,
        Cyan = 2,
        Blue = 3,
        Yellow = 4,
        Green = 5,
        Red = 6,
        Magenta = 7,
    }
}