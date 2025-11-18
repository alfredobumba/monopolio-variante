// Importa classes para usar exceções e output
using System;
// Importa classes para usar listas
using System.Collections.Generic;
// Importa LINQ para usar métodos como FirstOrDefault, All
using System.Linq;
// Importa os enums (SpaceType)
using MonopolioVariante.Enums;
// Importa as classes de modelo (Player, Space)
using MonopolioVariante.Models;

// Define o namespace para os gerenciadores
namespace MonopolioVariante.Managers
{
    // Classe principal que gerencia um jogo de Monopoly
    // Controla o tabuleiro, turnos, movimentos, compras, cartas, etc
    public class Game
    {
        // Tabuleiro 7x7 com todos os espaços do jogo
        // board[y, x] acessa a linha y, coluna x
        private Space[,] board;
        
        // Lista dos 4 jogadores no jogo atual
        private List<Player> players;
        
        // Índice do jogador atual (0 a 3)
        // Indica de quem é a vez
        private int currentPlayerIndex;
        
        // Gerador de números aleatórios (para dados e cartas)
        private Random random;
        
        // Dinheiro acumulado no FreePark
        // Quando alguém paga impostos, o dinheiro vai para cá
        private int freeParkMoney;

        // Construtor: cria um novo jogo com os jogadores fornecidos
        // gamePlayers: lista com os 4 jogadores que vão jogar
        public Game(List<Player> gamePlayers)
        {
            players = gamePlayers;  // Guarda a lista de jogadores
            
            // Reseta cada jogador para o estado inicial do jogo
            foreach (var player in players)
            {
                player.ResetForNewGame();  // 600 de dinheiro, posição (3,3), etc
            }
            
            currentPlayerIndex = 0;    // Começa no jogador 0 (primeiro da lista)
            random = new Random();     // Cria gerador de números aleatórios
            freeParkMoney = 0;         // FreePark começa com 0 de dinheiro
            InitializeBoard();         // Cria o tabuleiro 7x7 com todos os espaços
        }

