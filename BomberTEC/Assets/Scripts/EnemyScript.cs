using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public Admi Administrador;
    /// <summary>
    /// Metodo que me regresa una accion por estadistica
    /// </summary>
    /// <returns>Regresa: 1 hide, 2 find power up,3 find enemy y 4 put bomb  </returns>
    public int getAccion(){
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
        else{//5 Para Put bomb
            return 4;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private float nextActionTime = 0.0f;
    public float period = 5f;
    // Update is called once per frame
    void Update() { 
        if (Time.time > nextActionTime)
        {
            nextActionTime = Time.time + period;
            int accion = getAccion();
            if (accion == 1)
            {
                Debug.Log("Hide");
            }
            else if (accion == 2)
            {
                Debug.Log("Find power up");
            }
            else if (accion == 2)
            {
                Debug.Log("Find Enemy");
            }else {
                Debug.Log("Put Bomb");
            }
            // execute block of code here/ 
        }
    }
}
