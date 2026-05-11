using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloPacientes;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;

namespace ControleDeMedicamentos.ConsoleApp.Utilidades;

public class TelaPrincipal
{
  private readonly IRepositorio<Fornecedor> repositorioFornecedor;

  private readonly IRepositorio<Paciente> repositorioPaciente;

  public TelaPrincipal(IRepositorio<Paciente> repositorioPaciente, IRepositorio<Fornecedor> repositorioFornecedor)
  {
    this.repositorioPaciente = repositorioPaciente;
    this.repositorioFornecedor = repositorioFornecedor;
  }

  public ITelaOpcoes? ApresentarMenuOpcoesPrincipal()
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine("Controle de Medicamentos");
        Console.WriteLine("---------------------------------");
        Console.WriteLine("1 - Gerenciar Pacientes");
        Console.WriteLine("2 - Gestão de Fornecedores");
        Console.WriteLine("S - Sair");
        Console.WriteLine("---------------------------------");
        Console.Write("> ");
        string? opcaoMenuPrincipal = Console.ReadLine()?.ToUpper();

        if (opcaoMenuPrincipal == "1")
            return new TelaPaciente(repositorioPaciente);              

        if (opcaoMenuPrincipal == "2")
            return new TelaFornecedor(repositorioFornecedor);

        return null;
    }
}