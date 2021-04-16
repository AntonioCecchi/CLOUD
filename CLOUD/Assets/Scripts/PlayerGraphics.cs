using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGraphics : MonoBehaviour
{
    public GameObject player;
    public GameObject CharFill;
    public GameObject CharBorder;

    public Sprite[] growingSprites;
    

    void Start()
    {
        //CharBorder.GetComponent<SpriteRenderer>().sprite = growingSprites[8];
    }

    void Update()
    {
        if (player.GetComponent<Player_Physic>().canJump == true)
        {
            CharFill.SetActive(true);
        }
        else
        if(player.GetComponent<Player_Physic>().canJump == false)
        {
            CharFill.SetActive(false);

        }
    }
}
