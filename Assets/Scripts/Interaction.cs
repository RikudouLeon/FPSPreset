using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    // Display message to player when looking at an interactable object
    public string promptMessage;

    public void BaseInteract() // Called from our player
    {
        Interact();
    }

    protected virtual void Interact()
    {
        // No code written for this function
        // Template function to be overridden by the subclasses
    }
}
