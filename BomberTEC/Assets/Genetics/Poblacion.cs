using System;
using Example.Genetics;
using Example.Properties;
using System.Collections.Generic;
namespace Example {
    public class Poblacion {
        public LinkedList people = new LinkedList();
        private Bomberman BestIndividuo;
        Conversiones help = new Conversiones();
        Random rnd = new Random();
        public int posicionBest;
        public Poblacion() {
            generarPoblacion();
        }
        
        public void generarPoblacion(){ 
            for (int i = 0; i < 7; i++) {
                Bomberman player = new Bomberman($"P{i}");
                people.addData(player);
            }
            posicionBest = 0;
            BestIndividuo = people.getData(0);
        }

        private bool sinMejora() {
            Bomberman temp;
            for (int j = 0; j < people.cont; j++) {
                temp = people.getData(j);
                if (fitness(temp) != 0)
                {
                    return false;
                }
            }
            return true;
        }
        private int fitness(Bomberman temp) {
            int rendimiento = (temp.hitsPlayer + temp.closehit) / 2;
            return rendimiento;
        }
        private void seleccion() {
            posicionBest = 0;
            BestIndividuo = people.getData(0);
            if(sinMejora()) {
                posicionBest = rnd.Next(0, people.cont);
                BestIndividuo = people.getData(posicionBest);
            }else{
                for (int j = 0; j < people.cont; j++) {
                    Bomberman temp = people.getData(j);
                    if (fitness(temp) > fitness(BestIndividuo)) {
                        BestIndividuo = temp;
                        posicionBest = j; 
                    }
                }
            }
        }

        private void Crossover() {
            //Todos los arrays son de 7 bits, pues es el numero maximo posible
            for (int i = 0; i < people.cont; i++) {
                if (posicionBest != i){
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
        private void Multacion() {
            int probabilidad = rnd.Next(0, 100);
            if (10 >= probabilidad)
            {
                for (int i = 0; i < people.cont; i++)
                {
                    if (posicionBest != i) {
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

        private void Reajustar() {
            for (int i = 0; i < people.cont; i++) {
                people.getData(i).reajustarAcciones();
            }
        }
        public void printAccion(){
            for (int i = 0; i < people.cont; i++){//$"P{i}"
                people.getData(i).printAccion();
            }
        }

        public void AG(){
            seleccion();
            Crossover();
            Multacion();
            Inversion();
            Reajustar();
        }
    }
}