using System.Linq;
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

    public new void Cadastrar()
    {
        ExibirCabecalho("Cadastro de Medicamento");

        try
        {
            Medicamento novoMedicamento = ObterDadosCadastrais();

            List<string> erros = novoMedicamento.Validar();

            if (erros.Count > 0)
            {
                Notificador.ExibirMensagensErro(erros);
                Cadastrar();
                return;
            }

            Medicamento? medicamentoExistente = repositorio.SelecionarTodos()
                .FirstOrDefault(m => m.Nome.Equals(novoMedicamento.Nome, StringComparison.OrdinalIgnoreCase)
                    && m.Fornecedor.Id == novoMedicamento.Fornecedor.Id);

            if (medicamentoExistente != null)
            {
                Medicamento medicamentoAtualizado = new Medicamento(
                    novoMedicamento.Nome,
                    novoMedicamento.Descricao,
                    medicamentoExistente.QuantidadeEstoque + novoMedicamento.QuantidadeEstoque,
                    novoMedicamento.Fornecedor
                );

                repositorio.Editar(medicamentoExistente.Id, medicamentoAtualizado);

                Notificador.ExibirMensagem($"A quantidade do medicamento \"{medicamentoExistente.Nome}\" foi atualizada para {medicamentoAtualizado.QuantidadeEstoque}.");
                return;
            }

            repositorio.Cadastrar(novoMedicamento);

            Notificador.ExibirMensagem($"O registro \"{novoMedicamento.Id}\" foi cadastrado com sucesso!");
        }
        catch (FormatException)
        {
            Notificador.ExibirMensagem("O formato do valor de um dos campos está inválido.");
            Cadastrar();
        }
        catch (Exception)
        {
            Notificador.ExibirMensagem("Ocorreu um erro inesperado. Tente novamente.");
            Cadastrar();
        }
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
           "{0, -7} | {1, -30} | {2, -15} | {3, -12} | {4, -20} | {5, -10}",
           "Id", "Nome", "Fornecedor", "Quantidade", "Descrição", "Status"
       );

        foreach (Medicamento m in medicamentos)
        {
            string status = m.QuantidadeEstoque < 20 ? "EM FALTA" : string.Empty;

            Console.WriteLine(
               "{0, -7} | {1, -30} | {2, -15} | {3, -12} | {4, -20} | {5, -10}",
               m.Id, m.Nome, m.Fornecedor.Nome, m.QuantidadeEstoque, m.Descricao, status
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
        string quantidadeEntrada = Console.ReadLine() ?? string.Empty;

        if (!int.TryParse(quantidadeEntrada, out int quantidadeEstoque))
            throw new FormatException();

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