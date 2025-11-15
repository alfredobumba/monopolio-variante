using System;
using System.Collections.Generic;
using System.Linq;

namespace MonopolioVariante
{
    public class Game
    {
        private Space[,] board;
        private List<Player> players;
        private int currentPlayerIndex;
        private Random random;
        private int freeParkMoney;

        public Game(List<Player> gamePlayers)
        {
            players = gamePlayers;
            foreach (var player in players)
            {
                player.ResetForNewGame();
            }
            
            currentPlayerIndex = 0;
            random = new Random();
            freeParkMoney = 0;
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            board = new Space[7, 7];
            
            // Linha 0 (topo)
            board[0, 0] = new Space("Prison", SpaceType.Prison);
            board[0, 1] = new Space("Green3", SpaceType.Property, 160, "Green");
            board[0, 2] = new Space("Violet1", SpaceType.Property, 150, "Violet");
            board[0, 3] = new Space("Train2", SpaceType.Train, 150);
            board[0, 4] = new Space("Red3", SpaceType.Property, 160, "Red");
            board[0, 5] = new Space("White1", SpaceType.Property, 160, "White");
            board[0, 6] = new Space("BackToStart", SpaceType.BackToStart);

            // Linha 1
            board[1, 0] = new Space("Blue3", SpaceType.Property, 170, "Blue");
            board[1, 1] = new Space("Community", SpaceType.Community);
            board[1, 2] = new Space("Red2", SpaceType.Property, 130, "Red");
            board[1, 3] = new Space("Violet2", SpaceType.Property, 130, "Violet");
            board[1, 4] = new Space("WaterWorks", SpaceType.Utility, 120);
            board[1, 5] = new Space("Chance", SpaceType.Chance);
            board[1, 6] = new Space("White2", SpaceType.Property, 180, "White");

            // Linha 2
            board[2, 0] = new Space("Blue2", SpaceType.Property, 140, "Blue");
            board[2, 1] = new Space("Red1", SpaceType.Property, 130, "Red");
            board[2, 2] = new Space("Chance", SpaceType.Chance);
            board[2, 3] = new Space("Brown2", SpaceType.Property, 120, "Brown");
            board[2, 4] = new Space("Community", SpaceType.Community);
            board[2, 5] = new Space("Black1", SpaceType.Property, 110, "Black");
            board[2, 6] = new Space("LuxTax", SpaceType.LuxTax, 80);

            // Linha 3 (centro)
            board[3, 0] = new Space("Train1", SpaceType.Train, 150);
            board[3, 1] = new Space("Green2", SpaceType.Property, 140, "Green");
            board[3, 2] = new Space("Teal1", SpaceType.Property, 90, "Teal");
            board[3, 3] = new Space("Start", SpaceType.Start);
            board[3, 4] = new Space("Teal2", SpaceType.Property, 130, "Teal");
            board[3, 5] = new Space("Black2", SpaceType.Property, 120, "Black");
            board[3, 6] = new Space("Train3", SpaceType.Train, 150);

            // Linha 4
            board[4, 0] = new Space("Blue1", SpaceType.Property, 140, "Blue");
            board[4, 1] = new Space("Green1", SpaceType.Property, 120, "Green");
            board[4, 2] = new Space("Community", SpaceType.Community);
            board[4, 3] = new Space("Brown1", SpaceType.Property, 100, "Brown");
            board[4, 4] = new Space("Chance", SpaceType.Chance);
            board[4, 5] = new Space("Black3", SpaceType.Property, 130, "Black");
            board[4, 6] = new Space("White3", SpaceType.Property, 190, "White");

            // Linha 5
            board[5, 0] = new Space("Pink1", SpaceType.Property, 160, "Pink");
            board[5, 1] = new Space("Chance", SpaceType.Chance);
            board[5, 2] = new Space("Orange1", SpaceType.Property, 120, "Orange");
            board[5, 3] = new Space("Orange2", SpaceType.Property, 120, "Orange");
            board[5, 4] = new Space("Orange3", SpaceType.Property, 140, "Orange");
            board[5, 5] = new Space("Community", SpaceType.Community);
            board[5, 6] = new Space("Yellow3", SpaceType.Property, 170, "Yellow");

            // Linha 6 (fundo)
            board[6, 0] = new Space("FreePark", SpaceType.FreePark);
            board[6, 1] = new Space("Pink2", SpaceType.Property, 180, "Pink");
            board[6, 2] = new Space("ElectricCompany", SpaceType.Utility, 120);
            board[6, 3] = new Space("Train4", SpaceType.Train, 150);
            board[6, 4] = new Space("Yellow1", SpaceType.Property, 140, "Yellow");
            board[6, 5] = new Space("Yellow2", SpaceType.Property, 140, "Yellow");
            board[6, 6] = new Space("Police", SpaceType.Police);
        }

