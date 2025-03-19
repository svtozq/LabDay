using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pausescript : MonoBehaviour
{
    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.P)) {
            canvas.SetActive(true);
        }  
    }

    void OnPause()
    {
        
        
    }
}
