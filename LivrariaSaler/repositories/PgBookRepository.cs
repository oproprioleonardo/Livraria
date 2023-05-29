using System;
using LivrariaSaler.models;
using Npgsql;

namespace LivrariaSaler.repositories;

public class PgBookRepository : IBookRepository
{
    private readonly NpgsqlConnection _connection;

    public PgBookRepository(string credentials)
    {
        var credentials1 = credentials ?? throw new ArgumentNullException(nameof(credentials));
        _connection = new NpgsqlConnection(credentials1);
    }


    public void Delete(string code)
    {
        const string sql = "DELETE FROM books WHERE code = @code";
        _connection.Open();
        var cmd = new NpgsqlCommand(sql, _connection);
        cmd.Parameters.AddWithValue("@code", code);
        cmd.ExecuteNonQuery();
        _connection.Close();
    }

    public void Insert(Book book)
    {
        const string sql = @"INSERT INTO books(code, name, pub_house, author, year, gender, quantity, price) 
                    VALUES (
                  @code,
                  @name,
                  @pubhouse,
                  @author,
                  @year,
                  @gender,
                  @quantity,
                  @price)";
        _connection.Open();
        var transaction = _connection.BeginTransaction();
        try
        {
            var cmd = new NpgsqlCommand(sql, _connection, transaction);
            cmd.Parameters.AddWithValue("@code", book.Code);
            cmd.Parameters.AddWithValue("@name", book.Name);
            cmd.Parameters.AddWithValue("@pubhouse", book.PubHouse);
            cmd.Parameters.AddWithValue("@author", book.Author);
            cmd.Parameters.AddWithValue("@year", book.Year);
            cmd.Parameters.AddWithValue("@gender", book.Gender);
            cmd.Parameters.AddWithValue("@quantity", book.Quantity);
            cmd.Parameters.AddWithValue("@price", book.Price);
            cmd.ExecuteNonQuery();
            transaction.Commit();
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw new Exception(e.Message);
        }

        _connection.Close();
    }

    public Book Find(string code)
    {
        var book = new Book
        {
            Code = code,
            Persisted = false
        };
        const string sql = "SELECT * FROM books WHERE code = @code";
        _connection.Open();
        var cmd = new NpgsqlCommand(sql, _connection);
        cmd.Parameters.AddWithValue("@code", book.Code);
        var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            book.Code = reader.GetString(0);
            book.Name = reader.GetString(1);
            book.PubHouse = reader.GetString(2);
            book.Author = reader.GetString(3);
            book.Year = reader.GetInt32(4);
            book.Gender = reader.GetString(5);
            book.Quantity = reader.GetInt32(6);
            book.Price = reader.GetDecimal(7);
            book.Persisted = true;
        }

        _connection.Close();
        return book;
    }

    public void Update(Book book)
    {
        const string sql = "UPDATE books " +
                           "SET name = @name, pub_house = @pubhouse, author = @author, year = @year, gender = @gender, quantity = @quantity, price = @price " +
                           "WHERE code = @code";
        _connection.Open();
        var transaction = _connection.BeginTransaction();
        try
        {
            var cmd = new NpgsqlCommand(sql, _connection, transaction);
            cmd.Parameters.AddWithValue("@code", book.Code);
            cmd.Parameters.AddWithValue("@name", book.Name);
            cmd.Parameters.AddWithValue("@pubhouse", book.PubHouse);
            cmd.Parameters.AddWithValue("@author", book.Author);
            cmd.Parameters.AddWithValue("@year", book.Year);
            cmd.Parameters.AddWithValue("@gender", book.Gender);
            cmd.Parameters.AddWithValue("@quantity", book.Quantity);
            cmd.Parameters.AddWithValue("@price", book.Price);
            cmd.ExecuteNonQuery();
            transaction.Commit();
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw new Exception(e.Message);
        }

        _connection.Close();
    }

    public int Count()
    {
        var count = 0;
        const string sql = "SELECT COUNT(*) FROM books";
        _connection.Open();
        var cmd = new NpgsqlCommand(sql, _connection);
        var reader = cmd.ExecuteReader();
        if (reader.Read())
            count = reader.GetInt32(0);
        _connection.Close();
        return count;
    }
}