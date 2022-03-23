using System;
using System.Collections.Generic;
using System.Linq;

namespace firstprogramBARS
{

    public class First
    {
        public event EventHandler<char> OnKeyPressed;
        public void Run()
        {
             var btn = Console.ReadKey();
            while(true)
                {
                if (btn.Key == ConsoleKey.C)
                {
                    Console.WriteLine("\n Завершение работы");
                    break;
                }
                else
                {
                    OnKeyPressed?.Invoke(this, btn.KeyChar);
                }
            }      
           
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var key = new First();
            key.OnKeyPressed += (Sender, Key) => Console.Write("\n" + Key + " ");
            key.Run();
        }
    }
}
