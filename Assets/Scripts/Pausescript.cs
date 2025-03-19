using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pausescript : MonoBehaviour
{
    public GameObject canvas;
    private bool isPaused = false;
    private float toggleCooldown = 0.2f; // Cooldown to prevent instant re-toggle
    private float nextToggleTime = 0f;

    void Start()
    {
        canvas.SetActive(false);
    }

    void Update()
    {
        // Ensure enough time has passed before allowing another toggle
        if (Time.unscaledTime > nextToggleTime && Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
            nextToggleTime = Time.unscaledTime + toggleCooldown; // Set the next allowed toggle time
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            canvas.SetActive(true);
            Time.timeScale = 0f;
            Debug.Log("- Game paused -");
        }
        else
        {
            canvas.SetActive(false);
            Time.timeScale = 1f;
            Debug.Log("- Game resumed -");
        }
    }

    void OnPause()
    {
        
        
    }
}
