using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terst : MonoBehaviour
{
    // variáveis do Animator
    private Animator animator;
    private bool andando = false;
    private bool correndoParameter = false;

    // variável que determina a velocidade do player
    public float velocity = 5f;
    public float velocityRun = 7f;

    // variável que determina se esta ou não tocando o chão
    bool isGrounded = false;
    // variável que determina a força do pulo
    public float jumpforce = 5.0f;
    // variável que determina a massa do personagem
    public float mass = 5.0f;
    // variável da classe de rigidbody
    private Rigidbody rigidbody;

    void Start()
    {
        // a variável de rigidbody recebe o rigidbody do player
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.mass = mass;

        // obtém o componente Animator
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // variáveis que determina as teclas e a direção do player
        float x = Input.GetAxis("Vertical");
        float y = Input.GetAxis("Horizontal");

        // Verifica se o player está se movendo para frente ou para os lados
        andando = x != 0f || y != 0f;

        // Verifica se o player está correndo
        correndoParameter = Input.GetKey(KeyCode.LeftShift) && andando;

        // Atualiza os parâmetros de animação
        animator.SetBool("andando", andando);
        animator.SetBool("correndoParameter", correndoParameter);

        // Calcula a direção do movimento do jogador
        Vector3 direction = new Vector3(x, 0, y).normalized;

        // Move o personagem na direção do movimento
        Vector3 movement = direction * (correndoParameter ? velocityRun : velocity) * Time.deltaTime;
        transform.Translate(movement, Space.World);

        // Rotaciona o personagem na direção do movimento (exceto se não estiver se movendo)
        if (andando)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.1f); // Ajuste o último parâmetro conforme necessário
        }

        // verifica se o player tem condição de pular, sendo elas, pressionar a tecla de pulo e estar no chão
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // adiciona uma força pra cima no rigidbody do player
            rigidbody.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // verifica se o player teve contato com o objeto "ground"
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = false;
        }
    }
}
