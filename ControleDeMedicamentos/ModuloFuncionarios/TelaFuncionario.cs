using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Utilidades;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios;

public class TelaFuncionario : TelaBase<Funcionario>, ITelaOpcoes, ITelaCrud
{
    public TelaFuncionario(IRepositorio<Funcionario> repositorio) : base("Funcionário", repositorio)
    {
    }
    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
            ExibirCabecalho("Visualização de Funcionários");

        List<Funcionario> funcionarios = repositorio.SelecionarTodos();

        if (funcionarios.Count == 0)
        {
            Notificador.ExibirMensagem("Nenhum funcionário registrado");
            return;
        }

        Console.WriteLine(
            "{0, -7} | {1, -30} | {2, -15} | {3, -20}",
            "Id", "Nome", "Telefone", "CPF"
        );

        foreach (Funcionario f in funcionarios)
        {
            Console.WriteLine(
                "{0, -7} | {1, -30} | {2, -15} | {3, -20}",
                f.Id, f.Nome, f.Telefone, f.Cpf
            );
        }

        if (deveExibirCabecalho)
        {
        Console.WriteLine("---------------------------------");
        Console.WriteLine("Pressione Enter para voltar ao menu...");
        Console.ReadLine();
        }

    }

    protected override Funcionario ObterDadosCadastrais()
    {
        Console.Write("Digite o nome do funcionário: ");
        string nome = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite o telefone do funcionário: ");
        string telefone = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite o CPF do funcionário: ");
        string cpf = Console.ReadLine() ?? string.Empty;

        return new Funcionario(nome, telefone, cpf);
    }

    protected override List<string> ValidarRegistroDuplicado(Funcionario novaEntidade, string? idIgnorado = null)
    {
        List<string> erros = new List<string>();

        List<Funcionario> funcionarios = repositorio.SelecionarTodos();

        foreach (Funcionario f in funcionarios)
        {
            if (f.Id != idIgnorado && f.Cpf == novaEntidade.Cpf)
            {
                erros.Add($"Já existe um funcionário com esse CPF \"{novaEntidade.Cpf}\"");
                break;
            }
        }

        return erros;
    }
}