using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonSpawner : MonoBehaviour
{
    public GameObject person;

    public GameObject leftSpawn;
    public GameObject rightSpawn;

    public GameObject player;

    public float spawnTime;
    public float spawnTimeMax;


    void Start()
    {
        spawnTime = spawnTimeMax;
    }

    void Update()
    {
        if (player.GetComponent<Player_Physic>().totalChildrenNumber == 0)
        {
            spawnTime -= Time.deltaTime;
        }

        if (player.GetComponent<Player_Physic>().totalChildrenNumber > 0)
        {
            spawnTime = spawnTimeMax;
        }

        if(spawnTime <= 0)
        {

            spawnTime = spawnTimeMax;

            SpawnPerson();
        }

    }

    private void SpawnPerson()
    {
        float randomChance = UnityEngine.Random.Range(0.0f, 1.0f);

        if (randomChance <= 0.5)
        {
            Instantiate(person, leftSpawn.transform.position, Quaternion.identity);

        }
        else
        {
            Instantiate(person, rightSpawn.transform.position, Quaternion.identity);
        }
    }
}