        // Inicializa o tabuleiro 7x7 com todos os espaços do jogo
        // Usa as posições e preços corretos das imagens fornecidas
        private void InitializeBoard()
        {
            // Cria array 7x7 para o tabuleiro
            board = new Space[7, 7];
            
            // --- LINHA 0 (topo do tabuleiro) ---
            board[0, 0] = new Space("Prison", SpaceType.Prison);                      // Prisão (apenas visitando)
            board[0, 1] = new Space("Green3", SpaceType.Property, 160, "Green");      // Propriedade verde (160)
            board[0, 2] = new Space("Violet1", SpaceType.Property, 150, "Violet");    // Propriedade violeta (150)
            board[0, 3] = new Space("Train2", SpaceType.Train, 150);                  // Estação de trem (150)
            board[0, 4] = new Space("Red3", SpaceType.Property, 160, "Red");          // Propriedade vermelha (160)
            board[0, 5] = new Space("White1", SpaceType.Property, 160, "White");      // Propriedade branca (160)
            board[0, 6] = new Space("BackToStart", SpaceType.BackToStart);            // Volta para Start

            // --- LINHA 1 ---
            board[1, 0] = new Space("Blue3", SpaceType.Property, 170, "Blue");        // Propriedade azul (170)
            board[1, 1] = new Space("Community", SpaceType.Community);                // Casa Comunidade
            board[1, 2] = new Space("Red2", SpaceType.Property, 130, "Red");          // Propriedade vermelha (130)
            board[1, 3] = new Space("Violet2", SpaceType.Property, 130, "Violet");    // Propriedade violeta (130)
            board[1, 4] = new Space("WaterWorks", SpaceType.Utility, 120);            // Utilidade água (120)
            board[1, 5] = new Space("Chance", SpaceType.Chance);                      // Casa Sorte
            board[1, 6] = new Space("White2", SpaceType.Property, 180, "White");      // Propriedade branca (180)

            // --- LINHA 2 ---
            board[2, 0] = new Space("Blue2", SpaceType.Property, 140, "Blue");        // Propriedade azul (140)
            board[2, 1] = new Space("Red1", SpaceType.Property, 130, "Red");          // Propriedade vermelha (130)
            board[2, 2] = new Space("Chance", SpaceType.Chance);                      // Casa Sorte
            board[2, 3] = new Space("Brown2", SpaceType.Property, 120, "Brown");      // Propriedade castanha (120)
            board[2, 4] = new Space("Community", SpaceType.Community);                // Casa Comunidade
            board[2, 5] = new Space("Black1", SpaceType.Property, 110, "Black");      // Propriedade preta (110)
            board[2, 6] = new Space("LuxTax", SpaceType.LuxTax, 80);                  // Imposto de luxo (80)

            // --- LINHA 3 (centro do tabuleiro) ---
            board[3, 0] = new Space("Train1", SpaceType.Train, 150);                  // Estação de trem (150)
            board[3, 1] = new Space("Green2", SpaceType.Property, 140, "Green");      // Propriedade verde (140)
            board[3, 2] = new Space("Teal1", SpaceType.Property, 90, "Teal");         // Propriedade teal/ciano (90)
            board[3, 3] = new Space("Start", SpaceType.Start);                        // Casa inicial START
            board[3, 4] = new Space("Teal2", SpaceType.Property, 130, "Teal");        // Propriedade teal/ciano (130)
            board[3, 5] = new Space("Black2", SpaceType.Property, 120, "Black");      // Propriedade preta (120)
            board[3, 6] = new Space("Train3", SpaceType.Train, 150);                  // Estação de trem (150)

            // --- LINHA 4 ---
            board[4, 0] = new Space("Blue1", SpaceType.Property, 140, "Blue");        // Propriedade azul (140)
            board[4, 1] = new Space("Green1", SpaceType.Property, 120, "Green");      // Propriedade verde (120)
            board[4, 2] = new Space("Community", SpaceType.Community);                // Casa Comunidade
            board[4, 3] = new Space("Brown1", SpaceType.Property, 100, "Brown");      // Propriedade castanha (100)
            board[4, 4] = new Space("Chance", SpaceType.Chance);                      // Casa Sorte
            board[4, 5] = new Space("Black3", SpaceType.Property, 130, "Black");      // Propriedade preta (130)
            board[4, 6] = new Space("White3", SpaceType.Property, 190, "White");      // Propriedade branca (190)

            // --- LINHA 5 ---
            board[5, 0] = new Space("Pink1", SpaceType.Property, 160, "Pink");        // Propriedade rosa (160)
            board[5, 1] = new Space("Chance", SpaceType.Chance);                      // Casa Sorte
            board[5, 2] = new Space("Orange1", SpaceType.Property, 120, "Orange");    // Propriedade laranja (120)
            board[5, 3] = new Space("Orange2", SpaceType.Property, 120, "Orange");    // Propriedade laranja (120)
            board[5, 4] = new Space("Orange3", SpaceType.Property, 140, "Orange");    // Propriedade laranja (140)
            board[5, 5] = new Space("Community", SpaceType.Community);                // Casa Comunidade
            board[5, 6] = new Space("Yellow3", SpaceType.Property, 170, "Yellow");    // Propriedade amarela (170)

            // --- LINHA 6 (fundo do tabuleiro) ---
            board[6, 0] = new Space("FreePark", SpaceType.FreePark);                  // Estacionamento grátis
            board[6, 1] = new Space("Pink2", SpaceType.Property, 180, "Pink");        // Propriedade rosa (180)
            board[6, 2] = new Space("ElectricCompany", SpaceType.Utility, 120);       // Utilidade elétrica (120)
            board[6, 3] = new Space("Train4", SpaceType.Train, 150);                  // Estação de trem (150)
            board[6, 4] = new Space("Yellow1", SpaceType.Property, 140, "Yellow");    // Propriedade amarela (140)
            board[6, 5] = new Space("Yellow2", SpaceType.Property, 140, "Yellow");    // Propriedade amarela (140)
            board[6, 6] = new Space("Police", SpaceType.Police);                      // Polícia (vai preso)
        }
        // PARTE 2 - Métodos RollDice e ProcessSpaceLanding

