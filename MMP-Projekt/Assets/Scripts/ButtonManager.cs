using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] public AudioSource buttonSound;

    public void PlayButtonSound()
    {
        buttonSound.Play();
    }
}
