using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person_Scr : MonoBehaviour
{

    private GameObject Player;
    public ParticleSystem PersonDeath;

    public bool isOnPlayer = false;

    private Animator myAnimator;

    private void Start()
    {
        myAnimator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        if (Player.GetComponent<Player_Scr>().onFirstChild)
        {
            
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            Instantiate(PersonDeath, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(other.gameObject);

            //se muore quando è sul player
            if(isOnPlayer)
            {
                Player.GetComponent<Player_Scr>().gravityStrenght += 0.3f;
                Player.GetComponent<Player_Scr>().jumpStrenght -= 0.5f;

                isOnPlayer = false;

                Instantiate(PersonDeath, transform.position, Quaternion.identity);
                Destroy(gameObject);
                Destroy(other.gameObject);
            }

        }

    }

    public void ScaleSize()
    {
        myAnimator.SetTrigger("isOnPlayer");
    }


}