        // Lança os dados para um jogador
        // playerName: nome do jogador que vai lançar os dados
        public void RollDice(string playerName)
        {
            // Procura o jogador pelo nome
            Player player = GetPlayerByName(playerName);
            
            // Se o jogador não existe no jogo
            if (player == null)
            {
                Console.WriteLine("Jogador não participa no jogo em curso.");
                return;  // Sai do método
            }

            // Verifica se é a vez deste jogador
            if (players[currentPlayerIndex] != player)
            {
                Console.WriteLine("Não é a vez do jogador");
                return;  // Sai do método
            }

            // Gera dois valores de dados (cada um de -3 a +3, exceto 0)
            int dice1 = GetRandomDiceValue();  // Primeiro dado
            int dice2 = GetRandomDiceValue();  // Segundo dado

            // Marca que o jogador já lançou os dados neste turno
            player.HasRolledDice = true;

            // --- VERIFICAR SE O JOGADOR ESTÁ PRESO ---
            if (player.InPrison)
            {
                // Incrementa o número de turnos que está preso
                player.TurnsInPrison++;
                
                // Se tirou double (dados iguais), sai da prisão
                if (dice1 == dice2)
                {
                    player.InPrison = false;        // Não está mais preso
                    player.TurnsInPrison = 0;       // Reseta contador de turnos
                    player.PositionX = 0;           // Move para Prison (0, 0)
                    player.PositionY = 0;
                }
                // Se completou 3 turnos na prisão, sai automaticamente
                else if (player.TurnsInPrison >= 3)
                {
                    player.InPrison = false;        // Não está mais preso
                    player.TurnsInPrison = 0;       // Reseta contador
                    player.PositionX = 0;           // Move para Prison (0, 0)
                    player.PositionY = 0;
                }
                // Se ainda está preso (não tirou double e não completou 3 turnos)
                else
                {
                    // Mostra mensagem e termina o turno
                    Console.WriteLine($"Saiu {dice1}/{dice2} – espaço Prison. Jogador preso.");
                    return;  // Não move o jogador
                }
            }

            // --- MOVER O JOGADOR ---
            // Calcula nova posição somando os valores dos dados
            int newX = player.PositionX + dice1;  // Nova posição X
            int newY = player.PositionY + dice2;  // Nova posição Y

            // Ajusta posições para ficarem dentro do tabuleiro 7x7 (wrap around)
            // Se X ou Y for negativo ou >= 7, ajusta para ficar entre 0 e 6
            while (newX < 0) newX += 7;      // Se negativo, soma 7
            while (newX >= 7) newX -= 7;     // Se >= 7, subtrai 7
            while (newY < 0) newY += 7;      // Se negativo, soma 7
            while (newY >= 7) newY -= 7;     // Se >= 7, subtrai 7

            // Atualiza a posição do jogador
            player.PositionX = newX;
            player.PositionY = newY;

            // Pega o espaço onde o jogador caiu
            Space currentSpace = board[newY, newX];

            // --- VERIFICAR DOUBLES (dados iguais) ---
            if (dice1 == dice2)
            {
                // Incrementa contador de doubles consecutivos
                player.ConsecutiveDoubles++;
                
                // Se tirou 2 ou mais doubles seguidos, vai preso
                if (player.ConsecutiveDoubles >= 2)
                {
                    player.InPrison = true;          // Marca como preso
                    player.PositionX = 0;            // Move para Prison (0, 0)
                    player.PositionY = 0;
                    player.ConsecutiveDoubles = 0;   // Reseta contador
                    // Mostra mensagem de prisão
                    Console.WriteLine($"Saiu {dice1}/{dice2} – espaço Police. Jogador preso.");
                    return;  // Termina o turno
                }
                
                // Se tirou double mas não foi preso, deve jogar de novo
                player.MustRollAgain = true;
            }
            else
            {
                // Se não tirou double, reseta o contador
                player.ConsecutiveDoubles = 0;
            }

            // Processa o efeito do espaço onde caiu
            ProcessSpaceLanding(player, currentSpace, dice1, dice2);
        }

