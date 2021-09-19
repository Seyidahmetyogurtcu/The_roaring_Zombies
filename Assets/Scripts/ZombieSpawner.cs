using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public int maxZombies, zombieQuantity, whichSide, remainingZombies;
    public float minX1, maxX1, minZ1, maxZ1, minX2, maxX2, minZ2, maxZ2, minX3, maxX3, minZ3, maxZ3, minX4, maxX4, minZ4, maxZ4, coolDown;
    public GameObject zombiePrefab;

    // Start is called before the first frame update
    void Start()
    {
        SpawnZombies();
        StartCoroutine(waitForWave());
    }

    public void SpawnZombies()
    {
        remainingZombies = maxZombies;
        while (zombieQuantity < maxZombies)
        {
            whichSide = Random.Range(1, 4);
            //whichSide = 1;
            if (whichSide == 1)
            {
                Vector3 position = new Vector3(Random.Range(minX1, maxX1), 2, Random.Range(minZ1, maxZ1));
                GameObject newZombie = Instantiate(zombiePrefab, position, Quaternion.identity);
            }
            else if (whichSide == 2)
            {
                Vector3 position = new Vector3(Random.Range(minX2, maxX2), 2, Random.Range(minZ2, maxZ2));
                GameObject newZombie = Instantiate(zombiePrefab, position, Quaternion.identity);
            }
            else if (whichSide == 3)
            {
                Vector3 position = new Vector3(Random.Range(minX3, maxX3), 2, Random.Range(minZ3, maxZ3));
                GameObject newZombie = Instantiate(zombiePrefab, position, Quaternion.identity);
            }
            else if (whichSide == 4)
            {
                Vector3 position = new Vector3(Random.Range(minX4, maxX4), 2, Random.Range(minZ4, maxZ4));
                GameObject newZombie = Instantiate(zombiePrefab, position, Quaternion.identity);
            }
            zombieQuantity++;
        }
        Debug.Log("new wave");
    }

    IEnumerator waitForWave()
    {
        while (true)
        {
            SpawnZombies();
            yield return new WaitForSeconds(coolDown);
            maxZombies += 10;
        }
    }
}
