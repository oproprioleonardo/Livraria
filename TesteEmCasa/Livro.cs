namespace TesteEmCasa
{
    public class Livro
    {

        public string Nome { get; set; }
        public string Autor { get; set; }
        public string Editora { get; set; }
        public int Ano { get; set; }
        public string Genero { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }

        public string Codigo { get; set; }

        public Livro(string nome, string autor, string editora, int ano, string genero, int quantidade, decimal preco, string codigo)
        {
            Nome = nome;
            Autor = autor;
            Editora = editora;
            Ano = ano;
            Genero = genero;
            Quantidade = quantidade;
            Preco = preco;
            Codigo = codigo;
        }

        public Livro()
        {
        }

    }
}
