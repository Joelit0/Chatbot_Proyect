using System.Collections.Generic;

namespace ChatBotProject
{
    public class UserLoginWithId
    {
      public void MyUser(long Myuserid)
      {
        foreach(User player in UsersList.GetInstance().Users)
        {
          if (player.ID == Myuserid)
          {
            
          }
        }
        
      }
    }
}