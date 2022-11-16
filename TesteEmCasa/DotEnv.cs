using System;
using System.IO;

namespace TesteEmCasa
{
    class DotEnv
    {
        
        private static void Carregar(string endereco)
        {
            if (!File.Exists(endereco)) return;
            Environment.SetEnvironmentVariable("credenciais", File.ReadAllLines(endereco)[0]);
        }

        public static void Carregar()
        {
            string dir = Directory.GetCurrentDirectory();
            Carregar(Path.Combine(dir, ".env"));
        }
    }
}