        // Processa o que acontece quando um jogador cai num espaço
        // player: jogador que caiu no espaço
        // space: espaço onde caiu
        // dice1, dice2: valores dos dados (para mostrar na mensagem)
        private void ProcessSpaceLanding(Player player, Space space, int dice1, int dice2)
        {
            // Switch para processar cada tipo de espaço diferente
            switch (space.Type)
            {
                // --- BACKTOSTART: Volta para o Start ---
                case SpaceType.BackToStart:
                    // Move o jogador para a posição do Start (3, 3)
                    player.PositionX = 3;
                    player.PositionY = 3;
                    // Mostra mensagem
                    Console.WriteLine($"Saiu {dice1}/{dice2} - Espaço BackToStart. Peça colocada no espaço Start.");
                    break;

                // --- POLICE: Vai para a prisão ---
                case SpaceType.Police:
                    player.InPrison = true;      // Marca como preso
                    player.PositionX = 0;        // Move para Prison (0, 0)
                    player.PositionY = 0;
                    Console.WriteLine($"Saiu {dice1}/{dice2} – espaço Police. Jogador preso.");
                    break;

                // --- PRISON: Apenas de passagem ---
                case SpaceType.Prison:
                    // Se não está preso, está apenas de passagem (nada acontece)
                    Console.WriteLine($"Saiu {dice1}/{dice2} – espaço Prison. Jogador só de passagem.");
                    break;

                // --- FREEPARK: Recebe o dinheiro acumulado ---
                case SpaceType.FreePark:
                    // Adiciona o dinheiro acumulado ao jogador
                    player.Money += freeParkMoney;
                    // Mostra quanto recebeu
                    Console.WriteLine($"Saiu {dice1}/{dice2} – espaço FreePark. Jogador recebe {freeParkMoney}.");
                    // Reseta o dinheiro do FreePark
                    freeParkMoney = 0;
                    break;

                // --- CHANCE ou COMMUNITY: Tirar carta ---
                case SpaceType.Chance:
                case SpaceType.Community:
                    // Marca que precisa tirar uma carta
                    player.NeedsToDrawCard = true;
                    player.HasDrawnCard = false;
                    // Mostra mensagem
                    Console.WriteLine($"Saiu {dice1}/{dice2} – espaço {space.Name}. Espaço especial. Tirar carta.");
                    break;

                // --- START: Recebe 200 ---
                case SpaceType.Start:
                    // Adiciona 200 ao dinheiro do jogador
                    player.Money += 200;
                    Console.WriteLine($"Saiu {dice1}/{dice2} – espaço {space.Name}. Espaço sem dono.");
                    break;

                // --- PROPRIEDADES, TRAINS, UTILITIES, LUXTAX ---
                default:
                    // Se o espaço não tem dono
                    if (space.Owner == null)
                    {
                        Console.WriteLine($"Saiu {dice1}/{dice2} – espaço {space.Name}. Espaço sem dono.");
                    }
                    // Se o jogador já é dono deste espaço
                    else if (space.Owner == player)
                    {
                        Console.WriteLine($"Saiu {dice1}/{dice2} – espaço {space.Name}. Espaço já comprada.");
                    }
                    // Se outro jogador é dono
                    else
                    {
                        // Marca que precisa pagar aluguer
                        player.NeedsToPayRent = true;
                        Console.WriteLine($"Saiu {dice1}/{dice2} – espaço {space.Name}. Espaço já comprada por outro jogador. Necessário pagar renda.");
                    }
                    break;
            }
        }
        // PARTE 3 - Métodos BuySpace, PayRent e BuyHouse

        // Compra o espaço onde o jogador está atualmente
        // playerName: nome do jogador que quer comprar
        public void BuySpace(string playerName)
        {
            // Procura o jogador pelo nome
            Player player = GetPlayerByName(playerName);
            
            // Se o jogador não existe no jogo
            if (player == null)
            {
                Console.WriteLine("Jogador não participa no jogo em curso.");
                return;
            }

            // Verifica se é a vez deste jogador
            if (players[currentPlayerIndex] != player)
            {
                Console.WriteLine("Não é a vez do jogador");
                return;
            }

            // Pega o espaço onde o jogador está
            Space currentSpace = board[player.PositionY, player.PositionX];

            // Verifica se este espaço pode ser comprado
            // (Property, Train, Utility, LuxTax)
            if (!currentSpace.CanBePurchased())
            {
                Console.WriteLine("Este espaço não está para venda.");
                return;
            }

            // Verifica se o espaço já tem dono
            if (currentSpace.Owner != null)
            {
                Console.WriteLine("O espaço já se encontra comprado.");
                return;
            }

            // Verifica se o jogador tem dinheiro suficiente
            if (player.Money < currentSpace.Price)
            {
                Console.WriteLine("O jogador não tem dinheiro suficiente para adquirir o espaço.");
                return;
            }

            // --- EFETUAR A COMPRA ---
            // Subtrai o preço do dinheiro do jogador
            player.Money -= currentSpace.Price;
            // Define o jogador como dono do espaço
            currentSpace.Owner = player;
            
            // Se for LuxTax, o dinheiro vai para o FreePark
            if (currentSpace.Type == SpaceType.LuxTax)
            {
                freeParkMoney += currentSpace.Price;
            }
            
            // Mostra mensagem de sucesso
            Console.WriteLine("Espaço comprado.");
        }

