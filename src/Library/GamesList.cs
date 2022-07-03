using System.Collections.Generic;

namespace ChatBotProject
{
    /// <summary>
    /// GamesList es el experto en conocer a todos los usuarios, y 
    /// por el patron Expert este tambien es quien
    /// posee la responsabilidad de agregar y/o remover Usuario.
    /// Debido a que GamesList contiene instancias de User, por Creator, esta clase
    /// debe tener la responsabilidad de crear dichas instancias.
    /// </summary>
    public class GamesList
    {
        /// <summary>
        /// La lista de usuarios.
        /// </summary>
        /// <returns></returns>
    
        public List<Game> Games {get; set;}

        /// <summary>
        /// Instancia necesaria para aplicar Singleton.
        /// </summary>
        private static GamesList _instance;
        
        /// <summary>
        /// 
        /// </summary>
        private User userToRemove;
        
        /// <summary>
        /// Constructor privado, necesario para aplicar Singleton, asegurando que solo haya una instancia creada de esta clase.
        /// </summary>
        private GamesList()
        {
            this.Games = new List<Game>();
        }

        /// <summary>
        /// Siguienfo el patrón Creator AddUser es el encargado de crear los nuevos usuarios y a su vez los añade a la lista de usuarios.
        /// </summary>
        /// <param name="name">El nombre del usuario</param>
        /// <param name="password">La contraseña del usuario</param>
        /// <param name="chatId">La id del chat del usuario</param>
        /// <returns></returns>
        public Game AddGame(List<User> users, int totalMins, int totalSecs, int minsPerRound, int secsPerRound)
        {
            Game NewGame = new Game(users, totalMins, totalSecs, minsPerRound, secsPerRound);
            this.Games.Add(NewGame);
            return NewGame;
        }

        /// <summary>
        /// RemoveUser es el encargado de remover usuarios de la lista.
        /// </summary>
        /// <param name="gameToBeRemoved">Es el nombre del usuario que será removido</param>
        public void RemoveUser(Game gameToBeRemoved)
        {
          /*
          foreach (Game game in this.Games)
          {
            if (user.Name == gameToBeRemoved)
            {
                userToRemove = user;
            }
          }
          */
          this.Games.Remove(gameToBeRemoved);
        }

        /// <summary>
        /// Sirve para aplicar el singleton, verifica si la propiedad GamesList es nula y si no es nula te devuelve el 
        /// valor de la propiedad.
        /// </summary>
        /// <returns></returns>
        public static GamesList GetInstance()
        {
            if (_instance == null)
            {
                _instance = new GamesList();
            }
            return _instance;
        }
    }
}