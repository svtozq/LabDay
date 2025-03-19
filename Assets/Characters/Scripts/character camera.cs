using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public float mouseSensitivity = 100f; // Sensibilité de la souris
    public Transform playerBody; // Référence au corps du joueur

    private float xRotation = 0f; // Pour limiter la rotation verticale

    void Start()
    {
        // Verrouiller le curseur au centre de l'écran
        Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
    }

    void Update()
    {
        // Vérification de la référence du playerBody
        if (playerBody == null)
        {
            Debug.LogError("Le playerBody n'est pas assigné dans l'inspecteur !");
            return;
        }

        // Récupérer les mouvements de la souris
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Limiter la rotation verticale
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Appliquer la rotation sur la caméra pour la vue verticale
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Appliquer la rotation horizontale au corps du joueur
        playerBody.Rotate(Vector3.up * mouseX);
    }
}