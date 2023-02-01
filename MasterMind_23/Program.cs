using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Game;

namespace MasterMind_23
{
    class Program
    {
        public static void Main(string[] args)
        {
            Game game = new Game();
            game.CreateCode();
            //how many guesses loop
            while (game.Guesses.Count < 12)
            {
                Console.WriteLine("Make your choices");
                //which input are allowed
                string input = Console.ReadLine();
                while (!Regex.IsMatch(input, "^[WCBYGRM]-[WCBYGRM]-[WCBYGRM]-[WCBYGRM]$"))
                {
                    Console.WriteLine("Invalid input");
                    input = Console.ReadLine();
                }

                game.MakeGuess(input);

                foreach (var guess in game.Guesses)
                {
                    foreach (Pin pin in guess.Value)
                    {
                        WritePin(pin.Color);
                    }
                    List<ResultPin> results = game.Results[guess.Key];
                    foreach(ResultPin result in results)
                    {
                        WriteResult(result.Type);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                //Win condition
                List<ResultPin> lastResult = game.Results.Values.Last();
                if (lastResult.Count(x => x.Type == ResultType.Correct) == 4)
                {
                    Console.WriteLine("Congratulations! You have won!");
                    break;
                }
            }
            //Lose condition
            if (game.Guesses.Count >= 12)
            {
                Console.WriteLine("You have lost");
            }            
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
                case Color.White:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case Color.Green:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
            }
            Console.Write("\x25A0 ");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        public static void WriteResult(ResultType type)
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
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }

    public class Game
    {
        public List<Pin> Code { get; set; }
        public Dictionary<int, List<Pin>> Guesses { get; set; }
        public Dictionary<int, List<ResultPin>> Results { get; set; }

        public Game()
        {
            Code = new List<Pin>();
            Guesses = new Dictionary<int, List<Pin>>();
            Results = new Dictionary<int, List<ResultPin>>();
        }
        //add correct pin to each answer
        public void CheckCode(List<Pin> guessCode)
        {
            List<ResultPin> resultPins = new List<ResultPin>();
            foreach (var pin in guessCode)
            {
                //correct answer condition
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
        // Random color generator. Get the 4 colors to guess on. 
        public void CreateCode()
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
            List<string> inputs = guess.Split('.').ToList();
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
    }
}