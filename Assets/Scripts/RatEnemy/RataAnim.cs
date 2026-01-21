using UnityEngine;

public class RataAnim : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ActualizarAnimaciones();
    }

    private void ActualizarAnimaciones()
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
}
