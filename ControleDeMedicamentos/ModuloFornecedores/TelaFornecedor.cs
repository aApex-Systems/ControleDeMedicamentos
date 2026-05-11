using ControleDeMedicamentos.ConsoleApp.Compartilhado;

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

        Console.WriteLine(
            "{0, -7} | {1, -30} | {2, -15} | {3, -17}",
            "Id", "Nome", "Telefone", "CNPJ"
        );

        List<Fornecedor> registros = repositorio.SelecionarTodos();

        foreach (Fornecedor f in registros)
        {
            Console.WriteLine(
                "{0, -7} | {1, -30} | {2, -15} | {3, -17}",
                f.Id, f.Nome, f.Telefone, f.Cnpj
            );
        }

        if (deveExibirCabecalho)
        {
            Console.WriteLine("---------------------------------");
            Console.Write("Digite ENTER para continuar...");
            Console.ReadLine();
        }
    }

    protected override Fornecedor ObterDadosCadastrais()
    {
        Console.Write("Digite o nome do fornecedor: ");
        string nome = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite o telefone do fornecedor: ");
        string telefone = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite o CNPJ do fornecedor: ");
        string cnpj = Console.ReadLine() ?? string.Empty;

        return new Fornecedor(nome, telefone, cnpj);
    }

    protected override List<string> ValidarRegistroDuplicado(Fornecedor novaEntidade, string? idIgnorado = null)
    {
        List<string> erros = [];

        List<Fornecedor> registros = repositorio.SelecionarTodos();

        foreach (Fornecedor f in registros)
        {
            if (f.Id != idIgnorado && f.Cnpj == novaEntidade.Cnpj)
                erros.Add("Já existe o registro de um fornecedor com o CNPJ informado.");
        }

        return erros;
    }
}
