using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class GunScript : MonoBehaviour
{
    //floats
    public float recoil, speed, drop, FOV1, FOV2;
    //integers
    public int maxAmmo, currentAmmo;
    //booleans
    public bool isAiming = false;
    //components
    Rigidbody rb, bulletRb, caseRb;
    public Collider hand;
    public Animator animator;
    //gameobjects
    public GameObject bullet, bulletcase, bulletSpawn, caseSpawn, player, newBullet, bulletItem;
    public Camera camera;
    public Text ammoIndicator;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        reLoad();
        animator.SetFloat("Blend", 0);
        camera.fieldOfView = FOV1;
    }
    private void Update()
    {
        //checks if there are enough bullets
        if(currentAmmo > 0)
        {
            //shoots the gun
            if (Input.GetButtonDown("Fire1"))
            {
                shoot();
                Vector3 bulletSpawn = new Vector3(0, 10, 0);
                newBullet = Instantiate(bulletItem, bulletSpawn, Quaternion.identity);
                newBullet.GetComponent<Rigidbody>().useGravity = true;
            }
        }

        if (Input.GetButtonDown("Fire2"))
        {
            if (isAiming == false)
            {
                aim();
                isAiming = true;
            }
            else
            {
                aimNot();
                isAiming = false;
            }
        }
        //controls the position of the gun
        transform.position = hand.transform.position;
    }

    public void shoot()
    {
        GameObject newBullet = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity);
        bulletRb = newBullet.GetComponent<Rigidbody>();
        bulletRb.AddForce(hand.transform.forward * recoil);
        Destroy(newBullet, 30f);
        GameObject newCase = Instantiate(bulletcase, caseSpawn.transform.position, Quaternion.identity);
        caseRb = newCase.GetComponent<Rigidbody>();
        caseRb.AddForce(hand.transform.right * 50);
        Destroy(newCase, 30f);
        player.transform.Translate(Vector3.forward * -1);
        currentAmmo -= 1;
        ammoIndicator.text = ("Ammo: 0");
    }

    public void aim()
    {
        //plays animation of the player placing the gun on the eye to aim more precisely
        animator.SetFloat("Blend", 1);
        camera.fieldOfView = FOV2;
        player.GetComponent<PlayerScript>().speed *= 0.5f;
    }

    public void aimNot()
    {
        //plays animation of the player placing the gun on the shoulder to move faster
        animator.SetFloat("Blend", 0);
        camera.fieldOfView = FOV1;
        player.GetComponent<PlayerScript>().speed *= 2;
    }

    public void reLoad()
    {
        currentAmmo = maxAmmo;
        ammoIndicator.text = ("Ammo: 1");
    }
}
