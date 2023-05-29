using System;
using System.Collections.Generic;

namespace LivrariaSaler.ui;

public class Menu
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public List<Option> Options = new();
    private int _optionIndex;
    private bool _optionIsSelected;
    public int LineTop = 3;
    public readonly List<IReadable> Inputs = new();
    public int InputIndex;

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
        foreach (var t in Inputs)
            t.ReadResult(this);
    }


    private void ShowOptions()
    {
        if (Options.Count < 2) return;
        var topOffset = Console.CursorTop;
        Console.CursorVisible = false;
        while (!_optionIsSelected)
        {
            for (var i = 0; i < Options.Count; i++) ShowOption(i);

            var keyInfo = Console.ReadKey(true);

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    _optionIndex = _optionIndex == 0 ? Options.Count - 1 : _optionIndex - 1;
                    break;
                case ConsoleKey.DownArrow:
                    _optionIndex = _optionIndex == Options.Count - 1 ? 0 : _optionIndex + 1;
                    break;
                case ConsoleKey.Enter:
                    _optionIsSelected = true;
                    break;
            }

            Console.SetCursorPosition(0, topOffset);
        }

        Console.CursorVisible = true;
        Options[_optionIndex].Callback();
    }

    private void ShowOption(int index)
    {
        if (index == _optionIndex)
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
        }

        var text = Options[index].Name;
        Console.WriteLine(text);
        Console.ResetColor();
    }

    private void ShowInputs()
    {
        Inputs.ForEach(input =>
        {
            if (!input.Read)
                Console.WriteLine(input.Label + " " + input.Rs);
            else Console.WriteLine(input.Label);
        });
    }


    private void MarcaDAgua()
    {
        var topOffset = Console.CursorTop;
        Console.SetCursorPosition(10, 20);
        Console.Write("Livraria SaLer");
        Console.SetCursorPosition(0, topOffset);
    }
}