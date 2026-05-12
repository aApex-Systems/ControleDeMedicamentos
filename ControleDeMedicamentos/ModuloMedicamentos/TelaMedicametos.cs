using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;
using ControleDeMedicamentos.ConsoleApp.Utilidades;

namespace ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;

public class TelaMedicamento : TelaBase<Medicamento>, ITelaOpcoes, ITelaCrud
{
    private readonly IRepositorio<Fornecedor> repositorioFornecedor;

    public TelaMedicamento(IRepositorio<Medicamento> repositorio, IRepositorio<Fornecedor> repositorioFornecedor) : base("Medicamento", repositorio)
    {
        this.repositorioFornecedor = repositorioFornecedor;
    }

 public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
            ExibirCabecalho("Visualização de Medicamentos");

        List<Medicamento> medicamentos = repositorio.SelecionarTodos();

        if (medicamentos.Count == 0)
        {
            Notificador.ExibirMensagem("Nenhum medicamento registrado");
            return;
        }

        Console.WriteLine(
           "{0, -7} | {1, -30} | {2, -15} | {3, -15} | {4, -20}",
           "Id", "Nome", "Fornecedor", "Quantidade", "Descrição"
       );

        foreach (Medicamento m in medicamentos)
        {
            Console.WriteLine(
               "{0, -7} | {1, -30} | {2, -15} | {3, -15} | {4, -20}",
               m.Id, m.Nome, m.Fornecedor.Nome, m.QuantidadeEstoque, m.Descricao
           );
        }

        if (deveExibirCabecalho)
        {
        Console.WriteLine("---------------------------------");
        Console.WriteLine("Pressione Enter para voltar ao menu...");
        Console.ReadLine();
        }
    }

    protected override Medicamento ObterDadosCadastrais()
    {
        Console.Write("Digite o nome do medicamento: ");
        string nome = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite a descrição do medicamento: ");
        string descricao = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite a quantidade em estoque: ");
        int quantidadeEstoque = Convert.ToUInt16(Console.ReadLine());

        Fornecedor? fornecedorSelecionado;

        do
        {
            Console.WriteLine("---------------------------------");
            VisualizarFornecedores();
            Console.WriteLine("---------------------------------");

            Console.Write("Digite o Id do fornecedor do medicamento: ");
            string idSelecionado = Console.ReadLine() ?? string.Empty;

            fornecedorSelecionado = repositorioFornecedor.SelecionarPorId(idSelecionado);

        } while (fornecedorSelecionado == null);

        return new Medicamento(nome, descricao, quantidadeEstoque, fornecedorSelecionado);
    }

    private void VisualizarFornecedores()
    {
        List<Fornecedor> fornecedores = repositorioFornecedor.SelecionarTodos();

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
    }
}