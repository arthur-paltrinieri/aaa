using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerController : MonoBehaviour
{
    // Método chamado quando outro Collider entra neste Collider (gatilho)
    void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto que entrou é o personagem
        if (other.CompareTag("Play er"))
        {
            // Carrega a próxima cena
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

            // Verifica se a próxima cena existe
            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                // Carrega a próxima cena
                SceneManager.LoadScene(nextSceneIndex);
            }
        }
    }
}