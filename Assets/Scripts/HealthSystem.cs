using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public GameObject player;
    public int HP;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        //updates health
        player.GetComponent<PlayerScript>().healthUI.text = "Health: " + HP.ToString();

        //kills the player if health reaches 0
        if (HP <= 0)
        {
            dies();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        HP--;
    }

    IEnumerator Damaging()
    {
        while (true)
        {
            HP--;
            yield return new WaitForSeconds(1f);
        }
    }

    public void dies()
    {
        player.GetComponent<PlayerScript>().speed = 0;
        player.GetComponent<PlayerScript>().playerCam = null;
        player.GetComponent<PlayerScript>().deathScreen.SetActive(true);
    }
}
