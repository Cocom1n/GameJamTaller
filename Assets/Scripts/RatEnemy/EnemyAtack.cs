using UnityEngine;

public class EnemyAtack : MonoBehaviour
{
    [SerializeField] private int danio = 10;
    
    [SerializeField] private float tiempoDeRecarga = 1f;
    private float tiempoActual = 0f;
    private bool puedeAtacar = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!puedeAtacar)
        {
            tiempoActual += Time.deltaTime;
            if (tiempoActual >= tiempoDeRecarga)
            {
                puedeAtacar = true;
                tiempoActual = 0f;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(puedeAtacar)
            {
                IDaniable daniable = collision.transform.GetComponent<IDaniable>();
                if(daniable != null)
                {
                    daniable.RecibirDanio(danio);
                }
                puedeAtacar = false;
            }
        }
    }


}
