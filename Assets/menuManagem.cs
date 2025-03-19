using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Backtomenu : MonoBehaviour
{
    public GameObject canva;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickSettings()
    {
       canva.SetActive(true);
    }

    public void ClickBack()
    {
        canva.SetActive(false);
    }

    public void backMenu()
    {
        SceneManager.LoadScene("Menu-island");
    }

    

   
}