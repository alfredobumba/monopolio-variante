using System; // Importa funcionalidades básicas do sistema (Console, Random, etc)
using System.Collections.Generic; // Permite usar List<T> e outras coleções genéricas
using System.Linq; // Permite usar métodos LINQ como FirstOrDefault, All, etc

namespace MonopolioVariante // Define o espaço de nomes para organizar o código
{
    // Classe principal que gere toda a lógica do jogo Monopólio
    public class Game
    {
        // ═══════════════════════════════════════════════════════════════════════
        //                         VARIÁVEIS DE INSTÂNCIA
        // ═══════════════════════════════════════════════════════════════════════
        
        // Tabuleiro 7x7 que contém todos os espaços do jogo
        // [linha, coluna] ou [Y, X]
        private Space[,] board;
        
        // Lista de todos os jogadores participantes no jogo
        private List<Player> players;
        
        // Índice do jogador atual (0 = primeiro jogador, 1 = segundo, etc)
        private int currentPlayerIndex;
        
        // Gerador de números aleatórios para dados e cartas
        private Random random;
        
        // Dinheiro acumulado no espaço FreePark
        private int freeParkMoney;

        // ═══════════════════════════════════════════════════════════════════════
        //                            CONSTRUTOR
        // ═══════════════════════════════════════════════════════════════════════
        // Função: Inicializa um novo jogo com os jogadores fornecidos
        // Parâmetros: gamePlayers - lista de jogadores que vão participar
        public Game(List<Player> gamePlayers)
        {
            // Guarda a lista de jogadores na variável de instância
            players = gamePlayers;
            
            // Para cada jogador na lista
            foreach (var player in players)
            {
                // Reinicia o estado do jogador para um novo jogo
                // (dinheiro inicial, posição inicial, etc)
                player.ResetForNewGame();
            }
            
            // Define que o primeiro jogador (índice 0) é o jogador atual
            currentPlayerIndex = 0;
            
            // Cria uma nova instância do gerador de números aleatórios
            random = new Random();
            
            // Inicializa o dinheiro do FreePark com 0
            freeParkMoney = 0;
            
            // Inicializa o tabuleiro 7x7 com todos os espaços
            InitializeBoard();
        }

