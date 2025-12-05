// Importa classes para usar exceções e output
using System;
// Importa classes para usar listas
using System.Collections.Generic;
// Importa LINQ para usar métodos como FirstOrDefault, Any, OrderBy
using System.Linq;
// Importa as classes de modelo (Player)
using MonopolioVariante.Models;

// Define o namespace para os gerenciadores
namespace MonopolioVariante.Managers
{
    // Classe que gerencia o sistema de jogadores e jogos
    // Responsável por registar jogadores, listar jogadores e iniciar jogos
    public class GameManager
    {
        // Lista de todos os jogadores registados no sistema
        // Guarda jogadores mesmo entre jogos diferentes
        private List<Player> registeredPlayers;
        
        // Jogo atualmente em curso (null se não houver jogo a decorrer)
        private Game currentGame;

        // Construtor: inicializa o gerenciador
        // Cria listas vazias e não há jogo no início
        public GameManager()
        {
            registeredPlayers = new List<Player>();  // Cria lista vazia de jogadores
            currentGame = null;                      // Não há jogo no início
        }

        // Processa um comando recebido do utilizador
        // input: string com o comando (ex: "RJ Alice", "IJ Alice Bob Charlie Diana")
        public void ProcessCommand(string input)
        {
            // Divide o comando em partes, separando por espaços
            // RemoveEmptyEntries remove espaços vazios extra
            string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            
            // Se não há partes (linha vazia), não faz nada
            if (parts.Length == 0)
            {
                return;  // Sai do método
            }

            // A primeira parte é o comando (ex: "RJ", "LJ", "IJ")
            string command = parts[0];

            // Try-catch para capturar erros e mostrar "Instrução inválida."
            try
            {
                // Switch para executar a ação correta baseada no comando
                switch (command)
                {
                    // RJ <nome> - Registar Jogador
                    case "RJ":
                        // Verifica se tem exatamente 2 partes (RJ + nome)
                        if (parts.Length != 2)
                        {
                            Console.WriteLine("Instrução inválida.");
                            return;  // Sai do método
                        }
                        // Chama método para registar o jogador
                        RegisterPlayer(parts[1]);
                        break;
                    
                    // LJ - Listar Jogadores
                    case "LJ":
                        // Verifica se tem exatamente 1 parte (só "LJ")
                        if (parts.Length != 1)
                        {
                            Console.WriteLine("Instrução inválida.");
                            return;
                        }
                        // Chama método para listar jogadores
                        ListPlayers();
                        break;
                    
                    // IJ <j1> <j2> <j3> <j4> - Iniciar Jogo
                    case "IJ":
                        // Verifica se tem exatamente 5 partes (IJ + 4 nomes)
                        if (parts.Length != 5)
                        {
                            Console.WriteLine("Instrução inválida.");
                            return;
                        }
                        // Chama método para iniciar jogo com os 4 jogadores
                        StartGame(parts[1], parts[2], parts[3], parts[4]);
                        break;
                    
                    // LD <nome> - Lançar Dados
                    case "LD":
                        // Verifica se tem exatamente 2 partes (LD + nome)
                        if (parts.Length != 2)
                        {
                            Console.WriteLine("Instrução inválida.");
                            return;
                        }
                        // Se há um jogo em curso
                        if (currentGame != null)
                        {
                            // Delega ao jogo para processar o lançamento de dados
                            currentGame.RollDice(parts[1]);
                        }
                        else
                        {
                            // Se não há jogo, mostra erro
                            Console.WriteLine("Não existe um jogo em curso.");
                        }
                        break;
                    
                    // CE <nome> - Comprar Espaço
                    case "CE":
                        // Verifica se tem exatamente 2 partes (CE + nome)
                        if (parts.Length != 2)
                        {
                            Console.WriteLine("Instrução inválida.");
                            return;
                        }
                        // Se há um jogo em curso
                        if (currentGame != null)
                        {
                            // Delega ao jogo para processar a compra
                            currentGame.BuySpace(parts[1]);
                        }
                        else
                        {
                            Console.WriteLine("Não existe um jogo em curso.");
                        }
                        break;
                    
                    // DJ - Detalhes do Jogo
                    case "DJ":
                        // Verifica se tem exatamente 1 parte (só "DJ")
                        if (parts.Length != 1)
                        {
                            Console.WriteLine("Instrução inválida.");
                            return;
                        }
                        // Se há um jogo em curso
                        if (currentGame != null)
                        {
                            // Delega ao jogo para mostrar detalhes
                            currentGame.ShowGameDetails();
                        }
                        else
                        {
                            Console.WriteLine("Não existe jogo em curso.");
                        }
                        break;
                    
                    // TT <nome> - Terminar Turno
                    case "TT":
                        // Verifica se tem exatamente 2 partes (TT + nome)
                        if (parts.Length != 2)
                        {
                            Console.WriteLine("Instrução inválida.");
                            return;
                        }
                        // Se há um jogo em curso
                        if (currentGame != null)
                        {
                            // Delega ao jogo para terminar o turno
                            currentGame.EndTurn(parts[1]);
                        }
                        else
                        {
                            Console.WriteLine("Não existe jogo em curso.");
                        }
                        break;
                    
                    // PA <nome> - Pagar Aluguer
                    case "PA":
                        // Verifica se tem exatamente 2 partes (PA + nome)
                        if (parts.Length != 2)
                        {
                            Console.WriteLine("Instrução inválida.");
                            return;
                        }
                        // Se há um jogo em curso
                        if (currentGame != null)
                        {
                            // Delega ao jogo para processar o pagamento
                            currentGame.PayRent(parts[1]);
                        }
                        else
                        {
                            Console.WriteLine("Não existe um jogo em curso.");
                        }
                        break;
                    
                    // CC <nome> <espaço> - Comprar Casa
                    case "CC":
                        // Verifica se tem exatamente 3 partes (CC + nome + espaço)
                        if (parts.Length != 3)
                        {
                            Console.WriteLine("Instrução inválida.");
                            return;
                        }
                        // Se há um jogo em curso
                        if (currentGame != null)
                        {
                            // Delega ao jogo para comprar casa
                            currentGame.BuyHouse(parts[1], parts[2]);
                        }
                        else
                        {
                            Console.WriteLine("Não existe um jogo em curso.");
                        }
                        break;
                    
                    // TC <nome> - Tirar Carta
                    case "TC":
                        // Verifica se tem exatamente 2 partes (TC + nome)
                        if (parts.Length != 2)
                        {
                            Console.WriteLine("Instrução inválida.");
                            return;
                        }
                        // Se há um jogo em curso
                        if (currentGame != null)
                        {
                            // Delega ao jogo para tirar carta
                            currentGame.DrawCard(parts[1]);
                        }
                        else
                        {
                            Console.WriteLine("Não existe um jogo em curso.");
                        }
                        break;
                    
                    // Comando desconhecido
                    default:
                        Console.WriteLine("Instrução inválida.");
                        break;
                }
            }
            catch (Exception)
            {
                // Se qualquer erro acontecer, mostra mensagem genérica
                Console.WriteLine("Instrução inválida.");
            }
        }

