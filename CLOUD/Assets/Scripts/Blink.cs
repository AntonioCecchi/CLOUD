using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    private GameObject Player;
    private Animator myAnim;

    public float inputTimer;
    public float firstPersonTimer;


    private bool phase1;
    private bool phase2;
    private bool phase3;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        myAnim = GetComponent<Animator>();
        Player.GetComponent<Player_Physic>().isFrozen = true;

        phase1 = true;
    }


    void Update()
    {
        if(phase1)
        {
            inputTimer -= Time.deltaTime;

            if (inputTimer <= 0)
            {
                Player.GetComponent<Player_Physic>().isFrozen = false;
            }

            firstPersonTimer -= Time.deltaTime;
            if (firstPersonTimer <= 0)
            {
                myAnim.SetTrigger("firstPerson");
            }

            if (Player.GetComponent<Player_Physic>().totalChildrenNumber == 1)
            {
                phase1 = false;
                phase2 = true;
            }
        }

        if(phase2)
        {
            myAnim.SetTrigger("firstLamp");
        }
        
    }
}