        // ═══════════════════════════════════════════════════════════════════════
        //                      INICIALIZAÇÃO DO TABULEIRO
        // ═══════════════════════════════════════════════════════════════════════
        // Função: Cria e preenche o tabuleiro 7x7 com todos os espaços do jogo
        // Esta função define a localização, tipo e propriedades de cada espaço
        private void InitializeBoard()
        {
            // Cria um array bidimensional 7x7 para o tabuleiro
            board = new Space[7, 7];
            
            // ═══ LINHA 0 (TOPO DO TABULEIRO) ═══
            // Posição [0,0] - Prisão (canto superior esquerdo)
            board[0, 0] = new Space("Prison", SpaceType.Prison);
            
            // Posição [0,1] - Propriedade verde #3, preço 160, cor "Green"
            board[0, 1] = new Space("Green3", SpaceType.Property, 160, "Green");
            
            // Posição [0,2] - Propriedade violeta #1, preço 150
            board[0, 2] = new Space("Violet1", SpaceType.Property, 150, "Violet");
            
            // Posição [0,3] - Estação #2, preço 150
            board[0, 3] = new Space("Train2", SpaceType.Train, 150);
            
            // Posição [0,4] - Propriedade vermelha #3, preço 160
            board[0, 4] = new Space("Red3", SpaceType.Property, 160, "Red");
            
            // Posição [0,5] - Propriedade branca #1, preço 160
            board[0, 5] = new Space("White1", SpaceType.Property, 160, "White");
            
            // Posição [0,6] - Espaço especial "Voltar ao Início" (canto superior direito)
            board[0, 6] = new Space("BackToStart", SpaceType.BackToStart);

            // ═══ LINHA 1 ═══
            // Posição [1,0] - Propriedade azul #3, preço 170
            board[1, 0] = new Space("Blue3", SpaceType.Property, 170, "Blue");
            
            // Posição [1,1] - Espaço de carta Community
            board[1, 1] = new Space("Community", SpaceType.Community);
            
            // Posição [1,2] - Propriedade vermelha #2, preço 130
            board[1, 2] = new Space("Red2", SpaceType.Property, 130, "Red");
            
            // Posição [1,3] - Propriedade violeta #2, preço 130
            board[1, 3] = new Space("Violet2", SpaceType.Property, 130, "Violet");
            
            // Posição [1,4] - Companhia de Águas (utilitário), preço 120
            board[1, 4] = new Space("WaterWorks", SpaceType.Utility, 120);
            
            // Posição [1,5] - Espaço de carta Chance
            board[1, 5] = new Space("Chance", SpaceType.Chance);
            
            // Posição [1,6] - Propriedade branca #2, preço 180
            board[1, 6] = new Space("White2", SpaceType.Property, 180, "White");

            // ═══ LINHA 2 ═══
            // Posição [2,0] - Propriedade azul #2, preço 140
            board[2, 0] = new Space("Blue2", SpaceType.Property, 140, "Blue");
            
            // Posição [2,1] - Propriedade vermelha #1, preço 130
            board[2, 1] = new Space("Red1", SpaceType.Property, 130, "Red");
            
            // Posição [2,2] - Espaço de carta Chance
            board[2, 2] = new Space("Chance", SpaceType.Chance);
            
            // Posição [2,3] - Propriedade castanha #2, preço 120
            board[2, 3] = new Space("Brown2", SpaceType.Property, 120, "Brown");
            
            // Posição [2,4] - Espaço de carta Community
            board[2, 4] = new Space("Community", SpaceType.Community);
            
            // Posição [2,5] - Propriedade preta #1, preço 110
            board[2, 5] = new Space("Black1", SpaceType.Property, 110, "Black");
            
            // Posição [2,6] - Taxa de Luxo, custa 80
            board[2, 6] = new Space("LuxTax", SpaceType.LuxTax, 80);

            // ═══ LINHA 3 (CENTRO DO TABULEIRO) ═══
            // Posição [3,0] - Estação #1, preço 150
            board[3, 0] = new Space("Train1", SpaceType.Train, 150);
            
            // Posição [3,1] - Propriedade verde #2, preço 140
            board[3, 1] = new Space("Green2", SpaceType.Property, 140, "Green");
            
            // Posição [3,2] - Propriedade turquesa #1, preço 90
            board[3, 2] = new Space("Teal1", SpaceType.Property, 90, "Teal");
            
            // Posição [3,3] - INÍCIO (centro do tabuleiro)
            board[3, 3] = new Space("Start", SpaceType.Start);
            
            // Posição [3,4] - Propriedade turquesa #2, preço 130
            board[3, 4] = new Space("Teal2", SpaceType.Property, 130, "Teal");
            
            // Posição [3,5] - Propriedade preta #2, preço 120
            board[3, 5] = new Space("Black2", SpaceType.Property, 120, "Black");
            
            // Posição [3,6] - Estação #3, preço 150
            board[3, 6] = new Space("Train3", SpaceType.Train, 150);

            // ═══ LINHA 4 ═══
            // Posição [4,0] - Propriedade azul #1, preço 140
            board[4, 0] = new Space("Blue1", SpaceType.Property, 140, "Blue");
            
            // Posição [4,1] - Propriedade verde #1, preço 120
            board[4, 1] = new Space("Green1", SpaceType.Property, 120, "Green");
            
            // Posição [4,2] - Espaço de carta Community
            board[4, 2] = new Space("Community", SpaceType.Community);
            
            // Posição [4,3] - Propriedade castanha #1, preço 100
            board[4, 3] = new Space("Brown1", SpaceType.Property, 100, "Brown");
            
            // Posição [4,4] - Espaço de carta Chance
            board[4, 4] = new Space("Chance", SpaceType.Chance);
            
            // Posição [4,5] - Propriedade preta #3, preço 130
            board[4, 5] = new Space("Black3", SpaceType.Property, 130, "Black");
            
            // Posição [4,6] - Propriedade branca #3, preço 190
            board[4, 6] = new Space("White3", SpaceType.Property, 190, "White");

            // ═══ LINHA 5 ═══
            // Posição [5,0] - Propriedade rosa #1, preço 160
            board[5, 0] = new Space("Pink1", SpaceType.Property, 160, "Pink");
            
            // Posição [5,1] - Espaço de carta Chance
            board[5, 1] = new Space("Chance", SpaceType.Chance);
            
            // Posição [5,2] - Propriedade laranja #1, preço 120
            board[5, 2] = new Space("Orange1", SpaceType.Property, 120, "Orange");
            
            // Posição [5,3] - Propriedade laranja #2, preço 120
            board[5, 3] = new Space("Orange2", SpaceType.Property, 120, "Orange");
            
            // Posição [5,4] - Propriedade laranja #3, preço 140
            board[5, 4] = new Space("Orange3", SpaceType.Property, 140, "Orange");
            
            // Posição [5,5] - Espaço de carta Community
            board[5, 5] = new Space("Community", SpaceType.Community);
            
            // Posição [5,6] - Propriedade amarela #3, preço 170
            board[5, 6] = new Space("Yellow3", SpaceType.Property, 170, "Yellow");

            // ═══ LINHA 6 (FUNDO DO TABULEIRO) ═══
            // Posição [6,0] - Parque Grátis (canto inferior esquerdo)
            board[6, 0] = new Space("FreePark", SpaceType.FreePark);
            
            // Posição [6,1] - Propriedade rosa #2, preço 180
            board[6, 1] = new Space("Pink2", SpaceType.Property, 180, "Pink");
            
            // Posição [6,2] - Companhia Elétrica (utilitário), preço 120
            board[6, 2] = new Space("ElectricCompany", SpaceType.Utility, 120);
            
            // Posição [6,3] - Estação #4, preço 150
            board[6, 3] = new Space("Train4", SpaceType.Train, 150);
            
            // Posição [6,4] - Propriedade amarela #1, preço 140
            board[6, 4] = new Space("Yellow1", SpaceType.Property, 140, "Yellow");
            
            // Posição [6,5] - Propriedade amarela #2, preço 140
            board[6, 5] = new Space("Yellow2", SpaceType.Property, 140, "Yellow");
            
            // Posição [6,6] - Polícia (canto inferior direito)
            board[6, 6] = new Space("Police", SpaceType.Police);
        }

