using System;
namespace Example {
    internal class Program {
        public static void Main(string[] args){ 
            Poblacion foo=new Poblacion();
            foo.people.getData(5).hitsPlayer=6;
            foo.people.getData(3).hitsPlayer=2;
            foo.AG();
            foo.printAccion();
            foo.people.deleteNode("P0");
            foo.people.deleteNode("P2");
            foo.people.deleteNode("P5");
            Console.WriteLine("---------------------------------------------------------------");
            foo.AG();
            foo.printAccion();
        }
    }
}