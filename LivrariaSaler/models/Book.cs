using System;

namespace LivrariaSaler.models;

public class Book
{
    public Book()
    {
    }

    public Book(string code, string name, string author, string pubHouse, int year, string gender, int quantity,
        decimal price)
    {
        Code = code ?? throw new ArgumentNullException(nameof(code));
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Author = author ?? throw new ArgumentNullException(nameof(author));
        PubHouse = pubHouse ?? throw new ArgumentNullException(nameof(pubHouse));
        Year = year;
        Gender = gender ?? throw new ArgumentNullException(nameof(gender));
        Quantity = quantity;
        Price = price;
    }

    public string Code { get; set; }

    public string Name { get; set; }

    public string Author { get; set; }

    public string PubHouse { get; set; }

    public int Year { get; set; }

    public string Gender { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }
        
    public bool Persisted { get; set; }
}