        // ═══════════════════════════════════════════════════════════════════════
        //                          LANÇAR DADOS
        // ═══════════════════════════════════════════════════════════════════════
        // Função: Lança os dados para um jogador e move-o no tabuleiro
        // Parâmetros: playerName - nome do jogador que vai lançar os dados
        public void RollDice(string playerName)
        {
            // Procura o jogador pelo nome
            Player player = GetPlayerByName(playerName);
            
            // Se o jogador não foi encontrado (null)
            if (player == null)
            {
                // Informa que o jogador não está no jogo
                Console.WriteLine("Jogador não participa no jogo em curso.");
                return; // Sai da função
            }

            // Verifica se é a vez deste jogador
            // Compara o jogador atual (pela índice) com o jogador solicitado
            if (players[currentPlayerIndex] != player)
            {
                // Se não for a vez dele, informa
                Console.WriteLine("Não é a vez do jogador");
                return; // Sai da função
            }

            // Gera valores aleatórios para os dois dados
            // Cada dado pode ser -3, -2, -1, 1, 2, 3 (nunca 0)
            int dice1 = GetRandomDiceValue();
            int dice2 = GetRandomDiceValue();

            // Marca que o jogador já lançou os dados neste turno
            player.HasRolledDice = true;

            // ═══ VERIFICAR SE O JOGADOR ESTÁ PRESO ═══
            if (player.InPrison)
            {
                // Incrementa o número de turnos que está preso
                player.TurnsInPrison++;
                
                // Se tirou doubles (dados iguais), sai da prisão
                if (dice1 == dice2)
                {
                    player.InPrison = false; // Já não está preso
                    player.TurnsInPrison = 0; // Reseta contador de turnos na prisão
                    player.PositionX = 0; // Coloca na posição X da prisão
                    player.PositionY = 0; // Coloca na posição Y da prisão
                }
                // Se já está preso há 3 turnos, sai automaticamente
                else if (player.TurnsInPrison >= 3)
                {
                    player.InPrison = false; // Já não está preso
                    player.TurnsInPrison = 0; // Reseta contador
                    player.PositionX = 0; // Coloca na posição da prisão
                    player.PositionY = 0;
                }
                // Se ainda não pode sair
                else
                {
                    // Informa que continua preso
                    Console.WriteLine($"Saiu {dice1}/{dice2} – espaço Prison. Jogador preso.");
                    return; // Sai da função sem mover o jogador
                }
            }

            // ═══ CALCULAR NOVA POSIÇÃO ═══
            // Adiciona o valor do dado1 à posição X atual
            int newX = player.PositionX + dice1;
            // Adiciona o valor do dado2 à posição Y atual
            int newY = player.PositionY + dice2;

            // ═══ APLICAR WRAP-AROUND (TABULEIRO CIRCULAR) ═══
            // Se a posição X for negativa, adiciona 7 até ficar entre 0-6
            while (newX < 0) newX += 7;
            // Se a posição X for >= 7, subtrai 7 até ficar entre 0-6
            while (newX >= 7) newX -= 7;
            // Se a posição Y for negativa, adiciona 7 até ficar entre 0-6
            while (newY < 0) newY += 7;
            // Se a posição Y for >= 7, subtrai 7 até ficar entre 0-6
            while (newY >= 7) newY -= 7;

            // Atualiza a posição do jogador para a nova posição calculada
            player.PositionX = newX;
            player.PositionY = newY;

            // Obtém o espaço onde o jogador caiu
            // Note que o array é [Y, X] (linha, coluna)
            Space currentSpace = board[newY, newX];

            // ═══ VERIFICAR DOUBLES (DADOS IGUAIS) ═══
            if (dice1 == dice2)
            {
                // Incrementa o contador de doubles consecutivos
                player.ConsecutiveDoubles++;
                
                // Se tirou 2 ou mais doubles consecutivos
                if (player.ConsecutiveDoubles >= 2)
                {
                    // Jogador vai para a prisão
                    player.InPrison = true;
                    player.PositionX = 0; // Posição da prisão
                    player.PositionY = 0;
                    player.ConsecutiveDoubles = 0; // Reseta contador de doubles
                    Console.WriteLine($"Saiu {dice1}/{dice2} – espaço Police. Jogador preso.");
                    return; // Sai da função
                }
                // Se foi o primeiro double, pode jogar novamente
                player.MustRollAgain = true;
            }
            else
            {
                // Se os dados não são iguais, reseta o contador de doubles
                player.ConsecutiveDoubles = 0;
            }

            // Processa as ações do espaço onde o jogador caiu
            ProcessSpaceLanding(player, currentSpace, dice1, dice2);
        }

