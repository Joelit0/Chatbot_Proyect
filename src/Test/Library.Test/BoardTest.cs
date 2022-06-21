using System;
using ChatBotProject;
using NUnit.Framework;

namespace ChatBotProject.Test
{
  public class BoardTest
  {
    [SetUp]
    public void Setup() {}

    // Tests de clase
    [Test]
    public void TestGetWidth()
    {
      Board board = new Board(10, 5);

      Assert.AreEqual(100, totalRows * totalCols);
    }

    ublic int getWidth()
    {
      return this.Width;
    }

    public int getHeight()
    {
      return this.Height;
    }

    public string getHeaderLetters()
    {
      return HeaderLetters;
    }

    public string[,] getFields()
    {
      return this.Fields;
    }
    // Tests de escenario

    // En este test se verifica que el board tenga la cantidad de filas correctas
    [Test]
    public void TestCreateBoard()
    {
      Board board = new Board(10, 10);
      int totalRows = board.getFields().GetLength(0);
      int totalCols = board.getFields().GetLength(1);

      Assert.AreEqual(100, totalRows * totalCols);
    }
  }
}
