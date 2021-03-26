using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Scr : MonoBehaviour
{

    #region Movement Var
    [Header("Movement Variables")]
    public float moveSpeed = 5f;
    private Rigidbody2D myRb;
    public float dirX;
    public bool canMove = true;
    #endregion

    #region Jump Var
    [Header("Jump Variables")]
    public float gravityStrenght = 5f;
    public bool canJump = true;

    public GameObject jumpFXPrefab;

    public float jumpStrenght = 1.0f;
    public float jumpStrenghtMax = 1.0f;
    #endregion

    #region Person Var
    [Header("Person Variables")]
    public bool onFirstChild = false;
    public bool onSecondChild = false;
    public bool onThirdChild = false;
    public bool onFourthChild = false;
    public bool onFifthChild = false;

    private GameObject firstChild;
    private GameObject secondChild;
    private GameObject thirdChild;
    private GameObject fourthChild;
    private GameObject fifthChild;

    private GameObject firstPerson;
    private GameObject secondPerson;
    private GameObject thirdPerson;
    private GameObject fourthPerson;
    private GameObject fifthPerson;
    #endregion

    [Header("Death Variables")]
    public bool isDying;


    private Animator myAnimator;

    public GameObject fade;

    private void Start()
    {
        myAnimator = gameObject.GetComponent<Animator>();

        myRb = gameObject.GetComponent<Rigidbody2D>();

        firstChild = GameObject.FindGameObjectWithTag("firstChild");
        secondChild = GameObject.FindGameObjectWithTag("secondChild");
        thirdChild = GameObject.FindGameObjectWithTag("thirdChild");
        fourthChild = GameObject.FindGameObjectWithTag("fourthChild");
        fifthChild = GameObject.FindGameObjectWithTag("fifthChild");
    }

    private void Update()
    {
        myRb.AddForce(new Vector2(0f, -gravityStrenght), ForceMode2D.Force);

        if(jumpStrenght >= jumpStrenghtMax)
        {
            jumpStrenght = jumpStrenghtMax;
        }

        #region Movement Mobile
        if (canMove)
        {
            dirX = Input.acceleration.x * moveSpeed;
            transform.position = new Vector2(Mathf.Clamp(transform.position.x, -5, 5), transform.position.y);
            Vector3 movement = new Vector3(dirX, 0f, 0f);

            transform.position += movement * Time.deltaTime * moveSpeed;
        }
        #endregion
        
        #region Movement PC
        if (canMove)
        {

            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);

            transform.position += movement * Time.deltaTime * moveSpeed;
        }
        #endregion

      
        #region Jump

        if(myRb.velocity.y <= 0)
        {
            canJump = true;
        }
        else
        if(myRb.velocity.y >= 0)
        {
            canJump = false;
        }

        if (Input.touchCount > 0 && canJump == true || Input.GetKeyDown(KeyCode.Space) && canJump == true)
        {
            Jump();
        }

        //if (Input.acceleration.z > -0.5 && canJump == true)
        //{
        //    Jump();
        //}
        #endregion

        if (onFirstChild)
        {
            jumpStrenght += Time.deltaTime / 160;
        }
        if(onSecondChild)
        {
            jumpStrenght += Time.deltaTime / 160;
        }
        if (onThirdChild)
        {
            jumpStrenght += Time.deltaTime / 160;
        }
        if (onFourthChild)
        {
            jumpStrenght += Time.deltaTime / 160;
        }
        if (onFifthChild)
        {
            jumpStrenght += Time.deltaTime / 160;
        }

        if(gameObject.transform.childCount >= 4)
        {
            isDying = true;
        }

        if(isDying)
        {
            Destroy(gameObject.transform.GetChild(1).gameObject);
            Destroy(gameObject.transform.GetChild(2).gameObject);
            Destroy(gameObject.transform.GetChild(3).gameObject);

            canMove = false;

            myAnimator.SetTrigger("isDead");

            myRb.constraints = RigidbodyConstraints2D.FreezePosition;

            isDying = false;
        }

    }


    public void Jump()
    {
        Instantiate(jumpFXPrefab, transform.position, Quaternion.identity);

        myRb.constraints = RigidbodyConstraints2D.None;
        myRb.velocity = Vector2.up * jumpStrenght;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Person")
        {
           
            if (onFirstChild == false)
            {
                firstPerson = other.gameObject;

                other.transform.parent = firstChild.transform;

                other.transform.position = firstChild.transform.position;

                onFirstChild = true;
                firstPerson.GetComponent<Person_Scr>().isOnPlayer = true;

                firstPerson.GetComponent<Person_Scr>().ScaleSize();

                gravityStrenght -= 0.3f;
                jumpStrenght += 0.5f;
            }
            else
            if (onSecondChild == false)
            {
                secondPerson = other.gameObject;

                other.transform.parent = secondChild.transform;

                other.transform.position = secondChild.transform.position;

                onSecondChild = true;
                secondPerson.GetComponent<Person_Scr>().isOnPlayer = true;

                secondPerson.GetComponent<Person_Scr>().ScaleSize();

                gravityStrenght -= 0.3f;
                jumpStrenght += 0.5f;

            }
            else
            if (onThirdChild == false)
            {
                thirdPerson = other.gameObject;

                other.transform.parent = thirdChild.transform;

                other.transform.position = thirdChild.transform.position;

                onThirdChild = true;
                thirdPerson.GetComponent<Person_Scr>().isOnPlayer = true;

                thirdPerson.GetComponent<Person_Scr>().ScaleSize();

                gravityStrenght -= 0.3f;
                jumpStrenght += 0.5f;


            }
            else
            if (onFourthChild == false)
            {
                fourthPerson = other.gameObject;

                other.transform.parent = fourthChild.transform;

                other.transform.position = fourthChild.transform.position;

                onFourthChild = true;
                fourthPerson.GetComponent<Person_Scr>().isOnPlayer = true;

                fourthPerson.GetComponent<Person_Scr>().ScaleSize();

                gravityStrenght -= 0.3f;
                jumpStrenght += 0.5f;


            }
            else
            if (onFifthChild == false)
            {
                fifthPerson = other.gameObject;

                other.transform.parent = fifthChild.transform;

                other.transform.position = fifthChild.transform.position;

                onFifthChild = true;
                fifthPerson.GetComponent<Person_Scr>().isOnPlayer = true;

                fifthPerson.GetComponent<Person_Scr>().ScaleSize();

                gravityStrenght -= 0.3f;
                jumpStrenght += 0.5f;

            }
        }
    }

    public void Death()
    {
        if (firstPerson != null)
        {
            firstPerson.transform.SetParent(null);
        }

        if (secondPerson != null)
        {
            secondPerson.transform.SetParent(null);

        }

        if (thirdPerson != null)
        {
            thirdPerson.transform.SetParent(null);

        }

        if (fourthPerson != null)
        {
            fourthPerson.transform.SetParent(null);

        }

        if (fifthPerson != null)
        {
            fifthPerson.transform.SetParent(null);

        }



        if (onFirstChild)
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.transform.position = firstPerson.transform.position;

            firstPerson.GetComponent<SpriteRenderer>().sortingOrder = -1;

            myAnimator.SetTrigger("isReborn");

            //per adesso poi capiamo come fare meglio
            Destroy(firstPerson);

        }
        else
        if (onSecondChild)
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.transform.position = secondPerson.transform.position;

            secondPerson.GetComponent<SpriteRenderer>().sortingOrder = -1;

            myAnimator.SetTrigger("isReborn");

            //per adesso poi capiamo come fare meglio
            Destroy(secondPerson);

        }
        else
        if (onThirdChild)
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.transform.position = thirdPerson.transform.position;

            thirdPerson.GetComponent<SpriteRenderer>().sortingOrder = -1;

            myAnimator.SetTrigger("isReborn");

            //per adesso poi capiamo come fare meglio
            Destroy(thirdPerson);

        }
        else
        if (onFourthChild)
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.transform.position = fourthPerson.transform.position;

            fourthPerson.GetComponent<SpriteRenderer>().sortingOrder = -1;

            myAnimator.SetTrigger("isReborn");

            //per adesso poi capiamo come fare meglio
            Destroy(fourthPerson);

        }
        else
        if (onFifthChild)
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.transform.position = fifthPerson.transform.position;

            fifthPerson.GetComponent<SpriteRenderer>().sortingOrder = -1;

            myAnimator.SetTrigger("isReborn");

            //per adesso poi capiamo come fare meglio
            Destroy(fifthPerson);

        }
        else
        {
            //ENDGAME
            fade.GetComponent<Fade>().PlayerDeath();
        }



    }

    public void ResetPlayerStats()
    {
        jumpStrenght = 2;
        canMove = true;
        gameObject.GetComponent<Collider2D>().enabled = true;

        myRb.constraints = RigidbodyConstraints2D.None;
    }

}
