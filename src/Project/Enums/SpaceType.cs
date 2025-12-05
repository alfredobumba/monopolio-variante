// Define o namespace (agrupamento) para os tipos enumerados do jogo
namespace MonopolioVariante.Enums
{
    // Enumeração que define todos os tipos de espaços possíveis no tabuleiro
    // Cada espaço do tabuleiro tem um destes tipos
    public enum SpaceType
    {
        Property,    // Propriedade normal (casas coloridas que podem ser compradas)
        Train,       // Estação de trem (Train1, Train2, etc)
        Utility,     // Utilidades (WaterWorks, ElectricCompany)
        Chance,      // Casa Sorte (tira carta de Chance)
        Community,   // Casa Comunidade (tira carta de Community)
        Prison,      // Prisão (apenas visitando)
        Police,      // Polícia (vai para a prisão)
        FreePark,    // Estacionamento grátis (recebe dinheiro acumulado)
        BackToStart, // Volta para o Start
        LuxTax,      // Imposto de luxo (espaço especial)
        Start        // Casa inicial
    }
}