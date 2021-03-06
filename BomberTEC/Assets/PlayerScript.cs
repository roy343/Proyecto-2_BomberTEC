﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float health;
    public float MovementSpeed = 5;
    [SerializeField] private Text healthText;

    public void UpdateHealth()
    {
        healthText.text = health.ToString("0");
    }

    void Update()
    {

        UpdateHealth();
        movePlayer();
        if (health == 0)
        {
            Destroy(gameObject);
        }
    }

    void movePlayer()
    {
        var moveX = Input.GetAxis("Horizontal");
        var moveY = Input.GetAxis("Vertical");
        transform.position += new Vector3(moveX, moveY, 0) * Time.deltaTime * MovementSpeed;
    }


}
