using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    public float health;
    public float MovementSpeed;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PowerUp"))
        {
            health -= 1;
        } else if (collision.CompareTag("Potion"))
        {
            health += 1;
        } else if (collision.CompareTag("Shoe"))
        {
            MovementSpeed += 3;
        }
    }
}
