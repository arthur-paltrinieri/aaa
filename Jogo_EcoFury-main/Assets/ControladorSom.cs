using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class ControladorSom : MonoBehaviour
    
{
    [SerializeField] private AudioSource fundoMusical;
   public void VolumeMusical(float value)
    {
        fundoMusical.volume = value;
    }
}
