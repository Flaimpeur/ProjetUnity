using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    private Sign currentSign; // Référence au panneau avec lequel interagit le joueur

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Sign")
        {
            currentSign = other.gameObject.GetComponent<Sign>();
            currentSign.ui.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Sign")
        {
            Sign sb = other.gameObject.GetComponent<Sign>();
            sb.ui.SetActive(false);
            if (sb.text.activeSelf)
            {
                sb.text.SetActive(false);
            }
            currentSign = null; // Réinitialiser la référence du panneau actuel
        }
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.performed && currentSign != null) // Vérifiez que l'action a été exécutée et qu'il y a un panneau en interaction
        {
            currentSign.text.SetActive(true);
        }
    }
}
