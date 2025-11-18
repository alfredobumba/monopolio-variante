// Importa a classe Console para ler e escrever no terminal
using System;
// Importa o GameManager para gerenciar o jogo
using MonopolioVariante.Managers;

// Define o namespace principal da aplicação
namespace MonopolioVariante
{
    // Classe principal do programa
    // Esta é a classe que é executada quando o programa inicia
    class Program
    {
        // Método Main: ponto de entrada do programa
        // É o primeiro método executado quando o programa inicia
        // args: argumentos da linha de comando (não usado neste caso)
        static void Main(string[] args)
        {
            // Cria um novo gerenciador de jogo
            // Este objeto vai processar todos os comandos do utilizador
            GameManager gameManager = new GameManager();
            
            // Loop infinito: continua até o utilizador dar uma linha vazia
            while (true)
            {
                // Lê uma linha de texto do terminal (comando do utilizador)
                string input = Console.ReadLine();
                
                // Verifica se a linha está vazia ou só tem espaços em branco
                if (string.IsNullOrWhiteSpace(input))
                {
                    // Se estiver vazia, sai do loop (termina o programa)
                    break;
                }
                
                // Processa o comando recebido
                // O GameManager vai interpretar e executar o comando
                gameManager.ProcessCommand(input);
            }
            // Quando sair do loop, o programa termina
        }
    }
}