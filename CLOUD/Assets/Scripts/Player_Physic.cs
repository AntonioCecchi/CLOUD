﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Physic : MonoBehaviour
{
    #region Movement Var
    [Header("Movement Variables")]

    public float moveSpeed = 5f;
    private Rigidbody2D myRb;
    private float dirX;
    public bool canMove = true;
    #endregion


    #region Jump Var
    [Header("Jump Variables")]

    public float gravityStrenght = 5f;
    public float gravityChanger = 5f;

    public bool canJump = true;

    public GameObject jumpFXPrefab;
    public GameObject GroundCheck;

    public float jumpStrenght = 1.0f;
    public float jumpStrenghtMax = 1.0f;
    #endregion

    #region Children Var

    private GameObject ChildrenManager;

    private GameObject first;
    private GameObject second;
    private GameObject third;
    private GameObject fourth;
    private GameObject fifth;
    private GameObject sixth;
    private GameObject seventh;
    private GameObject eighth;

    public int totalChildrenNumber;

    #endregion

    public float[] scaleChanges;
    private GameObject graphics;
    [Space (10)]
    public float growingSpeed;
    GameObject[] persons;

    void Start()
    {
        myRb = gameObject.GetComponent<Rigidbody2D>();
        ChildrenManager = GameObject.FindGameObjectWithTag("ChildrenManager");

        first = ChildrenManager.transform.GetChild(0).gameObject;
        second = ChildrenManager.transform.GetChild(1).gameObject;
        third = ChildrenManager.transform.GetChild(2).gameObject;
        fourth = ChildrenManager.transform.GetChild(3).gameObject;
        fifth = ChildrenManager.transform.GetChild(4).gameObject;
        sixth = ChildrenManager.transform.GetChild(5).gameObject;
        seventh = ChildrenManager.transform.GetChild(6).gameObject;
        eighth = ChildrenManager.transform.GetChild(7).gameObject;


        graphics = transform.GetChild(4).gameObject;

    }

    void Update()
    {
        persons = GameObject.FindGameObjectsWithTag("Person");

        myRb.AddForce(new Vector2(0f, -gravityStrenght), ForceMode2D.Force);

        totalChildrenNumber = first.transform.childCount + second.transform.childCount + third.transform.childCount + fourth.transform.childCount + fifth.transform.childCount + sixth.transform.childCount + seventh.transform.childCount + eighth.transform.childCount; //quante persone attaccate in totale al player (conta anche le persone attaccate alle altre persone)


        if (jumpStrenght >= jumpStrenghtMax)
        {
            jumpStrenght = jumpStrenghtMax;
        }

        if(canMove)
        {
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);

            transform.position += movement * Time.deltaTime * moveSpeed;
        }

        #region Movement Mobile
        if (canMove)
        {
            dirX = Input.acceleration.x * moveSpeed;
            transform.position = new Vector2(Mathf.Clamp(transform.position.x, -5, 5), transform.position.y);

            if(dirX > 0 && dirX < 0.1)
            {
                
            }
            else
            if(dirX < 0 && dirX > - 0.1)
            {

            }
            else
            {
                Vector3 movement = new Vector3(dirX, 0f, 0f);

                transform.position += movement * Time.deltaTime * moveSpeed;
            }
            
        }
        #endregion


        #region Jump
        

        jumpStrenght = jumpStrenght + (totalChildrenNumber / 2);

        if (GroundCheck.activeSelf == false) //se il mio ground check NON è attivo (quindi ho figli addosso) fammi saltare solo quando la mia veloctità in Y è negativa
        {
            if (myRb.velocity.y <= 0)
            {
                gravityStrenght = gravityChanger / (totalChildrenNumber + 1);
                canJump = true;
            }
            else
            if (myRb.velocity.y >= 0)
            {
                gravityStrenght = gravityChanger;
                canJump = false;
            }
        }
        else
        if(GroundCheck.activeSelf == true)
        {
            if (myRb.velocity.y < 0)
            {
                canJump = false;
            }
            else
            if (myRb.velocity.y == 0)
            {
                canJump = true;
            }
        }

        if (Input.touchCount > 0 && canJump == true || Input.GetKeyDown(KeyCode.Space) && canJump == true) //da fixare il poter tener premuto
        {
            Jump();
        }
        #endregion

        #region scale feedback

        if(graphics.transform.localScale.x == scaleChanges[totalChildrenNumber])
        {
            foreach (GameObject Person in persons)
            {
                Person.GetComponent<CircleCollider2D>().enabled = true;
                Person.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
        else
        {
            foreach (GameObject Person in persons)
            {
                Person.GetComponent<CircleCollider2D>().enabled = false;
                Person.GetComponent<BoxCollider2D>().enabled = false;
            }
        }

        switch(totalChildrenNumber)
        {
            case 0:
                float newScale0 = Mathf.Lerp(graphics.transform.localScale.x, scaleChanges[0], Time.deltaTime * growingSpeed);
                graphics.transform.localScale = new Vector2(newScale0, newScale0);

                if (graphics.transform.localScale.x > scaleChanges[0] + 0.1f)
                {
                    graphics.transform.localScale = new Vector2(scaleChanges[0], scaleChanges[0]);
                }
                //da concludere con aggiunta figlio del player nell'array 
                break;
            case 1:
                float newScale1 = Mathf.Lerp(graphics.transform.localScale.x, scaleChanges[1], Time.deltaTime * growingSpeed);
                graphics.transform.localScale = new Vector2(newScale1, newScale1);

                if(graphics.transform.localScale.x > scaleChanges[1] - 0.1)
                {
                    graphics.transform.localScale = new Vector2(scaleChanges[1], scaleChanges[1]);
                }
                break;
            case 2:
                float newScale2 = Mathf.Lerp(graphics.transform.localScale.x, scaleChanges[2], Time.deltaTime * growingSpeed);
                graphics.transform.localScale = new Vector2(newScale2, newScale2);

                if (graphics.transform.localScale.x > scaleChanges[2] - 0.1)
                {
                    graphics.transform.localScale = new Vector2(scaleChanges[2], scaleChanges[2]);
                }
                break;
            case 3:
                float newScale3 = Mathf.Lerp(graphics.transform.localScale.x, scaleChanges[3], Time.deltaTime * growingSpeed);
                graphics.transform.localScale = new Vector2(newScale3, newScale3);

                if (graphics.transform.localScale.x > scaleChanges[3] - 0.1)
                {
                    graphics.transform.localScale = new Vector2(scaleChanges[3], scaleChanges[3]);
                }
                break;
            case 4:
                float newScale4 = Mathf.Lerp(graphics.transform.localScale.x, scaleChanges[4], Time.deltaTime * growingSpeed);
                graphics.transform.localScale = new Vector2(newScale4, newScale4);

                if (graphics.transform.localScale.x > scaleChanges[4] - 0.1)
                {
                    graphics.transform.localScale = new Vector2(scaleChanges[4], scaleChanges[4]);
                }
                break;
            case 5:
                float newScale5 = Mathf.Lerp(graphics.transform.localScale.x, scaleChanges[5], Time.deltaTime * growingSpeed);
                graphics.transform.localScale = new Vector2(newScale5, newScale5);

                if (graphics.transform.localScale.x > scaleChanges[5] - 0.1)
                {
                    graphics.transform.localScale = new Vector2(scaleChanges[5], scaleChanges[5]);
                }
                break;
            case 6:
                float newScale6 = Mathf.Lerp(graphics.transform.localScale.x, scaleChanges[6], Time.deltaTime * growingSpeed);
                graphics.transform.localScale = new Vector2(newScale6, newScale6);

                if (graphics.transform.localScale.x > scaleChanges[6] - 0.1)
                {
                    graphics.transform.localScale = new Vector2(scaleChanges[6], scaleChanges[6]);
                }
                break;
            case 7:
                float newScale7 = Mathf.Lerp(graphics.transform.localScale.x, scaleChanges[7], Time.deltaTime * growingSpeed);
                graphics.transform.localScale = new Vector2(newScale7, newScale7);

                if (graphics.transform.localScale.x > scaleChanges[7] - 0.1)
                {
                    graphics.transform.localScale = new Vector2(scaleChanges[7], scaleChanges[7]);
                }
                break;
            case 8:
                float newScale8 = Mathf.Lerp(graphics.transform.localScale.x, scaleChanges[8], Time.deltaTime * growingSpeed);
                graphics.transform.localScale = new Vector2(newScale8, newScale8);
                
                if (graphics.transform.localScale.x > scaleChanges[8] - 0.1)
                {
                    graphics.transform.localScale = new Vector2(scaleChanges[8], scaleChanges[8]);
                }
                break;
        }
        #endregion

    }


    public void Jump()
    {
        Instantiate(jumpFXPrefab, transform.position, Quaternion.identity);

        //myRb.constraints = RigidbodyConstraints2D.None;
        myRb.velocity = Vector2.up * jumpStrenght;

    }
}
