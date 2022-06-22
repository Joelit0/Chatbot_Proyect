using System.Collections.Generic;

namespace ChatBotProject
{
    /// <summary>
    /// UsersList es el experto en conocer a todos los usuarios, y 
    /// por el patron Expert este tambien es quien
    /// posee la responsabilidad de agregar y/o remover Usuario.
    /// Debido a que UsersList contiene instancias de User, por Creator, esta clase
    /// debe tener la responsabilidad de crear dichas instancias.
    /// </summary>
    public class UsersList
    {
        /// <summary>
        /// La lista de usuarios.
        /// </summary>
        /// <returns></returns>
    
        public List<User> Users {get; set;}

        /// <summary>
        /// Instancia necesaria para aplicar Singleton.
        /// </summary>
        private static UsersList _instance;
        
        /// <summary>
        /// 
        /// </summary>
        private User userToRemove;
        
        /// <summary>
        /// Constructor privado, necesario para aplicar Singleton, asegurando que solo haya una instancia creada de esta clase.
        /// </summary>
        private UsersList()
        {
            this.Users = new List<User>();
        }

        /// <summary>
        /// Siguienfo el patrón Creator AddUser es el encargado de crear los nuevos usuarios y a su vez los añade a la lista de usuarios.
        /// </summary>
        /// <param name="name">El nombre del usuario</param>
        /// <param name="password">La contraseña del usuario</param>
        /// <param name="chatId">La id del chat del usuario</param>
        /// <returns></returns>
        public User AddUser(string name, string password, long chatId)
        {
            User NewUser = new User(name, password);
            NewUser.ID = chatId;
            this.Users.Add(NewUser);
            return NewUser;
        }

        /// <summary>
        /// RemoveUser es el encargado de remover usuarios de la lista.
        /// </summary>
        /// <param name="nameOfUserToRemove">Es el nombre del usuario que será removido</param>
        public void RemoveUser(string nameOfUserToRemove)
        {
          foreach (User user in this.Users)
          {
            if (user.Name == nameOfUserToRemove)
            {
                userToRemove = user;
            }
          }
          this.Users.Remove(userToRemove);
        }

        /// <summary>
        /// Sirve para aplicar el singleton, verifica si la propiedad UsersList es nula y si no es nula te devuelve el 
        /// valor de la property.
        /// </summary>
        /// <returns></returns>
        public static UsersList GetInstance()
        {
            if (_instance == null)
            {
                _instance = new UsersList();
            }
            return _instance;
        }
    }
}