using System.Collections.Generic;

namespace ChatBotProject
{
    public class UserLogin
    {
      public User LogedInPlayer { get; private set; }
      private static UserLogin _instance;

      private UserLogin()
      {
        this.LogedInPlayer = new User("","");
      }

      public void setLogedUser(User Logedplayer)
      {
        this.LogedInPlayer = Logedplayer;
      }

      public static UserLogin GetInstance()
        {
            if (_instance == null)
            {
                _instance = new UserLogin();
            }
            return _instance;
        }
        
    }
}