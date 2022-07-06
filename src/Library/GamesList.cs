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
        /// La lista de games.
        /// </summary>
        /// <returns></returns>
    
        public List<Game> Games {get; set;}

        /// <summary>
        /// Instancia necesaria para aplicar Singleton.
        /// </summary>
        private static GamesList _instance;
    
        /// <summary>
        /// Constructor privado, necesario para aplicar Singleton, asegurando que solo haya una instancia creada de esta clase.
        /// </summary>
        private GamesList()
        {
            this.Games = new List<Game>();
        }

        /// <summary>
        /// Siguiendo el patrón Creator AddGame es el encargado de crear los nuevos games y a su vez los añade a la lista de games.
        /// </summary>
        /// <param name="users">La lista de users</param>
        /// <param name="totalMins">Minutos totales por partida</param>
        /// <param name="totalSecs">Segundos totales por partida</param>
        /// <param name="minsPerRound">Minutos totales por ronda</param>
        /// <param name="secsPerRound">Segundos totales por ronda</param>
        /// <returns></returns>
        public void AddGame(List<User> users, int totalMins, int totalSecs, int minsPerRound, int secsPerRound)
        {
            Game NewGame = new Game(users, totalMins, totalSecs, minsPerRound, secsPerRound);
            this.Games.Add(NewGame);
        }

        /// <summary>
        /// RemoveGame es el encargado de remover un Game de la lista.
        /// </summary>
        /// <param name="gameToBeRemoved">Es la instancia de Game que será removida de la lista</param>
        public void RemoveGame(Game gameToBeRemoved)
        {
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