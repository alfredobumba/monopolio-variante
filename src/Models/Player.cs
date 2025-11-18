// Define o namespace para as classes de modelo
namespace MonopolioVariante.Models
{
    // Classe que representa um jogador no jogo
    // Cada jogador tem dinheiro, posição, status de prisão, estatísticas, etc
    public class Player
    {
        // Nome do jogador (ex: "Alice", "Bob")
        public string Name { get; set; }
        
        // Quantidade de dinheiro que o jogador tem
        // Começa com 600, pode ficar negativo (-1 quando não pode pagar)
        public int Money { get; set; }
        
        // Posição X no tabuleiro (coluna de 0 a 6)
        public int PositionX { get; set; }
        
        // Posição Y no tabuleiro (linha de 0 a 6)
        public int PositionY { get; set; }
        
        // Se o jogador está preso (true) ou não (false)
        public bool InPrison { get; set; }
        
        // Quantos turnos o jogador já ficou preso (0 a 3)
        // Após 3 turnos, sai automaticamente
        public int TurnsInPrison { get; set; }
        
        // Se o jogador já lançou os dados neste turno
        // true = já lançou, false = ainda não lançou
        public bool HasRolledDice { get; set; }
        
        // Se o jogador precisa pagar aluguer neste turno
        // true = precisa pagar, false = não precisa
        public bool NeedsToPayRent { get; set; }
        
        // Se o jogador precisa tirar uma carta neste turno
        // true = precisa tirar, false = não precisa
        public bool NeedsToDrawCard { get; set; }
        
        // Quantos doubles (dados iguais) o jogador tirou consecutivamente
        // Se tirar 2 doubles seguidos, vai preso
        public int ConsecutiveDoubles { get; set; }
        
        // Se o jogador deve lançar os dados novamente (tirou double)
        // true = deve jogar de novo, false = não deve
        public bool MustRollAgain { get; set; }
        
        // Se o jogador já tirou uma carta neste turno
        // true = já tirou, false = ainda não tirou
        public bool HasDrawnCard { get; set; }
        
        // --- ESTATÍSTICAS DO JOGADOR (para múltiplos jogos) ---
        
        // Total de jogos que o jogador participou
        public int GamesPlayed { get; set; }
        
        // Total de vitórias do jogador
        public int Wins { get; set; }
        
        // Total de empates do jogador
        public int Draws { get; set; }
        
        // Total de derrotas do jogador
        public int Losses { get; set; }

        // Construtor: cria um novo jogador com valores iniciais
        // name: nome do jogador
        public Player(string name)
        {
            Name = name;                    // Define o nome
            Money = 600;                    // Começa com 600 de dinheiro
            PositionX = 3;                  // Posição inicial X (Start)
            PositionY = 3;                  // Posição inicial Y (Start)
            InPrison = false;               // Não está preso
            TurnsInPrison = 0;              // Zero turnos na prisão
            HasRolledDice = false;          // Ainda não jogou os dados
            NeedsToPayRent = false;         // Não precisa pagar aluguer
            NeedsToDrawCard = false;        // Não precisa tirar carta
            ConsecutiveDoubles = 0;         // Zero doubles consecutivos
            MustRollAgain = false;          // Não precisa jogar de novo
            HasDrawnCard = false;           // Não tirou carta ainda
            GamesPlayed = 0;                // Zero jogos jogados
            Wins = 0;                       // Zero vitórias
            Draws = 0;                      // Zero empates
            Losses = 0;                     // Zero derrotas
        }

        // Reseta o jogador para começar um novo jogo
        // Mantém as estatísticas (Wins, Losses, etc) mas reseta posição e dinheiro
        public void ResetForNewGame()
        {
            Money = 600;                    // Volta para 600 de dinheiro
            PositionX = 3;                  // Volta para posição inicial X
            PositionY = 3;                  // Volta para posição inicial Y
            InPrison = false;               // Não está mais preso
            TurnsInPrison = 0;              // Zero turnos na prisão
            HasRolledDice = false;          // Ainda não jogou os dados
            NeedsToPayRent = false;         // Não precisa pagar aluguer
            NeedsToDrawCard = false;        // Não precisa tirar carta
            ConsecutiveDoubles = 0;         // Zero doubles consecutivos
            MustRollAgain = false;          // Não precisa jogar de novo
            HasDrawnCard = false;           // Não tirou carta ainda
            // Nota: GamesPlayed, Wins, Draws, Losses NÃO são resetados
        }
    }
}