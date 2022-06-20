using System.Collections.Generic;

namespace ChatBotProject
{
    /// <summary>
    /// ListaUsuarios es el experto en conocer a todos los Usuario, y 
    /// por el patron Expert este tambien es quien
    /// posee la responsabilidad de agregar y/o remover Usuario.
    /// A su vez depende de IUsuario para agregar cualquier tipo de usuario (emprendedor,
    /// administrador o Usuario).
    /// </summary>
    public class UsersList
    {
        /// <summary>
        /// Variable estatica Usuario, porque es una lista de instancias de Usuario
        /// que lleva un registro de todos las Usuario que hay. Esta property hace uso de Polimorfismo, todos los usuarios
        /// ya sean emprendedores, empresarios o administradores implementan IUsuario y esta clase al usarlos a todos ellos
        /// usa como parametro IUsuario.
        /// </summary>
        /// <returns></returns>
    
        public List<User> Users {get; set;}
        private static UsersList _instance;

        private UsersList()
        {
            this.Users = new List<User>();
        }

        /// <summary>
        /// AddUsuario es el encargado de agregar Usuario a la lista.
        /// </summary>
        /// <param name="userToAdd"></param>
        public User AddUser(string name, string password)
        {
            User NewUser = new User(name, password);
            int NewId = 0;
            foreach (User user in this.Users)
            {
              NewId += 1;
            }
            NewUser.SetID(NewId);
            this.Users.Add(NewUser);
            return NewUser;
        }

        /// <summary>
        /// RemoveUsuario es el encargado de remover Usuario de la lista.
        /// </summary>
        /// <param name="userToRemove"></param>
        public void RemoveUser(User userToRemove)
        {
          Users.Remove(userToRemove);
        }

        /// <summary>
        /// Sirve para aplicar el singleton, verifica si la property ListaUsuarios es nula y si no es nula te devuelve el 
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