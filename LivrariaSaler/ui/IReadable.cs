namespace LivrariaSaler.ui;

public interface IReadable
{
    bool Read { get; set; }
    string Label { get; set; }
    string Rs { get; }
    void ReadResult(Menu menu);
}