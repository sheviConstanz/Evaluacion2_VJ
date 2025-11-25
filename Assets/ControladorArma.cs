using UnityEngine;

public class ControladorArma : MonoBehaviour
{
    [Header("Daño del arma")]
    public float danio = 25f;

    [Header("Estado ataque (lo enciende la animación)")]
    public bool estaAtacando = false;

    private void OnTriggerEnter(Collider other)
    {
        // Si NO está atacando, no hace nada
        if (!estaAtacando) return;

        // Busca si el otro tiene un objetivo golpeable
        var objetivo = other.GetComponent<ControladorObjetivoGolpeable>();
        if (objetivo != null)
        {
            // Dispara el evento con el daño del arma
            objetivo.alGolpearObjetivo.Invoke(danio);
        }
    }

    // Estas dos funciones serán llamadas por la animación
    public void ComenzarAtaque()
    {
        estaAtacando = true;
    }

    public void TerminarAtaque()
    {
        estaAtacando = false;
    }
}