        // Paga o aluguer do espaço onde o jogador está
        // playerName: nome do jogador que vai pagar
        public void PayRent(string playerName)
        {
            // Procura o jogador pelo nome
            Player player = GetPlayerByName(playerName);
            
            // Se o jogador não existe no jogo
            if (player == null)
            {
                Console.WriteLine("Jogador não participa no jogo em curso.");
                return;
            }

            // Verifica se é a vez deste jogador
            if (players[currentPlayerIndex] != player)
            {
                Console.WriteLine("Não é a vez do jogador.");
                return;
            }

            // Verifica se realmente precisa pagar aluguer
            if (!player.NeedsToPayRent)
            {
                Console.WriteLine("Não é necessário pagar aluguer");
                return;
            }

            // Pega o espaço onde o jogador está
            Space currentSpace = board[player.PositionY, player.PositionX];
            
            // --- CALCULA O VALOR DO ALUGUER ---
            // Fórmula: 25% do preço + 75% do preço por cada casa
            // rent = price * 0.25 + price * 0.75 * houses
            int rent = (int)(currentSpace.Price * 0.25 + currentSpace.Price * 0.75 * currentSpace.Houses);

            // Verifica se o jogador tem dinheiro suficiente
            if (player.Money < rent)
            {
                Console.WriteLine("O jogador não tem dinheiro suficiente.");
                return;
            }

            // --- EFETUAR O PAGAMENTO ---
            // Subtrai o aluguer do dinheiro do jogador
            player.Money -= rent;
            // Adiciona o aluguer ao dono do espaço
            currentSpace.Owner.Money += rent;
            // Marca que já não precisa pagar aluguer
            player.NeedsToPayRent = false;
            
            // Mostra mensagem de sucesso
            Console.WriteLine("Aluguer pago.");
        }

        // Compra uma casa num espaço específico
        // playerName: nome do jogador que quer comprar a casa
        // spaceName: nome do espaço onde quer comprar (ex: "Green1")
        public void BuyHouse(string playerName, string spaceName)
        {
            // Procura o jogador pelo nome
            Player player = GetPlayerByName(playerName);
            
            // Se o jogador não existe no jogo
            if (player == null)
            {
                Console.WriteLine("Jogador não participa no jogo em curso.");
                return;
            }

            // Verifica se é a vez deste jogador
            if (players[currentPlayerIndex] != player)
            {
                Console.WriteLine("Não é a vez do jogador.");
                return;
            }

            // Procura o espaço pelo nome
            Space targetSpace = FindSpaceByName(spaceName);
            
            // Verifica se o espaço existe E se o jogador é o dono
            if (targetSpace == null || targetSpace.Owner != player)
            {
                Console.WriteLine("Não é possível comprar casa no espaço indicado.");
                return;
            }

            // Verifica se o espaço tem cor (só propriedades coloridas podem ter casas)
            if (string.IsNullOrEmpty(targetSpace.Color))
            {
                Console.WriteLine("Não é possível comprar casa no espaço indicado.");
                return;
            }

            // --- VERIFICA SE POSSUI TODOS OS ESPAÇOS DA COR ---
            // Pega todos os espaços com a mesma cor
            List<Space> colorSpaces = GetAllSpacesOfColor(targetSpace.Color);
            
            // Verifica se o jogador é dono de TODOS os espaços desta cor
            // All() retorna true só se TODOS os espaços tiverem este jogador como dono
            if (!colorSpaces.All(s => s.Owner == player))
            {
                Console.WriteLine("O jogador não possui todos os espaços da cor");
                return;
            }

            // Verifica se já tem 4 casas (máximo)
            if (targetSpace.Houses >= 4)
            {
                Console.WriteLine("Não é possível comprar casa no espaço indicado.");
                return;
            }

            // --- CALCULA O PREÇO DA CASA ---
            // Preço da casa = 60% do preço do espaço
            int housePrice = (int)(targetSpace.Price * 0.6);
            
            // Verifica se o jogador tem dinheiro suficiente
            if (player.Money < housePrice)
            {
                Console.WriteLine("O jogador não possui dinheiro suficiente.");
                return;
            }

            // --- EFETUAR A COMPRA ---
            // Subtrai o preço da casa do dinheiro do jogador
            player.Money -= housePrice;
            // Incrementa o número de casas no espaço
            targetSpace.Houses++;
            
            // Mostra mensagem de sucesso
            Console.WriteLine("Casa adquirida.");
        }
        // PARTE 4 - Métodos DrawCard e processamento de cartas

