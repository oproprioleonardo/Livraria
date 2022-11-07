using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteEmCasa
{
    interface IReadable
    {
        bool Read { get; set; }
        string Label { get; set; }
        string rs { get; }
        void ReadResult(Menu menu);
    }
}
