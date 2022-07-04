
using System.Collections.Generic;

namespace ChatBotProject
{
  /// <summary>
  /// GamesVsIAList es el experto en conocer a todos los usuarios, y 
  /// por el patron Expert este tambien es quien
  /// posee la responsabilidad de agregar y/o remover Usuario.
  /// Debido a que GamesVsIAList contiene instancias de User, por Creator, esta clase
  /// debe tener la responsabilidad de crear dichas instancias.
  /// </summary>
  public class GamesVsIAList
  {
    /// <summary>
    /// La lista de GamesVsIAList.
    /// </summary>
    /// <returns></returns>

    public List<GameVsIA> GamesVsIA { get; set; }

    /// <summary>
    /// Instancia necesaria para aplicar Singleton.
    /// </summary>
    private static GamesVsIAList _instance;
    
    /// <summary>
    /// Constructor privado, necesario para aplicar Singleton, asegurando que solo haya una instancia creada de esta clase.
    /// </summary>
    private GamesVsIAList()
    {
      this.GamesVsIA = new List<GameVsIA>();
    }

    /// <summary>
    /// Siguiendo el patrón Creator AddGame es el encargado de crear las nuevas GameVsIA y a su vez los añade a GamesVsIA.
    /// </summary>
    /// <param name="name">El usuario que juega la partida</param>
    /// <returns></returns>
    public void AddGameVsIA(User player)
    {
      GameVsIA NewGameVsIA = new GameVsIA(player);
      this.GamesVsIA.Add(NewGameVsIA);
    }

    /// <summary>
    /// RemoveGameVsIA es el encargado de remover una GameVsIA de GamesVsIA.
    /// </summary>
    /// <param name="gameToBeRemoved">El Game que será removido</param>
    public void RemoveGame(GameVsIA gameToBeRemoved)
    {
      this.GamesVsIA.Remove(gameToBeRemoved);
    }

    /// <summary>
    /// Sirve para aplicar el singleton, verifica si la propiedad GamesVsIAList es nula y si no es nula te devuelve el 
    /// valor de la propiedad.
    /// </summary>
    /// <returns></returns>
    public static GamesVsIAList GetInstance()
    {
        if (_instance == null)
        {
            _instance = new GamesVsIAList();
        }
        return _instance;
    }
  }
}
