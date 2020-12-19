using UnityEngine;
namespace EBomberman{
    /// <summary>
    /// Clase de bomberman
    /// </summary>
    public class Bomberman{
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
        /// <summary>
        /// Constructor de la clase bomberman
        /// </summary>
        /// <param name="pID">String para el id, unico para cada bomberman</param>
        public Bomberman(string pID)
        {
            this.pID = pID;
            hitsPlayer = 0;
            setAcciones();
            reajustarAcciones();
        }
        /// <summary>
        /// Metodo que genera las probabilidades base para cada jugador
        /// </summary>
        private void setAcciones(){
            this.hide = Random.Range(0, 25);
            this.findEnemy = Random.Range(0, 25);
            this.findPowerUp = Random.Range(0, 25);
            this.putBomb = Random.Range(0, 25);
        }
        /// <summary>
        /// Metodo que reajusta las probabilidades de la poblacion, para que esten en 100%
        /// </summary>
        public void reajustarAcciones()
        {
            while (sumaTotal() != 100)
            {
                if (sumaTotal() > 100){
                    sumMayor();
                }else{
                    sumMenor();
                }
            }
        }
        /// <summary>
        /// Metodo que baja las probabilidades de cada jugador, hasta llegar a 100%
        /// </summary>
        public void sumMayor()
        {
            if (sumaTotal() > 100)
            {
                hide--;
            }
            if (sumaTotal() > 100)
            {
                findEnemy--;
            }
            if (sumaTotal() > 100)
            {
                findPowerUp--;
            }
            if (sumaTotal() > 100)
            {
                putBomb--;
            }
        }
        /// <summary>
        /// Metodo que sube las probabilidades de cada jugador, hasta llegar a 100%
        /// </summary>
        public void sumMenor()
        {
            if (sumaTotal() < 100)
            {
                hide++;
            }
            if (sumaTotal() < 100)
            {
                findEnemy++;
            }
            if (sumaTotal() < 100)
            {
                findPowerUp++;
            }
            if (sumaTotal() < 100)
            {
                putBomb++;
            }
        }
        /// <summary>
        /// Suma todas las probabilidades
        /// </summary>
        /// <returns>Un numero </returns>
        public int sumaTotal()
        {
            return findEnemy + hide + putBomb + findPowerUp;
        }
        public void setFindPowerUp(int findPowerUp)
        {
            this.findPowerUp = findPowerUp;
        }
        public void setHide(int hide)
        {
            this.hide = hide;
        }
        public void setPutBomb(int putBomb)
        {
            this.putBomb = putBomb;
        }
        public void setFindEnemy(int findEnemy)
        {
            this.findEnemy = findEnemy;
        }
        public int getHide()
        {
            return hide;
        }
        public int getPutBomb()
        {
            return putBomb;
        }
        public int getFindEnemy()
        {
            return findEnemy;
        }
        public int getFindPowerUp()
        {
            return findPowerUp;
        }
    }
}