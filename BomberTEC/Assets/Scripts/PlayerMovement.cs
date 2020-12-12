using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// Define la velocidad del jugador
    /// </summary>
    public float MovementSpeed = 5;
    private void Start()
    {

    }


    /// <summary>
    /// Funcion que se encarga del movimiento del jugador
    /// </summary>
    /// <returns>Cambia la posicion del jugador</returns>
    void Update()
    {
        var moveX = Input.GetAxis("Horizontal");
        var moveY = Input.GetAxis("Vertical");
        transform.position += new Vector3(moveX, moveY, 0) * Time.deltaTime * MovementSpeed;
    }

    
}
