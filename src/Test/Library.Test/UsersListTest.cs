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
      }

      [Test]
      public void AddUserTest() //Prueba la funcionalidad del método AddUser para crear y añadir un usuario
      {
        UsersList.GetInstance().AddUser("Benzema", "16", 1234);
        int contador = 0;
        foreach (User player in UsersList.GetInstance().Users)
        { 
          if (player.Name == "Benzema")
          {
            contador += 1;
          }
        }
        int expected = 1;
        Assert.AreEqual(expected, contador);
        UsersList.GetInstance().RemoveUser("Rodri");
      }

      [Test]
      public void RemoveUserTest() //Prueba la funcionalidad del método RemoveUser para remover un usuario
      {
        UsersList.GetInstance().AddUser("Ro", "61", 1111);
        UsersList.GetInstance().RemoveUser("Ro");
        int contador = 0;
        foreach (User player in UsersList.GetInstance().Users)
        {
          if (player.Name == "Ro")
          {
            contador += 1;
          }
        }
        int expectedInstances = 0;
        Assert.AreEqual(expectedInstances, contador);
      }
  }

}