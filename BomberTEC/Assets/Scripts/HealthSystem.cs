using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    /// <summary>
    /// Lista de objetos
    /// </summary>
    public GameObject[] hearts;

    /// <summary>
    /// Entero que representa la vida de los jugadores
    /// </summary>
    public int life;


    /// <summary>
    /// Funcion que se encarga de administrar la vida del jugador
    /// </summary>
    /// <returns>Elimina o agrega un valor a la lista de vidas</returns>
    void Update()
    {
        if (life < 1)
        {
            Destroy(hearts[0].gameObject);
           
        }
        else if (life < 2)
        {
            Destroy(hearts[1].gameObject);
        }
        else if (life < 3)
        {
            Destroy(hearts[2].gameObject);
        }
    }

    public void Damage(int d)
    {
        life -= d;
    }
}