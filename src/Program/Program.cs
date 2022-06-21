﻿namespace ChatBotProject
{
  public class Program
  {
    static void Main(string[] args)
    {
      // Creamos el tablero de 10x10
      Board board = new Board(10, 10);

      // Agregamos un barco al board
      List<string> positions = new List<string>();
      positions.Add("A1".ToUpper());
      positions.Add("B1".ToUpper());
      positions.Add("C1".ToUpper());
      positions.Add("D1".ToUpper());

      board.addShip(positions);

      // Mostramos los barcos e imprimimos el tablero
      board.showShips();

      // Crear ConsolePrinter de tipo IPrinter e imprimir el board
      IPrinter consolePrinter = new ConsolePrinter();

      consolePrinter.printBoard(board);

      // Agregamos un barco que se pise con el otro
      positions = new List<string>();
      positions.Add("d1".ToUpper());
      positions.Add("d2".ToUpper());
      positions.Add("d3".ToUpper());
      positions.Add("D4".ToUpper());

      board.addShip(positions);

      // Atacar el barco completo
      board.attack("A1".ToUpper());
      consolePrinter.printBoard(board);

      board.attack("b1".ToUpper());
      consolePrinter.printBoard(board);

      board.attack("c1".ToUpper());
      consolePrinter.printBoard(board);

      board.attack("D1".ToUpper());
      consolePrinter.printBoard(board);

     // Ocultuamos los barcos y mostramos el tablero
      board.hideShips();
      consolePrinter.printBoard(board);
    }
  }
}
