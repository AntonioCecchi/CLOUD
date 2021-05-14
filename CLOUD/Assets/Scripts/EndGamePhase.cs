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

            Blink.GetComponent<Blink>().phase5 = true;
        }
    }

    public IEnumerator FirstPart()
    {
        //setup Player
        Player.GetComponent<Player_Physic>().enabled = false;
        Rigidbody2D rb = Player.GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        //stacca i figli dal player
        ChildrenManager = Player.transform.GetChild(0).gameObject;
        ChildrenManagerChildren = ChildrenManager.GetComponent<PlayerChildrenManager>().PlayerCMChildren;

        foreach (GameObject child in ChildrenManagerChildren)
        {
            if(child.transform.childCount > 0)
            {
                child.transform.GetChild(0).parent = null;
            }
        }

        Player.transform.position = Blink.transform.position;
        yield return new WaitForSeconds(1f);

        //posizionali a semicerchio
        
    }

}
