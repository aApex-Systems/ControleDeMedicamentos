using System.Text.Json;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Compartilhado.Arquivos;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;
using ControleDeMedicamentos.ConsoleApp.ModuloPacientes;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;
using ControleDeMedicamentos.ConsoleApp.Utilidades;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios;
using ControleDeMedicamentos.ConsoleApp.ModuloEstoque.Entradas;
using ControleDeMedicamentos.ConsoleApp.ModuloEstoque.Saidas;

ContextoJson contexto = new ContextoJson();

try
{
    contexto.Carregar();
}
catch (JsonException)
{
    Notificador.ExibirMensagem("O arquivo de armazenamento está corrompido! Contate o suporte.");
    return;
}

IRepositorio<Fornecedor> repositorioFornecedor = new RepositorioFornecedorEmArquivo(contexto);
IRepositorio<Paciente> repositorioPaciente = new RepositorioPacienteEmArquivo(contexto);
IRepositorio<Medicamento> repositorioMedicamento = new RepositorioMedicamentoEmArquivo(contexto);
IRepositorio<Funcionario> repositorioFuncionario = new RepositorioFuncionarioEmArquivo(contexto);
IRepositorio<Entrada> repositorioEntrada = new RepositorioEntradaEmArquivo(contexto);
IRepositorio<Saida> repositorioSaida = new RepositorioSaidaEmArquivo(contexto);

TelaPrincipal telaPrincipal = new TelaPrincipal(
    repositorioFornecedor, 
    repositorioPaciente, 
    repositorioMedicamento, 
    repositorioFuncionario, 
    repositorioEntrada, 
    repositorioSaida
    );

    while (true)
{
  ITelaOpcoes? telaSelecionada = telaPrincipal.ApresentarMenuOpcoesPrincipal();

  if (telaSelecionada == null)
  {
    Console.Clear();
    break;
  }

  while (true)
  {
    string? opcaoSubMenu = telaSelecionada.ObterOpcaoMenu();

    if (opcaoSubMenu == "S")
    {
      Console.Clear();
      break;
    }

    if (telaSelecionada is ITelaCrud telaCrud)
    {
      if (opcaoSubMenu == "1")
        telaCrud.Cadastrar();

      else if (opcaoSubMenu == "2")
        telaCrud.Editar();

      else if (opcaoSubMenu == "3")
        telaCrud.Excluir();

      else if (opcaoSubMenu == "4")
        telaCrud.VisualizarTodos(deveExibirCabecalho: true);
    }
  }
}


