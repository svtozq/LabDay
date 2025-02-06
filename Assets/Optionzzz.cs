using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Optionzzz : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // Fonction pour découvrir l'île (changer de scène)
    
    public void StartIsland()
    {
        SceneManager.LoadScene("Island 1.1");
    }
}