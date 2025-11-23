using UnityEngine;

public class WeaponEquipController : MonoBehaviour
{
    [Header("Weapon Objects")]
    public GameObject weaponOnHip;   // espada en la cadera
    public GameObject weaponInHand;  // espada en la mano

    [Header("References")]
    public Animator animator;

    bool isArmed = false;

    void Start()
    {
        SetArmed(false); // empezamos con el arma guardada
    }

    void Update()
    {
        // tecla para equipar / guardar
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleWeapon();
        }
    }

    void ToggleWeapon()
    {
        SetArmed(!isArmed);
    }

    void SetArmed(bool armed)
    {
        isArmed = armed;

        if (weaponOnHip != null)
            weaponOnHip.SetActive(!armed);

        if (weaponInHand != null)
            weaponInHand.SetActive(armed);

        if (animator != null)
            animator.SetBool("IsArmed", armed);
    }
}

