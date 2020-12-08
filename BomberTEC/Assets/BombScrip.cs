using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScrip : MonoBehaviour
{

    public float timer = 2f;

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Debug.Log("BOOM!");
            Destroy(gameObject);
        }
    }
}
