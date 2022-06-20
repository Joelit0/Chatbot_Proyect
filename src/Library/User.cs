namespace ChatBotProject
{
  public class User
  {
    private int id {get; set;}
    public string Name {get; set;}
    public string Password {get; set;}
    public bool InGame = false;
    public string State { get; set; } = "start";

    public User(string name, string password)
    {
      this.Name = name;
      this.Password = password;
    }

    public int GetID
    {
      get
      {
        return this.id;
      }
    }

    public void SetID(int value)
    {
      this.id = value;  
    }

    public void SetName(string newName)
    {
      this.Name = newName;
    }


  }
}