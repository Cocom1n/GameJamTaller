using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody2D rb;
    [Header("Parametros")]
    [SerializeField] private bool doubleJump;
    [SerializeField] private int saltosAdicionales = 1;
    [SerializeField] private bool coyoteTime;
    [SerializeField] private float valorCoyote = 0.1f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float velocidadCaida = 5f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Colicion piso")]
    [SerializeField] private float rCircle;
    [SerializeField] private Vector2 posCircle;

    private bool isGrounded;
    private float coyoteTimer;
    private float gravedadInicial;
    private int saltosRestantes;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gravedadInicial = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        // Hacemos un colisionador circular desde la parte inferior del personaje para detectar la colision con la capa suelo
        isGrounded = Physics2D.OverlapCircle((Vector2)transform.position + posCircle, rCircle, groundLayer);
        CaidaRapida();

        if (isGrounded)
        {
            coyoteTimer = valorCoyote;
            saltosRestantes = saltosAdicionales;
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                Jump();
            }
            else if (coyoteTime && coyoteTimer > 0)
            {
                Jump();
                coyoteTimer = 0;
            }
            else if (doubleJump && saltosRestantes > 0)
            {
                Jump();
                saltosRestantes--;
            }
        }
    }

    public void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    public void CaidaRapida()
    {
        if (rb.linearVelocity.y < 0)
        {
            rb.gravityScale = gravedadInicial * velocidadCaida;
        }
        else
        {
            rb.gravityScale = gravedadInicial;
        }
    }

    // Visualiza el colisionador en Scene para depuración
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(new Vector2(transform.position.x + posCircle.x, transform.position.y + posCircle.y), rCircle);
    }

}
