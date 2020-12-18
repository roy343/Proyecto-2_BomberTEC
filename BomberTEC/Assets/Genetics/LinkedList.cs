using EBomberman;
namespace ELinkedList
{
    /// <summary>
    /// Clase de la lista enlasada
    /// </summary>
    public class LinkedList
    {
        /// <summary>
        /// Primer nodo
        /// </summary>
        private Nodo first;
        /// <summary>
        /// Ultimo nodo
        /// </summary>
        private Nodo last;
        /// <summary>
        /// Contador de la lista
        /// </summary>
        public int cont;
        /// <summary>
        /// Constructor de la lista enlasada
        /// </summary>
        public LinkedList()
        {
            first = null;
            last = null;
            cont = 0;
        }
        /// <summary>
        /// Metodo que agrega un bomberman a la lista
        /// </summary>
        /// <param name="jugador">Un bomberman</param>
        public void addData(Bomberman jugador)
        {
            Nodo temp = new Nodo();
            temp.data = jugador;
            if (first == null)
            {
                first = temp;
                last = temp;
            }
            else
            {
                last.Next = temp;
                last = temp;
            }
            cont++;
        }
        /// <summary>
        /// Metodo que me regresa un bomberman, al pedirlo por index
        /// </summary>
        /// <param name="index">Un numero</param>
        /// <returns>Un bomberman</returns>
        public Bomberman getData(int index)
        {
            Nodo temp = first;
            for (int i = 0; i < cont; i++)
            {
                if (i == index)
                {
                    return temp.data;
                }
                temp = temp.Next;
            }
            return null;
        }
        /// <summary>
        /// Metodo que me elimina un nodo de la lista
        /// </summary>
        /// <param name="id">Un string que es el id, unico de cada bomberman</param>
        public void deleteNode(string id)
        {
            Nodo current = first;
            Nodo temp = null;
            if (current != null && current.data.pID == id)
            {
                first = current.Next;
                cont--;
                return;
            }
            while (current != null && current.data.pID != id)
            {
                temp = current;
                current = current.Next;
            }
            if (current == null)
            {
                cont--;
                return;
            }
            cont--;
            temp.Next = current.Next;
        }
    }
    /// <summary>
    /// Clase del nodo para la lista
    /// </summary>
    public class Nodo
    {
        /// <summary>
        /// Data per se del nodo
        /// </summary>
        public Bomberman data;
        /// <summary>
        /// El siguiente nodo, dado por referencia
        /// </summary>
        public Nodo Next;
        /// <summary>
        /// Constructor de la clase nodo
        /// </summary>
        public Nodo()
        {
            Next = null;
        }/// <summary>
         /// Constructor de la clase nodo
         /// </summary>
         /// <param name="jugador">Un bomberman</param>
        public Nodo(Bomberman jugador)
        {
            data = jugador;
            Next = null;
        }
    }
}
