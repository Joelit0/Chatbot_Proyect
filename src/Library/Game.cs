namespace ChatBotProject
{
  /// <summary>
  /// Esta clase se utiliza para jugar partidas de un Usuario contra otro usuario.
  /// Cada Game tiene una lista de usuarios(InMatchUsers), un Time(Timer), un TimePerRound(Timer)
  /// un InProgress(bool), un Winner(string), un InMatchUsersBoard(Dictionary) contiene un diccionario
  /// con clave User y valor Board, telegramPrinter(TelegramPrinter).
  /// Esta clase es creator de Board y de Timer.
  /// </summary>
  public class Game
  {
    private List<User> InMatchUsers;
    private Timer Time;
    private Timer TimePerRound;
    private bool InProgress;
    private string Winner;
    private Dictionary<User, Board> InMatchUsersBoard;
    private TelegramPrinter telegramPrinter;

    /// <summary>
    /// Consturctor de Game
    /// </summary>
    /// <param name="InMatchUsers">Una lista con instancias de User</param>
    /// <param name="totalMins">Un int que refiere a los minutos totales de la partida</param>
    /// <param name="totalSecs">Un int que refiere a los segundos totales de la partida</param>
    /// <param name="minsPerRound">Un int que refiere a los minutos totales de cada ronda</param>
    /// <param name="secsPerRound">Un int que refiere a los segundos totales de cada ronda</param>

    public Game(List<User> InMatchUsers, int totalMins, int totalSecs, int minsPerRound, int secsPerRound)
    {
      // Si los users son 2
      if (InMatchUsers.Count() == 2)
      {
        this.InMatchUsers = InMatchUsers;
        this.InProgress = false;
        this.InMatchUsersBoard = new Dictionary<User, Board>();
        this.Time = new Timer(totalMins, totalSecs); // Creo el timer de la partida
        this.TimePerRound = new Timer(minsPerRound, secsPerRound); // Creo el timer por ronda
        this.telegramPrinter = new TelegramPrinter();

        // Llenar el diccionario con clave User y Valor Board
        foreach(User user in this.InMatchUsers)
        {
          this.InMatchUsersBoard.Add(user, new Board(10, 10));
        }
      }
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
    /// Este método se encarga de imprimir el board del player a si mismo
    /// </summary>
    public void PrintPlayerBoardToSelf()
    {
      Board firstUserBoard = getUserBoard(this.InMatchUsers[0]); // Obtiene el board del Player
      firstUserBoard.showShips(); // Hablita la visualización de los barcos del board del Player
      this.telegramPrinter.printBoard(firstUserBoard, this.InMatchUsers[0].ID); // Le manda un mensaje al player con su board
    }

    /// <summary>
    /// Este método se encarga de imprimir el board del player al rival player
    /// </summary>
    public void PrintPlayerBoardToEnemy()
    {
      Board firstUserBoard = getUserBoard(this.InMatchUsers[0]); // Obtiene el board del player
      firstUserBoard.hideShips(); // Deshablita la visualización de los barcos del board del Player
      this.telegramPrinter.printBoard(firstUserBoard, this.InMatchUsers[1].ID); // Le manda un mensaje al rival player con el board del player
    }

    /// <summary>
    /// Este método se encarga de agregar un ship al board del player
    /// </summary>
    /// <param name="positions">Una lista con strings de las posiciones del barco a agregar</param>
    public void PlayerAddShipToBoard(List<string> positions)
    {
      Board firstUserBoard = getUserBoard(this.InMatchUsers[0]); // Obtiene el board del player
      firstUserBoard.addShip(positions); // Agrega el ship al board
    }

    /// <summary>
    /// Este método se encarga de, mediante un booleano, decir si el board del player contiene barcos o no
    /// </summary>
    /// <returns>True si contiene o False si no</returns>
    public bool PlayerBoardHasShips()
    {
      Board firstUserBoard = getUserBoard(this.InMatchUsers[0]);
      return firstUserBoard.getShips().Count != 0;
    }

    /// <summary>
    /// Este método se encarga de atacar al board del rival player
    /// </summary>
    /// <param name="attackPosition">Un string con la posición a atacar</param>
    public void AttackRivalPlayerBoard(string attackPosition)
    {
      Board secondUserBoard = getUserBoard(this.InMatchUsers[1]); // Obtiene el board del rival player
      secondUserBoard.attack(attackPosition); // Ataca al board del rival player con la posición que llegó por parámetro
    }

    /// <summary>
    /// Este método se encarga de imprimir el board del rival player a si mismo
    /// </summary>
    public void PrintRivalPlayerBoardToSelf()
    {
      Board secondUserBoard = getUserBoard(this.InMatchUsers[1]);
      secondUserBoard.showShips();
      this.telegramPrinter.printBoard(secondUserBoard, this.InMatchUsers[1].ID);
    }

    /// <summary>
    /// Este método se encarga de imprimir el board del rival player al player
    /// </summary>
    public void PrintRivalPlayerBoardToEnemy()
    {
      Board secondUserBoard = getUserBoard(this.InMatchUsers[1]);
      secondUserBoard.hideShips();
      this.telegramPrinter.printBoard(secondUserBoard, this.InMatchUsers[0].ID);
    }

    /// <summary>
    /// Este método se encarga de agregar un ship al board del rival player
    /// </summary>
    /// <param name="positions">Una lista con strings de las posiciones del barco a agregar</param>
    public void RivalPlayerAddShipToBoard(List<string> positions)
    {
      Board secondUserBoard = getUserBoard(this.InMatchUsers[1]);
      secondUserBoard.addShip(positions);
    }

    /// <summary>
    /// Este método se encarga de, mediante un booleano, decir si el board del rival player contiene barcos o no
    /// </summary>
    /// <returns>True si contiene o False si no</returns>
    public bool RivalPlayerBoardHasShips()
    {
      Board secondUserBoard = getUserBoard(this.InMatchUsers[1]);
      return secondUserBoard.getShips().Count != 0;
    }

    /// <summary>
    /// Este método se encarga de atacar al board del player
    /// </summary>
    /// <param name="attackPosition">Un string con la posición a atacar</param>
    public void AttackPlayerBoard(string attackPosition)
    {
      
      Board firstUserBoard = getUserBoard(this.InMatchUsers[0]);
      firstUserBoard.attack(attackPosition);
      Console.WriteLine($"{attackPosition}");
      foreach (Ship ship in firstUserBoard.getShips())
      {
        Console.WriteLine(ship.getLarge());
      }
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
      foreach( User player in this.InMatchUsers)
      {
        if (player.Name == winner)
        {
          this.Winner = winner;
        }
        
      }
    }

    /// <summary>
    /// Este método es el getter de los users de la partida
    /// </summary>
    /// <returns>Una lista de instancias de User que son los users del Game</returns>
    public List<User> getInMatchUsers()
    {
      return this.InMatchUsers;
    }
    
    /// <summary>
    /// Este método es el getter de Time de la partida
    /// </summary>
    /// <returns>Retorna el tiempo de la partida de tipo Timer</returns>
    public Timer getTime()
    {
      return this.Time;
    }

    /// <summary>
    /// Este método es el setter del Time de la partida
    /// </summary>
    /// <param name="mins">Int que determina los minutos del Time</param>
    /// <param name="secs">Int que determina los segundos del Time</param>
    public void setTime(int mins, int secs)
    {
      this.Time.setMins(mins);
      this.Time.setSecs(secs);
    }

    /// <summary>
    /// Este método es el getter de TimePerRound de la partida
    /// </summary>
    /// <returns>Retorna el tiempo de cada ronda de tipo TimePerRound</returns>
    public Timer getTimePerRound()
    {
      return this.TimePerRound;
    }

    /// <summary>
    /// Este método es el setter del TimePerRound de cada ronda
    /// </summary>
    /// <param name="mins">Int que determina los minutos del TimePerRound</param>
    /// <param name="secs">Int que determina los segundos del TimePerRound</param>
    public void setTimePerRound(int mins, int secs)
    {
      this.TimePerRound.setMins(mins);
      this.TimePerRound.setSecs(secs);
    }

    /// <summary>
    /// Este método se encarga de devolver el board del usuario que se le pase por parámetro
    /// </summary>
    /// <param name="user">Recibe una instancia de tipo User</param>
    /// <returns>Retorna una instancia de tipo Board o null en caso de que el user no sea de esta partida</returns>
    public Board getUserBoard(User user)
    {
      if (this.InMatchUsers.Contains(user)) 
      {
        foreach (KeyValuePair<User, Board> userBoard in this.InMatchUsersBoard)
        {
          if (userBoard.Key == user)
          {
            return userBoard.Value;
          }
        }
      }

      return null;
    }
  }
}
