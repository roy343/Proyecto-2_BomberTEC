using UnityEngine;
using EStar;
using UnityEngine.Tilemaps;
using System.Collections;

public class EnemyScript : MonoBehaviour
{
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
    public string pID;
    public int hide;
    public int putBomb;
    public int findEnemy;
    public int findPowerUp;
    public Tile wallTile;
    public Tile barrel;
    public GameObject expPrefab;
    public Tilemap tilemap;
    public GameObject Bomb;

    [SerializeField]
    public Admi Administrador;



    /// <summary> 
    /// Metodo que me regresa una accion por estadistica 
    /// </summary> 
    /// <returns>Regresa: 1 hide, 2 find power up,3 find enemy y 4 put bomb  </returns> 
    public int getAccion()
    {
        int probabilidad = Random.Range(0, 100);
        if (probabilidad < hide)
        {//1 Para hide 
            return 1;
        }
        else if (probabilidad < hide + findPowerUp)
        {// 2 Para Find power up  
            return 2;
        }
        else if (probabilidad < hide + findPowerUp + findEnemy)
        {// 3 Para find enemy 
            return 3;
        }
        else
        {
            return 4;//4 Para Put bomb 
        }
    }

    /// <summary> 
    /// /Pruebas 
    /// </summary> 
    [SerializeField] private Vector3 target= new Vector3(1, 1, 0);
    [SerializeField] private float speed = 5f;
    
    int accion;
    public bool primeravez;
    float posix;
    float posiy;
    // Start is called before the first frame update 
    void Start(){
        accion= getAccion();
        primeravez = true;
        posix = GameObject.Find("Player").transform.position.x;
        posiy = GameObject.Find("Player").transform.position.y;
    }
    public float period = 5;
    public float seconds = 0;
    public float timer;
     
    void Update(){
        death();
        if (timer <= seconds)
        {
            if (accion == 1)
            {
                target.Set(0, 0, 0);
                transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
            }else if (accion == 2){
                target.Set(posix, posiy, 0);
                transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
            }
            else if (accion == 3)
            {
                target.Set(9, 5, 0);
                transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
            }
            else{
                if (primeravez){
                    plantBomb();
                    primeravez = false;
                }target.Set(9, 5, 0);
                transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
            }timer += Time.deltaTime;
        }
        else{
            posix = GameObject.Find("Player").transform.position.x;
            posiy = GameObject.Find("Player").transform.position.y;
            seconds = Time.time + period;
            accion = getAccion();
            primeravez = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Explosion") || maxLife > 1)
        {
            maxLife -= 1;
        }
        if (collision.CompareTag("Potion"))
        {
            maxLife += 1;
        }
        if (collision.CompareTag("Shoe"))
        {
            moveSpeed += 3;
        }
    }

    public void Explode(Vector2 worldPos)
    {
        Vector3Int originCell = tilemap.WorldToCell(worldPos);

        ExplodeCells(originCell);
        if (ExplodeCells(originCell + new Vector3Int(1, 0, 0)))
        {
            ExplodeCells(originCell + new Vector3Int(2, 0, 0));
        }
        if (ExplodeCells(originCell + new Vector3Int(0, 1, 0)))
        {
            ExplodeCells(originCell + new Vector3Int(0, 2, 0));
        }
        if (ExplodeCells(originCell + new Vector3Int(-1, 0, 0)))
        {
            ExplodeCells(originCell + new Vector3Int(-2, 0, 0));
        }
        if (ExplodeCells(originCell + new Vector3Int(0, -1, 0)))
        {
            ExplodeCells(originCell + new Vector3Int(0, -2, 0));
        }
    }

    bool ExplodeCells(Vector3Int cell)
    {
        Tile tile = tilemap.GetTile<Tile>(cell);

        if (tile == wallTile)
        {
            return false;
        }
        if (tile == barrel)
        {
            tilemap.SetTile(cell, null);
        }

        Vector3 pos = tilemap.GetCellCenterWorld(cell);
        Instantiate(expPrefab, pos, Quaternion.identity);
        return true;
    }

    void plantBomb()
    {

        Debug.Log("The bomb has been planted");
        Vector3 position = transform.position;
        Vector3Int cell = tilemap.WorldToCell(position);
        Vector3 center = tilemap.GetCellCenterWorld(cell);


        Instantiate(Bomb, center, Quaternion.identity);

    }
    void death()
    {
        if (maxLife <= 0)
        {
            Destroy(gameObject);
        }
    }
}