        // ═══════════════════════════════════════════════════════════════════════
        //                    PROCESSAR ATERRAGEM NO ESPAÇO
        // ═══════════════════════════════════════════════════════════════════════
        // Função: Executa as ações específicas do espaço onde o jogador caiu
        // Parâmetros: 
        //   player - jogador que caiu no espaço
        //   space - espaço onde caiu
        //   dice1, dice2 - valores dos dados lançados
        private void ProcessSpaceLanding(Player player, Space space, int dice1, int dice2)
        {
            // Verifica o tipo de espaço e executa a ação correspondente
            switch (space.Type)
            {
                // ═══ ESPAÇO: VOLTAR AO INÍCIO ═══
                case SpaceType.BackToStart:
                    // Move o jogador para o espaço Start (posição 3,3)
                    player.PositionX = 3;
                    player.PositionY = 3;
                    Console.WriteLine($"Saiu {dice1}/{dice2} - Espaço BackToStart. Peça colocada no espaço Start.");
                    break;

                // ═══ ESPAÇO: POLÍCIA (VAI PARA A PRISÃO) ═══
                case SpaceType.Police:
                    // Marca o jogador como preso
                    player.InPrison = true;
                    // Move para a posição da prisão (0,0)
                    player.PositionX = 0;
                    player.PositionY = 0;
                    Console.WriteLine($"Saiu {dice1}/{dice2} – espaço Police. Jogador preso.");
                    break;

                // ═══ ESPAÇO: PRISÃO (APENAS DE PASSAGEM) ═══
                case SpaceType.Prison:
                    // Jogador está apenas visitando, não fica preso
                    Console.WriteLine($"Saiu {dice1}/{dice2} – espaço Prison. Jogador só de passagem.");
                    break;

                // ═══ ESPAÇO: PARQUE GRÁTIS ═══
                case SpaceType.FreePark:
                    // Jogador recebe todo o dinheiro acumulado no FreePark
                    player.Money += freeParkMoney;
                    Console.WriteLine($"Saiu {dice1}/{dice2} – espaço FreePark. Jogador recebe {freeParkMoney}.");
                    // Reseta o dinheiro do FreePark para 0
                    freeParkMoney = 0;
                    break;

                // ═══ ESPAÇOS: CHANCE OU COMMUNITY ═══
                case SpaceType.Chance:
                case SpaceType.Community:
                    // Marca que o jogador precisa tirar uma carta
                    player.NeedsToDrawCard = true;
                    player.HasDrawnCard = false;
                    Console.WriteLine($"Saiu {dice1}/{dice2} – espaço {space.Name}. Espaço especial. Tirar carta.");
                    break;

                // ═══ ESPAÇO: START (INÍCIO) ═══
                case SpaceType.Start:
                    // Jogador recebe 200 por cair no Start
                    player.Money += 200;
                    Console.WriteLine($"Saiu {dice1}/{dice2} – espaço {space.Name}. Espaço sem dono.");
                    break;

                // ═══ OUTROS ESPAÇOS (PROPRIEDADES, ESTAÇÕES, UTILITÁRIOS) ═══
                default:
                    // Verifica se o espaço não tem dono
                    if (space.Owner == null)
                    {
                        Console.WriteLine($"Saiu {dice1}/{dice2} – espaço {space.Name}. Espaço sem dono.");
                    }
                    // Verifica se o jogador é o dono do espaço
                    else if (space.Owner == player)
                    {
                        Console.WriteLine($"Saiu {dice1}/{dice2} – espaço {space.Name}. Espaço já comprada.");
                    }
                    // O espaço pertence a outro jogador
                    else
                    {
                        // Marca que o jogador precisa pagar aluguer
                        player.NeedsToPayRent = true;
                        Console.WriteLine($"Saiu {dice1}/{dice2} – espaço {space.Name}. Espaço já comprada por outro jogador. Necessário pagar renda.");
                    }
                    break;
            }
        }

        // ═══════════════════════════════════════════════════════════════════════
        //                          COMPRAR ESPAÇO
        // ═══════════════════════════════════════════════════════════════════════
        // Função: Permite ao jogador comprar o espaço onde está atualmente
        // Parâmetros: playerName - nome do jogador que quer comprar
        public void BuySpace(string playerName)
        {
            // Procura o jogador pelo nome
            Player player = GetPlayerByName(playerName);
            
            // Se o jogador não foi encontrado
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

            // Obtém o espaço onde o jogador está atualmente
            // Note: board[Y, X]
            Space currentSpace = board[player.PositionY, player.PositionX];

            // Verifica se o espaço pode ser comprado
            // (alguns espaços como Start, Prison, etc não podem)
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

            // ═══ EFETUAR A COMPRA ═══
            // Subtrai o preço do dinheiro do jogador
            player.Money -= currentSpace.Price;
            // Define o jogador como dono do espaço
            currentSpace.Owner = player;
            
            // Se for uma taxa de luxo, o dinheiro vai para o FreePark
            if (currentSpace.Type == SpaceType.LuxTax)
            {
                freeParkMoney += currentSpace.Price;
            }
            
            Console.WriteLine("Espaço comprado.");
        }

