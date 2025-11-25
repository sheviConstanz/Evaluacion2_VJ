using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class ControladorObjetivoGolpeable : MonoBehaviour
{
    public UnityEvent<float> alGolpearObjetivo;
    public float danioPorDefecto = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Arma"))
        {
            Debug.Log("HIT al dummy por: " + other.name);
            alGolpearObjetivo?.Invoke(danioPorDefecto);
        }
    }
}
