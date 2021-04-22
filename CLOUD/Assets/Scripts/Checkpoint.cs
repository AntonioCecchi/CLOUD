using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameObject player;
    
    

    private bool playerIsTriggering;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if(player.GetComponent<Rigidbody2D>().velocity.y <= 0 && playerIsTriggering == false && player.GetComponent<Player_Physic>().totalChildrenNumber == 0)
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
        }
        else
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            playerIsTriggering = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerIsTriggering = false;
        }
    }

}