        // ═══════════════════════════════════════════════════════════════════════
        //                          PAGAR ALUGUER
        // ═══════════════════════════════════════════════════════════════════════
        // Função: Processa o pagamento de aluguer de um espaço
        // Fórmula: Aluguer = Preço × 0.25 + Preço × 0.75 × NúmeroDeCasas
        // Parâmetros: playerName - nome do jogador que vai pagar
        public void PayRent(string playerName)
        {
            // Procura o jogador pelo nome
            Player player = GetPlayerByName(playerName);
            
            // Se o jogador não foi encontrado
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

            // Verifica se o jogador realmente precisa pagar aluguer
            if (!player.NeedsToPayRent)
            {
                Console.WriteLine("Não é necessário pagar aluguer");
                return;
            }

            // Obtém o espaço onde o jogador está
            Space currentSpace = board[player.PositionY, player.PositionX];
            
            // ═══ CÁLCULO DO ALUGUER ═══
            // Fórmula: Preço × 0.25 + Preço × 0.75 × NúmeroDeCasas
            // Base (25% do preço) + Adicional (75% × casas)
            int rent = (int)(currentSpace.Price * 0.25 + currentSpace.Price * 0.75 * currentSpace.Houses);

            // Verifica se o jogador tem dinheiro suficiente
            if (player.Money < rent)
            {
                Console.WriteLine("O jogador não tem dinheiro suficiente.");
                return;
            }

            // ═══ EFETUAR O PAGAMENTO ═══
            // Subtrai o aluguer do dinheiro do jogador
            player.Money -= rent;
            // Adiciona o aluguer ao dinheiro do proprietário
            currentSpace.Owner.Money += rent;
            // Marca que já não precisa pagar aluguer
            player.NeedsToPayRent = false;
            
            Console.WriteLine("Aluguer pago.");
        }

        // ═══════════════════════════════════════════════════════════════════════
        //                          COMPRAR CASA
        // ═══════════════════════════════════════════════════════════════════════
        // Função: Permite ao jogador comprar uma casa num espaço que possui
        // Requisitos: Ter todas as propriedades da mesma cor
        // Custo: 60% do preço do espaço
        // Limite: Máximo 4 casas por espaço
        // Parâmetros: 
        //   playerName - nome do jogador
        //   spaceName - nome do espaço onde quer comprar a casa
        public void BuyHouse(string playerName, string spaceName)
        {
            // Procura o jogador pelo nome
            Player player = GetPlayerByName(playerName);
            
            // Se o jogador não foi encontrado
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
            
            // Verifica se o espaço existe E se pertence ao jogador
            if (targetSpace == null || targetSpace.Owner != player)
            {
                Console.WriteLine("Não é possível comprar casa no espaço indicado.");
                return;
            }

            // Verifica se o espaço tem cor (só propriedades coloridas podem ter casas)
            // Espaços como estações e utilitários não têm cor
            if (string.IsNullOrEmpty(targetSpace.Color))
            {
                Console.WriteLine("Não é possível comprar casa no espaço indicado.");
                return;
            }

            // ═══ VERIFICAR MONOPÓLIO (TER TODAS AS PROPRIEDADES DA COR) ═══
            // Obtém todos os espaços da mesma cor
            List<Space> colorSpaces = GetAllSpacesOfColor(targetSpace.Color);
            
            // Verifica se o jogador possui TODOS os espaços dessa cor
            // All() retorna true apenas se TODOS os espaços têm o jogador como dono
            if (!colorSpaces.All(s => s.Owner == player))
            {
                Console.WriteLine("O jogador não possui todos os espaços da cor");
                return;
            }

            // Verifica se já tem 4 casas (máximo permitido)
            if (targetSpace.Houses >= 4)
            {
                Console.WriteLine("Não é possível comprar casa no espaço indicado.");
                return;
            }

            // ═══ CÁLCULO DO PREÇO DA CASA ═══
            // Preço da casa = 60% do preço do espaço
            int housePrice = (int)(targetSpace.Price * 0.6);
            
            // Verifica se o jogador tem dinheiro suficiente
            if (player.Money < housePrice)
            {
                Console.WriteLine("O jogador não possui dinheiro suficiente.");
                return;
            }

            // ═══ EFETUAR A COMPRA ═══
            // Subtrai o preço da casa do dinheiro do jogador
            player.Money -= housePrice;
            // Incrementa o número de casas no espaço
            targetSpace.Houses++;
            
            Console.WriteLine("Casa adquirida.");
        }

