using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;

namespace ControleDeMedicamentos.ConsoleApp.ModuloEstoque.Entradas;

public class Entrada : EntidadeBase
{
  public DateTime Data { get; set; }
  public Medicamento Medicamento { get; set; }
  public Funcionario Funcionario { get; set; }
  public uint Quantidade { get; set; } = 0;

  public Entrada()
  {

  }

  public Entrada(Medicamento medicamento, Funcionario funcionario, uint quantidade) : this()
  {
    Data = DateTime.Now;
    Medicamento = medicamento;
    Funcionario = funcionario;
    Quantidade = quantidade;

    Medicamento.RegistrarRequisicaoEntrada(this);
  }
  public override void AtualizarDados(EntidadeBase entidadeAtualizada)
  {
    Entrada entradaAtualizada = (Entrada)entidadeAtualizada;

    Medicamento = entradaAtualizada.Medicamento;
    Funcionario = entradaAtualizada.Funcionario;
    Quantidade = entradaAtualizada.Quantidade;
  }

  public override List<string> Validar()
  {
    List<string> erros = new List<string>();

    if (Data == default || Data > DateTime.Now)
      erros.Add("O campo \"Data\" deve ser uma data válida e não pode ser uma data futura.");

    if (Medicamento == null)
      erros.Add("O campo \"Medicamento\" é obrigatório.");

    if (Funcionario == null)
      erros.Add("O campo \"Funcionário\" é obrigatório.");

    // if (Quantidade <= 0)
    //   erros.Add("O campo 'Quantidade' deve ser um número positivo.");

    return erros;
  }
}
