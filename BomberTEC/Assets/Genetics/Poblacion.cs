using System;
using Example.Genetics;
using Example.Properties;
namespace Example {
    public class Poblacion {
        private LinkedList people = new LinkedList();
        private Bomberman BestIndividuo;
        Conversiones help = new Conversiones();
        Random rnd = new Random();
        private int posicionBest = 0;
        public Poblacion() {}
        
        private void generarPoblacion() {
            if (people.cont == 0) {
                for (int i = 0; i < 7; i++) {
                    Bomberman player = new Bomberman($"P{i}");
                    people.addData(player);
                }
            }
        }

        private bool sinMejora()
        {
            for (int j = 0; j < people.cont; j++)
            {
                Bomberman temp = people.getData(j);
                if (fitness(temp) != 0)
                {
                    return true;
                }
            }
            return false;
        }

        private int fitness(Bomberman temp)
        {
            int rendimiento = (temp.hitsPlayer + temp.closehit) / 2;
            return rendimiento;
        }

        private void seleccion()
        {
            BestIndividuo = people.getData(0);
            if (!sinMejora())
            {
                posicionBest = rnd.Next(0, 7);
                BestIndividuo = people.getData(posicionBest);
            }
            else
            {
                for (int j = 0; j < people.cont; j++)
                {
                    if (people.getData(j).life != 0)
                    {
                        Bomberman temp = people.getData(j);
                        if (fitness(temp) > fitness(BestIndividuo))
                        {
                            BestIndividuo = temp;
                            posicionBest = j;
                        }
                    }
                }
            }
        }

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
        private void Multacion() {
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
        public void AG(){
            generarPoblacion();
            people.getData(0).hitsPlayer++;
            people.getData(0).printAccion();
            people.getData(1).printAccion();
            seleccion();
            Crossover();
            Multacion();
            Inversion();
            Reajustar();
            people.getData(1).printAccion();
        }
    }
}