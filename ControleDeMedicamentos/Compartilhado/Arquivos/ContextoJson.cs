using System.Text.Json;
using System.Text.Json.Serialization;
using ControleDeMedicamentos.ConsoleApp.ModuloPacientes;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios;
using ControleDeMedicamentos.ConsoleApp.ModuloEstoque.Entradas;
using ControleDeMedicamentos.ConsoleApp.ModuloEstoque.Saidas;
namespace ControleDeMedicamentos.ConsoleApp.Compartilhado.Arquivos;

public class ContextoJson
{
  private readonly string caminhoArquivo;
  public List<Fornecedor> Fornecedores { get; set; } = new List<Fornecedor>();
  public List<Paciente> Pacientes { get; set; } = new List<Paciente>();
  public List<Medicamento> Medicamentos { get; set; } = new List<Medicamento>();
  public List<Funcionario> Funcionarios { get; set; } = new List<Funcionario>();
  public List<Entrada> Entradas { get; set; } = new List<Entrada>();
  public List<Saida> Saidas { get; set; } = new List<Saida>();

  public ContextoJson()
  {
    string caminhoAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

    string caminhoDiretorio = Path.Combine(caminhoAppData, "ControleDeMedicamentos");

    Directory.CreateDirectory(caminhoDiretorio);

    caminhoArquivo = Path.Combine(caminhoDiretorio, "dados.json");
  }

  public void Salvar()
  {
    JsonSerializerOptions opcoesJson = new JsonSerializerOptions();
    opcoesJson.WriteIndented = true;
    opcoesJson.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    opcoesJson.ReferenceHandler = ReferenceHandler.Preserve;

    string jsonString = JsonSerializer.Serialize(this, opcoesJson);

    File.WriteAllText(caminhoArquivo, jsonString);
  }

  public void Carregar()
  {
    if (!File.Exists(caminhoArquivo))
      return;

    string jsonString = File.ReadAllText(caminhoArquivo);

    JsonSerializerOptions opcoesJson = new JsonSerializerOptions();
    opcoesJson.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    opcoesJson.ReferenceHandler = ReferenceHandler.Preserve;

    ContextoJson? contextoSalvo = JsonSerializer.Deserialize<ContextoJson>(jsonString, opcoesJson);

    if (contextoSalvo == null)
      return;

    this.Fornecedores = contextoSalvo.Fornecedores;
    this.Pacientes = contextoSalvo.Pacientes;
    this.Medicamentos = contextoSalvo.Medicamentos;
    this.Funcionarios = contextoSalvo.Funcionarios;
    this.Entradas = contextoSalvo.Entradas;
    this.Saidas = contextoSalvo.Saidas;
  }
}
