using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HoldComponent : MonoBehaviour
{
    
    [SerializeField] private new Rigidbody rigidbody;
    public Rigidbody Rigidbody => rigidbody;

    private void Awake()
    {
        if(!rigidbody && !TryGetComponent<Rigidbody>(out rigidbody))
            Debug.LogError("No rigidbody attached");
    }

    public void MarkActive(bool active)
    {
        
    }

}
