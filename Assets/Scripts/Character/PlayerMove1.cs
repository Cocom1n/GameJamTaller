using UnityEngine;

public class PlayerMove1 : MonoBehaviour
{
    private Rigidbody2D rb;
    [Header("1: Movimiento directo, 2: Movimiento suave")]
    [SerializeField] private int tiposDeMovimiento = 1;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float acceleration = 10.0f;
    [SerializeField] private float deceleration = 10.0f;
    private Vector2 currentVelocity;
    private Vector2 movementInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical"); 
        
        movementInput = new Vector2(x, y);   
    }
    
    void FixedUpdate()
    {
        if (tiposDeMovimiento == 1)
        {
            movimientoDirecto();
        }
        else if (tiposDeMovimiento == 2)
        {
            movimientoSuave();
        }
    }

    private void movimientoDirecto()
    {
        rb.linearVelocity = new Vector2(movementInput.x * speed, rb.linearVelocity.y);
    }

    private void movimientoSuave()
    {
        Vector2 targetVelocity = movementInput * speed;
        float lerpFactor = 1f - Mathf.Exp(-acceleration * Time.deltaTime);
        currentVelocity = Vector2.Lerp(currentVelocity, targetVelocity, lerpFactor);

        if (movementInput == Vector2.zero)
        {
            float decelFactor = 1f - Mathf.Exp(-deceleration * Time.deltaTime);
            currentVelocity = Vector2.Lerp(currentVelocity, Vector2.zero, decelFactor);
        }

        rb.linearVelocity = new Vector2(currentVelocity.x, rb.linearVelocity.y);
    }
}
