namespace ChatBotProject
{
  public class User
  {
    private long id {get; set;}
    public string Name {get; set;}
    public string Password {get; set;}
    public bool InGame = false;
    public string State { get; set; } = "start";

    public User(string name, string password)
    {
      this.Name = name;
      this.Password = password;
    }

    public long GetID
    {
      get
      {
        return this.id;
      }
    }

    public void SetID(long value)
    {
      this.id = value;  
    }

    public void SetName(string newName)
    {
      this.Name = newName;
    }


  }
}