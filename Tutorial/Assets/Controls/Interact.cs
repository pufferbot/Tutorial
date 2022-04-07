using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    //Interacting with things
    [SerializeField] PlayerStats playerStats;
    public float interactRange;
    public LayerMask layerMask;

    //Holding physics objects
    [SerializeField] private Rigidbody holdLocation;
    [SerializeField] private float dragSpeed;
    public float throwForce = 10f;
    private HoldComponent currentHit;
    private HoldComponent currentHeld;
    private CharacterJoint currentJoint;

    private void Awake()
    {
        if(!holdLocation)
            holdLocation = GameObject.Find("PickedUpItemHolder").GetComponent<Rigidbody>();
    }

    //Activating an interactable object
    public void OnInteract()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactRange, layerMask))
        {
            if (hit.transform.GetComponent<InteractComponent>())
            {
                InteractComponent interactComponent = hit.transform.GetComponent<InteractComponent>();
                interactComponent.OnInteract(playerStats);
            }
        }
    }

    public void OnHold()
    {
        if(!currentHeld)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, interactRange, layerMask))
            {
                if (hit.transform.GetComponent<HoldComponent>())
                {
                    HoldComponent holdComponent = hit.transform.GetComponent<HoldComponent>();
                    //Is it a different object than the one we already have?
                    if(holdComponent != currentHit)
                    {
                        //If different deselect the current object
                        if(currentHit)
                            currentHit.MarkActive(false);
                        
                        //Hold the new object
                        currentHit = holdComponent;
                        currentHit.MarkActive(true);
                    }
                }
                else
                {
                    //The object pointed at wasn't holdable, so deselect the current object
                    if(currentHit)
                    {
                        currentHit.MarkActive(false);
                        currentHit = null;
                    }
                }
            }
            else
            {
                currentHit.MarkActive(false);
                currentHit = null;
            }

            //If it worked, then pick it up
            if(currentHit)
                PickUp();
        }
        
    }

    //Picking up a physics object
    public void PickUp()
    {
        currentJoint = currentHit.gameObject.AddComponent<CharacterJoint>();
        currentJoint.connectedBody = holdLocation;

        //currentHit.gameObject.set
        //currentHit.GetComponent<Rigidbody>().useGravity = false;

        currentHeld = currentHit;
    }

    //Releasing the held object
    public void Release()
    {
        Destroy(currentJoint);

        //currentHeld.transform.SetParent(null);

        currentHeld.Rigidbody.AddForce(transform.forward * throwForce, ForceMode.Impulse);
        currentHeld = null;
    }

}
