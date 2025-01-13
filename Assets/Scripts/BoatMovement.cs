using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    public float moveSpeed = 2f; // Vitesse de déplacement avant/arrière
    public float rotationSpeed = 100f; // Vitesse de rotation

    void Update()
    {
        // Déplacement avant/arrière
        if (Input.GetKey(KeyCode.UpArrow)) // Flèche haut
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.DownArrow)) // Flèche bas
        {
            transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);
        }

        // Rotation gauche/droite
        if (Input.GetKey(KeyCode.LeftArrow)) // Flèche gauche
        {
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow)) // Flèche droite
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}