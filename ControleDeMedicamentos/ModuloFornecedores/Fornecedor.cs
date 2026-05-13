using ControleDeMedicamentos.ConsoleApp.Compartilhado;

public class Fornecedor : EntidadeBase
{
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string Cnpj { get; set; }

    public Fornecedor()
    {
    }
    public Fornecedor(string nome, string telefone, string cnpj)
    {
        Nome = nome;
        Telefone = telefone;
        Cnpj = cnpj;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (Nome.Length < 3 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter entre 3 e 100 caracteres.");

        int contadorDigitos = 0;
        bool contemLetraOuSimbolo = false;

        string telefoneEncurtado = Telefone.Replace(" ", "").Replace("-", "");

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

        if (string.IsNullOrWhiteSpace(Cnpj))
            erros.Add("O campo \"CNPJ\" deve ser preenchido");

        int contadorDigitosCnpj = 0;
        bool contemLetraOuSimboloCnpj = false;

        string cnpjEncurtado = Cnpj.Replace(" ", "").Replace("-", "").Replace(".", "").Replace("/", "");

        for (int i = 0; i < cnpjEncurtado.Length; i++)
        {
            char caractereAtualCnpj = cnpjEncurtado[i];

            if (char.IsDigit(caractereAtualCnpj))
            {
                contadorDigitosCnpj++;
            }
            else
            {
                contemLetraOuSimboloCnpj = true;
                break;
            }
        }

        if (contadorDigitosCnpj != 14)
            erros.Add("O campo \"CNPJ\" deve conter 14 dígitos");

        if (contemLetraOuSimboloCnpj)
            erros.Add("O campo \"CNPJ\" deve conter somente dígitos");

        if (erros.Count == 0)
        {
            Cnpj = Convert.ToUInt64(cnpjEncurtado).ToString(@"00\.000\.000\/0000\-00");
        }

        return erros;
    }

    public override void AtualizarDados(EntidadeBase entidadeAtualizada)
    {
        Fornecedor fornecedorAtualizado = (Fornecedor)entidadeAtualizada;

        Nome = fornecedorAtualizado.Nome;
        Telefone = fornecedorAtualizado.Telefone;
        Cnpj = fornecedorAtualizado.Cnpj;
    }
}