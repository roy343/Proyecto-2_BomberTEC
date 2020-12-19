using UnityEngine;
using EPoblacion;
using EBomberman;
using EListEnemy;
public class Admi : MonoBehaviour
{
    // Start is called before the first frame update
    public Poblacion foo;
    private ListEnemy PeopleEnemy;

    public EnemyScript enemy0;//"P0"
    public EnemyScript enemy1;//"P1"
    public EnemyScript enemy2;//"P2"
    public EnemyScript enemy3;//"P3"
    public EnemyScript enemy4;//"P4"
    public EnemyScript enemy5;//"P5"
    public EnemyScript enemy6;//"P6"
    void MeterDatos(){
        for (int i = 0; i < foo.people.cont; i++){
            //if (PeopleEnemy.getData(i).maxLife!=0){
                Bomberman temp = foo.people.getData(i);
                PeopleEnemy.getData(i).pID = temp.pID;
                PeopleEnemy.getData(i).hide = temp.getHide();
                PeopleEnemy.getData(i).putBomb = temp.getPutBomb();
                PeopleEnemy.getData(i).findEnemy = temp.getFindEnemy();
                PeopleEnemy.getData(i).findPowerUp = temp.getFindPowerUp();
            //}
        }
    }
    void doList(){
        PeopleEnemy.addData(enemy0);
        PeopleEnemy.addData(enemy1);
        PeopleEnemy.addData(enemy2);
        PeopleEnemy.addData(enemy3);
        PeopleEnemy.addData(enemy4);
        PeopleEnemy.addData(enemy5);
        PeopleEnemy.addData(enemy6);
    }

    void Awake(){
        PeopleEnemy = new ListEnemy();
        foo = new Poblacion();
    }
    void Start(){
        doList();
        MeterDatos();
    }

    // Update is called once per frame
    private float nextActionTime = 0.0f;
    public float period = 5f;
    void Update(){
        if (Time.time > nextActionTime){
            nextActionTime = Time.time + period;
            Debug.Log("Recalculando");
            foo.AG();
            MeterDatos();
        }
    }
}
