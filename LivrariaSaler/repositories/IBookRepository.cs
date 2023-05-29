using LivrariaSaler.database;
using LivrariaSaler.models;

namespace LivrariaSaler.repositories;

public interface IBookRepository : IRepository<string, Book>
{
    
}