        // Tira uma carta (Chance ou Community)
        // playerName: nome do jogador que vai tirar a carta
        public void DrawCard(string playerName)
        {
            // Procura o jogador pelo nome
            Player player = GetPlayerByName(playerName);
            
            // Se o jogador não existe no jogo
            if (player == null)
            {
                Console.WriteLine("Jogador não participa no jogo em curso.");
                return;
            }

            // Verifica se é a vez deste jogador
            if (players[currentPlayerIndex] != player)
            {
                Console.WriteLine("Não é a vez do jogador.");
                return;
            }

            // Verifica se já tirou a carta neste turno
            if (player.HasDrawnCard)
            {
                Console.WriteLine("A carta já foi tirada.");
                return;
            }

            // Pega o espaço onde o jogador está
            Space currentSpace = board[player.PositionY, player.PositionX];
            
            // Verifica se o espaço atual é Chance ou Community
            if (currentSpace.Type != SpaceType.Chance && currentSpace.Type != SpaceType.Community)
            {
                Console.WriteLine("Não é possível tirar carta neste espaço.");
                return;
            }

            // Marca que já tirou a carta
            player.HasDrawnCard = true;
            player.NeedsToDrawCard = false;

            // Gera um número aleatório de 1 a 100 (para decidir qual carta)
            int cardRoll = random.Next(1, 101);

            // Processa a carta baseado no tipo do espaço
            if (currentSpace.Type == SpaceType.Chance)
            {
                // Se for Chance, processa carta de Chance
                ProcessChanceCard(player, cardRoll);
            }
            else
            {
                // Se for Community, processa carta de Community
                ProcessCommunityCard(player, cardRoll);
            }
        }

        // Processa uma carta de Chance (Sorte)
        // player: jogador que tirou a carta
        // roll: número de 1 a 100 que determina qual carta
        private void ProcessChanceCard(Player player, int roll)
        {
            // --- 20% (1-20): Recebe 150 ---
            if (roll <= 20)
            {
                player.Money += 150;  // Adiciona 150 ao dinheiro
                Console.WriteLine("O jogador recebe 150.");
            }
            // --- 10% (21-30): Recebe 200 ---
            else if (roll <= 30)
            {
                player.Money += 200;  // Adiciona 200 ao dinheiro
                Console.WriteLine("O jogador recebe 200.");
            }
            // --- 10% (31-40): Paga 70 ---
            else if (roll <= 40)
            {
                player.Money -= 70;        // Subtrai 70 do dinheiro
                freeParkMoney += 70;       // Adiciona 70 ao FreePark
                Console.WriteLine("O jogador tem de pagar 70.");
                
                // Se ficou com dinheiro negativo, define como -1
                if (player.Money < 0)
                {
                    player.Money = -1;
                }
            }
            // --- 20% (41-60): Move para Start e recebe 200 ---
            else if (roll <= 60)
            {
                player.PositionX = 3;      // Move para Start (3, 3)
                player.PositionY = 3;
                player.Money += 200;       // Recebe 200 por passar no Start
                Console.WriteLine("O jogador move-se para a casa Start.");
            }
            // --- 20% (61-80): Move para Prison (vai preso) ---
            else if (roll <= 80)
            {
                player.PositionX = 0;      // Move para Prison (0, 0)
                player.PositionY = 0;
                player.InPrison = true;    // Marca como preso
                Console.WriteLine("O jogador move-se para a casa Police.");
            }
            // --- 20% (81-100): Move para FreePark e recebe o dinheiro ---
            else
            {
                player.PositionX = 0;      // Move para FreePark (0, 6)
                player.PositionY = 6;
                player.Money += freeParkMoney;  // Recebe dinheiro do FreePark
                Console.WriteLine("O jogador move-se para a casa FreePark.");
                freeParkMoney = 0;         // Reseta o FreePark
            }
        }

