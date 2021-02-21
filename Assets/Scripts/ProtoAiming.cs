using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoAiming : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // get the difference between mouse position and object to look at mouse
        Vector3 mouseDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float lookAngle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(lookAngle, Vector3.forward);
    }
}
