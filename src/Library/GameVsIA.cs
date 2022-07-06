
namespace ChatBotProject
{
  /// <summary>
  /// Esta clase se utiliza para jugar partidas de un Usuario contra un Bot.
  /// Cada GameVsIA tiene un Player(Usuario que juega la partida), un booleano InProgress(Para saber si la partida está en juego),
  /// un string Winner(El cual dirá Bot o el nombre del usuario dependiendo de el ganador de la partida), PlayerBoard(Board del usuario),
  /// BotBoard(Board del Bot), una lista de strings BotAttacks(contendrá todos los ataques del Bot, sirve para que el bot no se repita) y
  /// un telegramPrinter(encargado de imprimir el board al usuario)
  /// Esta clase es creator de Board
  /// </summary>
  public class GameVsIA
  {
    private User Player;
    private bool InProgress;
    private string Winner;
    private Board PlayerBoard;
    private Board BotBoard;
    private List<string> BotAtacks;
    private TelegramPrinter telegramPrinter;

    /// <summary>
    /// Consturctor de GameVsIA
    /// </summary>
    /// <param name="player">El usuario que juega la partida</param>
    public GameVsIA(User player)
    {
      this.Player = player;
      this.InProgress = false;
      this.PlayerBoard = new Board(10, 10); // Genera el Board del usuario
      this.BotBoard = new Board(10, 10); // Genera el Board del Bot
      this.telegramPrinter = new TelegramPrinter(); // Crea la instancia del TelegramPrinter
      this.BotAtacks = new List<string>() {}; // Setea la lista de string vacía

      // Esto va en el constructor debido a que siempre será así. Solo tenemos un usuario en este tipo de Game.
      // Por lo que siempre verá sus barcos y no los del Bot
      this.PlayerBoard.showShips(); // Habilita la visualización de barcos del tablero del usuario
      this.BotBoard.hideShips(); // Deshabilita la visualización de barcos del tablero del usuario
    }

    /// <summary>
    /// Este método se encarga de imprimir su tablero al usuario
    /// </summary>
    public void printPlayerBoard()
    {
      this.telegramPrinter.printBoard(this.PlayerBoard, this.Player.ID);
    }

    /// <summary>
    /// Este método se encarga de imprimir el tablero del bot al usuario
    /// </summary>
    public void printBotBoard()
    {
      this.telegramPrinter.printBoard(this.BotBoard, this.Player.ID);
    }
  
    /// <summary>
    /// Este método se encarga de manejar el ataque del Bot al usuario. Se genera una posición aleatoria y se ataca a el board del usuario
    /// </summary>
    public void botAttack()
    {
      bool invalidBotAttack = true;
      string attackPosition = "";

      while(invalidBotAttack) // Mientras la posición sea inválida
      {
        string headerLetters = this.PlayerBoard.getHeaderLetters().Substring(0, 9); // Agarra las primeras nueve letras del HeaderLetters
        int randomLetterIndex = new Random().Next(0, 9); // Genera un número aleatorio entre 0 y 9
        int randomRowIndex= new Random().Next(1, 10); // Genera un número aleatorio entre 1 y 10

         // Busca una letra en base al indice aleatorio
         // Interpola en un string esa letra al lado de el otro número aleatorio del 1 al 10
        attackPosition = $"{headerLetters[randomLetterIndex]}{randomRowIndex}"; // De esta manera se generaría un ataque aleatorio, por ejemplo "A10"

        invalidBotAttack = this.BotAtacks.Contains(attackPosition); // Si esa posición está contenida en la lista de ataques, sigue en el bucle
      }

      this.BotAtacks.Add(attackPosition); // Si llegó hasta aca quiere decir que la posición es nueva, por lo tanto, la agrega a la lista
  
      this.PlayerBoard.attack(attackPosition); // Ataca al boar del usuario con esa posición generada
    }

