using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonSound : MonoBehaviour
{
    [SerializeField] public AudioSource buttonSound;

    public void PlaySound() // Sounds for menu buttons
    {
        buttonSound.Play();
    }
}
