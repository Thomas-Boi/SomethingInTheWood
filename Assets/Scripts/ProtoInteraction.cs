using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoInteraction : MonoBehaviour
{

    [SerializeField]
    private float interactRange = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If object is in specific range of player, allow to interact
        if (Input.GetKeyDown(KeyCode.E))
        {

        }
    }

    private bool ObjectInRange()
    {

        return false;
    }

    private void StartInteraction()
    {

    }
}
