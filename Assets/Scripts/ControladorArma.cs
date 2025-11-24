using UnityEngine;

public class ControladorEspada : MonoBehaviour
{
    [Header("Asigna aquí tus objetos desde la Jerarquía")]
    public GameObject objetoEspada;     // Arrastra aquí tu objeto "Espada"
    public Transform anclaCinto;        // Arrastra aquí tu "AnclaArmaCinto"
    public Transform anclaMano;         // Arrastra aquí tu "Ancla"

    // Variable interna para saber si la tienes en la mano o no
    private bool estaEquipada = false;

    void Update()
    {
        // Detectar si presionas la tecla 'F'
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (estaEquipada)
            {
                Guardar();
            }
            else
            {
                Sacar();
            }
        }
    }

    void Sacar()
    {
        // 1. Cambiamos el padre a la mano (Ancla)
        objetoEspada.transform.SetParent(anclaMano);

        // 2. Reseteamos posición y rotación para que se ajuste exacto al grip
        objetoEspada.transform.localPosition = Vector3.zero;
        objetoEspada.transform.localRotation = Quaternion.identity;

        estaEquipada = true;
        Debug.Log("Espada equipada en mano");
    }

    void Guardar()
    {
        // 1. Cambiamos el padre al cinto (AnclaArmaCinto)
        objetoEspada.transform.SetParent(anclaCinto);

        // 2. Reseteamos posición y rotación
        objetoEspada.transform.localPosition = Vector3.zero;
        objetoEspada.transform.localRotation = Quaternion.identity;

        estaEquipada = false;
        Debug.Log("Espada guardada en el cinto");
    }
}