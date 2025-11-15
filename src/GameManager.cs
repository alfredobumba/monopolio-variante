using System;
using System.Collections.Generic;
using System.Linq;

namespace MonopolioVariante
{
    // Classe que gerencia o fluxo do jogo, incluindo jogadores e partidas
    public class GameManager
    {
        // Lista de jogadores registados no sistema
        private List<Player> registeredPlayers;

        // Referência para o jogo atual em execução (null se não houver jogo)
        private Game currentGame;

        // Construtor do GameManager: inicializa a lista de jogadores e o jogo atual
        public GameManager()
        {
            registeredPlayers = new List<Player>();
            currentGame = null;
        }

        // Método que processa comandos recebidos do usuário
        public void ProcessCommand(string input)
        {
            // Divide o comando em partes separadas por espaço
            string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            // Se não houver comando, sai do método
            if (parts.Length == 0)
            {
                return;
            }

            // O primeiro elemento da entrada é o comando
            string command = parts[0];

            try
            {
                // Seleciona a ação a ser tomada com base no comando
                switch (command)
                {
                    case "RJ": // Registar jogador
                        if (parts.Length != 2)
                        {
                            Console.WriteLine("Instrução inválida.");
                            return;
                        }
                        RegisterPlayer(parts[1]);
                        break;

                    case "LJ": // Listar jogadores
                        if (parts.Length != 1)
                        {
                            Console.WriteLine("Instrução inválida.");
                            return;
                        }
                        ListPlayers();
                        break;

                    case "IJ": // Iniciar jogo com 4 jogadores
                        if (parts.Length != 5)
                        {
                            Console.WriteLine("Instrução inválida.");
                            return;
                        }
                        StartGame(parts[1], parts[2], parts[3], parts[4]);
                        break;

                    case "LD": // Lançar dados
                        if (parts.Length != 2)
                        {
                            Console.WriteLine("Instrução inválida.");
                            return;
                        }
                        if (currentGame != null)
                        {
                            currentGame.RollDice(parts[1]);
                        }
                        else
                        {
                            Console.WriteLine("Não existe um jogo em curso.");
                        }
                        break;

                    case "CE": // Comprar espaço
                        if (parts.Length != 2)
                        {
                            Console.WriteLine("Instrução inválida.");
                            return;
                        }
                        if (currentGame != null)
                        {
                            currentGame.BuySpace(parts[1]);
                        }
                        else
                        {
                            Console.WriteLine("Não existe um jogo em curso.");
                        }
                        break;

                    case "DJ": // Mostrar detalhes do jogo
                        if (parts.Length != 1)
                        {
                            Console.WriteLine("Instrução inválida.");
                            return;
                        }
                        if (currentGame != null)
                        {
                            currentGame.ShowGameDetails();
                        }
                        else
                        {
                            Console.WriteLine("Não existe jogo em curso.");
                        }
                        break;

                    case "TT": // Terminar turno
                        if (parts.Length != 2)
                        {
                            Console.WriteLine("Instrução inválida.");
                            return;
                        }
                        if (currentGame != null)
                        {
                            currentGame.EndTurn(parts[1]);
                        }
                        else
                        {
                            Console.WriteLine("Não existe um jogo em curso.");
                        }
                        break;

                    case "PA": // Pagar aluguel
                        if (parts.Length != 2)
                        {
                            Console.WriteLine("Instrução inválida.");
                            return;
                        }
                        if (currentGame != null)
                        {
                            currentGame.PayRent(parts[1]);
                        }
                        else
                        {
                            Console.WriteLine("Não existe um jogo em curso.");
                        }
                        break;

                    case "CC": // Comprar casa
                        if (parts.Length != 3)
                        {
                            Console.WriteLine("Instrução inválida.");
                            return;
                        }
                        if (currentGame != null)
                        {
                            currentGame.BuyHouse(parts[1], parts[2]);
                        }
                        else
                        {
                            Console.WriteLine("Não existe um jogo em curso.");
                        }
                        break;

                    case "TC": // Tirar carta (Chance ou Comunidade)
                        if (parts.Length != 2)
                        {
                            Console.WriteLine("Instrução inválida.");
                            return;
                        }
                        if (currentGame != null)
                        {
                            currentGame.DrawCard(parts[1]);
                        }
                        else
                        {
                            Console.WriteLine("Não existe um jogo em curso.");
                        }
                        break;

                    default:
                        // Comando não reconhecido
                        Console.WriteLine("Instrução inválida.");
                        break;
                }
            }
            catch (Exception)
            {
                // Qualquer exceção é tratada como comando inválido
                Console.WriteLine("Instrução inválida.");
            }
        }

        // Regista um novo jogador, evitando duplicatas
        private void RegisterPlayer(string playerName)
        {
            if (registeredPlayers.Any(p => p.Name == playerName))
            {
                Console.WriteLine("Jogador existente.");
                return;
            }

            registeredPlayers.Add(new Player(playerName));
            Console.WriteLine("Jogador registado com sucesso.");
        }

        // Lista todos os jogadores registados, ordenados por vitórias e nome
        private void ListPlayers()
        {
            if (registeredPlayers.Count == 0)
            {
                Console.WriteLine("Sem jogadores registados.");
                return;
            }

            // Ordena primeiro por vitórias descendentemente, depois pelo nome
            var sortedPlayers = registeredPlayers
                .OrderByDescending(p => p.Wins)
                .ThenBy(p => p.Name)
                .ToList();

            // Mostra as estatísticas de cada jogador
            foreach (var player in sortedPlayers)
            {
                Console.WriteLine($"{player.Name} {player.GamesPlayed} {player.Wins} {player.Draws} {player.Losses}");
            }
        }

        // Inicia um novo jogo com quatro jogadores
        private void StartGame(string p1, string p2, string p3, string p4)
        {
            if (currentGame != null)
            {
                Console.WriteLine("Existe um jogo em curso.");
                return;
            }

            string[] playerNames = { p1, p2, p3, p4 };
            List<Player> gamePlayers = new List<Player>();

            // Verifica se todos os jogadores existem e adiciona à lista do jogo
            foreach (string name in playerNames)
            {
                Player player = registeredPlayers.FirstOrDefault(p => p.Name == name);
                if (player == null)
                {
                    Console.WriteLine("Jogador inexistente.");
                    return;
                }
                gamePlayers.Add(player);
            }

            // Cria o novo jogo com os jogadores selecionados
            currentGame = new Game(gamePlayers);
            Console.WriteLine("Jogo iniciado com sucesso.");
        }
    }
}
