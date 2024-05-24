using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner2 : MonoBehaviour
{
    public int counter;
    public GameObject[] Carros;
    public Vector3 spawnPosition = new Vector3(2066f, 0.1f, 1490f); // Posição de spawn fixa

    void Start()
    {
        InvokeRepeating("SpawnCar", 0, 5f);
    }

    public void SpawnCar()
    {
        if (counter == 0)
        {
            CancelInvoke("SpawnCar");
            return;
        }

        Quaternion spawnRotation = Quaternion.Euler(0f, 270, 0f); // Rotação para ajustar para a direita

        Instantiate(Carros[Random.Range(0, Carros.Length)], spawnPosition, spawnRotation);
    }
}