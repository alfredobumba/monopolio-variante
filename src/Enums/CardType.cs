// Define o namespace para os tipos enumerados
namespace MonopolioVariante.Enums
{
    // Enumeração que define os tipos de cartas que podem ser tiradas
    // Quando um jogador cai em Chance ou Community, tira uma carta deste tipo
    public enum CardType
    {
        Chance,    // Carta de Sorte (20% recebe 150, 10% recebe 200, etc)
        Community  // Carta de Comunidade (10% paga casas, 10% recebe de outros, etc)
    }
}