using System;
using System.Collections.Generic;
using ChatBotProject;
using NUnit.Framework;

namespace ChatBotProject.Test
{
  public class BoardTest
  {
    [SetUp]
    public void Setup() {}

    // Tests de clase

    // Este test verifica que el getter del atributo Width sea correcto
    [Test]
    public void TestGetWidth()
    {
      Board board = new Board(10, 5);

      Assert.AreEqual(5, board.getWidth());
    }

    // Este test verifica que el getter del atributo Height sea correcto
    [Test]
    public void TestGetHeight()
    {
      Board board = new Board(5, 10);

      Assert.AreEqual(5, board.getHeight());
    }

    // Este test verifica que el getter de el atributo constante HeaderLetters sea correcto
    [Test]
    public void TestGetHeaderLetters()
    {
      Board board = new Board(10, 10);
      string alphabet = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ";
      Assert.AreEqual(alphabet, board.getHeaderLetters());
    }

    // Este test verifica que el getter de los ships del board sea correcto
    [Test]
    public void TestGetShips()
    {
      Board board = new Board(10, 10);
      List<string> positions =  new List<string>() { "A1", "B1", "C1", "D1" };

      board.addShip(positions);
      
      Assert.AreEqual(1, board.getShips().Count);
    }

    // Este test cambia el valor de una pocisión y verifica que en esa pocisión esté ese valor
    [Test]
    public void TestUpdateBoard1()
    {
      Board board = new Board(10, 10);
      board.updateBoard("new value", 0, 0);
      Assert.AreEqual("new value", board.getFields()[0, 0]);
    }

    // Este test cambia el valor de una pocisión y verifica que sea distinta de la anterior
    [Test]
    public void TestUpdateBoard2()
    {
      Board board = new Board(10, 10);
      string lastValue =  board.getFields()[0, 0];
      board.updateBoard("new value", 0, 0);
      string newValue =  board.getFields()[0, 0];

      Assert.AreNotEqual(lastValue, newValue);
    }
  
    // Este test verifica que un Ship válido se agregue. Verifica si el largo de la lista de Ships es 1
    [Test]
    public void TestAddValidShip1()
    {
      Board board = new Board(10, 10);

      List<string> positions =  new List<string>() { "A1", "A2", "A3", "A4" };

      board.addShip(positions);
      Assert.AreEqual(1, board.getShips().Count);
    }

    // Este test verifica que un Ship válido se agregue. Verifica si las pocisiones de ese barco agregado sean las mismas
    [Test]
    public void TestAddValidShip2()
    {
      Board board = new Board(10, 10);

      List<string> positions =  new List<string>() { "A1", "B1", "C1", "D1" };

      board.addShip(positions);
      Assert.AreEqual(positions, board.getShips()[0].getPositions());
    }

    // Este test verifica que un Ship inválido no se agregue. El barco es inválido ya que no es consecutivo en cuanto a sus filas
    [Test]
    public void TestAddShipInvalid1()
    {
      Board board = new Board(10, 10);
      List<string> positions =  new List<string>() { "A1", "A2", "A3", "A10" };

      board.addShip(positions);
      Assert.AreEqual(0, board.getShips().Count);
    }

    // Este test verifica que un Ship inválido no se agregue. El barco es inválido ya que no es consecutivo en cuanto a sus columnas
    [Test]
    public void TestAddShipInvalid2()
    {
      Board board = new Board(10, 10);
      List<string> positions =  new List<string>() { "A1", "B1", "C1", "F1" };

      board.addShip(positions);
      Assert.AreEqual(0, board.getShips().Count);
    }

    // Este test verifica que no se pueda agregar un ship que se "pise" con otro
    [Test]
    public void TestAddShipInvalid3()
    {
      Board board = new Board(10, 10);
      List<string> positions =  new List<string>() { "A1", "B1", "C1", "D1" };

      board.addShip(positions);
      board.addShip(positions);

      Assert.AreEqual(1, board.getShips().Count);
    }

    // Este test verifica que no se pueda agregar un ship que esté fuera del board
    [Test]
    public void TestAddShipInvalid4()
    {
      Board board = new Board(10, 10);
      List<string> positions =  new List<string>() { "K1", "K2", "K3", "K4" };

      board.addShip(positions);

      Assert.AreEqual(0, board.getShips().Count);
    }

    // Este test verifica que se elimine un barco de la lista de barcos del tablero
    [Test]
    public void TestRemoveShipValid()
    {
      Board board = new Board(10, 10);
      List<string> positions =  new List<string>() { "A1", "B1", "C1", "D1" };

      board.addShip(positions);
      Ship ship = board.getShips()[0];

      board.removeShip(ship);

      Assert.AreEqual(0, board.getShips().Count);
    }

