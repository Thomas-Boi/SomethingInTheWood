using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoAiming : MonoBehaviour
{
    // Reference to the gun object on the TestPlayer prefab. Hookup on start.
    private GameObject gun;
    private GameObject gunChamber;

    public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        // Hookup gun references. Doing it by tag for now, if that's bad we can change the method.
        gun = GameObject.FindGameObjectWithTag("PlayerGun");
        // This one is really just a gameobject containing a transform position to instantiate bullet objects from
        gunChamber = GameObject.FindGameObjectWithTag("PlayerGunChamber");
    }

    // Update is called once per frame
    void Update()
    {
        // AIMING
        // get the difference between mouse position and object to look at mouse
        Vector3 mouseDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float lookAngle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(lookAngle, Vector3.forward);


        if (transform.localRotation.z > 0)
        {
            GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
        else
        {
            GetComponent<SpriteRenderer>().sortingOrder = 1;
        }

        // SHOOTING
        if (Input.GetMouseButtonDown(0))
        {
            // Set the rotation to be same as parent since the parent is the barrel.
            GameObject bullet = Instantiate(bulletPrefab, gunChamber.transform.position, transform.rotation) as GameObject;
        }
    }
}
