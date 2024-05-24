using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int counter;
    public GameObject[] Carros;
    public Vector3 spawnPosition = new Vector3(1071f, 0.1f, 1507f); // Posição de spawn fixa

    void Start()
    {
        InvokeRepeating("SpawnCar", 0, 2f);
    }

    public void SpawnCar()
    {
        if (counter == 0)
        {
            CancelInvoke("SpawnCar");
            return;
        }

        Quaternion spawnRotation = Quaternion.Euler(0f, 90f, 0f); // Rotação para ajustar para a direita

        Instantiate(Carros[Random.Range(0, Carros.Length)], spawnPosition, spawnRotation);
    }
}
