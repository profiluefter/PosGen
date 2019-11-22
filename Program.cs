using System;
using System.ComponentModel.Design;

namespace PosGen
{
    class Program
    {
        private static string[] menuItems = {"One", "Two"};

        static void Main(string[] args)
        {
            var menu = new MenuSelector(menuItems);
            Console.WriteLine(menu.Run());
        }
    }
}