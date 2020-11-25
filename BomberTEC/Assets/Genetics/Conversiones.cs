using System;
using System.Reflection;

namespace Example.Genetics {
    public class Conversiones {
        public Conversiones(){}
        public int[] dec_Binario(int num) {
            int[] res = new int[7];
            for(int i=0; num>0; i++){
                res[i]=num%2;      
                num= num/2;
            }return res;
        }
        public int bin_Decimal(int[] num) {
            double res=0;
            for (int i = 0; i < num.Length; i++){
                res += num[i]*Math.Pow(2,i);
            }return Convert.ToInt32(res);
        }
        public int auxMutacion(int accion) {
            int[] binario = dec_Binario(accion);
            binario[0] = 1;
            accion = bin_Decimal(binario);
            return accion;
        }
        public int auxCruze(int best,int temp) {
            int[] binarioBest = dec_Binario(best);
            int[] binarioTemp = dec_Binario(temp); 
            binarioTemp[0] = binarioBest[4];
            binarioTemp[1] = binarioBest[5];
            binarioTemp[2] = binarioBest[6];
            return bin_Decimal(binarioTemp);
        }
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