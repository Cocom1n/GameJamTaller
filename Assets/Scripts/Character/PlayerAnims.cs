using UnityEngine;

public class PlayerAnims : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private PlayerJump playerJump;
    private Disparar disparar;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerJump = GetComponent<PlayerJump>();
        disparar = GetComponent<Disparar>();    
        anim.SetInteger("State", 0);
    }

    void Update()
    {
        OrientacionSprite();
        ActualizarAnimaciones();
    }

    private void ActualizarAnimaciones()
    {
        // IsFalling logic
        if (rb.linearVelocity.y < -0.1f)
        {
            anim.SetBool("IsFalling", true);
        }
        else
        {
            anim.SetBool("IsFalling", false);
        }

        if (playerJump != null)
        {
            if (playerJump.IsGrounded())
            {
                if (Mathf.Abs(rb.linearVelocity.x) > 0.1f)
                {
                    anim.SetInteger("State", 1);
                }
                else
                {
                    anim.SetInteger("State", 0);
                }
            }
            else
            {
                if (rb.linearVelocity.y > 0.1f)
                {
                    anim.SetInteger("State", 3);
                }
                else
                {
                    anim.SetInteger("State", 0);
                }
            }
        }
        else
        {
             if(Mathf.Abs(rb.linearVelocity.x) > 0.1f){
                anim.SetInteger("State", 1);
            }else{
                anim.SetInteger("State", 0);
            }
        }
        
        if(disparar != null)
        {
            if(disparar.GetArcoEquipado())
            {
                anim.SetInteger("State", 2);
            }
            else
            {
                anim.SetInteger("State", 0);
            }
        }
    }

    private void OrientacionSprite()
    {
        if (rb.linearVelocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (rb.linearVelocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }
}
