# Monopólio Variante - Documentação do Projeto

## 📋 Descrição

Este projeto implementa uma variante do jogo Monopólio em C# (.NET 8), utilizando um tabuleiro expandido de 7x7 (49 casas) com mecânicas de movimento baseadas em coordenadas bidimensionais.

## 🎯 Objetivos

- Implementar todas as regras do jogo conforme especificação
- Garantir comportamento determinístico e previsível
- Validar entrada/saída com rigor absoluto
- Suportar até 4 jogadores simultâneos

## 🏗️ Estrutura do Projeto

```
monopolio-project/
├── src/
│   ├── Program.cs          # Ponto de entrada do programa
│   ├── GameManager.cs      # Gerenciamento de jogadores e comandos
│   ├── Game.cs             # Lógica principal do jogo
│   ├── Player.cs           # Classe de jogador
│   ├── Space.cs            # Classe de espaço do tabuleiro
│   └── Enums.cs            # Enumerações (SpaceType, CardType)
├── MonopolioVariante.csproj # Arquivo de projeto .NET
├── Monopolio_Fluxograma    # Fluxograma Flowgorithm
└── README.md               # Esta documentação
```

## 🎮 Características do Jogo

### Tabuleiro 7x7

O tabuleiro possui 49 espaços distribuídos em uma grade 7x7, com o espaço inicial (Start) localizado no centro (posição 3,3).

#### Distribuição dos Espaços:

**Linha 0 (Topo):**
- Prison | Green3 | Violet1 | Train2 | Red3 | White1 | BackToStart

**Linha 1:**
- Blue3 | Community | Red2 | Violet2 | WaterWorks | Chance | White2

**Linha 2:**
- Blue2 | Red1 | Chance | Brown2 | Community | Black1 | LuxTax

**Linha 3 (Centro):**
- Train1 | Green2 | Teal1 | **Start** | Teal2 | Black2 | Train3

**Linha 4:**
- Blue1 | Green1 | Community | Brown1 | Chance | Black3 | White3

**Linha 5:**
- Pink1 | Chance | Orange1 | Orange2 | Orange3 | Community | Yellow3

**Linha 6 (Fundo):**
- FreePark | Pink2 | ElectricCompany | Train4 | Yellow1 | Yellow2 | Police

### Sistema de Dados

- **Dois dados especiais**: cada um com valores de -3 a 3 (excluindo 0)
- **Primeiro dado**: movimento horizontal (negativo = esquerda, positivo = direita)
- **Segundo dado**: movimento vertical (negativo = baixo, positivo = cima)
- **Wrap-around**: quando o movimento sai do tabuleiro, continua do lado oposto

#### Exemplos de Movimento:
- Dados 2/-1 no Start → Black3
- Dados -1/-3 no Start → ElectricCompany
- Dados 3/-3 no White2 → Community (com wrap-around)

### Regras Especiais

1. **Doubles (mesmo valor nos dois dados)**:
   - Permite jogar novamente após completar todas as ações
   - Dois doubles consecutivos = prisão automática

2. **Prisão**:
   - Liberdade ao tirar doubles ou após 3 turnos
   - Posição: espaço Prison (0,0)

3. **Espaço Start**:
   - Recebe 200 ao terminar movimento aqui

4. **FreePark**:
   - Acumula valores de taxas e penalidades
   - Jogador recebe tudo ao parar aqui

5. **LuxTax**:
   - Valor pago vai para FreePark

## 💻 Comandos Implementados

### 1. RJ - Registar Jogador
```
Entrada: RJ NomeJogador
Sucesso: Jogador registado com sucesso.
Erro: Jogador existente.
```

### 2. LJ - Listar Jogadores
```
Entrada: LJ
Sucesso: Lista ordenada por vitórias (decrescente) e alfabeticamente
Formato: NomeJogador NumJogos NumVitórias NumEmpates NumDerrotas
Erro: Sem jogadores registados.
```

### 3. IJ - Iniciar Jogo
```
Entrada: IJ NomeJogador1 NomeJogador2 NomeJogador3 NomeJogador4
Sucesso: Jogo iniciado com sucesso.
Erros: 
  - Existe um jogo em curso.
  - Jogador inexistente.
```

### 4. LD - Lançar Dados
```
Entrada: LD NomeJogador
Saídas possíveis:
  - Saiu X/Y – espaço NomeEspaço. Espaço sem dono.
  - Saiu X/Y – espaço NomeEspaço. Espaço já comprada.
  - Saiu X/Y – espaço NomeEspaço. Espaço já comprada por outro jogador. Necessário pagar renda.
  - Saiu X/Y – espaço NomeEspaço. Espaço especial. Tirar carta.
  - Saiu X/Y - Espaço BackToStart. Peça colocada no espaço Start.
  - Saiu X/Y – espaço Police. Jogador preso.
  - Saiu X/Y – espaço Prison. Jogador só de passagem.
  - Saiu X/Y – espaço FreePark. Jogador recebe [valor].
```

