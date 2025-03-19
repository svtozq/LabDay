using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class achivements : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject canva;
    void Start()
    {
        // Ensure achievement menu is desactivated by default.
        canva.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void clickAchievements()
    {
        canva.SetActive(true);
    }
    
    public void ClickBack()
    {
        canva.SetActive(false);
    }
}
