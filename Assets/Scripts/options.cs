using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NewBehaviourScript : MonoBehaviour
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
   	public void LoadScene ( string islandName )
    {
        SceneManager.LoadScene(islandName); // Changer la scène vers " l.1"
    }
}
