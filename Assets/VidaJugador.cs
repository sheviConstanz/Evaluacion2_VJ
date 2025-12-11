using UnityEngine;
using UnityEngine.UI; 

public class VidaJugador : MonoBehaviour
{
    [Header("Vida")]
    public float vidaMaxima = 100f;
    public float vidaActual;
    public bool estaMuerto = false;

    [Header("UI")]
    public Slider barraVida;

    [Header("Sonido al recibir golpe")]
    public AudioSource audioSource;
    public AudioClip sonidoGolpe;

    void Start()
    {
        vidaActual = vidaMaxima;
        estaMuerto = false;

        // para barra al inicio
        if (barraVida != null)
        {
            barraVida.maxValue = vidaMaxima;
            barraVida.value = vidaActual;
        }
    }

    public void RecibirDanio(float cantidad)
    {
        if (estaMuerto) return;

        vidaActual -= cantidad;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima); 

        Debug.Log("Jugador recibió daño: " + cantidad + " | Vida actual: " + vidaActual);

        if (audioSource != null && sonidoGolpe != null)
            audioSource.PlayOneShot(sonidoGolpe);

        ActualizarBarra();

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    void ActualizarBarra()
    {
        if (barraVida != null)
            barraVida.value = vidaActual;
    }

    void Morir()
    {
        if (estaMuerto) return;

        estaMuerto = true;
        Debug.Log("Jugador muerto");
        ActualizarBarra();  

        
    }
}
