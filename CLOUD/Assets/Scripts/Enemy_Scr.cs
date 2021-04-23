using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Enemy_Scr : MonoBehaviour
{
    public bool isAwake = false;

    private GameObject Player;
    private Rigidbody2D myRb;

    public float speed;
    public float rotateSpeed;

    private bool onPlayer = false;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        myRb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if(isAwake)
        {
            Vector2 direction = (Vector2)Player.transform.position - myRb.position;

            direction.Normalize();

            float rotateAmount = Vector3.Cross(direction, transform.up).z;

            myRb.angularVelocity = -rotateAmount * rotateSpeed;

            if (!onPlayer)
            {
                myRb.velocity = transform.up * speed;
            }
            else
            {
                //myRb.constraints = RigidbodyConstraints2D.FreezePosition;

                myRb.isKinematic = true;
            }
        }
        else
        {

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(PersonAway());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "CameraTrigger")
        {
            Destroy(gameObject);
        }
    }


    private IEnumerator PersonAway()
    {
        GetComponent<SpriteRenderer>().enabled = false;

        GameObject[] playerChildren = Player.transform.GetChild(0).GetComponent<PlayerChildrenManager>().PlayerCMChildren;
        foreach (GameObject child in playerChildren)
        {
            if (child.transform.childCount > 0)
            {
                child.transform.GetChild(0).GetComponent<Person>().GoAwayFromPlayer();
            }
        }

        yield return new WaitForSeconds(1);

        Destroy(gameObject);
    }
}
