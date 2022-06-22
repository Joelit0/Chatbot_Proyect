using NUnit.Framework;
using ChatBotProject;

namespace ChatBotProject.Test
{
    public class ChangeProfileInfoHandlerTests
    {
        ChangeProfileInfoHandler handler;

        [SetUp]
        public void Setup()
        {
          handler = new ChangeProfileInfoHandler(null);          
        }

        [Test]
        public void TestHandle() //Verifica que ocurre si el handler puede manejar el comando inicial
        {
            
            string message = "";
            long Testid = 98017487498;
            UsersList.GetInstance().AddUser("ChangeInfoHandlerTest", "", Testid);
            message = handler.Keywords[0];
            string response;

            IHandler result = handler.Handle(message, Testid , out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("Porfavor use /Name si quiere cambiar su nombre y /Password si quiere cambiar su contraseña"));
        }

        [Test]
        public void FullChangeNameInfoTest() //En este test comprobamos la funcionalidad del cambio de nombre.
        {
            
            string message = "";
            long Testid = 1028470927;
            UsersList.GetInstance().AddUser("ChangeNameTest", "", Testid);
            message = handler.Keywords[0];
            string response;
            handler.Handle(message, Testid, out response);

            message = "/Name";
            handler.Handle(message, Testid, out response);

            message = "Cristiano";
            IHandler result = handler.Handle(message, Testid , out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("Su nombre de usuario se ha cambiado a Cristiano"));
            UsersList.GetInstance().RemoveUser("Cristiano");
        }

        [Test]
        public void FullChangePasswordInfoTest() //En este test comprobamos la funcionalidad del cambio de contraseña.
        {
            string message = "";
            long Testid = 0912348743;
            UsersList.GetInstance().AddUser("ChangePasswordTest", "", Testid);
            message = handler.Keywords[0];
            string response;
            handler.Handle(message, Testid, out response);

            message = "/Password";
            handler.Handle(message, Testid, out response);

            message = "ChangedPassword";
            IHandler result = handler.Handle(message, Testid , out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("Su contraseña se ha cambiado a ChangedPassword"));
            UsersList.GetInstance().RemoveUser("ChangePasswordTest");
        }

        [Test]
        public void ChangeNameOnAlreadyRegisteredNameTest() //En este test verificamos que ocurre cuando ingresamos un nombre que ya esta registrado.
        { 
            long AlreadyRegisteredTestid = 747987427;
            UsersList.GetInstance().AddUser("AlreadyRegisteredName", "", AlreadyRegisteredTestid);
            string message = "";
            long Testid = 120287034;
            UsersList.GetInstance().AddUser("ChangeNameOnAlreadyRegisteredNameTest", "", Testid);
            message = handler.Keywords[0];
            string response;
            handler.Handle(message, Testid, out response);
            
            message = "/Name";
            handler.Handle(message, Testid, out response);
            
            message = "AlreadyRegisteredName";
            IHandler result = handler.Handle(message, Testid , out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("Este nombre de usuario ya existe, porfavor ingresa otro"));
        }

        [Test]
        public void ChangeToInvalidNameTest() //En este test verificamos que ocurre cuando ingresamos un nombre inválido.
        {
            string message = "";
            long Testid = 9087423892;
            UsersList.GetInstance().AddUser("ChangeToInvalidNameTest", "", Testid);
            message = handler.Keywords[0];
            string response;
            handler.Handle(message, Testid, out response);

            message = "/Name";
            handler.Handle(message, Testid, out response);

            message = "/Register";
            IHandler result = handler.Handle(message, Testid , out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("Este nombre de usuario no es válido"));
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