using System;
using static System.Console;

namespace TesteEmCasa
{
    //Modelos de página que vão ser exibidos no console
    public class ModelosG
    {

        public static void MarcaDAgua()
        {
            SetCursorPosition(10, 20);
            Write("Livraria SaLer");
        }

        public static void Consulta()
        {
            ForegroundColor = ConsoleColor.DarkGreen;
            WriteLine("======== CONSULTA DE LIVROS ({0}) =========", Program.livros.Count);
            ForegroundColor = ConsoleColor.Gray;
            WriteLine();
            Write(
                "Nome: \n" +
                "Código de barras: \n" +
                "Editora: \n" +
                "Autor: \n" +
                "Ano: \n" +
                "Gênero: \n" +
                "Preço: \n" +
                "Quantidade: \n"

                );
            MarcaDAgua();
        }

        public static void Consulta(Livro livro)
        {
            ForegroundColor = ConsoleColor.DarkGreen;
            WriteLine("======== CONSULTA DE LIVROS ({0}/{1}) =========", Program.livros.IndexOf(livro) + 1, Program.livros.Count);
            ForegroundColor = ConsoleColor.Gray;
            WriteLine();
            Write(
                "Nome: {0} \n" +
                "Código de barras: {1} \n" +
                "Editora: {2} \n" +
                "Autor: {3} \n" +
                "Ano: {4} \n" +
                "Gênero: {5} \n" +
                "Preço: {6} \n" +
                "Quantidade: {7} \n", livro.Nome, livro.Codigo, livro.Editora, livro.Autor, livro.Ano, livro.Genero, livro.Preco, livro.Quantidade
                );
            MarcaDAgua();
        }

        public static void Cadastro()
        {
            ForegroundColor = ConsoleColor.DarkGreen;
            WriteLine("======== CADASTRO DE LIVROS ({0}) =========", Program.livros.Count);
            ForegroundColor = ConsoleColor.Gray;
            WriteLine();
            Write(
                "Nome: \n" +
                "Código de barras: \n" +
                "Editora: \n" +
                "Autor: \n" +
                "Ano: \n" +
                "Gênero: \n" +
                "Preço: \n" +
                "Quantidade: \n"

                );
            MarcaDAgua();
        }

        public static void Cadastro(Livro livro)
        {
            ForegroundColor = ConsoleColor.DarkGreen;
            WriteLine("======== CADASTRO DE LIVROS ({0}) =========", Program.livros.Count);
            ForegroundColor = ConsoleColor.Gray;
            WriteLine();
            Write(
                "Nome: {0} \n" +
                "Código de barras: {1} \n" +
                "Editora: {2} \n" +
                "Autor: {3} \n" +
                "Ano: {4} \n" +
                "Gênero: {5} \n" +
                "Preço: {6} \n" +
                "Quantidade: \n", livro.Nome, livro.Codigo, livro.Editora, livro.Autor, livro.Ano, livro.Genero, livro.Preco
                );

            MarcaDAgua();
        }

    }
}
