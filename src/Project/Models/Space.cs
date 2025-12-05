// Importa o namespace dos enums para usar SpaceType
using MonopolioVariante.Enums;

// Define o namespace para as classes de modelo (dados do jogo)
namespace MonopolioVariante.Models
{
    // Classe que representa um espaço/casa no tabuleiro do jogo
    // Cada posição do tabuleiro 7x7 é um objeto Space
    public class Space
    {
        // Nome do espaço (ex: "Green1", "Train2", "Police")
        public string Name { get; set; }
        
        // Tipo do espaço (Property, Train, Chance, etc)
        public SpaceType Type { get; set; }
        
        // Preço para comprar o espaço (0 se não for comprável)
        public int Price { get; set; }
        
        // Cor do espaço (ex: "Green", "Blue", "Orange")
        // Vazio se não tiver cor (Trains, Utilities, etc)
        public string Color { get; set; }
        
        // Jogador que é dono deste espaço (null se ninguém é dono)
        public Player Owner { get; set; }
        
        // Número de casas construídas neste espaço (0 a 4)
        public int Houses { get; set; }

        // Construtor: cria um novo espaço com os dados fornecidos
        // name: nome do espaço
        // type: tipo do espaço (Property, Train, etc)
        // price: preço (padrão 0)
        // color: cor do espaço (padrão vazio)
        public Space(string name, SpaceType type, int price = 0, string color = "")
        {
            Name = name;           // Define o nome
            Type = type;           // Define o tipo
            Price = price;         // Define o preço
            Color = color;         // Define a cor
            Owner = null;          // Inicialmente sem dono
            Houses = 0;            // Inicialmente sem casas
        }

        // Verifica se este espaço pode ser comprado por um jogador
        // Retorna true se for Property, Train, Utility ou LuxTax
        // Retorna false para espaços como Prison, Police, Start, etc
        public bool CanBePurchased()
        {
            // Verifica se o tipo está na lista de tipos compráveis
            return Type == SpaceType.Property || 
                   Type == SpaceType.Train || 
                   Type == SpaceType.Utility || 
                   Type == SpaceType.LuxTax;
        }
    }
}