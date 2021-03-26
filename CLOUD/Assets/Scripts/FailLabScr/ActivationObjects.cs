using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationObjects : MonoBehaviour
{
    public GameObject groundObject;
    public GameObject spanwerEnemy;
    public GameObject spanwerEnemy2;
    public GameObject spawnerPerson;
    public GameObject spawnOneEnemy;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if(tag == "GroundTrigger")
            {
                groundObject.GetComponent<Animator>().SetTrigger("isStarted");

                Destroy(gameObject);
            }
            
            if(tag == "SpawnersTrigger")
            {
                spanwerEnemy.SetActive(true);
                spanwerEnemy2.SetActive(true);
                spawnerPerson.SetActive(true);

                Destroy(gameObject);
            }

            if(tag == "FirstEnemyTrigger")
            {
                spawnOneEnemy.SetActive(true);

                Destroy(gameObject);
            }

        }
    }

}