        // Processa uma carta de Community (Comunidade)
        // player: jogador que tirou a carta
        // roll: número de 1 a 100 que determina qual carta
        private void ProcessCommunityCard(Player player, int roll)
        {
            // --- 10% (1-10): Paga 20 por cada casa que possui ---
            if (roll <= 10)
            {
                // Conta o total de casas do jogador em todo o tabuleiro
                int totalHouses = 0;
                // Percorre todas as linhas do tabuleiro
                for (int y = 0; y < 7; y++)
                {
                    // Percorre todas as colunas do tabuleiro
                    for (int x = 0; x < 7; x++)
                    {
                        // Se este espaço pertence ao jogador
                        if (board[y, x].Owner == player)
                        {
                            // Soma o número de casas deste espaço
                            totalHouses += board[y, x].Houses;
                        }
                    }
                }
                // Calcula o pagamento (20 por casa)
                int payment = totalHouses * 20;
                player.Money -= payment;       // Subtrai do dinheiro do jogador
                freeParkMoney += payment;      // Adiciona ao FreePark
                Console.WriteLine($"O jogador paga {payment} pelas casas nos seus espaços.");
                
                // Se ficou com dinheiro negativo, define como -1
                if (player.Money < 0)
                {
                    player.Money = -1;
                }
            }
            // --- 10% (11-20): Recebe 10 de cada outro jogador ---
            else if (roll <= 20)
            {
                int total = 0;  // Total recebido
                // Para cada jogador no jogo
                foreach (var p in players)
                {
                    // Se não for o próprio jogador
                    if (p != player)
                    {
                        p.Money -= 10;  // Outros jogadores perdem 10
                        total += 10;    // Soma ao total
                    }
                }
                player.Money += total;  // Jogador recebe o total
                Console.WriteLine($"O jogador recebe {total} de outros jogadores.");
            }
            // --- 20% (21-40): Recebe 100 ---
            else if (roll <= 40)
            {
                player.Money += 100;  // Adiciona 100 ao dinheiro
                Console.WriteLine("O jogador recebe 100");
            }
            // --- 20% (41-60): Recebe 170 ---
            else if (roll <= 60)
            {
                player.Money += 170;  // Adiciona 170 ao dinheiro
                Console.WriteLine("O jogador recebe 170.");
            }
            // --- 10% (61-70): Paga 40 ---
            else if (roll <= 70)
            {
                player.Money -= 40;        // Subtrai 40 do dinheiro
                freeParkMoney += 40;       // Adiciona 40 ao FreePark
                Console.WriteLine("O jogador tem de pagar 40.");
                
                // Se ficou com dinheiro negativo, define como -1
                if (player.Money < 0)
                {
                    player.Money = -1;
                }
            }
            // --- 10% (71-80): Move para Pink1 ---
            else if (roll <= 80)
            {
                player.PositionX = 0;      // Move para Pink1 (0, 5)
                player.PositionY = 5;
                Console.WriteLine("O jogador move-se para Pink1.");
            }
            // --- 10% (81-90): Move para Teal2 ---
            else if (roll <= 90)
            {
                player.PositionX = 4;      // Move para Teal2 (4, 3)
                player.PositionY = 3;
                Console.WriteLine("O jogador move-se para Teal2.");
            }
            // --- 10% (91-100): Move para White2 ---
            else
            {
                player.PositionX = 6;      // Move para White2 (6, 1) - COORDENADA CORRIGIDA
                player.PositionY = 1;
                Console.WriteLine("O jogador move-se para White2.");
            }
        }
        // PARTE 5 - Métodos EndTurn, ShowGameDetails e métodos auxiliares

        // Termina o turno do jogador atual
        // playerName: nome do jogador que quer terminar o turno
        public void EndTurn(string playerName)
        {
            // Procura o jogador pelo nome
            Player player = GetPlayerByName(playerName);
            
            // Se o jogador não existe no jogo
            if (player == null)
            {
                Console.WriteLine("Jogador não participa no jogo em curso.");
                return;
            }

            // Verifica se é a vez deste jogador
            if (players[currentPlayerIndex] != player)
            {
                Console.WriteLine("Não é o turno do jogador indicado.");
                return;
            }

            // Verifica se o jogador ainda não lançou os dados
            if (!player.HasRolledDice)
            {
                Console.WriteLine("O jogador ainda tem ações a fazer.");
                return;
            }

            // Verifica se o jogador ainda precisa pagar aluguer ou tirar carta
            if (player.NeedsToPayRent || player.NeedsToDrawCard)
            {
                Console.WriteLine("O jogador ainda tem ações a fazer.");
                return;
            }

            // Verifica se o jogador tirou double e precisa jogar de novo
            if (player.MustRollAgain)
            {
                Console.WriteLine("O jogador ainda tem ações a fazer.");
                return;
            }

            // --- RESETA O ESTADO DO JOGADOR PARA O PRÓXIMO TURNO ---
            player.HasRolledDice = false;   // Ainda não jogou os dados no novo turno
            player.MustRollAgain = false;   // Não precisa jogar de novo
            player.HasDrawnCard = false;    // Ainda não tirou carta
            
            // --- PASSA PARA O PRÓXIMO JOGADOR ---
            // Incrementa o índice e usa módulo 4 para voltar ao 0 quando chegar ao 3
            // Se for jogador 3, volta para 0 (3+1 = 4, 4%4 = 0)
            currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
            
            // Mostra mensagem com o nome do próximo jogador
            Console.WriteLine($"Turno terminado. Novo turno do jogador {players[currentPlayerIndex].Name}.");
        }

