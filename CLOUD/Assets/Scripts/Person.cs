using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    #region GameObj
    private GameObject MagneticField;
    private GameObject playerChildrenManager;

    private GameObject [] playerCMChildren;

    public GameObject child;


    #endregion

    #region onPlayerVar
    public float minOnPlayerTime;
    public float maxOnPlayerTime;
    private float OnPlayerTime;
    private float OnPlayerTimeMax;
    private float goAwayChance;

    private bool isGoingAway = false;
    private bool doneRight = true;
    #endregion

    public bool isAttracted = false;
    public bool isFree = true;

    private float timer1 = 1;
    private float timer1Max = 1;
    private float timer2 = 5;
    private float timer2Max = 5;

    [SerializeField]
    [Range(0, 5)]
    private float magneticForceMultiplier;

    private void Start()
    {
        MagneticField = GameObject.FindGameObjectWithTag("MagneticField");
        playerChildrenManager = GameObject.FindGameObjectWithTag("ChildrenManager");

        OnPlayerTime = Random.Range(minOnPlayerTime, maxOnPlayerTime);
        OnPlayerTimeMax = OnPlayerTime;

        
        isFree = true;

        timer1Max = timer1;

        child.GetComponent<Animator>().SetBool("isFree", true);

    }

    private void Update()
    {

        if(isAttracted) //Quando sono nel raggio di azione vado verso il Player
        {
            float triggerRadius = MagneticField.GetComponent<CircleCollider2D>().radius;
            float scaleObj = MagneticField.GetComponent<Transform>().localScale.x;

            float distance = Vector2.Distance(MagneticField.transform.position, transform.position);
            float magneticForce = (triggerRadius * scaleObj) / distance;

            Vector3 dir = (MagneticField.transform.position - transform.position);


            transform.position = Vector2.MoveTowards(transform.position, MagneticField.transform.position, magneticForce * Time.deltaTime * magneticForceMultiplier);

            Debug.DrawLine(MagneticField.transform.position, MagneticField.transform.position - dir, Color.green);
        }

        if(!isFree)
        {
            child.GetComponent<Animator>().SetBool("isFree", false);

            if (goAwayChance > -2)
            {
                OnPlayerTime -= Time.deltaTime;
                
                if(OnPlayerTime <= 0)
                {
                    GoAwayFromPlayer();

                    OnPlayerTime = OnPlayerTimeMax;
                }
            }
            else
            {
                OnPlayerTime -= Time.deltaTime;

                if (OnPlayerTime <= 3)
                {
                    goAwayChance = Random.Range(-5, 5);

                    OnPlayerTime = OnPlayerTimeMax;
                }
            }
        }

        if(isGoingAway)
        {
            child.GetComponent<Animator>().SetBool("isFree", true);

            if (doneRight)
            {
                Vector2 left = new Vector2(-6, 5);
                transform.Translate(left * 0.05f * Time.deltaTime);

                timer1 -= Time.deltaTime;
                timer2 -= Time.deltaTime;

                if (timer1 <= 0)
                {
                    GetComponent<CircleCollider2D>().enabled = true;

                    timer1 = timer1Max;
                }

                if(timer2 <=0)
                {
                    isGoingAway = false;
                    timer2 = timer2Max;

                    doneRight = false;
                }
            }
            else
            if(!doneRight)
            {
                Vector2 right = new Vector2(6, 5);
                transform.Translate(right * 0.05f * Time.deltaTime);

                timer1 -= Time.deltaTime;
                timer2 -= Time.deltaTime;

                if (timer1 <= 0)
                {
                    GetComponent<CircleCollider2D>().enabled = true;

                    timer1 = timer1Max;
                }

                if (timer2 <= 0)
                {
                    isGoingAway = false;
                    doneRight = false;

                    timer2 = timer2Max;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isAttracted = false;
            isFree = false;
            isGoingAway = false;

            goAwayChance = Random.Range(-5, 5);
            OnPlayerTime = Random.Range(minOnPlayerTime, maxOnPlayerTime);
            OnPlayerTimeMax = OnPlayerTime;

            Debug.Log(goAwayChance);

            OneMoreChild();
        }
    }

    #region Magnetism
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "MagneticField" && !isGoingAway)
        {
            isAttracted = true;

            GetComponentInChildren<LineLegame>().drawLine();
        }
        else
        if(other.tag == "MagneticField" && isGoingAway)
        {
            GetComponentInChildren<LineLegame>().drawLine();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "MagneticField")
        {
            isAttracted = false;

            if(isFree)
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

    private void GoAwayFromPlayer()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = false;

        isGoingAway = true;
        isFree = true;
        gameObject.transform.parent = null;
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
