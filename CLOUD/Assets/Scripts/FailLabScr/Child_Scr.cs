using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child_Scr : MonoBehaviour
{
    public int numberOfChildren;
    private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        numberOfChildren = gameObject.transform.childCount;

        if(gameObject.tag == "firstChild")
        {
            if(numberOfChildren != 0)
            {
                Player.GetComponent<Player_Scr>().onFirstChild = true;
            }
            else
            if(numberOfChildren == 0)
            {
                Player.GetComponent<Player_Scr>().onFirstChild = false;
            }
        }
        if (gameObject.tag == "secondChild")
        {
            if (numberOfChildren != 0)
            {
                Player.GetComponent<Player_Scr>().onSecondChild = true;
            }
            else
            if (numberOfChildren == 0)
            {
                Player.GetComponent<Player_Scr>().onSecondChild = false;
            }
        }
        if (gameObject.tag == "thirdChild")
        {
            if (numberOfChildren != 0)
            {
                Player.GetComponent<Player_Scr>().onThirdChild = true;
            }
            else
            if (numberOfChildren == 0)
            {
                Player.GetComponent<Player_Scr>().onThirdChild = false;
            }
        }
        if (gameObject.tag == "fourthChild")
        {
            if (numberOfChildren != 0)
            {
                Player.GetComponent<Player_Scr>().onFourthChild = true;
            }
            else
            if (numberOfChildren == 0)
            {
                Player.GetComponent<Player_Scr>().onFourthChild = false;
            }
        }
        if (gameObject.tag == "fifthChild")
        {
            if (numberOfChildren != 0)
            {
                Player.GetComponent<Player_Scr>().onFifthChild = true;
            }
            else
            if (numberOfChildren == 0)
            {
                Player.GetComponent<Player_Scr>().onFifthChild = false;
            }
        }
    }
}
