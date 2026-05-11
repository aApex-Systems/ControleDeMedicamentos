using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPacientes;

public class TelaPaciente : TelaBase<Paciente>, ITelaCrud, ITelaOpcoes
{
    public TelaPaciente(IRepositorio<Paciente> repositorio) : base("Paciente", repositorio)
    {
    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
            ExibirCabecalho("Visualização de Pacientes");

        Console.WriteLine(
            "{0, -7} | {1, -30} | {2, -15} | {3, -17} | {4, -17}",
            "Id", "Nome", "Telefone", "CPF", "Cartão SUS"
        );

        List<Paciente> registros = repositorio.SelecionarTodos();

        foreach (Paciente p in registros)
        {
            Console.WriteLine(
                "{0, -7} | {1, -30} | {2, -15} | {3, -17} | {4, -17}",
                p.Id, p.Nome, p.Telefone, p.Cpf, p.CartaoSus
            );
        }

        if (deveExibirCabecalho)
        {
            Console.WriteLine("---------------------------------");
            Console.Write("Digite ENTER para continuar...");
            Console.ReadLine();
        }
    }

    protected override Paciente ObterDadosCadastrais()
    {
        Console.Write("Digite o nome do paciente: ");
        string nome = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite o telefone do paciente: ");
        string telefone = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite o CPF do paciente: ");
        string cpf = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite o cartão do SUS do paciente: ");
        string cartaoSus = Console.ReadLine() ?? string.Empty;

        return new Paciente(nome, telefone, cpf, cartaoSus);
    }

    protected override List<string> ValidarRegistroDuplicado(Paciente novaEntidade, string? idIgnorado = null)
    {
        List<string> erros = [];

        List<Paciente> registros = repositorio.SelecionarTodos();

        foreach (Paciente p in registros)
        {
            if (p.Id != idIgnorado && p.Cpf == novaEntidade.Cpf)
                erros.Add("Já existe o registro de um paciente com o CPF informado.");

            if (p.Id != idIgnorado && p.CartaoSus == novaEntidade.CartaoSus)
                erros.Add("Já existe o registro de um paciente com o Cartão SUS informado.");
        }

        return erros;
    }
}
