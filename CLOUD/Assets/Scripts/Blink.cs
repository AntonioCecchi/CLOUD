using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    private GameObject Player;
    public SpriteRenderer PlayerBorder;
    public SpriteRenderer PlayerFill;

    private Animator myAnim;

    public float inputTimer;
    public float firstPersonTimer;

    [Space(10)]
    public GameObject lampOn1;
    public GameObject lampOn2;
    public GameObject lampOn3;

    private bool phase1;
    private bool phase2;
    private bool phase3;
    private bool phase4;
    [HideInInspector]
    public bool phase5;

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

            if(lampOn1.activeSelf)
            {
                myAnim.SetTrigger("doneFirstLamp");
                phase2 = false;
                phase3 = true;
            }
        }

        if(phase3)
        {
            if(lampOn2.activeSelf)
            {
                myAnim.SetTrigger("doneSecondLamp");
                phase3 = false;
                phase4 = true;
            }
        }

        if(phase4)
        {
            if (lampOn3.activeSelf)
            {
                myAnim.SetTrigger("goToEnd");
                phase4 = false;
            }
        }

        if(phase5)
        {
            myAnim.SetTrigger("whiteScreen");
            phase5 = false;
        }
    }

    public IEnumerator SwitchPlayer()
    {
        Debug.Log("sono morto");
        //Player Muore
        PlayerBorder.enabled = false;
        PlayerFill.enabled = false;
        yield return new WaitForSeconds(1f);
    }
}
