using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float MovementSpeed = 5;
    private void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        var moveX = Input.GetAxis("Horizontal");
        var moveY = Input.GetAxis("Vertical");
        transform.position += new Vector3(moveX, moveY, 0) * Time.deltaTime * MovementSpeed;
    }

    
}
