using System;
using ChatBotProject;
using NUnit.Framework;

namespace ChatBotProject.Test
{
  public class UsersListTest
  {
      [SetUp]
      public void Setup()
      {
        UsersList.GetInstance().AddUser("Rodri", "16", 1234);
      }

      [Test]
      public void AddUserTest() //Prueba la funcionalidad del método AddUser para crear y añadir un usuario
      {
        int contador = 0;
        foreach (User player in UsersList.GetInstance().Users)
        {
          contador += 1;
        }
        int expected = 1;
        Assert.AreEqual(expected, contador);
      }

      [Test]
      public void RemoveUserTest() //Prueba la funcionalidad del método RemoveUser para remover un usuario
      {
        UsersList.GetInstance().AddUser("Ro", "61", 1111);
        UsersList.GetInstance().RemoveUser("Ro");
        int contador = 0;
        foreach (User player in UsersList.GetInstance().Users)
        {
          contador += 1;
        }
        int expected = 0;
        Assert.AreEqual(expected, contador);
      }
  }

}