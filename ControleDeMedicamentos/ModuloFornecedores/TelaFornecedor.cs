using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Utilidades;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;

public class TelaFornecedor : TelaBase<Fornecedor>, ITelaOpcoes, ITelaCrud
{
    public TelaFornecedor(IRepositorio<Fornecedor> repositorio) : base("Fornecedor", repositorio)
    {
    }

    public override string? ObterOpcaoMenu()
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"Gestão de Fornecedor");
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"1 - Cadastrar fornecedor");
        Console.WriteLine($"2 - Editar fornecedor");
        Console.WriteLine($"3 - Excluir fornecedor");
        Console.WriteLine($"4 - Visualizar fornecedores");
        Console.WriteLine("S - Voltar para o início");
        Console.WriteLine("---------------------------------");
        Console.Write("> ");
        string? opcaoMenu = Console.ReadLine()?.ToUpper();

        return opcaoMenu;
    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
            ExibirCabecalho("Visualização de Fornecedores");

        List<Fornecedor> fornecedores = repositorio.SelecionarTodos();

        if (fornecedores.Count == 0)
        {
            Notificador.ExibirMensagem("Nenhum fornecedor registrado.");
            return;
        }

        Console.WriteLine(
            "{0, -7} | {1, -30} | {2, -15} | {3, -20}",
            "Id", "Nome", "Telefone", "CNPJ"
        );

        foreach (Fornecedor f in fornecedores)
        {
            Console.WriteLine(
                "{0, -7} | {1, -30} | {2, -15} | {3, -20}",
                f.Id, f.Nome, f.Telefone, f.Cnpj
            );
        }

        if (deveExibirCabecalho)
        {
        Console.WriteLine("---------------------------------");
        Console.WriteLine("Pressione Enter para voltar ao menu...");
        Console.ReadLine();
        }
    }

    protected override Fornecedor ObterDadosCadastrais()
    {
        Console.Write("Digite o nome do fornecedor: ");
        string nome = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite o telefone do fornecedor com DDD: ");
        string telefone = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite o CNPJ do fornecedor: ");
        string cnpj = Console.ReadLine() ?? string.Empty;

        return new Fornecedor(nome, telefone, cnpj);
    }

    protected override List<string> ValidarRegistroDuplicado(Fornecedor novaEntidade, string? idIgnorado = null)
    {
        List<string> erros = new List<string>();

        List<Fornecedor> fornecedores = repositorio.SelecionarTodos();

        foreach (Fornecedor f in fornecedores)
        {
            if (f.Id != idIgnorado && f.Cnpj == novaEntidade.Cnpj)
            {
                erros.Add($"Já existe um fornecedor com o Cnpj \"{novaEntidade.Cnpj}\"");
                break;
            }
        }

        return erros;
    }
}
