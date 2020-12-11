using System;

namespace Example
{
    /// <summary>
    /// Clase de bomberman
    /// </summary>
    public class Bomberman {
        //Acciones
        ///<summary>Clase que me genera un numero aleatorio</summary>>
        Random rnd = new Random();
        ///<summary>Son numeros enteros, que sirven como probabilidades</summary>>
        private int hide;
        private int putBomb;
        private int findPowerUp;
        private int findEnemy;
        //Estadisticas para fitnes
        
        ///<summary>Son numeros enteros, que sirven como estadisiticas para el AG</summary>>
        public int hitsPlayer; //Esta debe de aumentar en 2 
        public int closehit; //Esta debe de aumentar en 1
        
        //Estadisticas del personaje
        public string pID;
        public int posicion;
        public int life;
        public int speed;
        public int dodge;
        public int radioExplo;
        
        /// <summary>
        /// Constructor de la clase bomberman
        /// </summary>
        /// <param name="pID">String para el id, unico para cada bomberman</param>
        public Bomberman(string pID){
            this.pID = pID;
            hitsPlayer = 0;
            setEstadisticas();
            setAcciones();
            reajustarAcciones();
        }
        /// <summary>
        /// Metodo que genera las estadisticas base para cada jugador
        /// </summary>
        private void setEstadisticas() {
            this.life = rnd.Next(0, 5);
            this.speed = rnd.Next(0, 5);
            this.dodge = rnd.Next(0, 5);
            this.radioExplo = rnd.Next(0, 5);
        }
        /// <summary>
        /// Metodo que genera las probabilidades base para cada jugador
        /// </summary>
        private void setAcciones() {
            this.hide = rnd.Next(0,25);
            this.findEnemy = rnd.Next(0, 25);
            this.findPowerUp = rnd.Next(0, 25);
            this.putBomb = rnd.Next(0, 25);
        }
        /// <summary>
        /// Metodo que reajusta las probabilidades de la poblacion, para que esten en 100%
        /// </summary>
        public void reajustarAcciones() {
            while (sumaTotal() != 100) {
                if (sumaTotal() > 100) {
                    sumMayor();
                }else {
                    sumMenor();
                }
            }
        }
        /// <summary>
        /// Metodo que baja las probabilidades de cada jugador, hasta llegar a 100%
        /// </summary>
        public void sumMayor() {
            if (sumaTotal() > 100){
                hide--;
            }if (sumaTotal() > 100){
                findEnemy--;
            }if (sumaTotal() > 100){
                findPowerUp--;
            }if (sumaTotal() > 100) {
                putBomb--;
            }
        }
        /// <summary>
        /// Metodo que sube las probabilidades de cada jugador, hasta llegar a 100%
        /// </summary>
        public void sumMenor() {
            if (sumaTotal() < 100){
                hide++;
            }if (sumaTotal() < 100){
                findEnemy++;
            }if (sumaTotal() < 100){
                findPowerUp++;
            }if (sumaTotal() < 100) {
                putBomb++;
            }
        }
        /// <summary>
        /// Metodo que imprime las probabilidades de accion y id, de cada jugador
        /// </summary>
        public void printAccion(){
            Console.WriteLine("\n");
            Console.WriteLine("ID: "+pID);
            Console.WriteLine("Hide: "+hide.ToString());
            Console.WriteLine("Find Enemy: "+findEnemy.ToString());
            Console.WriteLine("Find power up:"+findPowerUp.ToString());
            Console.WriteLine("Put bomb:"+putBomb.ToString());
            Console.WriteLine("Suma total: "+sumaTotal().ToString());
            Console.WriteLine("\n");
        }
        /// <summary>
        /// Suma todas las probabilidades
        /// </summary>
        /// <returns>Un numero </returns>
        public int sumaTotal() {
            return findEnemy + hide + putBomb + findPowerUp;
        }
        /// <summary>
        /// Metodo que me regresa una accion por estadistica
        /// </summary>
        /// <returns>Regresa: 1 hide, 2 find power up,3 find enemy y 4 put bomb  </returns>
        public int getAccion(){
            int probabilidad = rnd.Next(0, 100);
            if (probabilidad<hide){//1 Para hide
                return 1;
            }else if (probabilidad<hide+findPowerUp){// 2 Para Find power up 
                return 2;
            }else if (probabilidad < hide + findPowerUp + findEnemy) {// 3 Para find enemy
                return 3;
            }else{//5 Para Put bomb
                return 4;
            }
        }
        public void setFindPowerUp(int findPowerUp)
        {
            this.findPowerUp = findPowerUp;
        }
        public void setHide(int hide) {
            this.hide = hide;
        }
        public void setPutBomb(int putBomb) {
            this.putBomb = putBomb;
        }
        public void setFindEnemy(int findEnemy) {
            this.findEnemy = findEnemy;
        }
        public int getHide() {
            return hide;
        }
        public int getPutBomb() {
            return putBomb;
        }
        public int getFindEnemy() {
            return findEnemy;
        }

        public int getFindPowerUp() {
            return findPowerUp;
        }
    }
}