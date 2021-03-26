using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonSpawner_Scr: MonoBehaviour
{
    public GameObject person;

    private GameObject Player;

    public float timerSpawnMin;
    public float timerSpawn;
    public float timerSpawnMax;
    public bool playerIsFull = false;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        timerSpawn = Random.Range(timerSpawnMin, timerSpawnMax);
    }

    // Update is called once per frame
    void Update()
    {
        timerSpawn -= Time.deltaTime;
        
        if (timerSpawn <= 0 && !playerIsFull)
        {
            Instantiate(person, transform.position, Quaternion.identity);

            timerSpawn = Random.Range(timerSpawnMin, timerSpawnMax);
        }


        if(Player.GetComponent<Player_Scr>().onFirstChild && Player.GetComponent<Player_Scr>().onSecondChild && Player.GetComponent<Player_Scr>().onThirdChild && Player.GetComponent<Player_Scr>().onFourthChild && Player.GetComponent<Player_Scr>().onFifthChild)
        {
            playerIsFull = true;
        }
        else
        {
            playerIsFull = false;
        }
    }
}
