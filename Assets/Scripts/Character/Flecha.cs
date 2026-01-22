using UnityEngine;

public class Flecha : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private int danio = 10;
    [SerializeField] private float tiempoDeVida = 3f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * speed;        
        Destroy(gameObject, tiempoDeVida);
    }

    void OnCollisionEnter2D(Collision2D hitInfo)
    {
        if(hitInfo.gameObject.CompareTag("Enemy"))
        {
            IDaniable daniable = hitInfo.transform.GetComponent<IDaniable>();
            if(daniable != null)
            {
                daniable.RecibirDanio(danio);
                Destroy(gameObject); 
            }
        }
        
    }
}
