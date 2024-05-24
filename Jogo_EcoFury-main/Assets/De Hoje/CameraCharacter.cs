using UnityEngine;

public class CameraCharacter : MonoBehaviour
{
    public Transform target; // O objeto que a câmera seguirá
    public float distance = 10.0f; // Distância da câmera do alvo
    public float height = 5.0f; // Altura da câmera acima do alvo
    public float rotationDamping = 3.0f; // Suavização de rotação
    public float heightDamping = 2.0f; // Suavização de altura
    public float mouseSensitivity = 2.0f; // Sensibilidade do mouse

    private float desiredAngle = 0.0f; // Ângulo desejado da câmera
    private float desiredHeight = 0.0f; // Altura desejada da câmera

    void LateUpdate()
    {
        if (!target)
            return;

        // Calcula o ângulo desejado da câmera com base na posição do mouse
        desiredAngle += Input.GetAxis("Mouse X") * mouseSensitivity;

        // Calcula a altura desejada da câmera com base na roda do mouse
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            height -= scroll * mouseSensitivity;
            height = Mathf.Clamp(height, 1.0f, 20.0f); // Limita a altura entre 1 e 20 unidades
        }

        // Calcula a rotação suavizada da câmera
        float currentAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        currentAngle = Mathf.LerpAngle(currentAngle, desiredAngle, rotationDamping * Time.deltaTime);
        currentHeight = Mathf.Lerp(currentHeight, target.position.y + height, heightDamping * Time.deltaTime);

        // Converte o ângulo em uma rotação na vertical
        Quaternion currentRotation = Quaternion.Euler(0, currentAngle, 0);

        // Configura a posição da câmera
        Vector3 pos = target.position - currentRotation * Vector3.forward * distance;
        pos.y = currentHeight;

        // Aplica a nova posição e rotação da câmera
        transform.position = pos;
        transform.LookAt(target);
    }
}
