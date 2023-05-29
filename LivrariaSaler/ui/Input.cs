using System;
using System.Globalization;

namespace LivrariaSaler.ui;

public class Input<T> : IReadable
{
    public string Label { get; set; }
    private T? _result;

    public T? Value
    {
        get => _result;
        set
        {
            _result = value;
            Read = false;
        }
    }

    public string Rs
    {
        get
        {
            if (Value is decimal) return decimal.Parse(Value.ToString()).ToString("C", CultureInfo.CurrentCulture);
            if (Value != null) return Value.ToString();
            return "";
        }
    }

    public Action<T> OnRead { get; set; }
    public Func<string, T> Conversor { get; set; }
    public bool Read { get; set; }


    public Input(string label)
    {
        Label = label;
        Read = false;
    }

    public Input(string label, Action<T> onRead, Func<string, T> conversor)
    {
        Read = true;
        Label = label;
        OnRead = onRead;
        Conversor = conversor;
    }

    public Input(string label, Action<T> onRead, Func<string, T> conversor, bool read) : this(label, onRead,
        conversor)
    {
        Read = read;
    }

    public void ReadResult(Menu menu)
    {
        if (Read)
        {
            Console.SetCursorPosition(Label.Length + 1, menu.LineTop + menu.InputIndex);
            Value = Conversor(Console.ReadLine());
            OnRead(Value);
        }

        menu.InputIndex++;
    }
}