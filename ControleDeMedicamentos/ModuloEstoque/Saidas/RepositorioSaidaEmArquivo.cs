using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Compartilhado.Arquivos;

namespace ControleDeMedicamentos.ConsoleApp.ModuloEstoque.Saidas;

public class RepositorioSaidaEmArquivo : RepositorioBaseEmArquivo<Saida>, IRepositorio<Saida>
{
  public RepositorioSaidaEmArquivo(ContextoJson contexto) : base(contexto)
  {
  }

  protected override List<Saida> CarregarRegistros()
  {
    return contexto.Saidas;
  }
}
