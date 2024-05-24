using UnityEngine;
using UnityEngine.SceneManagement;

public class enemypatrol2 : MonoBehaviour
{
    public float speed = 30f; // Adjust this value to control the patrol speed
    public float maxX = 1068;  // Adjust this value to set the maximum x position

    private bool movingRight = true; // Indicates whether the enemy is moving right or left

    void Update()
    {
        // Calculate the enemy's new position
        Vector3 newPosition = transform.position + new Vector3(speed * (movingRight ? 1 : -1) * Time.deltaTime, 0f, 0f);

        // Check if the new position is within patrol limits
        if (newPosition.x <= -maxX)
        {
            movingRight = true; // Move right
        }
        else if (newPosition.x >= maxX)
        {
            movingRight = false; // Move left
        }

        // Apply the new position
        transform.position = newPosition;
    }

    // Check for collisions with the player
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Play er"))
        {
            // Restart the scene
            SceneManager.LoadScene("DS2");
        }
    }
}