using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner_Scr : MonoBehaviour
{
    [Space(10)]
    public List<GameObject> enemies = new List<GameObject>();

    [Space(10)]
    public GameObject WestSpawner;
    public GameObject SouthSpawner;
    public GameObject EastSpawner;

    private void Update()
    {
        foreach(GameObject enemy in enemies)
        {
            if(enemy == null)
            {
                enemies.Remove(enemy);
            }
        }
    }

    public void SpawnEnemy()
    {
        float randomChance = Random.Range(0, 3);

        if(randomChance >= 0 && randomChance < 1)
        {
            enemies[0].transform.position = WestSpawner.transform.position;
            enemies[0].GetComponent<Enemy_Scr>().isAwake = true;
            enemies[0].SetActive(true);
        }
        else
        if(randomChance >= 1 && randomChance < 2)
        {
            enemies[0].transform.position = EastSpawner.transform.position;
            enemies[0].GetComponent<Enemy_Scr>().isAwake = true;
            enemies[0].SetActive(true);
        }
        else
        {
            enemies[0].transform.position = SouthSpawner.transform.position;
            enemies[0].GetComponent<Enemy_Scr>().isAwake = true;
            enemies[0].SetActive(true);
        }
    }
}
