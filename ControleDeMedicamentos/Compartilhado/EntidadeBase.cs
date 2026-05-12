using System.Security.Cryptography;
using System.Text.Json.Serialization;

namespace ControleDeMedicamentos.ConsoleApp.Compartilhado;

public abstract class EntidadeBase
{
    [JsonInclude]
    public string Id { get; private set; } = string.Empty;

   public EntidadeBase()
{
    if (string.IsNullOrEmpty(Id))
    {
        Id = Convert
            .ToHexString(RandomNumberGenerator.GetBytes(4))
            .ToLower()
            .Substring(0, 7);
    }
}

    public abstract List<string> Validar();
    public abstract void AtualizarDados(EntidadeBase entidadeAtualizada);
}
