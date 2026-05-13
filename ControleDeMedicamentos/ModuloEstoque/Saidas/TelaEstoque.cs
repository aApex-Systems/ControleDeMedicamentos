using System;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloEstoque.Entradas;
using ControleDeMedicamentos.ConsoleApp.ModuloEstoque.Saidas;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;
using ControleDeMedicamentos.ConsoleApp.ModuloPacientes;

namespace ControleDeMedicamentos.ConsoleApp.ModuloEstoque;

public class TelaEstoque : ITelaOpcoes
{
  private readonly IRepositorio<Entrada> repositorioEntrada;
  private readonly IRepositorio<Saida> repositorioSaida;
  private readonly IRepositorio<Medicamento> repositorioMedicamento;
  private readonly IRepositorio<Funcionario> repositorioFuncionario;
  private readonly IRepositorio<Paciente> repositorioPaciente;

  public TelaEstoque(
      IRepositorio<Entrada> repositorioEntrada,
      IRepositorio<Saida> repositorioSaida,
      IRepositorio<Medicamento> repositorioMedicamento,
      IRepositorio<Funcionario> repositorioFuncionario,
      IRepositorio<Paciente> repositorioPaciente)
  {
    this.repositorioEntrada = repositorioEntrada;
    this.repositorioSaida = repositorioSaida;
    this.repositorioMedicamento = repositorioMedicamento;
    this.repositorioFuncionario = repositorioFuncionario;
    this.repositorioPaciente = repositorioPaciente;
  }

  public string? ObterOpcaoMenu()
  {
    Console.Clear();
    Console.WriteLine("---------------------------------");
    Console.WriteLine("Gestão de Estoque");
    Console.WriteLine("---------------------------------");
    Console.WriteLine("1 - Requisições de Entrada");
    Console.WriteLine("2 - Requisições de Saída");
    Console.WriteLine("S - Voltar para o início");
    Console.WriteLine("---------------------------------");
    Console.Write("> ");
    string? opcao = Console.ReadLine()?.ToUpper();

    if (opcao == "1")
      MenuEntrada();

    else if (opcao == "2")
      MenuSaida();

    if (opcao == "S")
      return "S";

    return string.Empty;
  }

  private void MenuEntrada()
  {
    TelaEntrada telaEntrada = new(
        repositorioEntrada,
        repositorioMedicamento,
        repositorioFuncionario);

    while (true)
    {
      string? opcao = telaEntrada.ObterOpcaoMenu();

      if (opcao == "S")
        break;

      if (opcao == "1")
        telaEntrada.Cadastrar();

      else if (opcao == "2")
        telaEntrada.VisualizarTodos();
    }
  }

  private void MenuSaida()
  {
    TelaSaida telaSaida = new TelaSaida(repositorioSaida, repositorioPaciente, repositorioMedicamento);

    while (true)
    {
      string? opcao = telaSaida.ObterOpcaoMenu();

      if (opcao == "S")
        break;

      if (opcao == "1")
        telaSaida.Cadastrar();

      else if (opcao == "2")
        telaSaida.VisualizarTodos();
    }
  }
}