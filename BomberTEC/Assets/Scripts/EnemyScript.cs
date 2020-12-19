using UnityEngine;
using EStar;
public class EnemyScript : MonoBehaviour{
    /// <summary>
    /// Float que define la velocidad del enemigo
    /// </summary>
    public float moveSpeed = 5f;
    /// <summary>
    /// Floar que defina la vida maxima del enemigo
    /// </summary>
    public float maxLife;
    /// <summary>
    /// Float que define la probabilidad que el enemigo esquive una bomba
    /// </summary>
    public float evade;
    /// <summary>
    /// Defina el radio de la explosion que deje
    /// </summary>
    public float explosionRadius;

    private A_star movimiento;
    public string pID;
    private int[,] terrainMap;

    public int hide;
    public int putBomb;
    public int findEnemy;
    public int findPowerUp;

    [SerializeField]
    public Admi Administrador;

    /// <summary>
    /// Metodo que me regresa una accion por estadistica
    /// </summary>
    /// <returns>Regresa: 1 hide, 2 find power up,3 find enemy y 4 put bomb  </returns>
    public int getAccion()
    {
        int probabilidad = Random.Range(0, 100);
        if (probabilidad < hide){//1 Para hide
            return 1;
        }
        else if (probabilidad < hide + findPowerUp){// 2 Para Find power up 
            return 2;
        }
        else if (probabilidad < hide + findPowerUp + findEnemy){// 3 Para find enemy
            return 3;
        }
        else{
            return 4;//4 Para Put bomb
        }
    }

    /// <summary>
    /// /Pruebas
    /// </summary>

    // Start is called before the first frame update
    void Start(){
        movimiento = new A_star(); 
        terrainMap = new int[20, 10];
    }
    private float nextActionTime = 0.0f;
    public float period = 10f;
    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime = Time.time + period;
            int accion = getAccion();
            if (accion == 1){
                movimiento.Do_Astar(terrainMap,0,0,10,10);
                //a_star(,)
                //a_star(terrainMap, 0, 0, 7, 7);
                //Debug.Log("Hide");
            }else if (accion == 2){
                //Debug.Log("Find power up");
            }else if (accion == 2){
                //Debug.Log("Find Enemy");
            }else{

                 Administrador.foo.people.getDataID(pID).hitsPlayer++;
                //Debug.Log("Put Bomb");
            }
        }
    }
}
