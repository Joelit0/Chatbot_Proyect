using System;
using ChatBotProject;
using NUnit.Framework;

namespace ChatBotProject.Test
{
  public class KeywordsListTest
  {
      [SetUp]
      public void Setup()
      {
      }

      [Test]
      public void AddBannedKeywordTest() //Prueba la funcionalidad del método AddBannedKeyword para crear y añadir una nueva palabra clave
      {
        KeywordsList.GetInstance().AddBannedKeyword("/ChangeInfo");
        int contador = 0;
        foreach (string keyword in KeywordsList.GetInstance().BannedKeywords)
        {
          contador += 1;
        }
        int expected = 6;
        Assert.AreEqual(expected, contador);
        KeywordsList.GetInstance().RemoveBannedKeyword("/ChangeInfo");
      }

      [Test]
      public void RemoveBannedKeywordTest() //Prueba la funcionalidad del método RemoveBannedKeyword para remover una nueva palabra clave
      {
        KeywordsList.GetInstance().AddBannedKeyword("/IA");
        int contador = 0;
        KeywordsList.GetInstance().RemoveBannedKeyword("/IA");
        foreach (string keyword in KeywordsList.GetInstance().BannedKeywords)
        {
          contador += 1;
        }
        int expected = 5;
        Assert.AreEqual(expected, contador);
      }
  }

}