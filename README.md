# Monopolio Variante - Projeto FP/EPL 2025-26

## 📋 Identificação do Grupo

| Nome | Número | Email |
|------|--------|-------|
| [Nome Completo 1] | [Número] | [email@universidadeeuropeia.pt] |
| [Nome Completo 2] | [Número] | [email@universidadeeuropeia.pt] |
| [Nome Completo 3] | [Número] | [email@universidadeeuropeia.pt] |
| [Nome Completo 4] | [Número] | [email@universidadeeuropeia.pt] |

---

## 🎯 Descrição do Projeto

Implementação de uma variante do jogo Monopoly em C# com tabuleiro 7x7, sistema de dados especiais (-3 a 3), movimento bidimensional e todas as mecânicas especificadas no briefing.

---

## 🏗️ Estrutura do Projeto

```
projeto/
├── src/
│   ├── Enums/
│   │   ├── SpaceType.cs      # Tipos de espaços
│   │   └── CardType.cs       # Tipos de cartas
│   ├── Models/
│   │   ├── Player.cs         # Classe jogador
│   │   └── Space.cs          # Classe espaço
│   ├── Managers/
│   │   ├── GameManager.cs    # Gerenciador de comandos
│   │   └── Game.cs           # Lógica principal do jogo
│   └── Program.cs            # Ponto de entrada
├── README.md                 # Este arquivo
└── MonopolioVariante.csproj  # Configuração do projeto
```

---

## 🚀 Como Executar

### Pré-requisitos
- .NET 8.0 SDK ou superior

### Compilar
```bash
dotnet build
```

### Executar
```bash
dotnet run
```

### Exemplo de Uso
```
RJ Alice
RJ Bob
RJ Charlie
RJ Diana
IJ Alice Bob Charlie Diana
LD Alice
DJ
CE Alice
TT Alice
```

---

## 💡 Estratégias de Implementação

### 1. Estrutura de Dados
- **Tabuleiro**: Array bidimensional 7x7 (`Space[,]`)
- **Jogadores**: Lista de objetos `Player`
- **Posições**: Coordenadas X e Y (0-6)

### 2. Sistema de Movimento
- Wrap-around implementado usando operador módulo
- Primeiro dado controla eixo X, segundo dado controla eixo Y
- Validação de doubles consecutivos para prisão

### 3. Sistema de Cartas
- Geração aleatória de 1-100 para determinar carta
- Probabilidades implementadas com intervalos de valores
- Efeitos aplicados automaticamente

### 4. Validações
- Verificação de turno correto
- Validação de ações obrigatórias (PA, TC)
- Controlo de estado do jogador

### 5. Comentários no Código
Todo o código está comentado linha por linha em português para facilitar a compreensão e manutenção.

---

## 📊 Distribuição de Tarefas

| Membro | Tarefas Principais |
|--------|-------------------|
| [Nome 1] | [Ex: Estrutura base, classes Player e Space] |
| [Nome 2] | [Ex: Sistema de movimento e dados, wrap-around] |
| [Nome 3] | [Ex: Sistema de cartas, lógica de compras] |
| [Nome 4] | [Ex: Validações, testes, documentação] |

**Nota:** Todos os membros participaram em todas as fases do projeto, com estas sendo as áreas de maior foco individual.

---

## 🧪 Testes Realizados

- ✅ Todos os comandos (RJ, LJ, IJ, LD, CE, DJ, TT, PA, CC, TC)
- ✅ Sistema de wrap-around
- ✅ Sistema de prisão (doubles, 3 turnos)
- ✅ Cartas Chance e Community
- ✅ Cálculo de alugueres
- ✅ Sistema FreePark
- ✅ Validações de turno e ações

---

## 📚 Funcionalidades Implementadas

### Comandos
- [x] RJ - Registar Jogador
- [x] LJ - Listar Jogadores  
- [x] IJ - Iniciar Jogo
- [x] LD - Lançar Dados
- [x] CE - Comprar Espaço
- [x] DJ - Detalhes do Jogo
- [x] TT - Terminar Turno
- [x] PA - Pagar Aluguer
- [x] CC - Comprar Casa
- [x] TC - Tirar Carta

### Mecânicas
- [x] Tabuleiro 7x7
- [x] Dados -3 a 3 (sem 0)
- [x] Movimento bidimensional
- [x] Wrap-around
- [x] Sistema de prisão
- [x] Doubles consecutivos
- [x] Cartas Chance (6 tipos)
- [x] Cartas Community (8 tipos)
- [x] Sistema de alugueres
- [x] Compra de casas (máx 4)
- [x] FreePark
- [x] Sistema de estatísticas

---

## 🐛 Problemas Conhecidos e Soluções

### [Se houver algum problema conhecido, descrever aqui]
Exemplo:
- **Problema:** [Descrição]
- **Solução:** [Como foi resolvido]

---

## 📖 Decisões de Design

### 1. Organização em Múltiplos Arquivos
Optámos por dividir o código em arquivos separados por responsabilidade (Enums, Models, Managers) para melhor organização e manutenibilidade.

### 2. Comentários Extensivos
Todo o código foi comentado linha por linha para facilitar a compreensão e revisão.

### 3. Validações Rigorosas
Implementámos validações em todas as operações para garantir que o jogo segue exatamente as regras especificadas.

### 4. Gerador de Números Aleatórios
Usámos `Random` para gerar valores de dados e cartas, garantindo variabilidade no jogo.

---

## 📝 Notas Adicionais

[Espaço para quaisquer comentários adicionais, dificuldades encontradas, aprendizagens, etc.]

---

## 🎓 Unidades Curriculares

- **Fundamentos da Programação**: Implementação em C#, estruturas de dados, algoritmos
- **Estruturação do Pensamento Lógico**: Estrutura lógica do programa, algoritmos

---

## 📅 Informações do Projeto

- **Ano Letivo:** 2025-2026
- **Data de Entrega:** 12/12/2025
- **Docentes:**
  - Anastasiya Zyenina
  - Andreia Artifice
  - Nathan Campos

---

**Nota:** Este projeto foi desenvolvido cumprindo rigorosamente todas as especificações do briefing fornecido.