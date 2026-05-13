using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Utilidades;

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

        List<Paciente> pacientes = repositorio.SelecionarTodos();

        if (pacientes.Count == 0)
        {
          Notificador.ExibirMensagem("Nenhum item registrado.");
          return;
        }

        Console.WriteLine(
            "{0, -7} | {1, -25} | {2, -20} | {3, -20} | {4, -15}",
            "Id", "Nome", "Telefone", "Cartão SUS", "CPF"
        );

        foreach (Paciente p in pacientes)
        {
            Console.WriteLine(
                "{0, -7} | {1, -25} | {2, -20} | {3, -20} | {4, -15}",
                p.Id, p.Nome, p.Telefone, p.CartaoSUS, p.CPF
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
           if (p.Id != idIgnorado && p.CartaoSUS == novaEntidade.CartaoSUS)
           {
              erros.Add($"Já existe um paciente com o Cartão SUS \"{novaEntidade.CartaoSUS}\"");
              break;
           }
        }

        return erros;
    }
}
