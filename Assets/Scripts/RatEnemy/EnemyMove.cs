using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private List<Transform> wayPoints;
    private int indiceActual = 0;
    private Transform targetPoint;

    [SerializeField] private float velocidad = 2f;
    [SerializeField] private float jumpForce = 5f;

    [SerializeField] private float obstacleCheckDistance = 2.0f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (wayPoints.Count > 0)
        {
            targetPoint = wayPoints[indiceActual];
            StartCoroutine(FollowPath());
        }
    }

    private void FixedUpdate()
    {
        CheckJump();
    }

    IEnumerator FollowPath()
    {
        while (true)
        {
            targetPoint = wayPoints[indiceActual];

            //Mientras q la distancia del Enemigo este en un rango de o.5 cerca del punto de destino (oscea serquita)
            while (Vector2.Distance(transform.position, targetPoint.position) > 0.5f) 
            {
                Vector2 direction = (targetPoint.position - transform.position).normalized;
                
                if (direction.x > 0) transform.localScale = new Vector3(1, 1, 1);
                else if (direction.x < 0) transform.localScale = new Vector3(-1, 1, 1);

                rb.linearVelocity = new Vector2(direction.x * velocidad, rb.linearVelocity.y);
                
                yield return null;
            }
            indiceActual++;
            if (indiceActual >= wayPoints.Count)
            {
                indiceActual = 0;
            }

            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            yield return new WaitForSeconds(1.0f); 
        }
    }

    

    private void CheckJump()
    {
        float facingDir = Mathf.Sign(transform.localScale.x);
        Vector2 direction = new Vector2(facingDir, 0);
        
        Vector2 origin = (Vector2)transform.position + new Vector2(0, -0.5f);

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, obstacleCheckDistance, groundLayer);
        
        Debug.DrawRay(origin, direction * obstacleCheckDistance, hit.collider != null ? Color.green : Color.red);

        if (hit.collider != null && Mathf.Abs(rb.linearVelocity.y) < 0.01f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }


    public void AddWaypoint(Transform newPoint)
    {
        wayPoints.Add(newPoint);
    }

    public void DetenerMovimiento()
    {
        StopAllCoroutines();
        rb.linearVelocity = Vector2.zero;
        enabled = false; // Desactiva el script para que no siga ejecutando FixedUpdate
    }
}
