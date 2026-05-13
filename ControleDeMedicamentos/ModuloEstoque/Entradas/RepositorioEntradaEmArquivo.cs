using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Compartilhado.Arquivos;

namespace ControleDeMedicamentos.ConsoleApp.ModuloEstoque.Entradas;

public class RepositorioEntradaEmArquivo : RepositorioBaseEmArquivo<Entrada>, IRepositorio<Entrada>
{
  public RepositorioEntradaEmArquivo(ContextoJson contexto) : base(contexto)
  {
  }

  protected override List<Entrada> CarregarRegistros()
  {
    return contexto.Entradas;
  }
}