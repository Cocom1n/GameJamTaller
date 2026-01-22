using UnityEngine;

public class RataAnim : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private bool estaMuerto = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!estaMuerto)
        {
            ActualizarAnimaciones();
        }
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

    public void ActivarAnimacionMuerte()
    {
        estaMuerto = true;
        anim.SetInteger("State", 2);
    }
}
