using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private float distance = 5f;
    [SerializeField] LayerMask mask;
    private PlayerUI playerUI;
    private InputManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        playerUI.UpdateText(string.Empty);
        
        // Create a ray shooting outwards from the center of cam
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);
        RaycastHit hitInfo; // Variable to store our collision information
        
        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            if (hitInfo.collider.GetComponent<Interaction>() != null)
            {
                Interaction interaction = hitInfo.collider.GetComponent<Interaction>();
                
                //Debug.Log(hitInfo.collider.GetComponent<Interaction>().promptMessage);
                playerUI.UpdateText(interaction.promptMessage);

                if (inputManager.onFoot.Interact.triggered)
                {
                    interaction.BaseInteract();
                }
            }
        }
    }
}
