using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Enemy_Scr : MonoBehaviour
{

    private GameObject Player;
    private Rigidbody2D myRb;

    public float speed;
    public float rotateSpeed;

    private bool onPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        myRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 direction = (Vector2)Player.transform.position - myRb.position;

        direction.Normalize();
       
        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        myRb.angularVelocity = -rotateAmount * rotateSpeed;

        if(!onPlayer)
        {
            myRb.velocity = transform.up * speed;
        }
        else
        {
            //myRb.constraints = RigidbodyConstraints2D.FreezePosition;

            myRb.isKinematic = true;
        }
        
                
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            GetComponent<PolygonCollider2D>().enabled = false;
            gameObject.transform.parent = Player.transform;
                        
            onPlayer = true;
        }

        if(other.tag == "EnemyTrigger")
        {
            Destroy(gameObject);
        }
    }
}
