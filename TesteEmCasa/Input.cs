using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteEmCasa
{
    class Input<T> : IReadable
    {
        public string Label { get; set; }
#nullable enable
        private T? result;
#nullable enable
        public T? Value
        {
            get { return result; }
            set
            {
                result = value;
                Read = false;
            }
        }
#nullable enable
        public string rs
        {
            get
            {
                if (Value is double) return double.Parse(Value.ToString()).ToString("C", CultureInfo.CurrentCulture);
                else if (Value != null)
                    return Value.ToString();
                else return "";
            }
        }
        public Action<T> OnRead { get; set; }
        public Func<string, T> Conversor { get; set; }
        public bool Read { get; set; }


        public Input()
        {
            Read = true;
        }

        public Input(string label)
        {
            Label = label;
            Read = false;
        }

        public Input(string label, T? value)
        {
            Read = false;
            Label = label;
            Value = value;
        }

        public Input(string label, Action<T> onRead, Func<string, T> conversor)
        {
            Read = true;
            Label = label;
            OnRead = onRead;
            Conversor = conversor;
        }

        public Input(string label, Action<T> onRead, Func<string, T> conversor, bool read) : this(label, onRead, conversor)
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
}
