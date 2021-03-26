using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner_Scr : MonoBehaviour
{
    public GameObject enemy;
    private Transform targetSpawn;

    public float timerSpawnMin;
    public float timerSpawn;
    public float timerSpawnMax;

    // Start is called before the first frame update
    void Start()
    {
        timerSpawn = Random.Range(timerSpawnMin, timerSpawnMax);
    }

    // Update is called once per frame
    void Update()
    {
        timerSpawn -= Time.deltaTime;

        targetSpawn = gameObject.transform.GetChild(Random.Range(0, 4));

        if (timerSpawn <= 0)
        {
            Instantiate(enemy, targetSpawn.position, targetSpawn.rotation);

            timerSpawn = Random.Range(timerSpawnMin, timerSpawnMax);
        }

    }
}