        // Regista um novo jogador no sistema
        // playerName: nome do jogador a registar
        private void RegisterPlayer(string playerName)
        {
            // Verifica se já existe um jogador com este nome
            // Any() retorna true se encontrar pelo menos um
            if (registeredPlayers.Any(p => p.Name == playerName))
            {
                // Se já existe, mostra mensagem e não regista
                Console.WriteLine("Jogador existente.");
                return;
            }

            // Cria um novo jogador e adiciona à lista
            registeredPlayers.Add(new Player(playerName));
            // Mostra mensagem de sucesso
            Console.WriteLine("Jogador registado com sucesso.");
        }

        // Lista todos os jogadores registados com suas estatísticas
        // Ordena por número de vitórias (decrescente) e depois por nome (crescente)
        private void ListPlayers()
        {
            // Se não há jogadores registados
            if (registeredPlayers.Count == 0)
            {
                Console.WriteLine("Sem jogadores registados.");
                return;
            }

            // Ordena jogadores: primeiro por vitórias (maior para menor)
            // Se empatar, ordena por nome (A-Z)
            var sortedPlayers = registeredPlayers
                .OrderByDescending(p => p.Wins)  // Ordena por vitórias (maior primeiro)
                .ThenBy(p => p.Name)             // Depois ordena por nome (A-Z)
                .ToList();                       // Converte para lista

            // Para cada jogador na lista ordenada
            foreach (var player in sortedPlayers)
            {
                // Mostra: Nome | Jogos | Vitórias | Empates | Derrotas
                Console.WriteLine($"{player.Name} {player.GamesPlayed} {player.Wins} {player.Draws} {player.Losses}");
            }
        }

        // Inicia um novo jogo com 4 jogadores
        // p1, p2, p3, p4: nomes dos 4 jogadores
        private void StartGame(string p1, string p2, string p3, string p4)
        {
            // Se já há um jogo em curso, não permite iniciar outro
            if (currentGame != null)
            {
                Console.WriteLine("Existe um jogo em curso.");
                return;
            }

            // Array com os nomes dos 4 jogadores
            string[] playerNames = { p1, p2, p3, p4 };
            // Lista para guardar os objetos Player encontrados
            List<Player> gamePlayers = new List<Player>();

            // Para cada nome fornecido
            foreach (string name in playerNames)
            {
                // Procura o jogador na lista de registados
                // FirstOrDefault retorna o jogador ou null se não encontrar
                Player player = registeredPlayers.FirstOrDefault(p => p.Name == name);
                
                // Se não encontrou o jogador
                if (player == null)
                {
                    // Mostra erro e não inicia o jogo
                    Console.WriteLine("Jogador inexistente.");
                    return;
                }
                
                // Adiciona o jogador encontrado à lista do jogo
                gamePlayers.Add(player);
            }

            // Cria um novo jogo com os 4 jogadores
            currentGame = new Game(gamePlayers);
            // Mostra mensagem de sucesso
            Console.WriteLine("Jogo iniciado com sucesso.");
        }
    }
}