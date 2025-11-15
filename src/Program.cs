using System; // Importa o namespace System, que contém classes básicas como Console para entrada e saída.

namespace MonopolioVariante // Define um namespace chamado MonopolioVariante, que serve para organizar o código.
{
    class Program // Define a classe principal do programa chamada Program.
    {
        static void Main(string[] args) // Método Main: ponto de entrada do programa. É aqui que a execução começa.
        {
            // Cria uma instância da classe GameManager, que provavelmente gerencia a lógica do jogo.
            GameManager gameManager = new GameManager();
            
            // Loop infinito que ficará esperando comandos do usuário.
            while (true)
            {
                // Lê uma linha digitada pelo usuário no console.
                string input = Console.ReadLine();
                
                // Se o usuário não digitar nada (linha vazia ou só espaços), o loop é interrompido.
                if (string.IsNullOrWhiteSpace(input))
                {
                    break;
                }
                
                // Caso haja algum comando, passa a entrada para o GameManager processar.
                gameManager.ProcessCommand(input);
            }
        }
    }
}
