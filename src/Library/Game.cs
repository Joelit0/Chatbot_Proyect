namespace ChatBotProject
{
  public class Game
  {
    private List<User> Users;
    private Timer Time;
    private Timer TimePerRound;
    private bool InProgress;
    private User Winner;
    private Dictionary<User, Board> UsersBoard;

    public Game(List<User> users, Timer Time, Timer TimePerRound)
    {
      if (users.Count() == 2)
      {
        this.Users = users;
        this.InProgress = false;
        this.UsersBoard = new Dictionary<User, Board>();

        // Llenar el diccionario con clave User y Valor Board
        foreach(User user in this.Users)
        {
          this.UsersBoard.Add(user, new Board(10, 10));
        }
      }
    }

    public void StartGame()
    {
    //   // Agregamos un barco al board
    //   List<string> positions = new List<string>();
    //   positions.Add("A1".ToUpper());
    //   positions.Add("B1".ToUpper());
    //   positions.Add("C1".ToUpper());
    //   positions.Add("D1".ToUpper());

    //   this.Board.addShip(positions);

    //   // Mostramos los barcos e imprimimos el tablero
    //   this.Board.showShips();

    //   // Crear ConsolePrinter de tipo IPrinter e imprimir el board
    //   IPrinter consolePrinter = new ConsolePrinter();

    //   consolePrinter.printBoard(board);

    //   // Agregamos un barco que se pise con el otro
    //   positions = new List<string>();
    //   positions.Add("d1".ToUpper());
    //   positions.Add("d2".ToUpper());
    //   positions.Add("d3".ToUpper());
    //   positions.Add("D4".ToUpper());

    //   this.Board.addShip(positions);

    //   // Atacar el barco completo
    //   board.attack("A1".ToUpper());
    //   consolePrinter.printBoard(board);

    //   board.attack("b1".ToUpper());
    //   consolePrinter.printBoard(board);

    //   board.attack("c1".ToUpper());
    //   consolePrinter.printBoard(board);

    //   board.attack("D1".ToUpper());
    //   consolePrinter.printBoard(board);

    //  // Ocultuamos los barcos y mostramos el tablero
    //   board.hideShips();
    //   consolePrinter.printBoard(board);

    }

    public void FinishGame()
    {

    }

    // Winner Getters & Setters
    public User getWinner()
    {
      return this.Winner;
    }

    public void setWinner(User user)
    {
      if (this.Users.Contains(user))
      {
        this.Winner = user;
      }
    }

    // Users Getter
    public User getUsers()
    {
      return this.Winner;
    }

    // Time Getters & Setters

    public Timer getTime()
    {
      return this.Time;
    }

    public void setTime(Timer time)
    {
      this.Time = time;
    }

    // TimePerRound Getters & Setters

    public Timer getTimePerRound()
    {
      return this.TimePerRound;
    }

    public void setTimePerRound(Timer timePerRound)
    {
      this.TimePerRound = timePerRound;
    }

    // Devuelve el board del usuario que se le pase
    public Board getUserBoard(User user)
    {
      if (this.Users.Contains(user)) 
      {
        foreach (KeyValuePair<User, Board> userBoard in this.UsersBoard)
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
