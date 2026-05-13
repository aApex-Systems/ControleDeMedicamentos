using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloEstoque.Base;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;
using ControleDeMedicamentos.ConsoleApp.Utilidades;

namespace ControleDeMedicamentos.ConsoleApp.ModuloEstoque.Entradas;

public interface ITelaEntrada
{
    void Cadastrar();
    void VisualizarTodos();
}

public class TelaEntrada : TelaBaseEstoque<Entrada>, ITelaOpcoes, ITelaCR, ITelaEntrada
{
    private readonly IRepositorio<Medicamento> repositorioMedicamento;
    private readonly IRepositorio<Funcionario> repositorioFuncionario;

    public TelaEntrada(IRepositorio<Entrada> repositorio,
      IRepositorio<Medicamento> repositorioMedicamento,
      IRepositorio<Funcionario> repositorioFuncionario)
      : base("Requisição de Entrada", repositorio)
    {
        this.repositorioMedicamento = repositorioMedicamento;
        this.repositorioFuncionario = repositorioFuncionario;
    }

    public override void Cadastrar()
    {
        ExibirCabecalho("Cadastro de Requisição de Entrada");

        VisualizarFuncionarios();

        string idFuncionario;

        do
        {
            Console.Write("Digite o ID do funcionário que fará a requisição: ");
            idFuncionario = Console.ReadLine() ?? string.Empty;

            if (idFuncionario.Length == 7)
                break;
        } while (true);

        Funcionario funcionario = repositorioFuncionario.SelecionarPorId(idFuncionario)!;

        VisualizarMedicamentos();

        string idMedicamento;

        do
        {
            Console.Write("Digite o ID do medicamento que será requisitado: ");
            idMedicamento = Console.ReadLine() ?? string.Empty;

            if (idMedicamento.Length == 7)
                break;
        } while (true);

        Medicamento medicamento = repositorioMedicamento.SelecionarPorId(idMedicamento)!;

        Console.Write("Digite a quantidade do medicamento que será requisitada: ");
        uint quantidade = Convert.ToUInt32(Console.ReadLine());

        Entrada entrada = new(
            medicamento,
            funcionario,
            quantidade
        );

        repositorio.Cadastrar(entrada);

        Notificador.ExibirMensagem($"O registro \"{entrada.Id}\" foi cadastrado com sucesso!");
    }

    public override void VisualizarTodos()
    {
        ExibirCabecalho("Visualização de Requisições de Entrada");

        Console.WriteLine(
            "{0, -7} | {1, -18} | {2, -20} | {3, -20} | {4, -18}",
            "Id", "Data de Criação", "Funcionário", "Medicamento", "Quantidade"
        );

        List<Entrada> requisicoes = repositorio.SelecionarTodos();

        foreach (Entrada req in requisicoes)
        {
            Console.WriteLine(
                "{0, -7} | {1, -18} | {2, -20} | {3, -20} | {4, -18}",
                req.Id,
                req.Data.ToShortDateString(),
                req.Funcionario.Nome,
                req.Medicamento.Nome,
                req.Quantidade
            );
        }

        Console.WriteLine("---------------------------------");
        Console.Write("Digite ENTER para continuar...");
        Console.ReadLine();
    }

    private void VisualizarMedicamentos()
    {
        List<Medicamento> medicamentos = repositorioMedicamento.SelecionarTodos();

        if (medicamentos.Count == 0)
        {
            Notificador.ExibirMensagem("Nenhum medicamento registrado");
            return;
        }

        Console.WriteLine("---------------------------------");

        Console.WriteLine(
           "{0, -7} | {1, -30} | {2, -15} | {3, -15} | {4, -20} | {5, -15}",
           "Id", "Nome", "Fornecedor", "Quantidade", "Descrição", "Status Estoque"
       );

        foreach (Medicamento m in medicamentos)
        {
            Console.WriteLine(
               "{0, -7} | {1, -30} | {2, -15} | {3, -15} | {4, -20} | {5, -10}",
               m.Id, m.Nome, m.Fornecedor.Nome, m.QuantidadeEstoque, m.Descricao, m.StatusEstoque
           );
        }

        Console.WriteLine("---------------------------------");
    }

    private void VisualizarFuncionarios()
    {
        List<Funcionario> funcionarios = repositorioFuncionario.SelecionarTodos();

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

        Console.WriteLine("---------------------------------");
    }
}