    // Este test verifica que no se pueda eliminar un barco que no pertenezca a la lista de barcos del tablero
    [Test]
    public void TestRemoveShipInvalid()
    {
      Board board = new Board(10, 10);
      List<string> positions =  new List<string>() { "A1", "B1", "C1", "D1" };

      board.addShip(positions);
      Ship ship = new Ship(positions);

      board.removeShip(ship);

      Assert.AreEqual(1, board.getShips().Count);
    }

    // Tests de escenario

    // Este test verifica que el board tenga la cantidad de filas correctas
    [Test]
    public void TestCreateBoard()
    {
      Board board = new Board(10, 10);
      
      // Obtengo el total de filas y columnas de la matriz
      int totalRows = board.getFields().GetLength(0);
      int totalCols = board.getFields().GetLength(1);

      Assert.AreEqual(100, totalRows * totalCols);
    }

    // Este test verifica que se ponga una "O" cuando un ataque no pega en un barco
    [Test]
    public void TestDontHitAShip()
    {
      Board board = new Board(10, 10);
      List<string> positions =  new List<string>() { "A10", "B10", "C10", "D10" };
      board.addShip(positions);
  
      board.attack("A1");
    
      Assert.AreEqual("O", board.getFields()[0, 0]);
    }

    // Este test verifica que se ponga una "X" cuando un ataque pega a un barco
    [Test]
    public void TestHitAShip1()
    {
      Board board = new Board(10, 10);
      List<string> positions =  new List<string>() { "A1", "B1", "C1", "D1" };
      board.addShip(positions);
  
      board.attack("A1");
    
      Assert.AreEqual("X", board.getFields()[0, 0]);
    }

    // Este test verifica que el largo del barco se reste en 1 cuando sea alcanzado por un ataque
    [Test]
    public void TestHitAShip2()
    {
      Board board = new Board(10, 10);
      List<string> positions =  new List<string>() { "A1", "B1", "C1", "D1" };
      board.addShip(positions);
      Ship ship = board.getShips()[0];

      board.attack("A1");
    
      Assert.AreEqual(3, ship.getLarge());
    }

    // Este test verifica que el barco sea eliminado de la lista de ships del board cuando todas sus pocisiones fueron atacadas
    [Test]
    public void TestKillShip()
    {
      Board board = new Board(10, 10);
      List<string> positions =  new List<string>() { "A1", "B1", "C1", "D1" };
      board.addShip(positions);

      board.attack("A1");
      board.attack("B1");
      board.attack("C1");
      board.attack("D1");

      Assert.AreEqual(0, board.getShips().Count);
    }


    // Este test verifica que cuando sea invocado el método showShips el barco que está en el tablero sea visible
    // En todas las pocisiones del barco se verá un "#"
    [Test]
    public void TestShowShips()
    {
      Board board = new Board(10, 10);
      List<string> positions =  new List<string>() { "A1", "B1", "C1", "D1" };
      board.addShip(positions);
      board.showShips();

      string boardPosition1 = board.getFields()[0,0];
      string boardPosition2 = board.getFields()[0,1];
      string boardPosition3 = board.getFields()[0,2];
      string boardPosition4 = board.getFields()[0,3];

    
      Assert.AreEqual(
        true,
        boardPosition1 == "#" &&
        boardPosition2 == "#" &&
        boardPosition3 == "#" &&
        boardPosition4 == "#"
      );
    }

    // Este test verifica que cuando sea invocado el método hideShips el barco que está en el tablero no sea visible
    // En todas las pocisiones del barco se verá un "-". Se camuflará con pocisiones aún no atacadas
    [Test]
    public void TestHideShips()
    {
      Board board = new Board(10, 10);
      List<string> positions =  new List<string>() { "A1", "B1", "C1", "D1" };
      board.addShip(positions);
      board.hideShips();

      string boardPosition1 = board.getFields()[0,0];
      string boardPosition2 = board.getFields()[0,1];
      string boardPosition3 = board.getFields()[0,2];
      string boardPosition4 = board.getFields()[0,3];

      Assert.AreEqual(
        true,
        boardPosition1 == "-" &&
        boardPosition2 == "-" &&
        boardPosition3 == "-" &&
        boardPosition4 == "-"
      );
    }

    [Test]
    public void TestGetPositionValue()
    {
      Board board = new Board(10, 10);

      Assert.AreEqual("-", board.getPositionValue("A1"));
    }
  }
}
