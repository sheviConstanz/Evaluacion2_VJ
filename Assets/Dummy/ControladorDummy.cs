using System.Collections;
using UnityEngine;

public class ControladorDummy : MonoBehaviour
{
    [Header("Vida del dummy")]
    public float saludMaxima = 100f;
    public float saludActual;

    [Header("Componentes")]
    public Animator animator;        // arrastra el Animator aquí
    public AudioSource audioSource;  // arrastra el AudioSource aquí (o se buscará solo)
    public Collider col;             // collider principal del dummy

    [Header("Sonidos")]
    public AudioClip sonidoGolpe;    // choque espada-dummy
    public AudioClip sonidoMuerte;   // sonido al morir

    [Header("Respawn")]
    public float tiempoRespawn = 5f; // segundos para reaparecer

    bool estaMuerto = false;
    Renderer[] renderers;            // para ocultar/mostrar el dummy

    private void Awake()
    {
        saludActual = saludMaxima;

        if (animator == null)
            animator = GetComponent<Animator>();

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        if (col == null)
            col = GetComponent<Collider>();

        // todos los renderers del dummy y sus hijos
        renderers = GetComponentsInChildren<Renderer>();
    }

    // FUNCIÓN que llama el evento alGolpearObjetivo(float)
    public void RecibirDanio(float cantidad)
    {
        if (estaMuerto) return;

        saludActual -= cantidad;
        Debug.Log($"Dummy recibió {cantidad} de daño. Salud actual: {saludActual}");

        // 🔹 Animación de golpe
        if (animator != null)
            animator.SetTrigger("golpe");   // necesitas el Trigger "golpe" en el Animator

        // 🔹 Sonido de golpe
        if (audioSource != null && sonidoGolpe != null)
            audioSource.PlayOneShot(sonidoGolpe);

        if (saludActual <= 0f)
        {
            Morir();
        }
    }

    private void Morir()
    {
        if (estaMuerto) return;
        estaMuerto = true;

        Debug.Log("Dummy muerto");

        // 🔹 Animación de muerte (si tienes un estado con Trigger "Die")
        if (animator != null)
            animator.SetTrigger("Die");   // si no tienes anim de muerte, puedes comentar esta línea

        // 🔹 Sonido de muerte
        if (audioSource != null && sonidoMuerte != null)
            audioSource.PlayOneShot(sonidoMuerte);

        // en vez de Destroy -> empezamos rutina de respawn
        StartCoroutine(RespawnRutina());
    }

    private IEnumerator RespawnRutina()
    {
        // 1) Esperamos un poquito para que se vea la anim de muerte (ajusta si quieres)
        yield return new WaitForSeconds(1f);

        // 2) Desactivamos collider y apariencia = "desaparece"
        if (col != null)
            col.enabled = false;

        CambiarVisibilidad(false);

        // 3) Esperamos el tiempo de respawn
        yield return new WaitForSeconds(tiempoRespawn);

        // 4) Reseteamos vida y estado
        saludActual = saludMaxima;
        estaMuerto = false;

        // 5) Volvemos a activar collider y apariencia
        if (col != null)
            col.enabled = true;

        CambiarVisibilidad(true);

        // 6) Forzamos al estado idle al reaparecer
        if (animator != null)
            animator.Play("dummyEstatico", 0, 0f); // usa el nombre exacto de tu clip idle
    }

    private void CambiarVisibilidad(bool visible)
    {
        if (renderers == null) return;

        foreach (var r in renderers)
        {
            if (r != null)
                r.enabled = visible;
        }
    }
}