        public void RollDice(string playerName)
        {
            Player player = GetPlayerByName(playerName);
            if (player == null)
            {
                Console.WriteLine("Jogador não participa no jogo em curso.");
                return;
            }

            if (players[currentPlayerIndex] != player)
            {
                Console.WriteLine("Não é a vez do jogador");
                return;
            }

            int dice1 = GetRandomDiceValue();
            int dice2 = GetRandomDiceValue();

            player.HasRolledDice = true;

            // Verificar se está preso
            if (player.InPrison)
            {
                player.TurnsInPrison++;
                
                if (dice1 == dice2)
                {
                    player.InPrison = false;
                    player.TurnsInPrison = 0;
                    player.PositionX = 0;
                    player.PositionY = 0;
                }
                else if (player.TurnsInPrison >= 3)
                {
                    player.InPrison = false;
                    player.TurnsInPrison = 0;
                    player.PositionX = 0;
                    player.PositionY = 0;
                }
                else
                {
                    Console.WriteLine($"Saiu {dice1}/{dice2} – espaço Prison. Jogador preso.");
                    return;
                }
            }

            // Mover jogador
            int newX = player.PositionX + dice1;
            int newY = player.PositionY + dice2;

            // Wrap around
            while (newX < 0) newX += 7;
            while (newX >= 7) newX -= 7;
            while (newY < 0) newY += 7;
            while (newY >= 7) newY -= 7;

            player.PositionX = newX;
            player.PositionY = newY;

            Space currentSpace = board[newY, newX];

            // Verificar doubles
            if (dice1 == dice2)
            {
                player.ConsecutiveDoubles++;
                if (player.ConsecutiveDoubles >= 2)
                {
                    player.InPrison = true;
                    player.PositionX = 0;
                    player.PositionY = 0;
                    player.ConsecutiveDoubles = 0;
                    Console.WriteLine($"Saiu {dice1}/{dice2} – espaço Police. Jogador preso.");
                    return;
                }
                player.MustRollAgain = true;
            }
            else
            {
                player.ConsecutiveDoubles = 0;
            }

            ProcessSpaceLanding(player, currentSpace, dice1, dice2);
        }

        private void ProcessSpaceLanding(Player player, Space space, int dice1, int dice2)
        {
            switch (space.Type)
            {
                case SpaceType.BackToStart:
                    player.PositionX = 3;
                    player.PositionY = 3;
                    Console.WriteLine($"Saiu {dice1}/{dice2} - Espaço BackToStart. Peça colocada no espaço Start.");
                    break;

                case SpaceType.Police:
                    player.InPrison = true;
                    player.PositionX = 0;
                    player.PositionY = 0;
                    Console.WriteLine($"Saiu {dice1}/{dice2} – espaço Police. Jogador preso.");
                    break;

                case SpaceType.Prison:
                    Console.WriteLine($"Saiu {dice1}/{dice2} – espaço Prison. Jogador só de passagem.");
                    break;

                case SpaceType.FreePark:
                    player.Money += freeParkMoney;
                    Console.WriteLine($"Saiu {dice1}/{dice2} – espaço FreePark. Jogador recebe {freeParkMoney}.");
                    freeParkMoney = 0;
                    break;

                case SpaceType.Chance:
                case SpaceType.Community:
                    player.NeedsToDrawCard = true;
                    player.HasDrawnCard = false;
                    Console.WriteLine($"Saiu {dice1}/{dice2} – espaço {space.Name}. Espaço especial. Tirar carta.");
                    break;

                case SpaceType.Start:
                    player.Money += 200;
                    Console.WriteLine($"Saiu {dice1}/{dice2} – espaço {space.Name}. Espaço sem dono.");
                    break;

                default:
                    if (space.Owner == null)
                    {
                        Console.WriteLine($"Saiu {dice1}/{dice2} – espaço {space.Name}. Espaço sem dono.");
                    }
                    else if (space.Owner == player)
                    {
                        Console.WriteLine($"Saiu {dice1}/{dice2} – espaço {space.Name}. Espaço já comprada.");
                    }
                    else
                    {
                        player.NeedsToPayRent = true;
                        Console.WriteLine($"Saiu {dice1}/{dice2} – espaço {space.Name}. Espaço já comprada por outro jogador. Necessário pagar renda.");
                    }
                    break;
            }
        }

