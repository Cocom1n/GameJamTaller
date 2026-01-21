using System.Collections;
using UnityEngine;
using TMPro;

public class GetDamage : MonoBehaviour,IDaniable
{
    [SerializeField] private int vidaMaxima = 100;
    private int vidaActual = 100;
    private bool estaMuerto = false;
    [SerializeField] private TextMeshProUGUI textoVida;
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
        if(!estaMuerto && textoVida != null)
        {
            textoVida.text = vidaActual.ToString("0");
        }
    }

    public void RecibirDanio(int cantidad)
    {
        vidaActual -= cantidad;
        spriteRenderer.color = new Color32(255, 184, 184, 212);
        Invoke("RestaurarColor", 0.1f);
        if(vidaActual <= 0)
        {
            Morir();
        }
    }

    public void Morir()
    {
        estaMuerto = true;
        if(textoVida != null)
        {
            textoVida.text = "0";
        }
        Destroy(gameObject);
    }

    private void RestaurarColor()
    {
        spriteRenderer.color = Color.white;
    }
}
