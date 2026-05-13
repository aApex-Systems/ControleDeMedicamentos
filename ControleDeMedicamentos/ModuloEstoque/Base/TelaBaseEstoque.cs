namespace ControleDeMedicamentos.ConsoleApp.Compartilhado;

public abstract class TelaBaseEstoque<T> where T : EntidadeBase
{
  protected string nomeEntidade;
  protected IRepositorio<T> repositorio;

  protected TelaBaseEstoque(string nomeEntidade, IRepositorio<T> repositorio)
  {
    this.nomeEntidade = nomeEntidade;
    this.repositorio = repositorio;
  }

  public virtual string? ObterOpcaoMenu()
  {
    Console.Clear();
    Console.WriteLine("---------------------------------");
    Console.WriteLine($"Gestão de {nomeEntidade}");
    Console.WriteLine("---------------------------------");
    Console.WriteLine($"1 - Registrar {nomeEntidade}");
    Console.WriteLine($"2 - Visualizar {nomeEntidade}s");
    Console.WriteLine("S - Voltar para o Início");
    Console.WriteLine("---------------------------------");
    Console.Write("> ");
    string? opcaoMenu = Console.ReadLine()?.ToUpper();

    return opcaoMenu;
  }

  public abstract void Cadastrar();

  public abstract void VisualizarTodos();

  protected void ExibirCabecalho(string titulo)
  {
    Console.Clear();
    Console.WriteLine("---------------------------------");
    Console.WriteLine($"Gestão de Estoque");
    Console.WriteLine("---------------------------------");
    Console.WriteLine(titulo);
    Console.WriteLine("---------------------------------");
  }

}
