using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keypad : Interaction
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Design our interaction using code
    protected override void Interact()
    {
        Debug.Log("Interacted with " + gameObject.name);
    }
}