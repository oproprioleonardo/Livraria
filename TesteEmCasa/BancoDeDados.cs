using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace TesteEmCasa
{
    public class BancoDeDados
    {
        private static string credenciais;
        private static SqlConnection con;
        private static string sql;
        private static SqlCommand cmd;

        public static void NovaConexao()
        {
            credenciais = Environment.GetEnvironmentVariable("credenciais");
            con = new SqlConnection(credenciais);
        }

        public static void CriarTabela()
        {
            sql = "CREATE TABLE livros(" +
                "codigo VARCHAR(13) PRIMARY KEY, " +
                "nome VARCHAR(50), " +
                "editora VARCHAR(30), " +
                "autor VARCHAR(50)," +
                "ano INT, " +
                "genero VARCHAR(25), " +
                "quantidade INT, " +
                "preco MONEY" +
                ")";
            con.Open();
            cmd = new(sql, con);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch { }
            finally
            {
                con.Close();
            }

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
            cmd.Parameters.Add(new SqlParameter("@codigo", SqlDbType.VarChar, 13)).Value = livro.Codigo;
            cmd.Parameters.Add(new SqlParameter("@nome", SqlDbType.VarChar, 50)).Value = livro.Nome;
            cmd.Parameters.Add(new SqlParameter("@editora", SqlDbType.VarChar, 30)).Value = livro.Editora;
            cmd.Parameters.Add(new SqlParameter("@autor", SqlDbType.VarChar, 50)).Value = livro.Autor;
            cmd.Parameters.Add(new SqlParameter("@ano", SqlDbType.Int)).Value = livro.Ano;
            cmd.Parameters.Add(new SqlParameter("@genero", SqlDbType.VarChar, 25)).Value = livro.Genero;
            cmd.Parameters.Add(new SqlParameter("@quantidade", SqlDbType.Int)).Value = livro.Quantidade;
            cmd.Parameters.Add(new SqlParameter("@preco", SqlDbType.Money)).Value = livro.Preco;
            cmd.Prepare();
            cmd.ExecuteNonQuery();

            con.Close();
        }

        public static Livro PegarLivro(string codigo)
        {
            Livro livro = new();
            sql = "SELECT * FROM livros WHERE codigo = @codigo";
            con.Open();
            cmd = new(sql, con);
            cmd.Parameters.Add(new SqlParameter("@codigo", SqlDbType.VarChar, 13)).Value = codigo;
            cmd.Prepare();
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                livro.Codigo = rdr.GetString(0);
                livro.Nome = rdr.GetString(1);
                livro.Editora = rdr.GetString(2);
                livro.Autor = rdr.GetString(3);
                livro.Ano = rdr.GetInt32(4);
                livro.Genero = rdr.GetString(5);
                livro.Quantidade = rdr.GetInt32(6);
                livro.Preco = ((decimal)rdr.GetSqlMoney(7));
            }
            con.Close();
            return livro;
        }

        public static bool ExisteLivro(string codigo)
        {
            sql = "SELECT * FROM livros WHERE codigo = @codigo";
            con.Open();
            cmd = new(sql, con);
            cmd.Parameters.Add(new SqlParameter("@codigo", SqlDbType.VarChar, 13)).Value = codigo;
            cmd.Prepare();
            SqlDataReader rdr = cmd.ExecuteReader();
            bool result = rdr.Read();
            con.Close();
            return result;
        }

        public static List<Livro> PegarLivros()
        {
            List<Livro> livros = new();
            sql = "SELECT * FROM livros";
            con.Open();
            cmd = new(sql, con);
            SqlDataReader rdr = cmd.ExecuteReader();
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
                livro.Preco = ((decimal)rdr.GetSqlMoney(7));

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
            cmd.Parameters.Add(new SqlParameter("@codigo", SqlDbType.VarChar, 13)).Value = codigo;
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
            cmd.Parameters.Add(new SqlParameter("@codigo", SqlDbType.VarChar, 13)).Value = livro.Codigo;
            cmd.Parameters.Add(new SqlParameter("@nome", SqlDbType.VarChar, 50)).Value = livro.Nome;
            cmd.Parameters.Add(new SqlParameter("@editora", SqlDbType.VarChar, 30)).Value = livro.Editora;
            cmd.Parameters.Add(new SqlParameter("@autor", SqlDbType.VarChar, 50)).Value = livro.Autor;
            cmd.Parameters.Add(new SqlParameter("@ano", SqlDbType.Int)).Value = livro.Ano;
            cmd.Parameters.Add(new SqlParameter("@genero", SqlDbType.VarChar, 25)).Value = livro.Genero;
            cmd.Parameters.Add(new SqlParameter("@quantidade", SqlDbType.Int)).Value = livro.Quantidade;
            cmd.Parameters.Add(new SqlParameter("@preco", SqlDbType.Money)).Value = livro.Preco;

            cmd.Prepare();
            cmd.ExecuteNonQuery();

            con.Close();
        }

    }
}
