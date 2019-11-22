using System;

namespace PosGen
{
    public class MenuSelector
    {
        private readonly string[] _menuItems;
        private readonly int _yOffset;
        
        private int _selection;
        private bool _running = true;
        
        public MenuSelector(string[] menuItems)
        {
            _menuItems = menuItems;
            _yOffset = Console.CursorTop;
        }

        public int Run()
        {
            InitialWrite();
            do
            {
                var previousSelection = _selection;
                UpdateSelection();

                UpdateMenuOption(_selection);
                UpdateMenuOption(previousSelection);
                Console.SetCursorPosition(0, _menuItems.Length + _yOffset);
            } while (_running);

            return _selection;
        }

        private void InitialWrite()
        {
            Console.Clear();
            Console.SetCursorPosition(0, _yOffset);
            var originalBackgroundColor = Console.BackgroundColor;
            var originalForegroundColor = Console.ForegroundColor;
            for (var i = 0; i < _menuItems.Length; i++)
            {
                if (_selection == i)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                Console.WriteLine(i + ") " + _menuItems[i]);

                if (_selection != i) continue;
                Console.BackgroundColor = originalBackgroundColor;
                Console.ForegroundColor = originalForegroundColor;
            }
        }

        private void UpdateMenuOption(int i)
        {
            Console.SetCursorPosition(0, i + _yOffset);

            var originalBackgroundColor = Console.BackgroundColor;
            var originalForegroundColor = Console.ForegroundColor;

            if (_selection == i)
            {
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.ForegroundColor = ConsoleColor.Black;
            }

            Console.WriteLine(i + ") " + _menuItems[i]);

            if (_selection != i) return;
            Console.BackgroundColor = originalBackgroundColor;
            Console.ForegroundColor = originalForegroundColor;
        }

        private void UpdateSelection()
        {
            var key = Console.ReadKey();

            // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    _selection--;
                    break;
                case ConsoleKey.DownArrow:
                    _selection++;
                    break;
                case ConsoleKey.Enter:
                    _running = false;
                    break;
            }

            _selection = Math.Clamp(_selection, 0, _menuItems.Length - 1);
        }
    }
}