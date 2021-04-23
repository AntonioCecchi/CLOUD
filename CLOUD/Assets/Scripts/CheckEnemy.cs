using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnemy : MonoBehaviour
{

    public GameObject enemySpawner;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "EnemyTrigger")
        {
            enemySpawner.GetComponent<EnemySpawner_Scr>().SpawnEnemy();
            Destroy(other.gameObject);
        }
    }


}
