namespace MonopolioVariante
{
    // Classe que representa um espaço (casa) no tabuleiro do jogo
    public class Space
    {
        // Nome do espaço, ex: "Avenida Atlântica"
        public string Name { get; set; }

        // Tipo do espaço, utilizando a enum SpaceType
        public SpaceType Type { get; set; }

        // Preço de compra do espaço (se aplicável)
        public int Price { get; set; }

        // Cor do espaço (usada para propriedades agrupadas por cor)
        public string Color { get; set; }

        // Jogador que atualmente possui o espaço (null se ninguém possui)
        public Player Owner { get; set; }

        // Número de casas construídas neste espaço (para propriedades)
        public int Houses { get; set; }

        // Construtor da classe Space
        // Permite definir nome, tipo, preço e cor. Owner inicia como null e Houses como 0
        public Space(string name, SpaceType type, int price = 0, string color = "")
        {
            Name = name;
            Type = type;
            Price = price;
            Color = color;
            Owner = null;   // Inicialmente, o espaço não tem dono
            Houses = 0;     // Inicialmente, não há casas construídas
        }

        // Método que verifica se o espaço pode ser comprado
        public bool CanBePurchased()
        {
            // Retorna true se o espaço for uma propriedade, estação de trem, utilidade ou imposto de luxo
            // (Note que normalmente LuxTax não é comprável no Monopólio padrão, mas aqui foi incluído)
            return Type == SpaceType.Property || Type == SpaceType.Train || 
                   Type == SpaceType.Utility || Type == SpaceType.LuxTax;
        }
    }
}
