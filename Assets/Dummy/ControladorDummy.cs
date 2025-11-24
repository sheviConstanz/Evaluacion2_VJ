using UnityEngine;

public class ControladorDummy : MonoBehaviour
{
    [Header("Vida del dummy")]
    public float saludMaxima = 100f;
    public float saludActual;

    private void Awake()
    {
        // Al iniciar, la vida actual parte llena
        saludActual = saludMaxima;
    }

    // Esta función la llamará el evento cuando reciba un golpe
    public void RecibirDanio(float cantidad)
    {
        saludActual -= cantidad;
        Debug.Log($"Dummy recibió {cantidad} de daño. Salud actual: {saludActual}");

        if (saludActual <= 0f)
        {
            Morir();
        }
    }

    private void Morir()
    {
        Debug.Log("Dummy destruido");
        // Aquí puedes poner animación de muerte en vez de Destroy
        Destroy(gameObject);
    }
}
