using UnityEngine;

public class Disparar : MonoBehaviour
{
    public Transform firePoint;
    public GameObject arrowPrefab;
    public float force = 20f;
    
    private SpriteRenderer spriteRenderer;
    private bool arcoEquipado = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            arcoEquipado = !arcoEquipado;
            spriteRenderer.enabled = arcoEquipado;
        }

        if (arcoEquipado)
        {
            LookAtMouse();

            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
    }

    void LookAtMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    void Shoot()
    {
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * force, ForceMode2D.Impulse);
    }

    public bool GetArcoEquipado()
    {
        return arcoEquipado;
    }
}