    /// <summary>
    /// Este método se encarga de atacar al board del Bot en base a un parámetro attackPosition
    /// </summary>
    /// <param name="attackPosition">Un string con la posición a atacar</param>
    public void attackBotBoard(string attackPosition)
    {
      this.BotBoard.attack(attackPosition);
    }

    /// <summary>
    /// Este método se encarga de agregar un ship al board del usuario.
    /// </summary>
    /// <param name="positions">Una lista de strings con las posiciones del barco</param>
    public void AddShipToBoard(List<string> positions)
    {
      this.PlayerBoard.addShip(positions);
    }
  
  
    /// <summary>
    /// Este método se encarga de generar los ships en el board del Bot
    /// Basicamente lo que hace es elegir entre 3 combinaciones de barcos pre-seteadas
    // Luego agrega cada barco al board del Bot
    /// </summary>
    public void generateBotShips()
    {
      List<List<List<string>>> possibleShips = new List<List<List<string>>>() {
        new List<List<string>>() {
          new List<string>() {"J1", "J2"},
          new List<string>() {"H1", "H2", "H3"},
          new List<string>() {"H10", "G10", "I10", "J10"},
          new List<string>() {"B3", "B4", "B5", "B6", "B7"}
        },
        new List<List<string>>() {
          new List<string>() {"I1", "I2"},
          new List<string>() {"C4", "C5", "C6"},
          new List<string>() {"D10", "D9", "D8", "D7"},
          new List<string>() {"J2", "I2", "H2", "G2", "F2"}
        },
        new List<List<string>>() {
          new List<string>() {"F5", "F6"},
          new List<string>() {"A1", "A2", "A3"},
          new List<string>() {"A10", "B10", "C10", "D10"},
          new List<string>() {"J8", "I8", "H8", "G8", "F8"}
        }
      };

      // Numero random que se utilizará para elegir la combinación de barcos
      int shipNumber = new Random().Next(0, 2);

      // Se iteran los barcos de esa combinación
      foreach (List<string> shipList in possibleShips[shipNumber])
      {
        // Agrega cada barco al board del bot
        this.BotBoard.addShip(shipList);
      }
    }

    /// <summary>
    /// Este método se encarga de decir si el board del usuario contiene barcos o no mediante un booleano
    /// </summary>
    /// <returns>True si el board del usuario contiene barcos o False si no</returns>
    public bool playerBoardHasShips()
    {
      return this.PlayerBoard.getShips().Count > 0;
    }

    /// <summary>
    /// Este método se encarga de decir si el board del bot contiene barcos o no mediante un booleano
    /// </summary>
    /// <returns>True si el board del bot contiene barcos o False si no</returns>
    public bool botBoardHasShips()
    {
      return this.BotBoard.getShips().Count > 0;
    }

    /// <summary>
    /// Cuando comienza la partida.
    /// Este método se encarga de cambiar el estado de la partida. Pone el atributo InProgress en true
    /// </summary>
    public void StartGame()
    {
      this.InProgress = true;
    }

    /// <summary>
    /// Cuando termina la partida.
    /// Este método se encarga de cambiar el estado de la partida. Pone el atributo InProgress en false
    /// </summary>
    public void FinishGame()
    {
      this.InProgress = false;
    }

    /// <summary>
    /// Este método es el getter del winner de la partida
    /// </summary>
    /// <returns>String con el nombre del winner</returns>
    public string getWinner()
    {
      return this.Winner;
    }

    /// <summary>
    /// Este método es el setter del winner de la partida
    /// </summary>
    /// <param name="winner">Un string con el nombre del winner</param>
    public void setWinner(string winner)
    {
      this.Winner = winner;
    }

    /// <summary>
    /// Este método es el getter del Usuario de la partida. Devuelve el this.Player
    /// </summary>
    /// <returns>Una instancia de User</returns>
    public User getPlayer()
    {
      return this.Player;
    }
  }
}
