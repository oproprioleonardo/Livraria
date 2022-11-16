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
