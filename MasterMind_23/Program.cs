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
            Console.WriteLine(1 + 1);



            /*
            //testing basic
            string input = Console.ReadLine();
            Console.WriteLine(input);
            //testing to change color
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine('\u2B24' + "⬤ Testing the colors");
            */
        }
    }
}
public class Game
{

}
public class Pin
{
    int position;

    public void SetPosition() 
    { 
    
    }
    public int GetPosition()
    {

    }
    public int Position { get; set; }
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