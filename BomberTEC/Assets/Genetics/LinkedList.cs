namespace Example.Properties
{
    public class LinkedList
    {
        private Nodo first;
        private Nodo last;
        public int cont;
        public LinkedList() {
            first = null;
            last = null;
            cont = 0;
        }
        public void addData(Bomberman jugador) {
            Nodo temp= new Nodo();
            temp.data = jugador;
            if (first==null) {
                first = temp;
                last = temp;
            }
            else {
                last.Next = temp;
                last = temp;
            }
            cont++;
        }
        public Bomberman getData(int index) {
            Nodo temp = first;
            for (int i = 0; i < cont; i++){
                if (i==index) {
                    return temp.data;
                }
                temp=temp.Next;
            }
            return null;
        }
        public void deleteData(int index){
            Nodo temp = first;
            for (int i = 0; i < cont; i++){
                if (i==index) {
                    temp.data = null;
                    cont--;
                }
                temp=temp.Next;
            }
        }
    }
    public class Nodo {
        public Bomberman data;
        public Nodo Next;
        public Nodo() {
            Next = null;
        }
        public Nodo(Bomberman jugador) {
            data = jugador;
            Next = null;
        }
    }
}