### 5. CE - Comprar Espaço
```
Entrada: CE NomeJogador
Sucesso: Espaço comprado.
Erros:
  - O espaço já se encontra comprado.
  - Este espaço não está para venda.
  - O jogador não tem dinheiro suficiente para adquirir o espaço.
```

#### Preços dos Espaços:
| Espaço | Preço | Espaço | Preço |
|--------|-------|--------|-------|
| Brown1/2 | 100/120 | Red1/2/3 | 130/130/160 |
| Teal1/2 | 90/130 | Green1/2/3 | 120/140/160 |
| Orange1/2/3 | 120/120/140 | Blue1/2/3 | 140/140/170 |
| Black1/2/3 | 110/120/130 | Pink1/2 | 160/180 |
| White1/2/3 | 160/180/190 | Yellow1/2/3 | 140/140/170 |
| Violet1/2 | 150/130 | Train1/2/3/4 | 150 cada |
| Electric/Water | 120 cada | LuxTax | 80 |

### 6. DJ - Detalhes de Jogo
```
Entrada: DJ
Sucesso: Mostra tabuleiro completo com:
  - Propriedades e donos
  - Número de casas construídas
  - Posição de todos os jogadores
  - Dinheiro do jogador atual
```

### 7. TT - Terminar Turno
```
Entrada: TT NomeJogador
Sucesso: Turno terminado. Novo turno do jogador [próximo].
Erros:
  - Não é o turno do jogador indicado.
  - O jogador ainda tem ações a fazer.
  - Não existe jogo em curso.
```

### 8. PA - Pagar Aluguer
```
Entrada: PA NomeJogador
Sucesso: Aluguer pago.
Cálculo: PreçoEspaço * 0.25 + PreçoEspaço * 0.75 * NúmeroCasas
Erros:
  - Não é necessário pagar aluguer
  - O jogador não tem dinheiro suficiente.
```

### 9. CC - Comprar Casa
```
Entrada: CC NomeJogador NomeEspaço
Sucesso: Casa adquirida.
Preço: PreçoEspaço * 0.6
Limite: 4 casas por espaço
Requisito: Possuir todos os espaços da mesma cor
Erros:
  - Não é possível comprar casa no espaço indicado.
  - O jogador não possui todos os espaços da cor
  - O jogador não possui dinheiro suficiente.
```

### 10. TC - Tirar Carta
```
Entrada: TC NomeJogador
Sucesso: Mensagem da carta sorteada
Erros:
  - Não é possível tirar carta neste espaço.
  - A carta já foi tirada.
```

#### Cartas Chance:
- 20%: Recebe 150
- 10%: Recebe 200
- 10%: Paga 70
- 20%: Move-se para Start
- 20%: Move-se para Police (prisão)
- 20%: Move-se para FreePark

#### Cartas Community:
- 10%: Paga 20 por cada casa
- 10%: Recebe 10 de cada jogador
- 20%: Recebe 100
- 20%: Recebe 170
- 10%: Paga 40
- 10%: Move-se para Pink1
- 10%: Move-se para Teal2
- 10%: Move-se para White2

## 🔧 Compilação e Execução

### Pré-requisitos
- .NET 8.0 SDK ou superior
- Visual Studio Code (opcional)

### Compilar o Projeto
```bash
cd monopolio-project
dotnet build
```

### Executar o Projeto
```bash
dotnet run --project MonopolioVariante.csproj
```

### Teste Manual
```bash
# Exemplo de sequência de comandos
RJ Alice
RJ Bob
RJ Carol
RJ Dave
LJ
IJ Alice Bob Carol Dave
DJ
LD Alice
CE Alice
TT Alice
```

## 🏛️ Arquitetura do Código

### Estrutura Modular

O código está organizado em arquivos separados para melhor manutenibilidade:

#### 1. **Program.cs**
- Ponto de entrada da aplicação
- Gerencia o loop principal de entrada de comandos
- Mantém a aplicação rodando até linha em branco

#### 2. **Enums.cs**
- **SpaceType**: Define tipos de espaços (Property, Train, Utility, etc.)
- **CardType**: Define tipos de cartas (Chance, Community)

#### 3. **Space.cs**
- Representa um espaço no tabuleiro
- Armazena: nome, tipo, preço, cor, dono e casas
- Método: `CanBePurchased()` para verificar se pode ser comprado

