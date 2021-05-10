using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public GameObject Player;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            Player.GetComponent<Player_Physic>().canJump = true;
            Player.GetComponent<Player_Physic>().isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            Player.GetComponent<Player_Physic>().isGrounded = false;
        }
    }
}