        // ═══════════════════════════════════════════════════════════════════════
        //                          TIRAR CARTA
        // ═══════════════════════════════════════════════════════════════════════
        // Função: Permite ao jogador tirar uma carta Chance ou Community
        // Só pode tirar carta em espaços Chance ou Community
        // Parâmetros: playerName - nome do jogador
        public void DrawCard(string playerName)
        {
            // Procura o jogador pelo nome
            Player player = GetPlayerByName(playerName);
            
            // Se o jogador não foi encontrado
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

            // Verifica se o jogador já tirou a carta
            if (player.HasDrawnCard)
            {
                Console.WriteLine("A carta já foi tirada.");
                return;
            }

            // Obtém o espaço onde o jogador está
            Space currentSpace = board[player.PositionY, player.PositionX];
            
            // Verifica se o espaço atual é Chance ou Community
            if (currentSpace.Type != SpaceType.Chance && currentSpace.Type != SpaceType.Community)
            {
                Console.WriteLine("Não é possível tirar carta neste espaço.");
                return;
            }

            // ═══ TIRAR A CARTA ═══
            // Marca que o jogador já tirou a carta
            player.HasDrawnCard = true;
            // Marca que já não precisa tirar carta
            player.NeedsToDrawCard = false;

            // Gera um número aleatório entre 1 e 100
            // Este número determina qual carta foi sorteada
            int cardRoll = random.Next(1, 101);

            // Processa a carta conforme o tipo de espaço
            if (currentSpace.Type == SpaceType.Chance)
            {
                // Processa carta Chance
                ProcessChanceCard(player, cardRoll);
            }
            else
            {
                // Processa carta Community
                ProcessCommunityCard(player, cardRoll);
            }
        }

        // ═══════════════════════════════════════════════════════════════════════
        //                      PROCESSAR CARTA CHANCE
        // ═══════════════════════════════════════════════════════════════════════
        // Função: Processa os efeitos de uma carta Chance baseado no número sorteado
        // Parâmetros:
        //   player - jogador que tirou a carta
        //   roll - número aleatório entre 1 e 100
        private void ProcessChanceCard(Player player, int roll)
        {
            // ═══ CARTA 1: RECEBER 150 (20% de probabilidade) ═══
            if (roll <= 20)
            {
                // Adiciona 150 ao dinheiro do jogador
                player.Money += 150;
                Console.WriteLine("O jogador recebe 150.");
            }
            // ═══ CARTA 2: RECEBER 200 (10% de probabilidade) ═══
            else if (roll <= 30)
            {
                // Adiciona 200 ao dinheiro do jogador
                player.Money += 200;
                Console.WriteLine("O jogador recebe 200.");
            }
            // ═══ CARTA 3: PAGAR 70 (10% de probabilidade) ═══
            else if (roll <= 40)
            {
                // Subtrai 70 do dinheiro do jogador
                player.Money -= 70;
                // O dinheiro vai para o FreePark
                freeParkMoney += 70;
                Console.WriteLine("O jogador tem de pagar 70.");
                
                // Se o dinheiro ficou negativo, define como -1
                // (indica que o jogador perdeu)
                if (player.Money < 0)
                {
                    player.Money = -1;
                }
            }
            // ═══ CARTA 4: IR PARA START (20% de probabilidade) ═══
            else if (roll <= 60)
            {
                // Move jogador para o Start (posição 3,3)
                player.PositionX = 3;
                player.PositionY = 3;
                // Recebe 200 por passar pelo Start
                player.Money += 200;
                Console.WriteLine("O jogador move-se para a casa Start.");
            }
            // ═══ CARTA 5: IR PARA A PRISÃO (20% de probabilidade) ═══
            else if (roll <= 80)
            {
                // Move para a posição da prisão
                player.PositionX = 0;
                player.PositionY = 0;
                // Marca como preso
                player.InPrison = true;
                Console.WriteLine("O jogador move-se para a casa Police.");
            }
            // ═══ CARTA 6: IR PARA FREEPARK (20% de probabilidade) ═══
            else
            {
                // Move para o FreePark (posição 0,6)
                player.PositionX = 0;
                player.PositionY = 6;
                // Recebe todo o dinheiro do FreePark
                player.Money += freeParkMoney;
                Console.WriteLine("O jogador move-se para a casa FreePark.");
                // Reseta o dinheiro do FreePark
                freeParkMoney = 0;
            }
        }

