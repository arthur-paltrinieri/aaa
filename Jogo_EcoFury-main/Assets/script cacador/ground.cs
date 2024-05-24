using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ground : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o collider que entrou em contato é o do projectile
        if (other.CompareTag("projectile"))
        {
            // Destroi o projectile
            Destroy(other.gameObject);
        }
    }
}