#### 4. **Player.cs**
- Representa um jogador
- Armazena estado: dinheiro, posição, flags de ações
- Armazena estatísticas: jogos, vitórias, empates, derrotas
- Método: `ResetForNewGame()` para reiniciar estado

#### 5. **GameManager.cs**
- Gerencia jogadores registrados
- Processa e valida comandos
- Controla estado global do jogo
- Inicializa novos jogos

#### 6. **Game.cs**
- Implementa lógica do jogo em curso
- Gerencia tabuleiro 7×7 e turnos
- Processa movimentos e ações
- Implementa todas as regras do jogo

### Princípios de Design

- **Separação de Responsabilidades**: Cada classe tem uma função específica
- **Encapsulamento**: Estado interno protegido e acessado por propriedades
- **Modularidade**: Código dividido em arquivos lógicos e independentes
- **Manutenibilidade**: Fácil de entender, modificar e estender

## 🎲 Algoritmos Importantes

### Movimento com Wrap-Around
```csharp
int newX = player.PositionX + dice1;
int newY = player.PositionY + dice2;

while (newX < 0) newX += 7;
while (newX >= 7) newX -= 7;
while (newY < 0) newY += 7;
while (newY >= 7) newY -= 7;
```

### Geração de Dados
```csharp
// Gera valores de -3 a 3, excluindo 0
int value = random.Next(1, 7);
return value <= 3 ? -value : value - 3;
```

### Cálculo de Aluguer
```csharp
int rent = (int)(space.Price * 0.25 + space.Price * 0.75 * space.Houses);
```

### Validação de Turno
```csharp
// Jogador deve:
// 1. Ter lançado os dados pelo menos uma vez
// 2. Não ter ações pendentes (pagar aluguer ou tirar carta)
// 3. Não precisar jogar novamente (doubles)
```

## 🔍 Validações Implementadas

### Ordem de Verificação de Erros
Conforme especificação, apenas a **primeira** mensagem de erro é exibida:

1. Jogo em curso/inexistente
2. Jogador inexistente/não participa
3. Vez do jogador
4. Condições específicas do comando

### Validação de Comandos
- Número correto de parâmetros
- Nomes de comandos válidos
- Estado do jogo apropriado

## 📊 Fluxograma

O fluxograma completo está disponível em (formato Flowgorithm), ilustrando:

- Loop principal do programa
- Processamento de comandos
- Lógica de lançamento de dados
- Sistema de compra de propriedades
- Mecânica de cartas
- Validações e fluxos de erro

## 🧪 Casos de Teste

### Exemplo 1: Jogo Básico
```
RJ Alice
RJ Bob
IJ Alice Bob Alice Bob
LJ
DJ
```

### Exemplo 2: Movimento e Compra
```
RJ Player1
RJ Player2
IJ Player1 Player2 Player1 Player2
LD Player1
CE Player1
TT Player1
```

### Exemplo 3: Wrap-Around
```
# Jogador no White2 (5,1), dados 3/-3
# Posição final: (1,5) = Community
```

## 🚫 Restrições e Limitações

- Não são utilizadas bibliotecas externas
- Apenas .NET 8 standard library
- Entrada via console (stdin)
- Saída via console (stdout)
- Sem interface gráfica
- Sem persistência de dados

## 📝 Notas de Implementação

### Decisões de Design

1. **Estrutura Modular**: Separação clara entre gerenciamento de jogadores, jogo e comandos
2. **Validação Rigorosa**: Todas as condições de erro são verificadas na ordem especificada
3. **Estado Explícito**: Flags claras para ações obrigatórias (NeedsToPayRent, NeedsToDrawCard)
4. **Determinismo**: Mesmo com aleatoriedade, o comportamento é previsível

### Complexidade

- **Temporal**: O(1) para maioria das operações
- **Espacial**: O(n) onde n é o número de jogadores e espaços

## 🔮 Possíveis Extensões (Não Implementadas)

As seguintes funcionalidades **não** foram implementadas pois não estavam no enunciado:

- Sistema de save/load
- Interface gráfica
- Modo multiplayer em rede
- IA para jogadores
- Estatísticas avançadas
- Animações

## 📄 Licença

Projeto acadêmico - Universidade Europeia-IADE.

## 👥 Autor: 
**Alfredo Bumba - 20221435**
**Marcio Nhanga - 20252075**
**Tiago Pascoal - 20252041**
**José Luemba   - 20251276**


Desenvolvido como projeto de Fundamento de programação e Estruturação do Pensamento Lógico, feito em C#.

---

**Data de Criação**:2025  
**Versão**: 1.0  
**Framework**: .NET 8.0  
**Linguagem**: C#