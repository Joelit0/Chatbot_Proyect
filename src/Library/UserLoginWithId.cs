using System.Collections.Generic;

namespace ChatBotProject
{
    public class UserLoginWithId
    {
      public void MyUser(int id)
      {
        foreach(User player in UsersList.GetInstance().Users)
        {
          if (player.GetID == id)
          {
            
          }
        }
        
      }
    }
}