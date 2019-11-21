using System;

namespace PosGen
{
    class Program
    {
        private static string[] menuItems = {"One", "Two"};

        private static int selection = 0;
        
        static void Main(string[] args)
        {
            initialWrite();
        }

        private static void initialWrite()
        {
            var originalBackgroundColor = Console.BackgroundColor;
            var originalForegroundColor = Console.ForegroundColor;
            for (var i = 0; i < menuItems.Length; i++)
            {
                if (selection == i)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                
                Console.WriteLine(i+") "+menuItems[i]);
                
                if (selection == i)
                {
                    Console.BackgroundColor = originalBackgroundColor;
                    Console.ForegroundColor = originalForegroundColor;
                }
            }
        }

        private static void updateMenuOption(int i)
        {
            //TODO: Write i. line again
        }
    }
}