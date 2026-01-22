using System.Collections;
using UnityEngine;

public class GetDamageEnemy : MonoBehaviour,IDaniable
{
    [SerializeField] private int vidaMaxima = 10;
    private int vidaActual = 10;
    private bool estaMuerto = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        vidaActual = vidaMaxima;
        estaMuerto = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RecibirDanio(int cantidad)
    {
        vidaActual -= cantidad;
        spriteRenderer.color = Color.red;
        Invoke("RestaurarColor", 0.1f);
        if(vidaActual <= 0)
        {
            Morir();
        }
    }

    public void Morir()
    {
        estaMuerto = true;
        
        RataAnim rataAnim = GetComponent<RataAnim>();
        if (rataAnim != null)
        {
            rataAnim.ActivarAnimacionMuerte();
        }
        
        EnemyMove enemyMove = GetComponent<EnemyMove>();
        if (enemyMove != null)
        {
            enemyMove.DetenerMovimiento();
        }
        
        Destroy(gameObject, 1f);
    }

    private void RestaurarColor()
    {
        spriteRenderer.color = Color.white;
    }
}
