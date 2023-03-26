using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObeliskInteract : MonoBehaviour
{
    private Obelisk currentObelisk; // The obelisk the player is currently interacting with

    private void Update()
    {
        if (currentObelisk != null && Input.GetKeyDown(KeyCode.E)) // Check if the player is interacting with an obelisk and has pressed the E key
        {
            currentObelisk.Interact(); // Interact with the obelisk
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obelisk"))
        {
            Obelisk obelisk = collision.GetComponent<Obelisk>();
            if (obelisk != null && !obelisk.collected) // Check if the object has an Obelisk component
            {
                currentObelisk = obelisk; // Set the current obelisk
                currentObelisk.ShowInteractionPrompt(true); // Show the interaction prompt
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obelisk")) // Check if the player has exited the trigger of an obelisk
        {
            Obelisk obelisk = collision.GetComponent<Obelisk>();
            if (obelisk != null && obelisk == currentObelisk) // Check if the object has an Obelisk component and is the current obelisk
            {
                currentObelisk.ShowInteractionPrompt(false); // Hide the interaction prompt
                currentObelisk = null; // Clear the current obelisk
            }
        }
    }
}
