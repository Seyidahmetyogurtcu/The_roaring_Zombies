using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "zombie" && this.tag == "bullet")
        {
            //turn zombie into ragdoll
            Debug.Log("hit");
            other.GetComponent<ZombieScript>().speed = 0;
            other.GetComponent<ZombieScript>().isDefeated = true;
            Destroy(this.gameObject);
        }
        if(other.tag == "gun" && this.tag == "bulletItem")
        {
            other.GetComponent<GunScript>().reLoad();
            Destroy(this.gameObject);
        }
    }
}
