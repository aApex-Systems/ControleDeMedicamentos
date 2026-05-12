using System;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios;

namespace ControleDeMedicamentos.ModuloEstoque.Entradas;

public class Entrada : EntidadeBase
{
    public DateTime Data { get; set; }
    public Medicamento Medicamento { get; set; }
    public Funcionario Funcionario { get; set; }
    public int Quantidade { get; set; }


public Entrada()
{
}

public Entrada(DateTime data, Medicamento medicamento, Funcionario funcionario, int quantidade)
{
    Data = data;
    Medicamento = medicamento;
    Funcionario = funcionario;
    Quantidade = quantidade;
}

public override void AtualizarDados(EntidadeBase entidadeAtualizada)
{
    throw new NotImplementedException();
}

public override List<string> Validar()
{
    List<string> erros = new List<string>();

    if (Medicamento == null)
        erros.Add("O medicamento é obrigatório.");

    if (Funcionario == null)
        erros.Add("O funcionário é obrigatório.");

    if (Quantidade <= 0)
        erros.Add("A quantidade deve ser maior que zero.");

    return erros;
}
}

