using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloPacientes;

namespace ControleDeMedicamentos.ConsoleApp.ModuloEstoque.Saidas;

public class Saida : EntidadeBase
{
  public DateTime Data { get; set; }
  public Paciente Paciente { get; set; }
  public List<MedicamentoPrescrito> MedicamentosPrescritos { get; set; } = [];


  public Saida(Paciente paciente, List<MedicamentoPrescrito> medicamentosPrescritos) : this()
  {
    Data = DateTime.Now;
    Paciente = paciente;
    MedicamentosPrescritos = medicamentosPrescritos;

    foreach (MedicamentoPrescrito medPresc in MedicamentosPrescritos)
    {
      medPresc.Medicamento.RegistrarRequisicaoSaida(this);
    }
  }
  public Saida()
  {
  }

  public override void AtualizarDados(EntidadeBase entidadeAtualizada)
  {
    Saida saidaAtualizada = (Saida)entidadeAtualizada;

    Paciente = saidaAtualizada.Paciente;
    MedicamentosPrescritos = saidaAtualizada.MedicamentosPrescritos;
  }

  public override List<string> Validar()
  {
    List<string> erros = [];

    if (Data == default || Data > DateTime.Now)
      erros.Add("O campo \"Data\" deve ser uma data válida e não pode ser futura.");

    if (Paciente == null)
      erros.Add("O campo \"Paciente\" é obrigatório.");

    if (MedicamentosPrescritos == null || MedicamentosPrescritos.Count == 0)
      erros.Add("É necessário selecionar um medicamento.");

    return erros;
  }
}