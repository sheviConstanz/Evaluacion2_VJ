using UnityEngine;
using UnityEngine.Events;

public class ControladorArma : MonoBehaviour
{
    public ControladorObjetivoGolpeable objetivoGolpeado;
    public float ataque = 5f;


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Golpeable")
        {
            Debug.Log("GOLPEE ALGO");
            objetivoGolpeado = other.gameObject.GetComponent<ControladorObjetivoGolpeable>();
            objetivoGolpeado.alGolpearObjetivo.Invoke(ataque);
        }
    }
}
