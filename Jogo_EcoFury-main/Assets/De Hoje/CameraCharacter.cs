using UnityEngine;

public class CameraCharacter : MonoBehaviour
{
    public Transform target; // O objeto que a c�mera seguir�
    public float distance = 10.0f; // Dist�ncia da c�mera do alvo
    public float height = 5.0f; // Altura da c�mera acima do alvo
    public float rotationDamping = 3.0f; // Suaviza��o de rota��o
    public float heightDamping = 2.0f; // Suaviza��o de altura
    public float mouseSensitivity = 2.0f; // Sensibilidade do mouse

    private float desiredAngle = 0.0f; // �ngulo desejado da c�mera
    private float desiredHeight = 0.0f; // Altura desejada da c�mera

    void LateUpdate()
    {
        if (!target)
            return;

        // Calcula o �ngulo desejado da c�mera com base na posi��o do mouse
        desiredAngle += Input.GetAxis("Mouse X") * mouseSensitivity;

        // Calcula a altura desejada da c�mera com base na roda do mouse
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            height -= scroll * mouseSensitivity;
            height = Mathf.Clamp(height, 1.0f, 20.0f); // Limita a altura entre 1 e 20 unidades
        }

        // Calcula a rota��o suavizada da c�mera
        float currentAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        currentAngle = Mathf.LerpAngle(currentAngle, desiredAngle, rotationDamping * Time.deltaTime);
        currentHeight = Mathf.Lerp(currentHeight, target.position.y + height, heightDamping * Time.deltaTime);

        // Converte o �ngulo em uma rota��o na vertical
        Quaternion currentRotation = Quaternion.Euler(0, currentAngle, 0);

        // Configura a posi��o da c�mera
        Vector3 pos = target.position - currentRotation * Vector3.forward * distance;
        pos.y = currentHeight;

        // Aplica a nova posi��o e rota��o da c�mera
        transform.position = pos;
        transform.LookAt(target);
    }
}
