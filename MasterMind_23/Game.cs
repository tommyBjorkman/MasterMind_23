using System;
using System.Collections.Generic;
using System.Linq;
using MasterMind_23;


namespace Game
{
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
                while (Code.Any(x => x.Color == randomColor))
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
}