        public void BuySpace(string playerName)
        {
            Player player = GetPlayerByName(playerName);
            if (player == null)
            {
                Console.WriteLine("Jogador não participa no jogo em curso.");
                return;
            }

            if (players[currentPlayerIndex] != player)
            {
                Console.WriteLine("Não é a vez do jogador");
                return;
            }

            Space currentSpace = board[player.PositionY, player.PositionX];

            if (!currentSpace.CanBePurchased())
            {
                Console.WriteLine("Este espaço não está para venda.");
                return;
            }

            if (currentSpace.Owner != null)
            {
                Console.WriteLine("O espaço já se encontra comprado.");
                return;
            }

            if (player.Money < currentSpace.Price)
            {
                Console.WriteLine("O jogador não tem dinheiro suficiente para adquirir o espaço.");
                return;
            }

            player.Money -= currentSpace.Price;
            currentSpace.Owner = player;
            
            if (currentSpace.Type == SpaceType.LuxTax)
            {
                freeParkMoney += currentSpace.Price;
            }
            
            Console.WriteLine("Espaço comprado.");
        }

        public void PayRent(string playerName)
        {
            Player player = GetPlayerByName(playerName);
            if (player == null)
            {
                Console.WriteLine("Jogador não participa no jogo em curso.");
                return;
            }

            if (players[currentPlayerIndex] != player)
            {
                Console.WriteLine("Não é a vez do jogador.");
                return;
            }

            if (!player.NeedsToPayRent)
            {
                Console.WriteLine("Não é necessário pagar aluguer");
                return;
            }

            Space currentSpace = board[player.PositionY, player.PositionX];
            int rent = (int)(currentSpace.Price * 0.25 + currentSpace.Price * 0.75 * currentSpace.Houses);

            if (player.Money < rent)
            {
                Console.WriteLine("O jogador não tem dinheiro suficiente.");
                return;
            }

            player.Money -= rent;
            currentSpace.Owner.Money += rent;
            player.NeedsToPayRent = false;
            
            Console.WriteLine("Aluguer pago.");
        }

        public void BuyHouse(string playerName, string spaceName)
        {
            Player player = GetPlayerByName(playerName);
            if (player == null)
            {
                Console.WriteLine("Jogador não participa no jogo em curso.");
                return;
            }

            if (players[currentPlayerIndex] != player)
            {
                Console.WriteLine("Não é a vez do jogador.");
                return;
            }

            Space targetSpace = FindSpaceByName(spaceName);
            if (targetSpace == null || targetSpace.Owner != player)
            {
                Console.WriteLine("Não é possível comprar casa no espaço indicado.");
                return;
            }

            if (string.IsNullOrEmpty(targetSpace.Color))
            {
                Console.WriteLine("Não é possível comprar casa no espaço indicado.");
                return;
            }

            // Verificar se possui todos os espaços da cor
            List<Space> colorSpaces = GetAllSpacesOfColor(targetSpace.Color);
            if (!colorSpaces.All(s => s.Owner == player))
            {
                Console.WriteLine("O jogador não possui todos os espaços da cor");
                return;
            }

            if (targetSpace.Houses >= 4)
            {
                Console.WriteLine("Não é possível comprar casa no espaço indicado.");
                return;
            }

            int housePrice = (int)(targetSpace.Price * 0.6);
            if (player.Money < housePrice)
            {
                Console.WriteLine("O jogador não possui dinheiro suficiente.");
                return;
            }

            player.Money -= housePrice;
            targetSpace.Houses++;
            
            Console.WriteLine("Casa adquirida.");
        }

