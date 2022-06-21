using NUnit.Framework;
using ChatBotProject;
using System.Text;

namespace ChatBotProject.Test
{
    public class HelpHandlerTest
    {
        HelpHandler handler;

        [SetUp]
        public void Setup()
        {
          handler = new HelpHandler(null);
        }

        [Test]
        public void TestHandle()
        {
            string message = "";
            long Testid = 123456;
            message = handler.Keywords[0];
            string response;
            StringBuilder helpStringBuilder = new StringBuilder("Lista de Comandos:\n")
                                                                          .Append("/Register: Registrate como un usuario nuevo\n")
                                                                          .Append("/LogIn: Inicia sesión con un usuario ya creado\n")
                                                                          .Append("/Profile: Accede a tu perfil\n")
                                                                          .Append("/Matchmaking: Busca partida con un jugador que conoces\n");
            string expected = helpStringBuilder.ToString();

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