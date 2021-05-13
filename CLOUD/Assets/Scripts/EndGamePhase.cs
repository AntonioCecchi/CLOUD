using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGamePhase : MonoBehaviour
{
    private GameObject Player;
    private GameObject ChildrenManager;
    private GameObject[] ChildrenManagerChildren;
    public GameObject Blink;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(FirstPart());
        }
    }

    public IEnumerator FirstPart()
    {
        Player.GetComponent<Player_Physic>().canJump = false;
        Player.GetComponent<Player_Physic>().canMove = false;
        Rigidbody2D rb = Player.GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        //stacca i figli dal player
        ChildrenManager = Player.transform.GetChild(0).gameObject;
        ChildrenManagerChildren = ChildrenManager.GetComponent<PlayerChildrenManager>().PlayerCMChildren;

        foreach (GameObject child in ChildrenManagerChildren)
        {
            if(child.transform.GetChild(0) != null)
            {
                child.transform.GetChild(0).parent = null;
            }
        }
        Player.transform.position = Blink.transform.position;
        yield return new WaitForSeconds(1f);

        //posizionali a semicerchio
        
        yield return new WaitForSeconds(1f);

        //Player Muore
        Player.GetComponent<SpriteRenderer>().enabled = false;
        
    }
}
