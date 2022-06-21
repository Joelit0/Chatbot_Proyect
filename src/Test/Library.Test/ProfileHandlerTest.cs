using NUnit.Framework;
using ChatBotProject;
using System.Text;

namespace ChatBotProject.Test
{
    public class ProfileHandlerTest
    {
        ProfileHandler handler;

        [SetUp]
        public void Setup()
        {
          handler = new ProfileHandler(null);
          UsersList.GetInstance().AddUser("Rodri", "16", 1234);
        }

        [Test]
        public void TestHandle()
        {
            string message = "";
            long Testid = 1234;
            message = handler.Keywords[0];
            string response;
              StringBuilder profileStringBuilder = new StringBuilder("Tu perfil de jugador:\n")
                                                                            .Append($"Nombre: Rodri\n")
                                                                            .Append($"Id: 1234\n")
                                                                            .Append($"Puedes usar /ChangeInfo para cambiar tu nombre y contraseña.\n");
                                                                                                                                 
              string expected = profileStringBuilder.ToString();

            IHandler result = handler.Handle(message, Testid , out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo(expected));
        }

        [Test]
        public void TestDoesNotHandle()
        {
            string message = "adios";
            long Testid = 123456;
            string response;

            IHandler result = handler.Handle(message, Testid, out response);

            Assert.That(result, Is.Null);
            Assert.That(response, Is.Empty);
        }
    }
}