using System;
using System.Collections.Generic;
using LivrariaSaler.models;
using LivrariaSaler.repositories;

namespace LivrariaSaler.ui;

public class MenuManager
{
    private readonly IBookRepository _repository;

    public MenuManager(IBookRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public void CreateMainMenu()
    {
        var continua = true;
        var options = new List<Option>
        {
            new("Cadastrar", BookCreateMenu),
            new("Consultar", BookQueryMenu),
            new("Alterar", BookUpdateMenu),
            new("Excluir", BookDeleteMenu),
            new("Sair", () => continua = false)
        };

        do
        {
            Console.Clear();
            var menu = new Menu
            {
                Title = "============ Livraria SaLer ============",
                Description = "Você deseja fazer qual operação com os livros?",
                Options = options
            };
            menu.Show();
        } while (continua);

        Console.Clear();
        Console.WriteLine("Volte sempre à livraria SaLer");
        Console.ReadKey();
    }

    private void BookQueryMenu()
    {
        var book = new Book();
        var menu = new Menu($"======== CONSULTA DE LIVROS ({_repository.Count()}) =========");

        var codInput = new Input<string>("Código:", entrada => book.Code = entrada, en => en);
        var nomInput = new Input<string>("Nome:");
        var editInput = new Input<string>("Editora:");
        var autInput = new Input<string>("Autor:");
        var anoInput = new Input<int>("Ano:");
        var genInput = new Input<string>("Genêro:");
        var precInput = new Input<decimal>("Preço:");
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
            book = _repository.Find(book.Code);
            if (book.Persisted)
            {
                menu.Title = "======== CONSULTA DE LIVROS =========";
                nomInput.Value = book.Name;
                editInput.Value = book.PubHouse;
                autInput.Value = book.Author;
                anoInput.Value = book.Year;
                genInput.Value = book.Gender;
                precInput.Value = book.Price;
                qntInput.Value = book.Quantity;
                menu.Show(true);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Livro não encontrado. Tecle para voltar ao menu.");
                Console.ReadKey();
            }
        });
    }

    private void BookDeleteMenu()
    {
        var book = new Book();
        var menu = new Menu($"======== EXCLUIR LIVROS ({_repository.Count()}) =========");

        var codInput = new Input<string>("Código:", entrada => book.Code = entrada, en => en);
        var nomInput = new Input<string>("Nome:");
        var editInput = new Input<string>("Editora:");
        var autInput = new Input<string>("Autor:");
        var anoInput = new Input<int>("Ano:");
        var genInput = new Input<string>("Genêro:");
        var precInput = new Input<decimal>("Preço:");
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
            book = _repository.Find(book.Code);
            if (book.Persisted)
            {
                menu.Title = "======== LIVRO EXCLUÍDO =========";
                nomInput.Value = book.Name;
                editInput.Value = book.PubHouse;
                autInput.Value = book.Author;
                anoInput.Value = book.Year;
                genInput.Value = book.Gender;
                precInput.Value = book.Price;
                qntInput.Value = book.Quantity;

                _repository.Delete(book.Code);

                menu.Show(true);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Livro não encontrado. Tecle para voltar ao menu.");
                Console.ReadKey();
            }
        });
    }

    private void BookCreateMenu()
    {
        var book = new Book();
        var menu = new Menu($"======== CADASTRO DE LIVROS ({_repository.Count()}) =========");

        var codInput = new Input<string>("Código:", entrada => book.Code = entrada, en => en);
        var nomInput = new Input<string>("Nome:", entrada => book.Name = entrada, en => en, false);
        var editInput = new Input<string>("Editora:", entrada => book.PubHouse = entrada, en => en, false);
        var autInput = new Input<string>("Autor:", entrada => book.Author = entrada, en => en, false);
        var anoInput = new Input<int>("Ano:", entrada => book.Year = entrada, int.Parse, false);
        var genInput = new Input<string>("Genêro:", entrada => book.Gender = entrada, en => en, false);
        var precInput = new Input<decimal>("Preço:", entrada => book.Price = entrada, decimal.Parse, false);
        var qntInput = new Input<int>("Quantidade:", entrada => book.Quantity = entrada, int.Parse, false);

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
            book = _repository.Find(book.Code);
            if (book.Persisted)
            {
                nomInput.Value = book.Name;
                editInput.Value = book.PubHouse;
                autInput.Value = book.Author;
                anoInput.Value = book.Year;
                genInput.Value = book.Gender;
                precInput.Value = book.Price;

                qntInput.OnRead = entrada => book.Quantity += entrada;
                qntInput.Read = true;

                menu.Show(() =>
                {
                    qntInput.Value = book.Quantity;
                    _repository.Update(book);
                    menu.Show(true);
                });
            }
            else
            {
                menu.Inputs.FindAll(inp => inp != codInput).ForEach(inp => inp.Read = true);
                menu.Show(() => _repository.Insert(book));
            }
        });
    }

    private void BookUpdateMenu()
    {
        var book = new Book();
        var menu = new Menu($"======== ALTERAÇÃO DE LIVROS ({_repository.Count()}) =========");

        var codInput = new Input<string>("Código:", entrada => book.Code = entrada, en => en);
        var nomInput = new Input<string>("Nome:", entrada => book.Name = entrada != "" ? entrada : book.Name,
            en => en, false);
        var editInput = new Input<string>("Editora:",
            entrada => book.PubHouse = entrada != "" ? entrada : book.PubHouse, en => en, false);
        var autInput = new Input<string>("Autor:", entrada => book.Author = entrada != "" ? entrada : book.Author,
            en => en, false);
        var anoInput = new Input<int>("Ano:", entrada => book.Year = entrada, int.Parse, false);
        var genInput = new Input<string>("Genêro:", entrada => book.Gender = entrada != "" ? entrada : book.Gender,
            en => en, false);
        var precInput = new Input<decimal>("Preço:", entrada => book.Price = entrada, decimal.Parse, false);
        var qntInput = new Input<int>("Quantidade:", entrada => book.Quantity = entrada, int.Parse, false);

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
            book = _repository.Find(book.Code);
            if (book.Persisted)
            {
                menu.Title = "======== ALTERAR LIVRO =========";
                menu.Inputs.FindAll(inp => inp != codInput).ForEach(inp => inp.Read = true);

                menu.Show(() =>
                {
                    _repository.Update(book);
                    nomInput.Value = book.Name;
                    editInput.Value = book.PubHouse;
                    autInput.Value = book.Author;
                    anoInput.Value = book.Year;
                    genInput.Value = book.Gender;
                    precInput.Value = book.Price;
                    qntInput.Value = book.Quantity;
                    menu.Show(true);
                });
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Livro não encontrado. Tecle para voltar ao menu.");
                Console.ReadKey();
            }
        });
    }
}