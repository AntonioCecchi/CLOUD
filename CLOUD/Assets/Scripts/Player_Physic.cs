using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Physic : MonoBehaviour
{
    [HideInInspector]
    public bool isFrozen;


    #region Movement Var
    [Header("Movement Variables")]

    private Rigidbody2D myRb;
    public bool canMove;
    #endregion

    #region Jump Var
    [Header("Jump Variables")]
    public bool canJump;
    [Space(10)]
    public float gravityStrenght = 5f;
    [Tooltip("il numero che viene diviso per il numero dei figli a te legati in quel momento. La formula è: Grv Str = Grv chng / totnumchild")]
    public float gravityChanger = 5f;
    public GameObject jumpFXPrefab;
    public GameObject GroundCheck;
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

    [Space(10)]
    public float totalChildrenNumber;

    #endregion

    #region scaling
    [Space(10)]
    public float[] scaleChanges;
    private GameObject graphics;
    [Space (10)]
    public float growingSpeed;
    GameObject[] persons;

    private Animator myAnim;

    public GameObject MagneticField;
    #endregion

    private float swipeStartTime;
    private float swipeEndTime;
    private float swipeTime;
    private float swipeLenght;
    [HideInInspector]
    public Vector2 startSwipePosition;
    [HideInInspector]
    public Vector2 endSwipePosition;

    public float maxSwipeTime;
    public float maxSwipeDistance;
    public float minSwipeDistance;

    [Space(10)]
    public float initialSwipeMovementSpeed;
    public float swipeMovementSpeed;
    public float ySwipeRange;


    public bool isGrounded = false;


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
        
        myAnim = GetComponent<Animator>();
    }

    void Update()
    {
        if(isFrozen)
        {
            myRb.constraints = RigidbodyConstraints2D.FreezeAll;
            canMove = false;
            canJump = false;
        }
        else
        if(!isFrozen)
        {
            canMove = true;
            myRb.constraints = RigidbodyConstraints2D.None;
            myRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }


        persons = GameObject.FindGameObjectsWithTag("Person");

        myRb.AddForce(new Vector2(0f, -gravityStrenght), ForceMode2D.Force);

        totalChildrenNumber = first.transform.childCount + second.transform.childCount + third.transform.childCount + fourth.transform.childCount + fifth.transform.childCount + sixth.transform.childCount + seventh.transform.childCount + eighth.transform.childCount; //quante persone attaccate in totale al player (conta anche le persone attaccate alle altre persone)


        #region Movement Mobile
        if (canMove && canJump)
        {
            if(Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    swipeStartTime = Time.time;
                    startSwipePosition = touch.position;
                }
                else
                if(touch.phase == TouchPhase.Ended)
                {
                    swipeEndTime = Time.time;
                    endSwipePosition = touch.position;
                    swipeTime = swipeEndTime - swipeStartTime;
                    swipeLenght = (endSwipePosition - startSwipePosition).magnitude;

                    if(swipeLenght > maxSwipeDistance)
                    {
                        swipeLenght = maxSwipeDistance;
                    }

                    if(endSwipePosition.y < startSwipePosition.y)
                    {

                    }
                    else
                    if(swipeTime < maxSwipeTime && swipeLenght > minSwipeDistance && swipeLenght <= maxSwipeDistance)
                    {
                        SwipeMovement();
                    }
                }
            }
        }

        #endregion

        #region Swipe
        swipeMovementSpeed = initialSwipeMovementSpeed + totalChildrenNumber / 6;
        Debug.Log(swipeMovementSpeed);

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

        #endregion

        #region scale feedback

        //if(graphics.transform.localScale.x == scaleChanges[totalChildrenNumber])
        //{
        //    foreach (GameObject Person in persons)
        //    {
        //        Person.GetComponent<CircleCollider2D>().enabled = true;
        //        Person.GetComponent<BoxCollider2D>().enabled = true;
        //        myAnim.SetBool("isGrowing", false);
        //    }
        //}
        //else
        //if(graphics.transform.localScale.x == scaleChanges[6])
        //{
        //    foreach (GameObject Person in persons)
        //    {
        //        Person.GetComponent<CircleCollider2D>().enabled = false;
        //        Person.GetComponent<BoxCollider2D>().enabled = false;
        //        myAnim.SetBool("isGrowing", false);
        //    }
        //}
        //else
        //{
        //    foreach (GameObject Person in persons)
        //    {
        //        Person.GetComponent<CircleCollider2D>().enabled = false;
        //        Person.GetComponent<BoxCollider2D>().enabled = false;
        //        myAnim.SetBool("isGrowing", true);
        //    }
        //}

        switch (totalChildrenNumber)
        {
            case 0:
                graphics.transform.localScale = new Vector2 (scaleChanges[0], scaleChanges[0]);
                GetComponent<CircleCollider2D>().radius = 0.2f;
                break;
            case 1:
                float newScale1 = Mathf.Lerp(graphics.transform.localScale.x, scaleChanges[1], Time.deltaTime * growingSpeed);
                graphics.transform.localScale = new Vector2(newScale1, newScale1);
                myAnim.SetBool("isGrowing", true);

                if (graphics.transform.localScale.x > scaleChanges[1] - 0.1)
                {
                    myAnim.SetBool("isGrowing", false);
                    graphics.transform.localScale = new Vector2(scaleChanges[1], scaleChanges[1]);
                    GetComponent<CircleCollider2D>().radius = 0.3f;
                }
                break;
            case 2:
                float newScale2 = Mathf.Lerp(graphics.transform.localScale.x, scaleChanges[2], Time.deltaTime * growingSpeed);
                graphics.transform.localScale = new Vector2(newScale2, newScale2);
                myAnim.SetBool("isGrowing", true);

                if (graphics.transform.localScale.x > scaleChanges[2] - 0.1)
                {
                    myAnim.SetBool("isGrowing", false);
                    graphics.transform.localScale = new Vector2(scaleChanges[2], scaleChanges[2]);
                    GetComponent<CircleCollider2D>().radius = 0.4f;
                }
                break;
            case 3:
                float newScale3 = Mathf.Lerp(graphics.transform.localScale.x, scaleChanges[3], Time.deltaTime * growingSpeed);
                graphics.transform.localScale = new Vector2(newScale3, newScale3);
                myAnim.SetBool("isGrowing", true);

                if (graphics.transform.localScale.x > scaleChanges[3] - 0.1)
                {
                    myAnim.SetBool("isGrowing", false);
                    graphics.transform.localScale = new Vector2(scaleChanges[3], scaleChanges[3]);
                    GetComponent<CircleCollider2D>().radius = 0.5f;
                }
                break;
            case 4:
                float newScale4 = Mathf.Lerp(graphics.transform.localScale.x, scaleChanges[4], Time.deltaTime * growingSpeed);
                graphics.transform.localScale = new Vector2(newScale4, newScale4);
                myAnim.SetBool("isGrowing", true);

                if (graphics.transform.localScale.x > scaleChanges[4] - 0.1)
                {
                    myAnim.SetBool("isGrowing", false);
                    graphics.transform.localScale = new Vector2(scaleChanges[4], scaleChanges[4]);
                    GetComponent<CircleCollider2D>().radius = 0.6f;
                }
                break;
            case 5:
                float newScale5 = Mathf.Lerp(graphics.transform.localScale.x, scaleChanges[5], Time.deltaTime * growingSpeed);
                graphics.transform.localScale = new Vector2(newScale5, newScale5);
                myAnim.SetBool("isGrowing", true);

                if (graphics.transform.localScale.x > scaleChanges[5] - 0.1)
                {
                    myAnim.SetBool("isGrowing", false);
                    graphics.transform.localScale = new Vector2(scaleChanges[5], scaleChanges[5]);
                    GetComponent<CircleCollider2D>().radius = 0.7f;
                }
                break;
            case 6:
                float newScale6 = Mathf.Lerp(graphics.transform.localScale.x, scaleChanges[6], Time.deltaTime * growingSpeed);
                graphics.transform.localScale = new Vector2(newScale6, newScale6);
                myAnim.SetBool("isGrowing", true);

                if (graphics.transform.localScale.x > scaleChanges[6] - 0.1)
                {
                    myAnim.SetBool("isGrowing", false);
                    graphics.transform.localScale = new Vector2(scaleChanges[6], scaleChanges[6]);
                    GetComponent<CircleCollider2D>().radius = 0.8f;
                }
                break;
            //case 7:
            //    float newScale7 = Mathf.Lerp(graphics.transform.localScale.x, scaleChanges[7], Time.deltaTime * growingSpeed);
            //    graphics.transform.localScale = new Vector2(newScale7, newScale7);

            //    if (graphics.transform.localScale.x > scaleChanges[7] - 0.1)
            //    {
            //        graphics.transform.localScale = new Vector2(scaleChanges[7], scaleChanges[7]);
            //    }
            //    break;
            //case 8:
            //    float newScale8 = Mathf.Lerp(graphics.transform.localScale.x, scaleChanges[8], Time.deltaTime * growingSpeed);
            //    graphics.transform.localScale = new Vector2(newScale8, newScale8);
                
            //    if (graphics.transform.localScale.x > scaleChanges[8] - 0.1)
            //    {
            //        graphics.transform.localScale = new Vector2(scaleChanges[8], scaleChanges[8]);
            //    }
            //    break;
        }
        #endregion

        if(totalChildrenNumber == 6)
        {
            StartCoroutine(DeactivatePerson());
        }
        else
        {
            StartCoroutine(ActivatePerson());
        }
    }

    public void SwipeMovement()
    {
        Vector3 swipeDirection = (endSwipePosition - startSwipePosition);

        if(totalChildrenNumber == 0 && isGrounded == false)
        {
            Vector3 horizontalSwipe = new Vector3(endSwipePosition.x - startSwipePosition.x, 0, 0);
            myRb.AddForce(horizontalSwipe * swipeMovementSpeed * Time.deltaTime, ForceMode2D.Impulse);
            Instantiate(jumpFXPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            myRb.AddForce(swipeDirection * swipeMovementSpeed *  Time.deltaTime, ForceMode2D.Impulse);
            Instantiate(jumpFXPrefab, transform.position, Quaternion.identity);
        }
    }

    public IEnumerator DeactivatePerson()
    {
        yield return new WaitForSeconds(0.5f);

        GameObject[] People = GameObject.FindGameObjectsWithTag("Person");

        foreach (GameObject person in People)
        {
            if (person.layer == 8)
            {

            }
            else
            {
                person.GetComponent<SpriteRenderer>().enabled = false;
                person.GetComponent<CircleCollider2D>().enabled = false;
                person.GetComponent<BoxCollider2D>().enabled = false;
                person.GetComponent<Person>().enabled = false;
                person.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }

    public IEnumerator ActivatePerson()
    {
        yield return new WaitForSeconds(0.5f);

        GameObject[] People = GameObject.FindGameObjectsWithTag("Person");

        foreach (GameObject person in People)
        {
            if (person.layer == 8)
            {

            }
            else
            {
                person.GetComponent<SpriteRenderer>().enabled = true;
                person.GetComponent<CircleCollider2D>().enabled = true;
                person.GetComponent<BoxCollider2D>().enabled = true;
                person.GetComponent<Person>().enabled = true;
                person.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }
}
