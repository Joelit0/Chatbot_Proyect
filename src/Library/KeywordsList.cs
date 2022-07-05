using System.Collections.Generic;

namespace ChatBotProject
{
    /// <summary>
    /// Clase singleton que contiene palabras clave baneadas, para que no puedan ser usadas como argumentos a la hora de crear
    /// usuarios.
    /// </summary>
    public class KeywordsList
    {    
        public List<string> BannedKeywords {get; set;}
        private static KeywordsList _instance;

        /// <summary>
        /// Constructor privado, necesario para aplicar Singleton, asegurando que solo haya una instancia creada de esta clase.
        /// </summary>
        private KeywordsList()
        {
            this.BannedKeywords = new List<string>() {"/LogIn","/Profile", "/Matchmacking", "/Help", "/Register", "/ChangeInfo"};
        }

        /// <summary>
        /// AddBannedKeyword es el encargado de agregar palabra clave a la lista.
        /// </summary>
        /// <param name="userToAdd"></param>
        public void AddBannedKeyword(string bannedKeyword)
        {
            this.BannedKeywords.Add(bannedKeyword);
        }

        /// <summary>
        /// RemoveBannedKeyword es el encargado de remover palabras clave de la lista.
        /// </summary>
        /// <param name="bannedKeyword"></param>
        public void RemoveBannedKeyword(string bannedKeyword)
        {
          this.BannedKeywords.Remove(bannedKeyword);
        }

        /// <summary>
        /// Sirve para aplicar el singleton, verifica si la propiedad KeywordsList es nula y si no es nula te devuelve el 
        /// valor de la property.
        /// </summary>
        /// <returns></returns>
        public static KeywordsList GetInstance()
        {
            if (_instance == null)
            {
                _instance = new KeywordsList();
            }
            return _instance;
        }

        public bool VerifyBannedKeywords(string messageToVerify)
        {
            bool isBanned = false;
            foreach (string bannedKeyword in BannedKeywords)
            {
                if (messageToVerify == bannedKeyword)
                {
                    return isBanned;
                }
                else
                {
                    isBanned = true;
                    return isBanned;
                }
            }
            return isBanned;
        }
        
    }
}