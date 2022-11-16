using System;
using System.Collections.Generic;

namespace TesteEmCasa
{
    class Menu
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public List<Option> Options = new();
        private int OptionIndex = 0;
        private bool OptionIsSelected = false;
        public int LineTop = 3;
        public List<IReadable> Inputs = new();
        public int InputIndex = 0;

        public Menu()
        {
        }

        public Menu(string title)
        {
            Title = title;
        }

        public Menu(string title, string description) : this(title)
        {
            Description = description;
        }

        public Menu(string title, string description, List<Option> options) : this(title, description)
        {
            Options = options;
        }

        public Menu(string title, List<IReadable> inputs)
        {
            Title = title;
            Inputs = inputs;

        }


        public void Show()
        {
            Console.Clear();
            InputIndex = 0;
            ShowTitle();
            ShowDescription();
            MarcaDAgua();
            ShowOptions();
            ShowInputs();
            ReadValues();
        }
        public void Show(Action callback)
        {
            Show();
            callback();
        }

        public void Show(bool wait)
        {
            Show();
            if (wait) Console.ReadKey();
        }

        private void ShowTitle()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"{Environment.NewLine} {Title} {Environment.NewLine}");
            Console.ResetColor();
        }

        private void ShowDescription()
        {
            if (Description != null) Console.WriteLine($"{Description} {Environment.NewLine}");
        }

        private void ReadValues()
        {
            for (int i = 0; i < Inputs.Count; i++)
                Inputs[i].ReadResult(this);

        }


        private void ShowOptions()
        {
            if (Options.Count < 2) return;
            int topOffset = Console.CursorTop;
            ConsoleKeyInfo keyInfo;
            Console.CursorVisible = false;
            while (!OptionIsSelected)
            {
                for (int i = 0; i < Options.Count; i++) ShowOption(i);

                keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        OptionIndex = (OptionIndex == 0) ? Options.Count - 1 : OptionIndex - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        OptionIndex = (OptionIndex == Options.Count - 1) ? 0 : OptionIndex + 1;
                        break;
                    case ConsoleKey.Enter:
                        OptionIsSelected = true;
                        break;
                }

                Console.SetCursorPosition(0, topOffset);
            }

            Console.CursorVisible = true;
            Options[OptionIndex].Callback();
        }

        private void ShowOption(int index)
        {
            if (index == OptionIndex)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
            }

            string text = Options[index].Name;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        private void ShowInputs()
        {
            Inputs.ForEach(input =>
            {
                if (!input.Read)
                    Console.WriteLine(input.Label + " " + input.rs);
                else Console.WriteLine(input.Label);
            });
        }


        private void MarcaDAgua()
        {
            int topOffset = Console.CursorTop;
            Console.SetCursorPosition(10, 20);
            Console.Write("Livraria SaLer");
            Console.SetCursorPosition(0, topOffset);
        }


    }

    class Option
    {
        public string Name { get; set; }
        public Action Callback { get; set; }

        public Option(string name, Action callback)
        {
            Name = name;
            Callback = callback;
        }

        public Option()
        {
        }
    }





}
