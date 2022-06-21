using System.Collections.Generic;

namespace ChatBotProject
{
    /// <summary>
    /// ListaUsuarios es el experto en conocer a todos los Usuario, y 
    /// por el patron Expert este tambien es quien
    /// posee la responsabilidad de agregar y/o remover Usuario.
    /// A su vez depende de IUsuario para agregar cualquier tipo de usuario (emprendedor,
    /// administrador o Usuario).
    /// </summary>
    public class KeywordsList
    {
        /// <summary>
        /// Variable estatica Usuario, porque es una lista de instancias de Usuario
        /// que lleva un registro de todos las Usuario que hay. Esta property hace uso de Polimorfismo, todos los usuarios
        /// ya sean emprendedores, empresarios o administradores implementan IUsuario y esta clase al usarlos a todos ellos
        /// usa como parametro IUsuario.
        /// </summary>
        /// <returns></returns>
    
        public List<string> BannedKeywords {get; set;}
        private static KeywordsList _instance;

        private KeywordsList()
        {
            this.BannedKeywords = new List<string>() {"/LogIn","/Profile", "/Matchmacking", "/Help", "/Register"};
        }

        /// <summary>
        /// AddUsuario es el encargado de agregar Usuario a la lista.
        /// </summary>
        /// <param name="userToAdd"></param>
        public void AddBannedKeyword(string bannedKeyword)
        {
            this.BannedKeywords.Add(bannedKeyword);
        }

        /// <summary>
        /// RemoveUsuario es el encargado de remover Usuario de la lista.
        /// </summary>
        /// <param name="bannedKeyword"></param>
        public void RemoveBannedKeyword(string bannedKeyword)
        {
          this.BannedKeywords.Remove(bannedKeyword);
        }

        /// <summary>
        /// Sirve para aplicar el singleton, verifica si la property ListaUsuarios es nula y si no es nula te devuelve el 
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