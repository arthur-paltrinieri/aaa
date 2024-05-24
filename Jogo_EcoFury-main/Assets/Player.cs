using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController character;
    private Animator animator;
    private Vector3 input;
    private float velocidade = 2.0f;
    private bool correndo = false;
    private float velocidadeCorrida = 4.0f;
    private float gravidade = -9.81f; // Valor negativo para a gravidade

    void Start()
    {
        character = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        input.Set(Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal"));

        // Corrida
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            correndo = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            correndo = false;
        }

        // Movimento do personagem
        if (correndo)
        {
            character.Move(input * velocidadeCorrida * Time.deltaTime);
        }
        else
        {
            character.Move(input * velocidade * Time.deltaTime);
        }

        // Aplicando gravidade
        if (!character.isGrounded) // Se o personagem não está no chão
        {
            character.Move(Vector3.up * gravidade * Time.deltaTime);
        }

        // Rotação do personagem
        if (input != Vector3.zero)
        {
            animator.SetBool("andando", true);
            transform.forward = Vector3.Slerp(transform.forward, input, Time.deltaTime * 10);
        }
        else
        {
            animator.SetBool("andando", false);
        }

        // Animação de corrida
        animator.SetBool("correndoParameter", correndo);
    }
}
