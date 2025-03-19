using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseHandler : MonoBehaviour
{ 
    public GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
     void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) // Vérifie si la touche P est appuyée une fois
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf); // Alterne entre affiché et caché
        }
    }
}
