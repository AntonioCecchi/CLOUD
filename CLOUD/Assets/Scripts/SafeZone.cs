using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZone : MonoBehaviour
{

    public GameObject[] enemies;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            foreach(GameObject enemy in enemies)
            {
                enemy.GetComponent<SpriteRenderer>().enabled = false; 
                enemy.GetComponent<Enemy_Scr>().isAwake = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<SpriteRenderer>().enabled = true;
                enemy.GetComponent<Enemy_Scr>().isAwake = false;
                Destroy(gameObject);
            }
        }
    }
}
