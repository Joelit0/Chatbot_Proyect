using NUnit.Framework;
using ChatBotProject;

namespace ChatBotProject.Test
{
    public class RegisterHandlerTests
    {
        RegisterHandler handler;

        [SetUp]
        public void Setup()
        {
          handler = new RegisterHandler(null);
          UsersList.GetInstance().AddUser("NameTest", "", 1);
          
        }

        [Test]
        public void TestHandle() //Verifica que ocurre si el handler puede manejar el comando inicial
        {
            string message = "";
            long Testid = 123456;
            message = handler.Keywords[0];
            string response;

            IHandler result = handler.Handle(message, Testid , out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("Como te gustaría llamar a tu usuario?"));
        }

        [Test]
        public void FullRegisterTest() //En este test comprobamos la funcionalidad de registrarse como un usuario nuevo.
        {
            string message = "";
            long Testid = 123456;
            message = handler.Keywords[0];
            string response;
            handler.Handle(message, Testid, out response);

            message = "Rodrigo";
            handler.Handle(message, Testid, out response);

            message = "16";
            handler.Handle(message, Testid, out response);

            message = "16";
            IHandler result = handler.Handle(message, Testid , out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("Felicidades, se ha registrado con éxito! Utilice /LogIn para iniciar sesión."));
        }

        [Test]
        public void AlreadyRegisteredNameTest() //En este test verificamos que ocurre cuando ingresamos un nombre que ya esta registrado.
        {
            string message = "";
            long Testid = 123456;
            message = handler.Keywords[0];
            string response;
            handler.Handle(message, Testid, out response);

            message = "NameTest";
            IHandler result = handler.Handle(message, Testid , out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("Este nombre de usuario ya existe, porfavor ingresa otro"));
        }

        [Test]
        public void InvalidNameTest() //En este test verificamos que ocurre cuando ingresamos un nombre inválido.
        {
            string message = "";
            long Testid = 123456;
            message = handler.Keywords[0];
            string response;
            handler.Handle(message, Testid, out response);

            message = "/Register";
            IHandler result = handler.Handle(message, Testid , out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("Este nombre de usuario no es válido"));
        }

        [Test]
        public void WrongPasswordConfirmationTest() //En este test verificamos que ocurre cuando ingresamos de manera incorrecta la confirmación de la contraseña.
        {
            string message = "";
            long Testid = 123456;
            message = handler.Keywords[0];
            string response;
            handler.Handle(message, Testid, out response);

            message = "Rodrygo";
            handler.Handle(message, Testid, out response);

            message = "16";
            handler.Handle(message, Testid, out response);

            message = "wrong";
            IHandler result = handler.Handle(message, Testid , out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("La contraseña no coincide, porfavor intentalo de nuevo, te quedan 3 intentos adicionales."));
        }

        [Test]
        public void WrongPasswordConfirmationOutOfAttemptsTest() //En este test verificamos que ocurre cuando ingresamos de manera incorrecta la confirmación de la contraseña y nos quedamos sin intentos.
        {
            string message = "";
            long Testid = 123456;
            message = handler.Keywords[0];
            string response;
            handler.Handle(message, Testid, out response);

            message = "Ronaldo";
            handler.Handle(message, Testid, out response);

            message = "7";
            handler.Handle(message, Testid, out response);

            message = "wrong";
            handler.Handle(message, Testid, out response);

            message = "wrong";
            handler.Handle(message, Testid, out response);

            message = "wrong";
            handler.Handle(message, Testid, out response);

            message = "wrong";
            IHandler result = handler.Handle(message, Testid , out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("Se han acabado los intentos, porfavor introduce una nueva contraseña."));
        }

        [Test]
        public void TestDoesNotHandle() //Verifica que ocurre si el handler no puede manejar el comando inicial
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