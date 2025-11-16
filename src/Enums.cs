namespace MonopolioVariante
{
    // Enumeração que representa os diferentes tipos de espaços (casas) do tabuleiro
    public enum SpaceType
    {
        Property,      // Casa de propriedade que pode ser comprada pelos jogadores
        Train,         // Estação de trem, geralmente gera aluguel quando outros jogadores passam
        Utility,       // Empresa de serviços públicos (como água ou eletricidade)
        Chance,        // Casa de sorte: jogador tira uma carta de "Chance"
        Community,     // Casa de comunidade: jogador tira uma carta de "Comunidade"
        Prison,        // Casa da prisão: jogador vai ou apenas visita
        Police,        // Casa de polícia: efeito especial ou apenas visita
        FreePark,      // Parque grátis: espaço neutro, sem efeito financeiro
        BackToStart,   // Volta ao início: jogador retorna para a posição inicial e possivelmente recebe dinheiro
        LuxTax,        // Imposto de luxo: jogador paga taxa ao banco
        Start          // Ponto inicial do tabuleiro, onde jogadores recebem dinheiro ao passar
    }

    // Enumeração que representa os tipos de cartas que o jogador pode tirar
    public enum CardType
    {
        Chance,        // Carta de sorte com efeito positivo ou negativo
        Community      // Carta de comunidade com efeitos variados
    }
}