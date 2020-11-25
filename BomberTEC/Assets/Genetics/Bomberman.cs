using System;

namespace Example
{
    public class Bomberman {
        //Acciones
        private int hide;
        private int putBomb;
        private int findPowerUp;
        private int findEnemy;//Unicas variables del genetico
        
        //Estadisticas para fitnes
        public int hitsPlayer; //Esta debe de aumentar en 2 
        public int closehit; //Esta debe de aumentar en 1
        
        //Estadisticas del personaje
        public string pID;
        public int life;
        public int speed;
        public int dodge;
        public int radioExplo;
        
        public Bomberman(string pID){
            this.pID = pID;
            hitsPlayer = 0;
            setEstadisticas();
            setAcciones();
            reajustarAcciones();
        }

        private void setEstadisticas() {
            Random rnd = new Random();
            this.life = rnd.Next(0, 5);
            this.speed = rnd.Next(0, 5);
            this.dodge = rnd.Next(0, 5);
            this.radioExplo = rnd.Next(0, 5);
        }
        private void setAcciones() {
            Random rnd = new Random();
            this.hide = rnd.Next(0,25);
            this.findEnemy = rnd.Next(0, 25);
            this.findPowerUp = rnd.Next(0, 25);
            this.putBomb = rnd.Next(0, 25);
        }
        public void reajustarAcciones() {
            while (sumaTotal() != 100) {
                if (sumaTotal() > 100) {
                    sumMayor();
                }else {
                    sumMenor();
                }
            }
        }
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

        public void printAccion(){
            Console.WriteLine("\n");
            Console.WriteLine("Hide: "+hide.ToString());
            Console.WriteLine("Find Enemy: "+findEnemy.ToString());
            Console.WriteLine("Find power up:"+findPowerUp.ToString());
            Console.WriteLine("Put bomb:"+putBomb.ToString());
            Console.WriteLine("Suma total: "+sumaTotal().ToString());
            Console.WriteLine("\n");
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
        public int sumaTotal()
        {
            return findEnemy + hide + putBomb + findPowerUp;
        }
    }
}

//
// double val = (double)rand() / RAND_MAX;
//
// int random;
// if (val < p_1)
//     random = n_1;
// else if (val < p_1 + p_2)
//     random = n_2;
// else if (val < p_1 + p_2 + p_3)
//     random = n_3;
// else
//     random = n_4;