        public void DrawCard(string playerName)
        {
            Player player = GetPlayerByName(playerName);
            if (player == null)
            {
                Console.WriteLine("Jogador não participa no jogo em curso.");
                return;
            }

            if (players[currentPlayerIndex] != player)
            {
                Console.WriteLine("Não é a vez do jogador.");
                return;
            }

            if (player.HasDrawnCard)
            {
                Console.WriteLine("A carta já foi tirada.");
                return;
            }

            Space currentSpace = board[player.PositionY, player.PositionX];
            
            if (currentSpace.Type != SpaceType.Chance && currentSpace.Type != SpaceType.Community)
            {
                Console.WriteLine("Não é possível tirar carta neste espaço.");
                return;
            }

            player.HasDrawnCard = true;
            player.NeedsToDrawCard = false;

            int cardRoll = random.Next(1, 101);

            if (currentSpace.Type == SpaceType.Chance)
            {
                ProcessChanceCard(player, cardRoll);
            }
            else
            {
                ProcessCommunityCard(player, cardRoll);
            }
        }

        private void ProcessChanceCard(Player player, int roll)
        {
            if (roll <= 20) // 20%
            {
                player.Money += 150;
                Console.WriteLine("O jogador recebe 150.");
            }
            else if (roll <= 30) // 10%
            {
                player.Money += 200;
                Console.WriteLine("O jogador recebe 200.");
            }
            else if (roll <= 40) // 10%
            {
                player.Money -= 70;
                freeParkMoney += 70;
                Console.WriteLine("O jogador tem de pagar 70.");
                
                if (player.Money < 0)
                {
                    player.Money = -1;
                }
            }
            else if (roll <= 60) // 20%
            {
                player.PositionX = 3;
                player.PositionY = 3;
                player.Money += 200;
                Console.WriteLine("O jogador move-se para a casa Start.");
            }
            else if (roll <= 80) // 20%
            {
                player.PositionX = 0;
                player.PositionY = 0;
                player.InPrison = true;
                Console.WriteLine("O jogador move-se para a casa Police.");
            }
            else // 20%
            {
                player.PositionX = 0;
                player.PositionY = 6;
                player.Money += freeParkMoney;
                Console.WriteLine("O jogador move-se para a casa FreePark.");
                freeParkMoney = 0;
            }
        }

        private void ProcessCommunityCard(Player player, int roll)
        {
            if (roll <= 10) // 10%
            {
                int totalHouses = 0;
                for (int y = 0; y < 7; y++)
                {
                    for (int x = 0; x < 7; x++)
                    {
                        if (board[y, x].Owner == player)
                        {
                            totalHouses += board[y, x].Houses;
                        }
                    }
                }
                int payment = totalHouses * 20;
                player.Money -= payment;
                freeParkMoney += payment;
                Console.WriteLine($"O jogador paga {payment} pelas casas nos seus espaços.");
                
                if (player.Money < 0)
                {
                    player.Money = -1;
                }
            }
            else if (roll <= 20) // 10%
            {
                int total = 0;
                foreach (var p in players)
                {
                    if (p != player)
                    {
                        p.Money -= 10;
                        total += 10;
                    }
                }
                player.Money += total;
                Console.WriteLine($"O jogador recebe {total} de outros jogadores.");
            }
            else if (roll <= 40) // 20%
            {
                player.Money += 100;
                Console.WriteLine("O jogador recebe 100");
            }
            else if (roll <= 60) // 20%
            {
                player.Money += 170;
                Console.WriteLine("O jogador recebe 170.");
            }
            else if (roll <= 70) // 10%
            {
                player.Money -= 40;
                freeParkMoney += 40;
                Console.WriteLine("O jogador tem de pagar 40.");
                
                if (player.Money < 0)
                {
                    player.Money = -1;
                }
            }
            else if (roll <= 80) // 10%
            {
                player.PositionX = 0;
                player.PositionY = 5;
                Console.WriteLine("O jogador move-se para Pink1.");
            }
            else if (roll <= 90) // 10%
            {
                player.PositionX = 4;
                player.PositionY = 3;
                Console.WriteLine("O jogador move-se para Teal2.");
            }
            else // 10%
            {
                player.PositionX = 5;
                player.PositionY = 1;
                Console.WriteLine("O jogador move-se para White2.");
            }
        }

