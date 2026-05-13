using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios;

public class Funcionario : EntidadeBase
{
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string Cpf { get; set; }

    public Funcionario()
    {
    }

    public Funcionario(string nome, string telefone, string cpf)
    {
        Nome = nome;
        Telefone = telefone;
        Cpf = cpf;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (Nome.Length < 3 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter entre 3 e 100 caracteres.");

        int contadorDigitos = 0;
        bool contemLetraOuSimbolo = false;

        string telefoneEncurtado = Telefone.Replace(" ", "").Replace("-", "").Replace(".", "").Replace(")", "").Replace("(", "");

        if (telefoneEncurtado.StartsWith("0"))
            telefoneEncurtado = telefoneEncurtado.Substring(1);

        for (int i = 0; i < telefoneEncurtado.Length; i++)
        {
            char caractereAtual = telefoneEncurtado[i];

            if (char.IsDigit(caractereAtual))
            {
                contadorDigitos++;
            }
            else
            {
                contemLetraOuSimbolo = true;
                break;
            }
        }

        if (contadorDigitos < 10 || contadorDigitos > 11)
            erros.Add("O campo \"Telefone\" deve conter entre 10 e 11 dígitos");

        if (contemLetraOuSimbolo)
            erros.Add("O campo \"Telefone\" deve conter apenas dígitos");

        if (erros.Count == 0)
        {
            if (contadorDigitos == 10)
            {
                Telefone = Convert.ToUInt64(telefoneEncurtado)
                            .ToString(@"\(00\) 0000\-0000");
            }
            else if (contadorDigitos == 11)
            {
                Telefone = Convert.ToUInt64(telefoneEncurtado)
                            .ToString(@"\(00\) 00000\-0000");
            }
        }

        if (string.IsNullOrWhiteSpace(Cpf))
            erros.Add("O campo \"CPF\" deve ser preenchido");

        int contadorDigitosCpf = 0;
        bool contemLetraOuSimboloCpf = false;

        string cpfEncurtado = Cpf.Replace(" ", "").Replace("-", "").Replace(".", "").Replace("/", "");

        for (int i = 0; i < cpfEncurtado.Length; i++)
        {
            char caractereAtualCpf = cpfEncurtado[i];

            if (char.IsDigit(caractereAtualCpf))
            {
                contadorDigitosCpf++;
            }
            else
            {
                contemLetraOuSimboloCpf = true;
                break;
            }
        }

        if (contadorDigitosCpf != 11)
            erros.Add("O campo \"CPF\" deve conter 11 dígitos");

        if (contemLetraOuSimboloCpf)
            erros.Add("O campo \"CPF\" deve conter somente dígitos");

        if (erros.Count == 0)
        {
            Cpf = Convert.ToUInt64(cpfEncurtado).ToString(@"000\.000\.000\-00");
        }

        return erros;
    }
    public override void AtualizarDados(EntidadeBase entidadeAtualizada)
    {
        Funcionario funcionarioAtualizado = (Funcionario)entidadeAtualizada;

        Nome = funcionarioAtualizado.Nome;
        Telefone = funcionarioAtualizado.Telefone;
        Cpf = funcionarioAtualizado.Cpf;
    }
}