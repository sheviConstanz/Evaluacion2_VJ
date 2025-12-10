using UnityEngine;
using UnityEngine.AI;

public class ControladorEnemigo : MonoBehaviour
{
    [Header("Componentes")]
    public Animator ani;
    public NavMeshAgent agent;

    [Header("Objetivo")]
    public Transform target;         
    private VidaJugador vidaJugador;

    [Header("Detección")]
    public float distanciaDeteccion = 12f; 
    public float distanciaAtaque = 2.5f;    

    [Header("Persecución")]
    public float distanciaOlvido = 30f;     
    private bool haVistoAlJugador = false;

    [Header("Ataque")]
    public float danio = 12f;
    public float tiempoEntreAtaques = 1.2f; 
    private float tiempoProximoAtaque = 0f; 

    void Start()
    {
        if (ani == null)
            ani = GetComponent<Animator>();

        if (agent == null)
            agent = GetComponent<NavMeshAgent>();

        GameObject playerGO = GameObject.FindWithTag("Player");
        if (playerGO != null)
        {
            target = playerGO.transform;
            vidaJugador = playerGO.GetComponent<VidaJugador>();
        }
        else
        {
            Debug.LogError("NO se encontró ningún objeto con tag 'Player'");
        }

        // que se acerque lo suficiente
        agent.stoppingDistance = distanciaAtaque * 0.9f;
    }

    void Update()
    {
        IA();
    }

    void IA()
    {
        if (target == null) return;

        // si el jugador está muerto  enemigo quieto
        if (vidaJugador != null && vidaJugador.estaMuerto)
        {
            ani.SetBool("run", false);
            ani.SetBool("attack", false);
            agent.isStopped = true;
            return;
        }

        float distancia = Vector3.Distance(transform.position, target.position);

        // Aun no ha visto al jugador: solo se activa cuando entras en distanciaDeteccion
        if (!haVistoAlJugador)
        {
            if (distancia <= distanciaDeteccion)
            {
                haVistoAlJugador = true;  
            }
            else
            {
                // sigue en idle
                ani.SetBool("run", false);
                ani.SetBool("attack", false);
                agent.isStopped = true;
                return;
            }
        }

        //  si ya te vio, te olvida solo si estás MUY lejos
        if (haVistoAlJugador && distancia > distanciaOlvido)
        {
            haVistoAlJugador = false;
            ani.SetBool("run", false);
            ani.SetBool("attack", false);
            agent.isStopped = true;
            return;
        }

        //  perseguir o atacar
        if (distancia > distanciaAtaque)
        {
        
            ani.SetBool("run", true);
            ani.SetBool("attack", false);

            agent.isStopped = false;
            agent.SetDestination(target.position);
        }
        else
        {
            // EN RANGO DE ATAQUE
            ani.SetBool("run", false);
            ani.SetBool("attack", true);  

            agent.isStopped = true;

            // mirar hacia el jugador
            Vector3 lookDir = target.position - transform.position;
            lookDir.y = 0;
            if (lookDir != Vector3.zero)
            {
                Quaternion rot = Quaternion.LookRotation(lookDir);
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 10f);
            }

            //aplicar daño cada cierto tiempo mientras esté cerca (quieto o moviéndose)
            if (Time.time >= tiempoProximoAtaque && vidaJugador != null)
            {
                // verifica que siga cerca antes de pegar
                if (Vector3.Distance(transform.position, target.position) <= distanciaAtaque + 0.5f)
                {
                    vidaJugador.RecibirDanio(danio);
                }

                tiempoProximoAtaque = Time.time + tiempoEntreAtaques;
            }
        }
    }

    // para evitar errores si la animación llama a este evento
    public void Final_Ani()
    {
        // no hace nada
    }
}
