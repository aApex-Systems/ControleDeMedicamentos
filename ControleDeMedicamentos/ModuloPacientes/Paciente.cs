using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPacientes;

public class Paciente : EntidadeBase
{
  public string Nome { get; set; }
  public string Telefone { get; set; }
  public string CartaoSUS { get; set; }
  public string CPF { get; set; }

  public Paciente()
  {

  }

  public Paciente(string nome, string telefone, string cartaoSUS, string cpf)
  {
     Nome = nome;
     Telefone = telefone;
     CartaoSUS = cartaoSUS;
     CPF = cpf;
  }

  public override void AtualizarDados(EntidadeBase entidadeAtualizada)
  {
     Paciente pacienteAtualizado = (Paciente)entidadeAtualizada;

     Nome = pacienteAtualizado.Nome;
     Telefone = pacienteAtualizado.Telefone;
     CartaoSUS = pacienteAtualizado.CartaoSUS;
     CPF = pacienteAtualizado.CPF;
  }

  public override List<string> Validar()
  {
     List<string> erros = [];

     ValidarNome(erros);
     ValidarTelefone(erros);
     ValidarCPF(erros);
     ValidarCartaoSUS(erros);

     return erros;
  }

  private void ValidarNome(List<string> erros)
  {
    if (Nome.Length < 3 || Nome.Length > 100)
      erros.Add("O campo \"Nome\" deve conter entre 3 e 100 caracteres.");
  }

  private void ValidarTelefone(List<string> erros)
  {
    string telefoneEncurtado = RemoverFormatacao(Telefone);

    if (telefoneEncurtado.StartsWith("0"))
      telefoneEncurtado = telefoneEncurtado.Substring(1);

    bool telefoneValido = true;

    if (telefoneEncurtado.Length < 10 || telefoneEncurtado.Length > 11)
    {
      erros.Add("O campo \"Telefone\" deve conter entre 10 e 11 dígitos.");
      telefoneValido = false;
    }

    if (!ContemSomenteDigitos(telefoneEncurtado))
    {
      erros.Add("O campo \"Telefone\" deve conter apenas dígitos.");
      telefoneValido = false;
    }

    if (telefoneValido)
    {
      if (telefoneEncurtado.Length == 10)
      {
        Telefone = Convert.ToUInt64(telefoneEncurtado)
            .ToString(@"\(00\) 0000\-0000");
      }
      else
      {
        Telefone = Convert.ToUInt64(telefoneEncurtado)
            .ToString(@"\(00\) 00000\-0000");
      }
    }
  }

  private void ValidarCPF(List<string> erros)
  {
    if (string.IsNullOrWhiteSpace(CPF))
    {
      erros.Add("O campo \"CPF\" deve ser preenchido.");
      return;
    }

    string cpfEncurtado = RemoverFormatacao(CPF);

    bool cpfValido = true;

    if (cpfEncurtado.Length != 11)
    {
      erros.Add("O campo \"CPF\" deve conter 11 dígitos.");
      cpfValido = false;
    }

    if (!ContemSomenteDigitos(cpfEncurtado))
    {
      erros.Add("O campo \"CPF\" deve conter somente dígitos.");
      cpfValido = false;
    }

    if (cpfValido)
    {
      CPF = Convert.ToUInt64(cpfEncurtado)
          .ToString(@"000\.000\.000\-00");
    }
  }

  private void ValidarCartaoSUS(List<string> erros)
  {
    string cartaoSUSFormatado = RemoverFormatacao(CartaoSUS);

    bool cartaoValido = true;

    if (cartaoSUSFormatado.Length != 15)
    {
      erros.Add("O campo \"Cartão SUS\" deve conter 15 dígitos.");
      cartaoValido = false;
    }

    if (!ContemSomenteDigitos(cartaoSUSFormatado))
    {
      erros.Add("O campo \"Cartão SUS\" deve conter apenas dígitos.");
      cartaoValido = false;
    }

    if (cartaoValido)
      CartaoSUS = cartaoSUSFormatado;
  }

  private bool ContemSomenteDigitos(string valor)
  {
    for (int i = 0; i < valor.Length; i++)
    {
      if (!char.IsDigit(valor[i]))
        return false;
    }

    return true;
  }

  private string RemoverFormatacao(string valor)
  {
    return valor
        .Replace(" ", "")
        .Replace("-", "")
        .Replace(".", "")
        .Replace("/", "")
        .Replace("(", "")
        .Replace(")", "");
  }
}
