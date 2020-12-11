using System;
using System.Reflection;

namespace Example.Genetics {
    public class Conversiones {
        public Conversiones(){}
        /// <summary>
        /// Me pasa un int a su representacion en binario, metido en un int[] 
        /// </summary>
        /// <returns>Regresa un array, con los bits de num</returns>>
        /// <param name="num">Un numero</param>
        public int[] dec_Binario(int num) {
            int[] res = new int[7];
            for(int i=0; num>0; i++){
                res[i]=num%2;      
                num= num/2;
            }return res;
        }
        /// <summary>
        /// Me trasforma un array de bits, a su representacion decimal
        /// </summary>
        /// <returns> Un numero segun el array de bits que le ingrese</returns>>
        /// <param name="num">Un array de bits</param>
        public int bin_Decimal(int[] num) {
            double res=0;
            for (int i = 0; i < num.Length; i++){
                res += num[i]*Math.Pow(2,i);
            }return Convert.ToInt32(res);
        }
        /// <summary>
        ///  Metodo auxiliar de mutacion, donde me cambia el primer bit a un 1
        /// </summary>
        /// <returns>Me regresa el int de la probabilidad de accion, pero con su primer bit cambiado a 1</returns>>
        /// <param name="accion">Un numero</param>
        public int auxMutacion(int accion) {
            int[] binario = dec_Binario(accion);
            binario[0] = 1;
            accion = bin_Decimal(binario);
            return accion;
        }
        /// <summary>
        /// Metodo auxiliar para cruze, donde agarra los int del mejor bomberman con otro bomberman
        /// y cruza sus bits
        /// </summary>
        /// <returns>Un numero, mezclado con best y temp</returns>>
        /// <param name="best">int del mejor bomberman </param>
        /// <param name="temp">int de otro bomberman </param>
        public int auxCruze(int best,int temp) {
            int[] binarioBest = dec_Binario(best);
            int[] binarioTemp = dec_Binario(temp); 
            binarioTemp[0] = binarioBest[4];
            binarioTemp[1] = binarioBest[5];
            binarioTemp[2] = binarioBest[6];
            return bin_Decimal(binarioTemp);
        }
        /// <summary>
        /// Metodo auxiliar para inversion, donde me cambia los 0 por 1 y viceversa, para un int 
        /// </summary>
        /// <returns>Un numero modificado </returns>>
        /// <param name="temp">Un numero</param>
        public int auxInversion(int temp){
            int[] binarioTemp = dec_Binario(temp);
            Random rnd=new Random();
            int max = rnd.Next(0, 7);
            for (int i = 0; i < max; i++) {
                if (binarioTemp[i]==0) {
                    binarioTemp[i] = 1;
                }else{
                    binarioTemp[i] = 0;
                }
            }
            return bin_Decimal(binarioTemp);
        }
    }
}