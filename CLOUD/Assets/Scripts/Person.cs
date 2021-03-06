using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    #region GameObj
    private GameObject MagneticField;
    private GameObject playerChildrenManager;
    private GameObject Player;

    private GameObject[] playerCMChildren;

    public GameObject child;


    #endregion

    #region onPlayerVar
    public float minOnPlayerTime;
    public float maxOnPlayerTime;
    private float OnPlayerTime;

    public bool isGoingAway = false;

    private float randomChance;
    #endregion

    public bool isAttracted = false;
    public bool isFree = true;
    public bool playerIsFull = false;

    private float timer1 = 1;
    private float timer1Max = 1;
    private float timer2 = 1;
    private float timer2Max = 1;

    #region WanderingVar
    private bool moveRight;
    private bool moveLeft;

    public float wanderingSpeed;
    #endregion

    float scaleChange = 1.5f;
    Vector2 scaleDefault = new Vector2(1, 1);

    private Animator fillAnimator;
    AudioSource audioSource;
    public AudioClip legameDone;
    public AudioClip legameBroken;

    [SerializeField]
    [Range(0, 5)]
    private float magneticForceMultiplier;


    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        MagneticField = GameObject.FindGameObjectWithTag("MagneticField");
        playerChildrenManager = GameObject.FindGameObjectWithTag("ChildrenManager");

        OnPlayerTime = Random.Range(minOnPlayerTime, maxOnPlayerTime);

        isFree = true;

        timer1Max = timer1;

        fillAnimator = transform.GetChild(0).GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {
        if(Player.GetComponent<Player_Physic>().totalChildrenNumber == 6)
        {
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
        }

        if(!isFree)
        {
            gameObject.layer = 8;
            float newScale = Mathf.Lerp(transform.localScale.x, scaleChange, Time.deltaTime * 0.1f);
            transform.localScale = new Vector2(newScale, newScale);
            fillAnimator.SetBool("isGrowing", true);
        }
        else
        if(isFree)
        {
            gameObject.layer = 0;
            transform.localScale = scaleDefault;
            fillAnimator.SetBool("isGrowing", true);
        }

        if (isFree && !isAttracted && !isGoingAway)
        {
            if (transform.position.x >= 4.5)
            {
                moveRight = false;
                moveLeft = true;
            }
            else
            if (transform.position.x <= -4.5)
            {
                moveRight = true;
                moveLeft = false;
            }

            if (moveRight)
            {
                transform.Translate(Vector2.right * wanderingSpeed * Time.deltaTime);
            }
            else
            if (moveLeft)
            {
                transform.Translate(Vector2.left * wanderingSpeed * Time.deltaTime);
            }
            else
            {
                float randomChance = Random.Range(0, 1);

                if (randomChance <= 0.5f)
                {
                    transform.Translate(Vector2.left * wanderingSpeed * Time.deltaTime);
                }
                else
                {
                    transform.Translate(Vector2.right * wanderingSpeed * Time.deltaTime);
                }
            }
        }

        if (isAttracted && GetComponent<BoxCollider2D>().enabled == true) 
        {
            float triggerRadius = MagneticField.GetComponent<CircleCollider2D>().radius;
            float scaleObj = MagneticField.GetComponent<Transform>().localScale.x;

            float distance = Vector2.Distance(MagneticField.transform.position, transform.position);
            float magneticForce = (triggerRadius * scaleObj) / distance;

            Vector3 dir = (MagneticField.transform.position - transform.position);


            transform.position = Vector2.MoveTowards(transform.position, MagneticField.transform.position, magneticForce * Time.deltaTime * magneticForceMultiplier);

            Debug.DrawLine(MagneticField.transform.position, MagneticField.transform.position - dir, Color.green);
        }

        if (isGoingAway)
        {
            if (randomChance < 3f)
            {
                Vector2 left = new Vector2(-5, -5);
                transform.Translate(left * 0.5f * Time.deltaTime);

                timer1 -= Time.deltaTime;
                timer2 -= Time.deltaTime;

                if (timer1 <= 0)
                {
                    GetComponent<CircleCollider2D>().enabled = true;
                    isFree = true;
                }

                if (timer2 <= 0)
                {
                    isGoingAway = false;
                    timer2 = timer2Max;
                    timer1 = timer1Max;
                }
            }
            else
            if (randomChance >= 3f && randomChance < 7f)
            {
                Vector2 right = new Vector2(5, -5);
                transform.Translate(right * 0.5f * Time.deltaTime);

                timer1 -= Time.deltaTime;
                timer2 -= Time.deltaTime;

                if (timer1 <= 0)
                {
                    GetComponent<CircleCollider2D>().enabled = true;
                    isFree = true;
                }

                if (timer2 <= 0)
                {
                    isGoingAway = false;
                    timer2 = timer2Max;
                    timer1 = timer1Max;
                }
            }
            else
            if (randomChance >= 7f)
            {
                Vector2 right = new Vector2(30, -3);
                transform.Translate(right * 0.2f * Time.deltaTime / 3);

                timer1 -= Time.deltaTime;
                timer2 -= Time.deltaTime;

                if (timer1 <= 0)
                {
                    GetComponent<CircleCollider2D>().enabled = true;
                    isFree = true;
                }

                if (timer2 <= 0)
                {
                    isGoingAway = false;
                    timer2 = timer2Max;
                    timer1 = timer1Max;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            audioSource.PlayOneShot(legameDone, 0.5f);
            isAttracted = false;
            isFree = false;
            isGoingAway = false;

            OneMoreChild();
        }
    }

    #region Magnetism
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "MagneticField" && !isGoingAway)
        {
            isAttracted = true;

            GetComponentInChildren<LineLegame>().drawLine();
        }
        else
        if (other.tag == "MagneticField" && isGoingAway)
        {
            GetComponentInChildren<LineLegame>().drawLine();
        }
    } 

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "MagneticField")
        {
            isAttracted = false;

            if (isFree)
            {
                GetComponentInChildren<LineLegame>().deleteLine();
            }
            else
            {
                GetComponentInChildren<LineRenderer>().enabled = false;
            }
        }
    } 
    #endregion

    public void GoAwayFromPlayer()
    {
        audioSource.PlayOneShot(legameBroken, 0.2f);
        GetComponent<CircleCollider2D>().enabled = false;
        randomChance = Random.Range(0, 10);

        isGoingAway = true;
        transform.parent = null;
    } 

    public void OneMoreChild()
    {
        playerCMChildren = playerChildrenManager.GetComponent<PlayerChildrenManager>().PlayerCMChildren; //prendi l'array dei figli del Player children manager

        foreach (GameObject playerChild in playerCMChildren) //per ogni figlio del player children manager cerca quello vuoto e assegnali a lui il parent della persona
        {
            if (playerChild.transform.childCount == 0) //se non ci sono persone attaccate
            {
                gameObject.transform.parent = playerChild.transform; //assegnazione parent persona -> attacca la persona al figlio del player
                gameObject.transform.position = playerChild.transform.position;
            }
        }
    } 
}