        // Mostra os detalhes completos do jogo (tabuleiro e info do jogador atual)
        public void ShowGameDetails()
        {
            // Verificar se há jogo em curso
            if (players == null || players.Count == 0)
            {
                Console.WriteLine("Não existe jogo em curso.");
                return;
            }

            Console.WriteLine(); // Linha em branco antes do tabuleiro

            // --- CALCULAR LARGURA DAS COLUNAS ---
            int[] columnWidths = new int[7];
            for (int x = 0; x < 7; x++)
            {
                int maxWidth = 10; // Largura mínima
                for (int y = 0; y < 7; y++)
                {
                    int width = GetCellContent(x, y).Length;
                    if (width > maxWidth) maxWidth = width;
                }
                columnWidths[x] = maxWidth;
            }

            // --- DESENHAR TABULEIRO COM BORDAS ---
            DrawHorizontalLine(columnWidths);

            for (int y = 0; y < 7; y++)
            {
                // Linha de conteúdo
                Console.Write("|");
                for (int x = 0; x < 7; x++)
                {
                    string content = GetCellContent(x, y);
                    Console.Write(" " + content.PadRight(columnWidths[x]) + " |");
                }
                Console.WriteLine();

                // Linha divisória
                DrawHorizontalLine(columnWidths);
            }

            Console.WriteLine(); // Linha em branco após o tabuleiro
            
            // --- MOSTRA INFO DO JOGADOR ATUAL ---
            Player currentPlayer = players[currentPlayerIndex];
            Console.WriteLine($"{currentPlayer.Name} - {currentPlayer.Money}");
        }

        // Método auxiliar para obter o conteúdo de uma célula
        private string GetCellContent(int x, int y)
        {
            Space space = board[y, x];
            string content = space.Name;

            // Adicionar dono e casas
            if (space.Owner != null)
            {
                content += $" ({space.Owner.Name}";
                if (space.Houses > 0)
                {
                    content += $" - {space.Houses}";
                }
                content += ")";
            }

            // Adicionar jogadores na posição
            var playersHere = players.Where(p => 
                (p.PositionX == x && p.PositionY == y && !p.InPrison) ||
                (p.InPrison && x == 0 && y == 0)
            ).Select(p => p.Name).ToList();

            if (playersHere.Count > 0)
            {
                content += " " + string.Join(" ", playersHere);
            }

            return content;
        }

        // Método auxiliar para desenhar linha horizontal
        private void DrawHorizontalLine(int[] columnWidths)
        {
            Console.Write("+");
            for (int x = 0; x < 7; x++)
            {
                Console.Write(new string('-', columnWidths[x] + 2) + "+");
            }
            Console.WriteLine();
        }

        // Gera um valor aleatório de dado
        // Retorna: -3, -2, -1, +1, +2 ou +3 (nunca 0)
        private int GetRandomDiceValue()
        {
            // Gera número de 1 a 6
            int value = random.Next(1, 7);
            
            // Se for 1, 2 ou 3: retorna negativo (-1, -2, -3)
            // Se for 4, 5 ou 6: retorna positivo (1, 2, 3)
            return value <= 3 ? -value : value - 3;
        }

        // Procura um jogador pelo nome
        // name: nome do jogador a procurar
        // Retorna: objeto Player se encontrar, null se não encontrar
        private Player GetPlayerByName(string name)
        {
            // FirstOrDefault retorna o primeiro jogador com este nome
            // ou null se não encontrar nenhum
            return players.FirstOrDefault(p => p.Name == name);
        }

        // Procura um espaço pelo nome em todo o tabuleiro
        // name: nome do espaço a procurar (ex: "Green1", "Train2")
        // Retorna: objeto Space se encontrar, null se não encontrar
        private Space FindSpaceByName(string name)
        {
            // Percorre todas as linhas
            for (int y = 0; y < 7; y++)
            {
                // Percorre todas as colunas
                for (int x = 0; x < 7; x++)
                {
                    // Se o nome deste espaço é igual ao nome procurado
                    if (board[y, x].Name == name)
                    {
                        return board[y, x];  // Retorna este espaço
                    }
                }
            }
            // Se não encontrou em nenhum lugar, retorna null
            return null;
        }

        // Retorna todos os espaços de uma cor específica
        // color: cor dos espaços (ex: "Green", "Blue", "Orange")
        // Retorna: lista com todos os espaços desta cor
        private List<Space> GetAllSpacesOfColor(string color)
        {
            // Cria lista vazia para guardar os espaços encontrados
            List<Space> spaces = new List<Space>();
            
            // Percorre todas as linhas
            for (int y = 0; y < 7; y++)
            {
                // Percorre todas as colunas
                for (int x = 0; x < 7; x++)
                {
                    // Se a cor deste espaço é igual à cor procurada
                    if (board[y, x].Color == color)
                    {
                        spaces.Add(board[y, x]);  // Adiciona à lista
                    }
                }
            }
            
            // Retorna a lista com todos os espaços encontrados
            return spaces;
        }
    }
}