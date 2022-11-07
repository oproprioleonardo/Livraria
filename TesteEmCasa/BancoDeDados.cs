using Npgsql;
using System;
using System.Collections.Generic;

namespace TesteEmCasa
{
    public class BancoDeDados
    {
        private static string credenciais;
        private static NpgsqlConnection con;
        private static string sql;
        private static NpgsqlCommand cmd;

        public static void NovaConexao()
        {   
            credenciais = Environment.GetEnvironmentVariable("credenciais");
            con = new NpgsqlConnection(credenciais);
        }

        public static void CriarTabela()
        {
            sql = "CREATE TABLE IF NOT EXISTS livros(" +
                "codigo VARCHAR(13) PRIMARY KEY, " +
                "nome VARCHAR(50), " +
                "editora VARCHAR(30), " +
                "autor VARCHAR(50)," +
                "ano INT, " +
                "genero VARCHAR(25), " +
                "quantidade INT, " +
                "preco real" +
                ")";
            con.Open();
            cmd = new(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static void InserirLivro(Livro livro)
        {
            sql = "INSERT INTO livros(codigo, nome, editora, autor, ano, genero, quantidade, preco) VALUES (" +
                "@codigo," +
                "@nome," +
                "@editora," +
                "@autor," +
                "@ano," +
                "@genero," +
                "@quantidade," +
                "@preco)";
            con.Open();
            cmd = new(sql, con);
            cmd.Parameters.AddWithValue("nome", livro.Nome);
            cmd.Parameters.AddWithValue("codigo", livro.Codigo);
            cmd.Parameters.AddWithValue("editora", livro.Editora);
            cmd.Parameters.AddWithValue("autor", livro.Autor);
            cmd.Parameters.AddWithValue("ano", livro.Ano);
            cmd.Parameters.AddWithValue("genero", livro.Genero);
            cmd.Parameters.AddWithValue("quantidade", livro.Quantidade);
            cmd.Parameters.AddWithValue("preco", livro.Preco);

            cmd.Prepare();
            cmd.ExecuteNonQuery();

            con.Close();
        }

        public static Livro PegarLivro(long codigo)
        {
            Livro livro = new();
            sql = "SELECT * FROM livros WHERE codigo = @codigo";
            con.Open();
            cmd = new(sql, con);
            cmd.Parameters.AddWithValue("codigo", codigo);
            cmd.Prepare();
            NpgsqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                livro.Codigo = rdr.GetString(0);
                livro.Nome = rdr.GetString(1);
                livro.Editora = rdr.GetString(2);
                livro.Autor = rdr.GetString(3);
                livro.Ano = rdr.GetInt32(4);
                livro.Genero = rdr.GetString(5);
                livro.Quantidade = rdr.GetInt32(6);
                livro.Preco = rdr.GetDouble(7);
            }
            con.Close();
            return livro;
        }

        public static List<Livro> PegarLivros()
        {
            List<Livro> livros = new();
            sql = "SELECT * FROM livros";
            con.Open();
            cmd = new(sql, con);
            NpgsqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Livro livro = new();
                livro.Codigo = rdr.GetString(0);
                livro.Nome = rdr.GetString(1);
                livro.Editora = rdr.GetString(2);
                livro.Autor = rdr.GetString(3);
                livro.Ano = rdr.GetInt32(4);
                livro.Genero = rdr.GetString(5);
                livro.Quantidade = rdr.GetInt32(6);
                livro.Preco = rdr.GetDouble(7);

                livros.Add(livro);
            }

            con.Close();
            return livros;
        }

        public static void ExcluirLivros()
        {
            sql = "DELETE FROM livros";
            con.Open();
            cmd = new(sql, con);
            cmd.Prepare();
            cmd.ExecuteNonQuery();

            con.Close();
        }

        public static void ExcluirLivro(string codigo)
        {
            sql = "DELETE FROM livros WHERE codigo = @codigo";
            con.Open();
            cmd = new(sql, con);
            cmd.Parameters.AddWithValue("codigo", codigo);
            cmd.Prepare();
            cmd.ExecuteNonQuery();

            con.Close();
        }

        public static void AtualizarLivro(Livro livro)
        {
            sql = "UPDATE livros " +
                "SET nome = @nome, editora = @editora, autor = @autor, ano = @ano, genero = @genero, quantidade = @quantidade, preco = @preco " +
                "WHERE codigo = @codigo";
            con.Open();
            cmd = new(sql, con);
            cmd.Parameters.AddWithValue("nome", livro.Nome);
            cmd.Parameters.AddWithValue("codigo", livro.Codigo);
            cmd.Parameters.AddWithValue("editora", livro.Editora);
            cmd.Parameters.AddWithValue("autor", livro.Autor);
            cmd.Parameters.AddWithValue("ano", livro.Ano);
            cmd.Parameters.AddWithValue("genero", livro.Genero);
            cmd.Parameters.AddWithValue("quantidade", livro.Quantidade);
            cmd.Parameters.AddWithValue("preco", livro.Preco);

            cmd.Prepare();
            cmd.ExecuteNonQuery();

            con.Close();
        }

    }
}
