namespace ChatBotProject
{
  /// <summary>
  /// Clase creada para poder guardar toda la información de los usuarios, este será el cimiento que utilizara el bot para
  /// poder identificar las distintas sesions y brindarles los distintos servicios que este provee.
  /// Esta clase aplica expert y srp, ya que es experta en conocer sus datos y a su vez siendo esta última su única responsabilidad.
  /// </summary>
  public class User
  {
    /// <summary>
    /// Esta propiedad privada guarda el id de la sesión por la cual el usuario esta interactuando
    /// en estre caso de Telegram.
    /// </summary>
    /// <value></value>
    private long id {get; set;}
    /// <summary>
    /// El nombre del usuario.
    /// </summary>
    /// <value></value>
    public string Name {get; set;}

    /// <summary>
    /// La contraseña del usuario. esta propiedad es creada para poder verificar ciertas acciones más
    /// delicadas, sirviendo como una forma de que el usuario de su consentimiento para ejecutar dichas funciones.
    /// </summary>
    /// <value></value>
    public string Password {get; set;}

    /// <summary>
    /// Esta propedad es utilizada para determinar si un jugador se encuentra o no en partida.
    /// </summary>
    public bool InGame = false;

    /// <summary>
    /// El constructor de la clase.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="password"></param>
    public User(string name, string password)
    {
      this.Name = name;
      this.Password = password;
    }

    /// <summary>
    /// Una manera de acceder a la id privada y cambiar sus valores,
    /// es de tipo long porque las id de telegram vienen en ese tipo.
    /// </summary>
    /// <value></value>
    public long ID
    {
      get
      {
        return this.id;
      }
      set
      {
        this.id = value;
      }

    }
  }
}