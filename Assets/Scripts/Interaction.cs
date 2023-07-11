using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    // Display message to player when looking at an interactable object
    [SerializeField] public string promptMessage;
    public bool useEvents; // Add or Remove an InteractEvent component to this game object

    public virtual string OnLook()
    {
        return promptMessage;
    }
    
    public void BaseInteract() // Called from our player
    {
        if (useEvents)
        {
            GetComponent<InteractEvent>().OnInteract.Invoke(); // this value should never be null because an editor script is used to add the interaction event component
        }
        Interact();
    }

    protected virtual void Interact()
    {
        // No code written for this function
        // Template function to be overridden by the subclasses
    }
}
