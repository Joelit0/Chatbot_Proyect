namespace ChatBotProject
{
    public class Ship
    {
        protected int score;
        protected int large;
        private bool isAlive;

        public Ship(int score, int large)
        {
            this.score = score;
            this.large = large;
        }
        public int Score    // Obtenemos el valor del barco.
        {
            get 
            {
                return this.score;
            }
        }
        public int Large    // Obtenemos el largo del barco.
        {
            get 
            {
                return this.large;
            }
        }
        public bool checkIsAlive(int Large) // Aquí chequeamos el largo del barco, si es igual a cero, quiere decir que ya se hundió completamente. 
        {
            if (Large==0)
            {
                return isAlive = false;
            }
            else
            {
                return isAlive = true;
            }       
        }
    }
}