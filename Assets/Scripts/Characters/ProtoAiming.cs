using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProtoAiming : MonoBehaviour
{
    // Reference to the gun object on the TestPlayer prefab. Hookup on start.
    private GameObject gun;
    private GameObject gunChamber;
    private int ammoCount;
    private bool reloading = false;
    private GameObject ammoUIObject;

    public GameObject bulletPrefab;
    public int maxAmmoCapacity = 12;

    // Start is called before the first frame update
    void Start()
    {
        // Hookup gun references. Doing it by tag for now, if that's bad we can change the method.
        gun = GameObject.FindGameObjectWithTag("PlayerGun");
        // This one is really just a gameobject containing a transform position to instantiate bullet objects from
        gunChamber = GameObject.FindGameObjectWithTag("PlayerGunChamber");
        // Set max capacity
        ammoCount = maxAmmoCapacity;
        // Get ref to UI object for ammo count
        ammoUIObject = GameObject.FindGameObjectWithTag("PlayerAmmo");
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
            if(ammoCount > 0 && reloading == false) // have ammo in gun and not reloading
            {
                // Set the rotation to be same as parent since the parent is the barrel.
                GameObject bullet = Instantiate(bulletPrefab, gunChamber.transform.position, transform.rotation) as GameObject;
                // Play shot sound
                SoundManager.PlayOneClipAtLocation(AudioClips.singleton.playerShot, gunChamber.transform.position, 0.15f);
                // Decrement ammo
                ammoCount -= 1;
            }
            else if (reloading == false) // can't pull trigger while reloading
            {
                SoundManager.PlayOneClipAtLocation(AudioClips.singleton.gunEmpty, gunChamber.transform.position, 0.15f);
            }
        }

        // Reload
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (ammoCount < 12 && reloading == false && ammoCount > 0) // if ammo isnt full SHORT RELOAD
            {
                // Start reload sound
                SoundManager.PlayOneClipAtLocation(AudioClips.singleton.gunReloadShort, gunChamber.transform.position, 0.5f);
                reloading = true;
                StartCoroutine(reloadWeapon("short"));
            }
            if (ammoCount <= 0 && reloading == false) // if ammo is empty LONG RELOAD
            {
                // Start reload sound
                SoundManager.PlayOneClipAtLocation(AudioClips.singleton.gunReloadLong, gunChamber.transform.position, 0.5f);
                reloading = true;
                StartCoroutine(reloadWeapon("long"));
            }
        }

        // Sync local variable with UI display
        ammoUIObject.GetComponent<Text>().text = ammoCount + "/12";
    }

    IEnumerator reloadWeapon(string reload)
    {
        switch (reload)
        {
            case "short":
                yield return new WaitForSeconds(1.75f);
                ammoCount = 12; // reload Ammo after delay
                reloading = false;
                break;


            case "long":
                yield return new WaitForSeconds(3.285f);
                ammoCount = 12; // reload Ammo after delay
                reloading = false;
                break;

            default:
                yield return new WaitForSeconds(3.285f);
                ammoCount = 12; // reload Ammo after delay
                reloading = false;
                break;
        }
        
    }
}
