using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class ControladorObjetivoGolpeable : MonoBehaviour
{
    [Tooltip("Evento que se dispara cuando este objeto es golpeado. El float es el daño.")]
    public UnityEvent<float> alGolpearObjetivo;

    [Tooltip("Daño por golpe si no se especifica otro.")]
    public float danioPorDefecto = 10f;

    private void Reset()
    {
        // Asegura que el collider sea trigger
        var col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Solo reaccionar a objetos con tag "Arma"
        if (other.CompareTag("Arma"))
        {
            float danio = danioPorDefecto;
            alGolpearObjetivo?.Invoke(danio);   // dispara el evento con el daño
            Debug.Log("Impacto en dummy por: " + other.name);
        }
    }
}
