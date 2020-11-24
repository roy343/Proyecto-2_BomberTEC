using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float increase = 10f;

    private void OnTriggerEnter2D(Collider2D collision) {
            Destroy(gameObject);    
    }


}
