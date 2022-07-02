using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;

namespace ChatBotProject
{
    /// <summary>
    /// Clase singleton para permitir tener acceso a el bot de telegram y sus métodos 
    /// en cualquier parte del programa.
    /// </summary>
    public class TelegramBot
    {       
        

        /// <summary>
        /// El bot de telegram al que podremos acceder globalmente gracias al singleton.
        /// </summary>
        /// <value></value>
        public TelegramBotClient botClient {get; set;}

        /// <summary>
        /// Instancia privada y estática necesaria para aplicar Singleton.
        /// </summary>
        private static TelegramBot _instance;
                
        /// <summary>
        /// Constructor privado, necesario para aplicar Singleton, asegurando que solo haya una instancia creada de esta clase.
        /// </summary>
        private TelegramBot()
        {
            this.botClient = new TelegramBotClient("5466014301:AAG2N5FeZRHo4xFQq0dAS9LG24onUy0Ng00");
        }


        /// <summary>
        /// Sirve para aplicar el singleton, verifica si la propiedad TelegramBot es nula y si no es nula te devuelve el 
        /// valor de la propiedad.
        /// </summary>
        /// <returns></returns>
        public static TelegramBot GetInstance()
        {
            if (_instance == null)
            {
                _instance = new TelegramBot();
            }
            return _instance;
        }
    }
}