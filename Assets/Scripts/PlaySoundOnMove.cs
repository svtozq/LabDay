using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnMove : MonoBehaviour
{
    public AudioSource audioSource;  
    private Vector3 lastPosition;    // Stocke la dernière position de l'objet

    void Start()
    {
        lastPosition = transform.position;  // Initialise la position
    }

    void Update()
    {
        if (transform.position != lastPosition) // Vérifie si l'objet a bougé
        {
            if (!audioSource.isPlaying)  // Joue le son uniquement s'il ne joue pas déjà
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();  // Arrête le son si l'objet ne bouge plus
        }

        lastPosition = transform.position; // Met à jour la dernière position
    }
}