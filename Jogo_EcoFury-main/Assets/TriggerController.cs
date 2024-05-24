using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerController : MonoBehaviour
{
    // M�todo chamado quando outro Collider entra neste Collider (gatilho)
    void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto que entrou � o personagem
        if (other.CompareTag("Play er"))
        {
            // Carrega a pr�xima cena
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

            // Verifica se a pr�xima cena existe
            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                // Carrega a pr�xima cena
                SceneManager.LoadScene(nextSceneIndex);
            }
        }
    }
}