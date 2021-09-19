using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class ZombieScript : MonoBehaviour
{
    public GameObject player, head, ZombieSpawner;
    public float speed, timeToDestroy;
    public bool isDefeated;
    public Animator body;
    
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ZombieSpawner = GameObject.FindGameObjectWithTag("zombieSpawner");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isDefeated == true)
        {
            TurnToRagdoll();
            ZombieSpawner.GetComponent<ZombieSpawner>().remainingZombies--;
        }
        else
        {
            ChasePlayer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "bullet")
        {
            //turn zombie into ragdoll
            Debug.Log("hit");
            isDefeated = true;
        }
    }

    public void ChasePlayer()
    {
        //plays animation
        body.enabled = true;
        //looks at player
        transform.LookAt(player.transform.position);
        //moves to the player
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        //disables ragdoll physics
        Collider[] colliders = this.gameObject.GetComponentsInChildren<Collider>();
        foreach(Collider c in colliders)
        {
            if(c.gameObject != this.gameObject)
            {
                c.isTrigger = true;
            }
        }
    }

    public void TurnToRagdoll()
    {
        body.enabled = false;
        //enables ragdoll physics
        Collider[] colliders = this.gameObject.GetComponentsInChildren<Collider>();
        foreach (Collider c in colliders)
        {
            if (c.gameObject != this.gameObject)
            {
                c.isTrigger = false;
            }
        }
        Destroy(this.gameObject, timeToDestroy);
    }
}
