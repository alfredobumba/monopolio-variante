namespace MonopolioVariante
{
    // Classe que representa um jogador no jogo
    public class Player
    {
        // Nome do jogador
        public string Name { get; set; }

        // Quantidade de dinheiro que o jogador possui
        public int Money { get; set; }

        // Posição X do jogador no tabuleiro (para controle gráfico ou lógico)
        public int PositionX { get; set; }

        // Posição Y do jogador no tabuleiro
        public int PositionY { get; set; }

        // Indica se o jogador está na prisão
        public bool InPrison { get; set; }

        // Contador de turnos que o jogador permanece na prisão
        public int TurnsInPrison { get; set; }

        // Indica se o jogador já rolou os dados neste turno
        public bool HasRolledDice { get; set; }

        // Indica se o jogador precisa pagar aluguel ao cair em propriedade de outro jogador
        public bool NeedsToPayRent { get; set; }

        // Indica se o jogador precisa tirar uma carta (Chance ou Comunidade)
        public bool NeedsToDrawCard { get; set; }

        // Contador de doubles consecutivos lançados pelo jogador
        public int ConsecutiveDoubles { get; set; }

        // Indica se o jogador deve rolar os dados novamente (por doubles)
        public bool MustRollAgain { get; set; }

        // Indica se o jogador já tirou uma carta neste turno
        public bool HasDrawnCard { get; set; }
        
        // Estatísticas do jogador
        public int GamesPlayed { get; set; }  // Total de jogos jogados
        public int Wins { get; set; }         // Total de vitórias
        public int Draws { get; set; }        // Total de empates
        public int Losses { get; set; }       // Total de derrotas

        // Construtor do jogador
        // Inicializa o jogador com valores padrão para um novo jogo
        public Player(string name)
        {
            Name = name;
            Money = 600;               // Quantia inicial de dinheiro
            PositionX = 3;             // Posição inicial X no tabuleiro
            PositionY = 3;             // Posição inicial Y no tabuleiro
            InPrison = false;          // Jogador começa fora da prisão
            TurnsInPrison = 0;         // Nenhum turno na prisão
            HasRolledDice = false;     // Ainda não rolou os dados
            NeedsToPayRent = false;    // Não precisa pagar aluguel
            NeedsToDrawCard = false;   // Não precisa tirar carta
            ConsecutiveDoubles = 0;    // Nenhum double consecutivo
            MustRollAgain = false;     // Não precisa rolar de novo
            HasDrawnCard = false;      // Não tirou carta ainda
            GamesPlayed = 0;           // Estatísticas começam zeradas
            Wins = 0;
            Draws = 0;
            Losses = 0;
        }

        // Método para resetar o jogador para um novo jogo
        // Mantém as estatísticas, mas reinicia posições, dinheiro e estados do turno
        public void ResetForNewGame()
        {
            Money = 600;
            PositionX = 3;
            PositionY = 3;
            InPrison = false;
            TurnsInPrison = 0;
            HasRolledDice = false;
            NeedsToPayRent = false;
            NeedsToDrawCard = false;
            ConsecutiveDoubles = 0;
            MustRollAgain = false;
            HasDrawnCard = false;
        }
    }
}
