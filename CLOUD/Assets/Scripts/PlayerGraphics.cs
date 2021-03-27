using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGraphics : MonoBehaviour
{
    public Sprite canJump;
    public Sprite cannotJump;

    public GameObject player;
    public GameObject charGraphics;

    void Start()
    {
        
    }

    void Update()
    {
        if(player.GetComponent<Player_Physic>().canJump == true)
        {
            charGraphics.GetComponent<SpriteRenderer>().sprite = canJump;
        }
        else
        if(player.GetComponent<Player_Physic>().canJump == false)
        {
            charGraphics.GetComponent<SpriteRenderer>().sprite = cannotJump;

        }
    }
}