        public void EndTurn(string playerName)
        {
            Player player = GetPlayerByName(playerName);
            if (player == null)
            {
                Console.WriteLine("Jogador não participa no jogo em curso.");
                return;
            }

            if (players[currentPlayerIndex] != player)
            {
                Console.WriteLine("Não é o turno do jogador indicado.");
                return;
            }

            if (!player.HasRolledDice)
            {
                Console.WriteLine("O jogador ainda tem ações a fazer.");
                return;
            }

            if (player.NeedsToPayRent || player.NeedsToDrawCard)
            {
                Console.WriteLine("O jogador ainda tem ações a fazer.");
                return;
            }

            if (player.MustRollAgain)
            {
                Console.WriteLine("O jogador ainda tem ações a fazer.");
                return;
            }

            // Reset player state
            player.HasRolledDice = false;
            player.MustRollAgain = false;
            player.HasDrawnCard = false;
            
            // Next player
            currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
            
            Console.WriteLine($"Turno terminado. Novo turno do jogador {players[currentPlayerIndex].Name}.");
        }

        public void ShowGameDetails()
        {
            for (int y = 0; y < 7; y++)
            {
                List<string> rowParts = new List<string>();
                
                for (int x = 0; x < 7; x++)
                {
                    Space space = board[y, x];
                    string spacePart = space.Name;
                    
                    if (space.Owner != null)
                    {
                        spacePart += $" ({space.Owner.Name}";
                        if (space.Houses > 0)
                        {
                            spacePart += $" – {space.Houses}";
                        }
                        spacePart += ")";
                    }
                    
                    // Adicionar jogadores neste espaço
                    List<string> playersHere = new List<string>();
                    foreach (var p in players)
                    {
                        if (p.PositionX == x && p.PositionY == y && !p.InPrison)
                        {
                            playersHere.Add(p.Name);
                        }
                        else if (p.InPrison && x == 0 && y == 0)
                        {
                            playersHere.Add(p.Name);
                        }
                    }
                    
                    if (playersHere.Count > 0)
                    {
                        spacePart += " " + string.Join(" ", playersHere);
                    }
                    
                    rowParts.Add(spacePart);
                }
                
                Console.WriteLine(string.Join(" ", rowParts));
            }
            
            Player currentPlayer = players[currentPlayerIndex];
            Console.WriteLine($"{currentPlayer.Name} - {currentPlayer.Money}");
        }

        private int GetRandomDiceValue()
        {
            int value = random.Next(1, 7);
            return value <= 3 ? -value : value - 3;
        }

        private Player GetPlayerByName(string name)
        {
            return players.FirstOrDefault(p => p.Name == name);
        }

        private Space FindSpaceByName(string name)
        {
            for (int y = 0; y < 7; y++)
            {
                for (int x = 0; x < 7; x++)
                {
                    if (board[y, x].Name == name)
                    {
                        return board[y, x];
                    }
                }
            }
            return null;
        }

        private List<Space> GetAllSpacesOfColor(string color)
        {
            List<Space> spaces = new List<Space>();
            for (int y = 0; y < 7; y++)
            {
                for (int x = 0; x < 7; x++)
                {
                    if (board[y, x].Color == color)
                    {
                        spaces.Add(board[y, x]);
                    }
                }
            }
            return spaces;
        }
    }
}