        // ═══════════════════════════════════════════════════════════════════════
        //                    PROCESSAR CARTA COMMUNITY
        // ═══════════════════════════════════════════════════════════════════════
        // Função: Processa os efeitos de uma carta Community baseado no número sorteado
        // Parâmetros:
        //   player - jogador que tirou a carta
        //   roll - número aleatório entre 1 e 100
        private void ProcessCommunityCard(Player player, int roll)
        {
            // ═══ CARTA 1: PAGAR PELAS CASAS (10% de probabilidade) ═══
            if (roll <= 10)
            {
                // Conta o total de casas que o jogador possui
                int totalHouses = 0;
                
                // Percorre todo o tabuleiro
                for (int y = 0; y < 7; y++)
                {
                    for (int x = 0; x < 7; x++)
                    {
                        // Se o espaço pertence ao jogador
                        if (board[y, x].Owner == player)
                        {
                            // Soma as casas desse espaço
                            totalHouses += board[y, x].Houses;
                        }
                    }
                }
                
                // Calcula o pagamento: 20 por cada casa
                int payment = totalHouses * 20;
                // Subtrai do dinheiro do jogador
                player.Money -= payment;
                // O dinheiro vai para o FreePark
                freeParkMoney += payment;
                Console.WriteLine($"O jogador paga {payment} pelas casas nos seus espaços.");
                
                // Se ficou com dinheiro negativo
                if (player.Money < 0)
                {
                    player.Money = -1; // Indica que perdeu
                }
            }
            // ═══ CARTA 2: RECEBER DE OUTROS JOGADORES (10% de probabilidade) ═══
            else if (roll <= 20)
            {
                // Variável para acumular o total recebido
                int total = 0;
                
                // Para cada jogador na lista
                foreach (var p in players)
                {
                    // Se não for o próprio jogador
                    if (p != player)
                    {
                        // Outros jogadores perdem 10
                        p.Money -= 10;
                        // Soma ao total
                        total += 10;
                    }
                }
                
                // O jogador recebe o total
                player.Money += total;
                Console.WriteLine($"O jogador recebe {total} de outros jogadores.");
            }
            // ═══ CARTA 3: RECEBER 100 (20% de probabilidade) ═══
            else if (roll <= 40)
            {
                player.Money += 100;
                Console.WriteLine("O jogador recebe 100");
            }
            // ═══ CARTA 4: RECEBER 170 (20% de probabilidade) ═══
            else if (roll <= 60)
            {
                player.Money += 170;
                Console.WriteLine("O jogador recebe 170.");
            }
            // ═══ CARTA 5: PAGAR 40 (10% de probabilidade) ═══
            else if (roll <= 70)
            {
                // Subtrai 40 do jogador
                player.Money -= 40;
                // Vai para o FreePark
                freeParkMoney += 40;
                Console.WriteLine("O jogador tem de pagar 40.");
                
                // Se ficou negativo
                if (player.Money < 0)
                {
                    player.Money = -1;
                }
            }
            // ═══ CARTA 6: IR PARA PINK1 (10% de probabilidade) ═══
            else if (roll <= 80)
            {
                // Move para Pink1 (posição 0,5)
                player.PositionX = 0;
                player.PositionY = 5;
                Console.WriteLine("O jogador move-se para Pink1.");
            }
            // ═══ CARTA 7: IR PARA TEAL2 (10% de probabilidade) ═══
            else if (roll <= 90)
            {
                // Move para Teal2 (posição 4,3)
                player.PositionX = 4;
                player.PositionY = 3;
                Console.WriteLine("O jogador move-se para Teal2.");
            }
            // ═══ CARTA 8: IR PARA WHITE2 (10% de probabilidade) ═══
            else
            {
                // Move para White2 (posição 5,1)
                player.PositionX = 5;
                player.PositionY = 1;
                Console.WriteLine("O jogador move-se para White2.");
            }
        }

        // ═══════════════════════════════════════════════════════════════════════
        //                          TERMINAR TURNO
        // ═══════════════════════════════════════════════════════════════════════
        // Função: Termina o turno do jogador atual e passa para o próximo
        // Verifica se o jogador completou todas as ações obrigatórias
        // Parâmetros: playerName - nome do jogador
        public void EndTurn(string playerName)
        {
            // Procura o jogador pelo nome
            Player player = GetPlayerByName(playerName);
            
            // Se o jogador não foi encontrado
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

            // Verifica se o jogador deve jogar novamente (por ter tirado doubles)
            if (player.MustRollAgain)
            {
                Console.WriteLine("O jogador ainda tem ações a fazer.");
                return;
            }

            // ═══ RESETAR ESTADO DO JOGADOR ═══
            // Marca que ainda não lançou dados no próximo turno
            player.HasRolledDice = false;
            // Reseta flag de jogar novamente
            player.MustRollAgain = false;
            // Reseta flag de ter tirado carta
            player.HasDrawnCard = false;
            
            // ═══ PASSAR PARA O PRÓXIMO JOGADOR ═══
            // Incrementa o índice e usa módulo para voltar ao início quando chegar ao fim
            // Exemplo: se há 3 jogadores (0,1,2), após 2 volta para 0
            currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
            
            // Informa qual é o próximo jogador
            Console.WriteLine($"Turno terminado. Novo turno do jogador {players[currentPlayerIndex].Name}.");
        }

