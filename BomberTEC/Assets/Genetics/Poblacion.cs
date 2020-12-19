using UnityEngine;
using EBomberman;
using EConversiones;
using ELinkedList;
namespace EPoblacion
{
    public class Poblacion
    {
        ///<summary>Lista que almacena todos los Bomberman</summary>>
        public LinkedList people = new LinkedList();
        /// <summary>Toma al mejor Bomberman</summary>>
        private Bomberman BestIndividuo;
        /// <summary> Clase que ayuda a comvertir los int a int[]</summary>>
        private Conversiones help = new Conversiones();
        ///<summary>Clase que me genera un numero aleatorio</summary>>
        private System.Random rnd = new System.Random();
        ///<summary>La posicion en la lista, del mejor bomberman</summary>
        public int posicionBest;
        public Poblacion()
        {
            generarPoblacion();
        }
        /// <summary>
        /// Genera la poblacion inicial de bombermans 
        /// </summary>
        public void generarPoblacion()
        {
            for (int i = 0; i < 7; i++)
            {
                Bomberman player = new Bomberman($"P{i}");
                people.addData(player);
            }
            posicionBest = 0;
            BestIndividuo = people.getData(0);
        }
        /// <summary>
        /// Genera un promedio entre los "hitsPlayer" y "closehit"
        /// </summary>
        /// <returns>Regresa un int, que es el rendimiento del personaje</returns>>
        /// <param name="temp">Un bomberman</param>
        private int fitness(Bomberman temp)
        {
            int rendimiento = (temp.hitsPlayer + temp.closehit) / 2;
            return rendimiento;
        }
        /// <summary>
        /// Metodo que selecciona al mejor jugador
        /// </summary>
        private void seleccion(){
            posicionBest = 0;
            BestIndividuo = people.getData(0);
            for (int j = 0; j < people.cont; j++){
                Bomberman temp = people.getData(j);
                if (fitness(temp) > fitness(BestIndividuo)){
                    BestIndividuo = temp;
                    posicionBest = j;
                }
            }
        }
        /// <summary>
        /// Me cruza al mejor jugador con todos los jugadores restantes, para que sus estadisticas mejoren
        /// </summary>
        private void Crossover()
        {
            //Todos los arrays son de 7 bits, pues es el numero maximo posible
            for (int i = 0; i < people.cont; i++)
            {
                if (posicionBest != i)
                {
                    Bomberman temp = people.getData(i);
                    int FPW = help.auxCruze(BestIndividuo.getFindPowerUp(), temp.getFindPowerUp());
                    people.getData(i).setFindPowerUp(FPW);

                    int HD = help.auxCruze(BestIndividuo.getHide(), temp.getHide());
                    people.getData(i).setHide(HD);

                    int FE = help.auxCruze(BestIndividuo.getFindEnemy(), temp.getFindEnemy());
                    people.getData(i).setFindEnemy(FE);

                    int PB = help.auxCruze(BestIndividuo.getPutBomb(), temp.getPutBomb());
                    people.getData(i).setPutBomb(PB);
                }
            }
        }
        /// <summary>
        /// Me muta a toda la poblacion, para que sus estadisticas se alteren 
        /// </summary>
        private void Multacion()
        {
            int probabilidad = rnd.Next(0, 100);
            if (10 >= probabilidad)
            {
                for (int i = 0; i < people.cont; i++)
                {
                    if (posicionBest != i)
                    {
                        Bomberman temp = people.getData(i);
                        int FPW = help.auxMutacion(temp.getFindPowerUp());
                        people.getData(i).setFindPowerUp(FPW);

                        int HD = help.auxMutacion(temp.getHide());
                        people.getData(i).setHide(HD);

                        int FE = help.auxMutacion(temp.getFindEnemy());
                        people.getData(i).setFindEnemy(FE);

                        int PB = help.auxMutacion(temp.getPutBomb());
                        people.getData(i).setPutBomb(PB);
                    }
                }
            }
        }
        /// <summary>
        /// Me invierte a toda la poblacion, para que sus estadisticas se alteren 
        /// </summary>
        private void Inversion()
        {
            int probabilidad = rnd.Next(0, 100);
            if (5 >= probabilidad)
            {
                for (int i = 0; i < people.cont; i++)
                {
                    if (posicionBest != i)
                    {
                        Bomberman temp = people.getData(i);

                        int FPW = help.auxInversion(temp.getFindPowerUp());
                        people.getData(i).setFindPowerUp(FPW);

                        int HD = help.auxInversion(temp.getHide());
                        people.getData(i).setHide(HD);

                        int FE = help.auxInversion(temp.getFindEnemy());
                        people.getData(i).setFindEnemy(FE);

                        int PB = help.auxInversion(temp.getPutBomb());
                        people.getData(i).setPutBomb(PB);
                    }
                }
            }
        }
        /// <summary>
        /// Me ajusta las probabilidades de cada jugador para que de 100% 
        /// </summary>
        private void Reajustar()
        {
            for (int i = 0; i < people.cont; i++)
            {
                people.getData(i).reajustarAcciones();
            }
        }
        /// <summary>
        /// Me hace el proceso completo del AG; es decir, la seleccion,cruze,mutacion,inversion y reajuste 
        /// </summary>
        public void AG()
        {
            seleccion();
            Crossover();
            Multacion();
            Inversion();
            Reajustar();
        }
    }
}