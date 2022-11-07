﻿using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using static System.Console;

namespace TesteEmCasa
{
    public class Program
    {

        static List<Livro> getLivros()
        {
            return BancoDeDados.PegarLivros();
        }

        static void Main(string[] args)
        {
            WriteLine("Iniciando...");
            bool continua = true;
            List<Option> options = new();

            options.Add(new Option("Cadastrar", CadastrarLivro));
            options.Add(new Option("Consultar", ConsultarLivro));
            options.Add(new Option("Alterar", () => continua = false));
            options.Add(new Option("Excluir", ExcluirLivro));
            options.Add(new Option("Sair", () => continua = false));

            DotEnv.Carregar();
            BancoDeDados.NovaConexao();
            BancoDeDados.CriarTabela();

            do
            {
                Clear();
                Menu menu = new();
                menu.Title = "============ Livraria SaLer ============";
                menu.Description = "Você deseja fazer qual operação com os livros?";
                menu.Options = options;
                menu.Show();


            } while (continua);

            Clear();
            WriteLine("Volte sempre à gerência da livraria SaLer");
            ReadKey();
        }

        public static void ConsultarLivro()
        {
            Livro livro = new();
            Menu menu = new($"======== CONSULTA DE LIVROS ({getLivros().Count}) =========");

            var codInput = new Input<string>("Código:", (entrada) => livro.Codigo = entrada, (en) => en);
            var nomInput = new Input<string>("Nome:");
            var editInput = new Input<string>("Editora:");
            var autInput = new Input<string>("Autor:");
            var anoInput = new Input<int>("Ano:");
            var genInput = new Input<string>("Genêro:");
            var precInput = new Input<double>("Preço:");
            var qntInput = new Input<int>("Quantidade:");

            menu.Inputs.Add(codInput);
            menu.Inputs.Add(nomInput);
            menu.Inputs.Add(editInput);
            menu.Inputs.Add(autInput);
            menu.Inputs.Add(anoInput);
            menu.Inputs.Add(genInput);
            menu.Inputs.Add(precInput);
            menu.Inputs.Add(qntInput);
            menu.Show(() =>
            {
                if (BancoDeDados.ExisteLivro(livro.Codigo))
                {
                    livro = BancoDeDados.PegarLivro(livro.Codigo);
                    menu.Title = $"======== CONSULTA DE LIVROS =========";
                    nomInput.Value = livro.Nome;
                    editInput.Value = livro.Editora;
                    autInput.Value = livro.Autor;
                    anoInput.Value = livro.Ano;
                    genInput.Value = livro.Genero;
                    precInput.Value = livro.Preco;
                    qntInput.Value = livro.Quantidade;
                    menu.Show(true);
                }

            });

        }

        public static void ExcluirLivro()
        {
            Livro livro = new();
            Menu menu = new($"======== EXCLUIR LIVROS ({getLivros().Count}) =========");

            var codInput = new Input<string>("Código:", (entrada) => livro.Codigo = entrada, (en) => en);
            var nomInput = new Input<string>("Nome:");
            var editInput = new Input<string>("Editora:");
            var autInput = new Input<string>("Autor:");
            var anoInput = new Input<int>("Ano:");
            var genInput = new Input<string>("Genêro:");
            var precInput = new Input<double>("Preço:");
            var qntInput = new Input<int>("Quantidade:");

            menu.Inputs.Add(codInput);
            menu.Inputs.Add(nomInput);
            menu.Inputs.Add(editInput);
            menu.Inputs.Add(autInput);
            menu.Inputs.Add(anoInput);
            menu.Inputs.Add(genInput);
            menu.Inputs.Add(precInput);
            menu.Inputs.Add(qntInput);
            menu.Show(() =>
            {
                if (BancoDeDados.ExisteLivro(livro.Codigo))
                {
                    livro = BancoDeDados.PegarLivro(livro.Codigo);
                    menu.Title = $"======== LIVRO EXCLUÍDO =========";
                    nomInput.Value = livro.Nome;
                    editInput.Value = livro.Editora;
                    autInput.Value = livro.Autor;
                    anoInput.Value = livro.Ano;
                    genInput.Value = livro.Genero;
                    precInput.Value = livro.Preco;
                    qntInput.Value = livro.Quantidade;

                    BancoDeDados.ExcluirLivro(livro.Codigo);

                    menu.Show(true);
                }

            });

        }

        public static void CadastrarLivro()
        {
            Livro livro = new();
            Menu menu = new($"======== CADASTRO DE LIVROS ({getLivros().Count}) =========");

            var codInput = new Input<string>("Código:", (entrada) => livro.Codigo = entrada, (en) => en);
            var nomInput = new Input<string>("Nome:", (entrada) => livro.Nome = entrada, (en) => en, false);
            var editInput = new Input<string>("Editora:", (entrada) => livro.Editora = entrada, (en) => en, false);
            var autInput = new Input<string>("Autor:", (entrada) => livro.Autor = entrada, (en) => en, false);
            var anoInput = new Input<int>("Ano:", (entrada) => livro.Ano = entrada, (en) => int.Parse(en), false);
            var genInput = new Input<string>("Genêro:", (entrada) => livro.Genero = entrada, (en) => en, false);
            var precInput = new Input<double>("Preço:", (entrada) => livro.Preco = entrada, (en) => double.Parse(en), false);
            var qntInput = new Input<int>("Quantidade:", (entrada) => livro.Quantidade = entrada, (en) => int.Parse(en), false);

            menu.Inputs.Add(codInput);
            menu.Inputs.Add(nomInput);
            menu.Inputs.Add(editInput);
            menu.Inputs.Add(autInput);
            menu.Inputs.Add(anoInput);
            menu.Inputs.Add(genInput);
            menu.Inputs.Add(precInput);
            menu.Inputs.Add(qntInput);


            menu.Show(() =>
            {
                if (BancoDeDados.ExisteLivro(livro.Codigo))
                {
                    livro = BancoDeDados.PegarLivro(livro.Codigo);
                    nomInput.Value = livro.Nome;
                    editInput.Value = livro.Editora;
                    autInput.Value = livro.Autor;
                    anoInput.Value = livro.Ano;
                    genInput.Value = livro.Genero;
                    precInput.Value = livro.Preco;

                    qntInput.OnRead = (entrada) => livro.Quantidade += entrada;
                    qntInput.Read = true;

                    menu.Show(() =>
                    {
                        qntInput.Value = livro.Quantidade;
                        BancoDeDados.AtualizarLivro(livro);
                        menu.Show(true);
                    });
                }
                else
                {
                    menu.Inputs.FindAll(inp => inp != codInput).ForEach(inp => inp.Read = true);
                    menu.Show(() => BancoDeDados.InserirLivro(livro));

                }

            });

        }
    }
}
