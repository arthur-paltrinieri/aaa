using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class deathScript : MonoBehaviour
{
    [SerializeField] private GameObject painelContinue;
    public void Reiniciar1()
    {
        SceneManager.LoadScene("Fase1");
    }
    public void Reiniciar2()
    {
        SceneManager.LoadScene("Fase2");
    }
    public void Reiniciar3()
    {
        SceneManager.LoadScene("Fase3");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MENU");
    }
}