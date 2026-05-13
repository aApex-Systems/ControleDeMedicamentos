using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloEstoque.Base;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;
using ControleDeMedicamentos.ConsoleApp.ModuloPacientes;
using ControleDeMedicamentos.ConsoleApp.Utilidades;

namespace ControleDeMedicamentos.ConsoleApp.ModuloEstoque.Saidas;

public class TelaSaida : TelaBaseEstoque<Saida>, ITelaOpcoes, ITelaCR
{
  private readonly IRepositorio<Paciente> repositorioPaciente;
  private readonly IRepositorio<Medicamento> repositorioMedicamento;

  public TelaSaida(IRepositorio<Saida> repositorio, IRepositorio<Paciente> repositorioPaciente, IRepositorio<Medicamento> repositorioMedicamento) : base("Requisição de Saída", repositorio)
  {
    this.repositorioPaciente = repositorioPaciente;
    this.repositorioMedicamento = repositorioMedicamento;
  }

  public override void Cadastrar()
  {
    ExibirCabecalho("Cadastro de Requisição de Saída");

    VisualizarPacientes();

    string idPaciente;

    do
    {
      Console.Write("Digite o ID do paciente que receberá a medicação: ");
      idPaciente = Console.ReadLine() ?? string.Empty;

      if (idPaciente.Length == 7)
        break;
    } while (true);

    Paciente paciente = repositorioPaciente.SelecionarPorId(idPaciente)!;

    List<MedicamentoPrescrito> medicamentosPrescritos = [];

    do
    {
      Console.WriteLine("---------------------------------");
      Console.WriteLine($"Paciente: {paciente.Nome}");
      Console.WriteLine("---------------------------------");

      if (medicamentosPrescritos.Count > 0)
      {
        Console.WriteLine("Medicamentos selecionados");
        Console.WriteLine("---------------------------------");

        Console.WriteLine(
            "{0, -30} | {1, -30}",
            "Medicamento", "Quantidade"
        );

        foreach (MedicamentoPrescrito medPresc in medicamentosPrescritos)
        {
          Console.WriteLine(
              "{0, -30} | {1, -30}",
              medPresc.Medicamento.Nome, medPresc.Quantidade
          );
        }

        Console.WriteLine("---------------------------------");
      }

      Console.WriteLine("Medicamentos para adicionar");

      VisualizarMedicamentos();

      Console.Write("Digite o ID do medicamento que deseja retirar (ou S para sair): ");
      string idMedicamento = Console.ReadLine() ?? string.Empty;

      if (idMedicamento.ToUpper() == "S")
        break;

      Medicamento? medicamentoSelecionado = repositorioMedicamento.SelecionarPorId(idMedicamento);

      if (medicamentoSelecionado == null)
      {
        Console.WriteLine("Medicamento não encontrado. Digite um ID válido.");
        continue;
      }

      Console.Write("Digite a quantidade do medicamento que deseja retirar: ");
      uint quantidade = Convert.ToUInt32(Console.ReadLine());

      if (quantidade > medicamentoSelecionado.QuantidadeEstoque)
      {
        Console.WriteLine($"Medicamento em falta.\nQuantidade em Estoque: {medicamentoSelecionado.QuantidadeEstoque}.");
        Console.ReadLine();
        Cadastrar();
        return;
      }

      MedicamentoPrescrito medPrescrito = new(medicamentoSelecionado, quantidade);

      medicamentosPrescritos.Add(medPrescrito);
    } while (true);

    Saida requisicaoSaida = new(paciente, medicamentosPrescritos);

    repositorio.Cadastrar(requisicaoSaida);

    Notificador.ExibirMensagem($"O registro \"{requisicaoSaida.Id}\" foi cadastrado com sucesso!");
  }

  public override void VisualizarTodos()
  {
    ExibirCabecalho("Visualização de Requisições de Saída");

    List<Saida> requisicoes = repositorio.SelecionarTodos();

    Console.WriteLine(
        "{0, -7} | {1, -18} | {2, -20} | {3, -40}",
        "Id", "Data de Criação", "Paciente", "Medicamentos Requisitados"
    );

    foreach (Saida s in requisicoes)
    {
      List<string> medicamentosStr = new List<string>();

      foreach (MedicamentoPrescrito medPresc in s.MedicamentosPrescritos)
      {
        string medicamentoStr = $"({medPresc.Medicamento.Nome}, {medPresc.Quantidade})";

        medicamentosStr.Add(medicamentoStr);
      }

      Console.Write("{0, -7} | ", s.Id);
      Console.Write("{0, -18} | ", s.Data.ToShortDateString());
      Console.Write("{0, -20} | ", s.Paciente.Nome);
      Console.Write("{0, -40}", string.Join(", ", medicamentosStr));

      Console.WriteLine();
    }

    Console.WriteLine("---------------------------------");
    Console.Write("Digite ENTER para continuar...");
    Console.ReadLine();
  }

  public void VisualizarPacientes()
  {
    List<Paciente> pacientes = repositorioPaciente.SelecionarTodos();

    if (pacientes.Count == 0)
    {
      Notificador.ExibirMensagem("Nenhum item registrado.");
      return;
    }

    Console.WriteLine("---------------------------------");

    Console.WriteLine(
        "{0, -7} | {1, -25} | {2, -20} | {3, -20} | {4, -15}",
        "ID", "Nome", "Telefone", "Cartão SUS", "CPF"
    );

    foreach (Paciente p in pacientes)
    {

      Console.WriteLine(
          "{0, -7} | {1, -25} | {2, -20} | {3, -20} | {4, -15}",
          p.Id, p.Nome, p.Telefone, p.CartaoSUS, p.CPF
      );
    }

    Console.WriteLine("---------------------------------");
  }

  private void VisualizarMedicamentos()
  {
    Console.WriteLine("---------------------------------");


    Console.WriteLine(
       "{0, -7} | {1, -30} | {2, -15} | {3, -15} | {4, -15} | {5, -20}",
       "Id", "Nome", "Fornecedor", "Quantidade", "Status Estoque", "Descrição"
   );

    List<Medicamento> medicamentos = repositorioMedicamento.SelecionarTodos();

    foreach (Medicamento m in medicamentos)
    {
      Console.WriteLine(
         "{0, -7} | {1, -30} | {2, -15} | {3, -15} | {4, -15} | {5, -20}",
         m.Id, m.Nome, m.Fornecedor.Nome, m.QuantidadeEstoque, m.StatusEstoque, m.Descricao
     );
    }

    Console.WriteLine("---------------------------------");
  }
}