namespace ChatBotProject
{
  public class Program
  {
    static void Main(string[] args)
    {
      Board board = new Board(10, 10);

      board.Attack("A10");
      board.printBoard();
    }
  }
}