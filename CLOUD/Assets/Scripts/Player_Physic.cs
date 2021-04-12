using System.Collections;
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

    public float totalChildrenNumber;

    #endregion


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

    }

    void Update()
    {
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

    }

    public void Jump()
    {
        Instantiate(jumpFXPrefab, transform.position, Quaternion.identity);

        //myRb.constraints = RigidbodyConstraints2D.None;
        myRb.velocity = Vector2.up * jumpStrenght;

    }
}
