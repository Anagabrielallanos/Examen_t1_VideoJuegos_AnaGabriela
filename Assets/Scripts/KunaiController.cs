using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunaiController : MonoBehaviour
{
    public float velocidad = 10f;
    private Rigidbody2D rb;
    private GameObject player;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        Destroy(gameObject, 5f);
        player = GameObject.FindGameObjectWithTag("Player");

        if (!player.GetComponent<SpriteRenderer>().flipX)
        {
            rb.velocity = new Vector2(velocidad, rb.velocity.y);
            sr.flipX = false; 
        }
        else
        {
            rb.velocity = new Vector2(-velocidad, rb.velocity.y);
            sr.flipX = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemigo")
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
