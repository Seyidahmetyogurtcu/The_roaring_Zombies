using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainSpawnScript : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] int bulletSpawnCircleSize;
    [SerializeField] int EnemySpawnCircleSize;
    public enemyList[] enemies;
    [SerializeField] float waveLength;
    public bool waveStarted;
    bool waveEnded;
    [SerializeField] float waveCooldown;
    private void Update()
    {
        if (waveStarted)
        {
            waveStarted = false; //Loop prevention
            //Spawn all the types of zombies
            for (int i = 0; i < enemies.Length; i++) 
            {
                StartCoroutine(spawnEnemyCo(enemies[i].enemyPrefab, enemies[i].amountSpawned));
            }
        }
    }
    private IEnumerator spawnEnemyCo(GameObject enemyPrefab, int amount)
    {
        //Spawns all the zombies over the duration of the wave
        for (int i = 0; i < amount; i++) 
        {
            yield return new WaitForSeconds(waveLength / amount);
            Vector2 enemySpawnPlain = Random.insideUnitCircle.normalized * EnemySpawnCircleSize; 
            Instantiate(enemyPrefab, new Vector3(enemySpawnPlain.x, 0, enemySpawnPlain.y), Quaternion.identity, transform);
            Vector2 BulletSpawnPlain = Random.insideUnitCircle * bulletSpawnCircleSize;
            Instantiate(bulletPrefab, new Vector3(BulletSpawnPlain.x, 0, BulletSpawnPlain.y), Quaternion.identity);
        }
        waveEnded = true;
        StartCoroutine(endOfWaveCo());
    }
    private IEnumerator endOfWaveCo()
    {
        if(waveEnded && transform.childCount == 0)
        {
            waveEnded = false;
            for(int i = 0; i < enemies.Length; i++)
            {
                enemies[i].amountSpawned++;
            }
            yield return new WaitForSeconds(waveCooldown);
            waveStarted = true;
        } else
        {
            yield return new WaitForSeconds(1);
            StartCoroutine(endOfWaveCo());
        }
    }
}
