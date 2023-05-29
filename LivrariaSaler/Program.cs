using System;
using LivrariaSaler.repositories;
using LivrariaSaler.ui;

namespace LivrariaSaler
{
    internal class Program
    {
        public static void Main()
        {
            Console.WriteLine("Olá, bem-vindo ao sistema!");
            Console.WriteLine("Tentando estabelecer conexão com o banco...");
            const string credentials = "";
            var repo = new PgBookRepository(credentials);
            Console.WriteLine("Conexão estabelecida! " + new DateTime());
            var menuManager = new MenuManager(repo);
            menuManager.CreateMainMenu();
        }
    }
}