using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    public float moveSpeed = 200f; // Vitesse de déplacement avant/arrière
    public float rotationSpeed = 100f; // Vitesse de rotation
    private bool isColliding = false; // Indique si le bateau est en collision
    private Coroutine collisionTimer; // Stocke la coroutine active

    
    
    void Start()
    {
        GameObject.Find("Options").GetComponent<Canvas>().enabled = false;
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            Component[] components = obj.GetComponents<Component>();
            foreach (Component comp in components)
            {
                if (comp == null)
                {
                    Debug.LogWarning($"Objet avec script manquant : {obj.name}", obj);
                }
            }
        }
    }

    void Update()
    {
        if (!isColliding) // Vérifie si le bateau n'est pas en collision
        {
            // Déplacement avant/arrière
            if ((Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.UpArrow))) // Flèche haut
            {
                transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);
            }
            else if ((Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.DownArrow))) // Flèche bas
            {
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);
            }

            // Rotation gauche/droite
            if ((Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.LeftArrow))) // Flèche gauche
            {
                transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
            }
            else if ((Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.RightArrow))) // Flèche droite
            {
                transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            }
        }
        // Fixer la position Y à 280 (éviter que le bateau se noie sur l'axe de Y)
        transform.position = new Vector3(transform.position.x, 280f, transform.position.z);

        // Fixer la rotation Z à 0 (éviter toute inclinaison)
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Island")
        {
            Debug.Log("Collision avec une île !");
            GameObject.Find("Options").GetComponent<Canvas>().enabled = true;

            isColliding = true; // Active l'état de collision
            
            if (collisionTimer != null)
            {
                StopCoroutine(collisionTimer);
            }
            collisionTimer = StartCoroutine(CollisionTimer());
        }
    }

    // Coroutine pour quitter la collision après 2 secondes
    IEnumerator CollisionTimer()
    {
        yield return new WaitForSeconds(2f); // Attendre 2 secondes
        QuitIsland(); // Appelle la fonction qui quitte la collision
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Island")
        {
            QuitIsland();
        }
    }

    // Fonction pour quitter la collision après 2 secondes
    void QuitIsland()
    {
        GameObject.Find("Options").GetComponent<Canvas>().enabled = false;
        Debug.Log("Le bateau a quitté l'île.");
        isColliding = false; // Désactive l'état de collision
    }
}
