using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseHandler : MonoBehaviour
{ 
    public float timeScale = 1f;
    public GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) // Vérifie si la touche P est pressée une fois
        {
            if (Time.timeScale == 1f) // Si le jeu est en cours
            {
                Time.timeScale = 0f; // Met le jeu en pause
                pauseMenu.SetActive(true); // Affiche le menu de pause
            }
            else // Si le jeu est déjà en pause
            {
                Time.timeScale = 1f; // Reprend le jeu
                pauseMenu.SetActive(false); // Cache le menu de pause
            }
        }
    }
}