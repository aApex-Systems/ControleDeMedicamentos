using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloEstoque.Entradas;
using ControleDeMedicamentos.ConsoleApp.ModuloEstoque.Saidas;

namespace ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;

public class Medicamento : EntidadeBase
{
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public int QuantidadeEstoque { get; set; }
    public List<Saida> RequisicoesDeSaida { get; set; } = [];
   public List<Entrada> RequisicoesDeEntrada { get; set; } = [];
   public Fornecedor Fornecedor { get; set; } 
   public string StatusEstoque => QuantidadeEstoque < 20 ? "EM FALTA" : "EM ESTOQUE";

    public Medicamento()
    {
    }

    public Medicamento(string nome, string descricao, int quantidadeEstoque, Fornecedor fornecedor)
    {
        Nome = nome;
        Descricao = descricao;
        QuantidadeEstoque = quantidadeEstoque;
        Fornecedor = fornecedor;
    }

    public void RegistrarRequisicaoEntrada(Entrada requisicao)
    {
        RequisicoesDeEntrada.Add(requisicao);
        QuantidadeEstoque += (int)requisicao.Quantidade;
    }
   public void RegistrarRequisicaoSaida(Saida requisicao)
  {
    RequisicoesDeSaida.Add(requisicao);

    var medPrescrito = requisicao.MedicamentosPrescritos.FirstOrDefault(mp => mp.Medicamento == this);

    if (medPrescrito != null)
      QuantidadeEstoque -= (int)medPrescrito.Quantidade;
  }

  public override List<string> Validar()
  {
    List<string> erros = new List<string>();

    if (Nome.Length < 3 || Nome.Length > 100)
      erros.Add("O campo \"Nome\" deve conter entre 3 e 100 caracteres.");

    if (Descricao.Length < 5 || Descricao.Length > 255)
      erros.Add("O campo \"Descrição\" deve conter entre 5 e 255 caracteres.");

    return erros;
  }
  public override void AtualizarDados(EntidadeBase entidadeAtualizada)
  {
    Medicamento medicamentoAtualizado = (Medicamento)entidadeAtualizada;

    Nome = medicamentoAtualizado.Nome;
    Descricao = medicamentoAtualizado.Descricao;
    Fornecedor = medicamentoAtualizado.Fornecedor;
    QuantidadeEstoque = medicamentoAtualizado.QuantidadeEstoque;
  }
}  