        // ═══════════════════════════════════════════════════════════════════════
        //                      MOSTRAR DETALHES DO JOGO
        // ═══════════════════════════════════════════════════════════════════════
        // Função: Mostra o estado completo do tabuleiro e informações dos jogadores
        // Exibe cada espaço com seu dono, casas e jogadores presentes
        public void ShowGameDetails()
        {
            // Percorre cada linha do tabuleiro (Y de 0 a 6)
            for (int y = 0; y < 7; y++)
            {
                // Lista para armazenar as partes de cada espaço nesta linha
                List<string> rowParts = new List<string>();
                
                // Percorre cada coluna desta linha (X de 0 a 6)
                for (int x = 0; x < 7; x++)
                {
                    // Obtém o espaço nesta posição
                    Space space = board[y, x];
                    
                    // Começa com o nome do espaço
                    string spacePart = space.Name;
                    
                    // Se o espaço tem dono
                    if (space.Owner != null)
                    {
                        // Adiciona o nome do dono entre parênteses
                        spacePart += $" ({space.Owner.Name}";
                        
                        // Se tem casas construídas
                        if (space.Houses > 0)
                        {
                            // Adiciona o número de casas
                            spacePart += $" – {space.Houses}";
                        }
                        
                        // Fecha os parênteses
                        spacePart += ")";
                    }
                    
                    // ═══ ADICIONAR JOGADORES PRESENTES NESTE ESPAÇO ═══
                    // Lista para armazenar os nomes dos jogadores aqui
                    List<string> playersHere = new List<string>();
                    
                    // Para cada jogador no jogo
                    foreach (var p in players)
                    {
                        // Se o jogador está nesta posição E não está preso
                        if (p.PositionX == x && p.PositionY == y && !p.InPrison)
                        {
                            // Adiciona o nome do jogador
                            playersHere.Add(p.Name);
                        }
                        // Se está preso E este é o espaço da prisão (0,0)
                        else if (p.InPrison && x == 0 && y == 0)
                        {
                            // Adiciona o nome do jogador preso
                            playersHere.Add(p.Name);
                        }
                    }
                    
                    // Se há jogadores neste espaço
                    if (playersHere.Count > 0)
                    {
                        // Adiciona os nomes dos jogadores separados por espaço
                        spacePart += " " + string.Join(" ", playersHere);
                    }
                    
                    // Adiciona este espaço completo à lista da linha
                    rowParts.Add(spacePart);
                }
                
                // Escreve toda a linha, com espaços separados por espaço
                Console.WriteLine(string.Join(" ", rowParts));
            }
            
            // ═══ MOSTRAR INFORMAÇÃO DO JOGADOR ATUAL ═══
            // Obtém o jogador atual
            Player currentPlayer = players[currentPlayerIndex];
            // Mostra o nome e o dinheiro do jogador atual
            Console.WriteLine($"{currentPlayer.Name} - {currentPlayer.Money}");
        }

        // ═══════════════════════════════════════════════════════════════════════
        //                          FUNÇÕES AUXILIARES
        // ═══════════════════════════════════════════════════════════════════════
        
        // Função: Gera um valor aleatório para um dado
        // Retorna: -3, -2, -1, 1, 2, ou 3 (nunca 0)
        // Funciona gerando 1-6 e convertendo:
        // 1→-1, 2→-2, 3→-3, 4→1, 5→2, 6→3
        private int GetRandomDiceValue()
        {
            // Gera número entre 1 e 6 (inclusive)
            int value = random.Next(1, 7);
            
            // Se for 1, 2 ou 3, retorna o negativo
            // Se for 4, 5 ou 6, retorna value-3 (que dá 1, 2, 3)
            return value <= 3 ? -value : value - 3;
        }

        // Função: Procura um jogador pelo nome
        // Parâmetros: name - nome do jogador a procurar
        // Retorna: O objeto Player se encontrado, ou null se não encontrado
        private Player GetPlayerByName(string name)
        {
            // FirstOrDefault retorna o primeiro jogador com este nome
            // ou null se nenhum jogador tiver este nome
            return players.FirstOrDefault(p => p.Name == name);
        }

        // Função: Procura um espaço no tabuleiro pelo nome
        // Parâmetros: name - nome do espaço a procurar
        // Retorna: O objeto Space se encontrado, ou null se não encontrado
        private Space FindSpaceByName(string name)
        {
            // Percorre todo o tabuleiro
            for (int y = 0; y < 7; y++)
            {
                for (int x = 0; x < 7; x++)
                {
                    // Se o nome do espaço corresponde
                    if (board[y, x].Name == name)
                    {
                        // Retorna este espaço
                        return board[y, x];
                    }
                }
            }
            
            // Se não encontrou, retorna null
            return null;
        }

        // Função: Obtém todos os espaços de uma determinada cor
        // Parâmetros: color - cor dos espaços a procurar
        // Retorna: Lista com todos os espaços dessa cor
        private List<Space> GetAllSpacesOfColor(string color)
        {
            // Cria uma lista vazia para armazenar os espaços
            List<Space> spaces = new List<Space>();
            
            // Percorre todo o tabuleiro
            for (int y = 0; y < 7; y++)
            {
                for (int x = 0; x < 7; x++)
                {
                    // Se a cor do espaço corresponde à cor procurada
                    if (board[y, x].Color == color)
                    {
                        // Adiciona este espaço à lista
                        spaces.Add(board[y, x]);
                    }
                }
            }
            
            // Retorna a lista com todos os espaços encontrados
            return spaces;
        }
    }
}