


internal class Program
{
    private static void Main(string[] args)
    {
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
}