using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractScript : MonoBehaviour
{
    public Camera cam;
    public LayerMask items;
    private Interactable focus;
    public GameObject inventoryMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Removes focus
        if (Input.GetMouseButtonDown(0))
        {
            RemoveFocus();
        }
        
        // Sets/changes the focus
        if (Input.GetKeyDown(KeyCode.F))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, 100))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }

        }
    }


    void SetFocus(Interactable newFocus)
    {
        
        if (newFocus != focus)
        {
            if (focus!= null)
            {
                // Makes sure the old focus is removed
                focus.OnDefocused();
            }

            // Sets new focus
            focus = newFocus;
        }

        newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
        // If there is a focus it gets removed
        if (focus != null)
        {
            
            focus.OnDefocused();
        }
        focus = null;
    }
}
