using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaController : MonoBehaviour
{
    public float velocidad = 5f;
    public float fuerzaSalto = 10f;
    private bool estaSaltando = false;
    private bool morir = false;

    private const int ANIMATION_QUIETO = 0;
    private const int ANIMATION_CORRER = 1;
    private const int ANIMATION_SALTAR = 2;
    private const int ANIMATION_DISPARAR = 3;
    private const int ANIMATION_MORIR = 4;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;
    public Transform balaTransformDe;
    public Transform balaTransformIz;
    public GameObject bala;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && !estaSaltando)
        {
            rb.velocity = Vector2.up * fuerzaSalto;
            estaSaltando = true;
            CambiarAnimation(ANIMATION_SALTAR);
        }
        
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(velocidad, rb.velocity.y);
            CambiarAnimation(ANIMATION_CORRER);
            sr.flipX = false;
        }
        
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-velocidad, rb.velocity.y);
            CambiarAnimation(ANIMATION_CORRER);
            sr.flipX = true;
        }
        
        else if (!estaSaltando)
        {
            CambiarAnimation(ANIMATION_QUIETO);
            rb.velocity = new Vector2(ANIMATION_QUIETO, rb.velocity.y);
        }
        
        if (morir==true)
        {
            CambiarAnimation(ANIMATION_MORIR);
            rb.velocity = new Vector2(0, rb.velocity.y);
            sr.flipX = false;
        }
        
        if (Input.GetKeyUp(KeyCode.C) && !morir )
        {
            if (!sr.flipX)
            {
                Instantiate(bala, balaTransformDe.position, Quaternion.identity);
                CambiarAnimation(ANIMATION_DISPARAR);
            }
            else
            {
                Instantiate(bala, balaTransformIz.position, Quaternion.identity);
                CambiarAnimation(ANIMATION_DISPARAR);
            }
            
        }
        
        
    }
    private void CambiarAnimation(int animation)
    {
        animator.SetInteger("Estado", animation);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemigo")
        {
            morir = true;
        }

        if (collision.gameObject.tag == "Suelo")
            estaSaltando = false;
        
        }
    
}
