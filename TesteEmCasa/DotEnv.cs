using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
