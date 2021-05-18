using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonSpawner : MonoBehaviour
{
    public GameObject person;

    public GameObject player;

    public float spawnTime;
    public float spawnTimeMax;
    private float timer;

    void Start()
    {
        spawnTime = spawnTimeMax;
        timer = 30f;
    }

    void Update()
    {
        if (player.GetComponent<Player_Physic>().isGrounded)
        {
            timer -= Time.deltaTime;
        }
        else
        {

        }

        if(timer <= 0)
        {
            timer = 0;
        }

        if (player.GetComponent<Player_Physic>().totalChildrenNumber == 0 && timer == 0)
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
        Instantiate(person, transform.position, Quaternion.identity);
    }
}
