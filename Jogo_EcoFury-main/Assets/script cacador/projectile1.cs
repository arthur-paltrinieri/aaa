using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class projectile2 : MonoBehaviour
{
    public float speed = 10f;
    public float gravity = 9.81f; // Aceleração da gravidade
    private Rigidbody rb;
    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o collider que entrou em contato é o do jogador
        if (other.CompareTag("Play er"))
        {
            // Restart the scene
            SceneManager.LoadScene("DS3");
        }
    }
    
    void Start()
    {

        rb = GetComponent<Rigidbody>(); // Obtém o componente Rigidbody do objeto
        rb.velocity = transform.forward * speed; // Aplica velocidade inicial ao objeto
    }
    void FixedUpdate()
    {
        // Aplica a força da gravidade ao objeto
        rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
    }
}