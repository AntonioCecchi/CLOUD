using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EndGamePhase : MonoBehaviour
{
    private GameObject Player;
    private GameObject ChildrenManager;
    private GameObject EndPositions;
    private GameObject[] ChildrenManagerChildren;
    public GameObject Blink;
    public GameObject fade;

    [HideInInspector]
    public GameObject newPlayer;

    public CinemachineVirtualCamera vCam;

    #region get children
    private GameObject firstChild;
    private Vector3 InitialFirst;
    private Transform EndFirst;

    private GameObject secondChild;
    private Vector3 InitialSecond;
    private Transform EndSecond;

    private GameObject thirdChild;
    private Vector3 InitialThird;
    private Transform EndThird;

    private GameObject fourthChild;
    private Vector3 InitialFourth;
    private Transform EndFourth;

    private GameObject fifthChild;
    private Vector3 InitialFifth;
    private Transform EndFifth;

    private GameObject sixthChild;
    private Vector3 InitialSixth;
    private Transform EndSixth;
    #endregion

    private bool doneCoroutine = false;
    private bool goNewPlayer = false;
    private bool goToBlink = false;
    private bool awayChildren = false;

    private GameObject[] Enemies;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        EndPositions = GameObject.FindGameObjectWithTag("EndPositions");

        ChildrenManager = GameObject.FindGameObjectWithTag("ChildrenManager");

        #region get children pos
        firstChild = ChildrenManager.transform.GetChild(0).gameObject;
        EndFirst = EndPositions.transform.GetChild(0);
        secondChild = ChildrenManager.transform.GetChild(1).gameObject;
        EndSecond = EndPositions.transform.GetChild(1);
        thirdChild = ChildrenManager.transform.GetChild(2).gameObject;
        EndThird = EndPositions.transform.GetChild(2);
        fourthChild = ChildrenManager.transform.GetChild(3).gameObject;
        EndFourth = EndPositions.transform.GetChild(3);
        fifthChild = ChildrenManager.transform.GetChild(4).gameObject;
        EndFifth = EndPositions.transform.GetChild(4);
        sixthChild = ChildrenManager.transform.GetChild(5).gameObject;
        EndSixth = EndPositions.transform.GetChild(5);

        InitialFirst = firstChild.transform.position;
        InitialSecond = secondChild.transform.position;
        InitialThird = thirdChild.transform.position;
        InitialFourth = fourthChild.transform.position;
        InitialFifth = fifthChild.transform.position;
        InitialSixth = sixthChild.transform.position;
        #endregion
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(GameObject enemy in Enemies)
            {
                Destroy(enemy.gameObject);
            }

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

        //posiziona i figli a semicerchio
        awayChildren = true;

        yield return new WaitForSeconds(1f);

        //stacca i figli dal player
        ChildrenManagerChildren = ChildrenManager.GetComponent<PlayerChildrenManager>().PlayerCMChildren;

        foreach (GameObject child in ChildrenManagerChildren)
        {
            bool newPlayerFound = false;

            if (child.transform.childCount > 0 && !newPlayerFound)
            {
                newPlayer = child;
                newPlayerFound = true;
            }
        }

        goToBlink = true;

        awayChildren = false;

        yield return new WaitForSeconds(8f);

        goToBlink = false;
        goNewPlayer = true;
        //newPlayer.transform.position = Player.transform.position;

        vCam.Follow = gameObject.transform;

        yield return new WaitForSeconds(5f);

        goNewPlayer = false;
        doneCoroutine = true;

        yield return new WaitForSeconds(2f);

        fade.GetComponent<Animator>().SetTrigger("playerIsDead");
    }

    private void Update()
    {
        if(awayChildren)
        {
            firstChild.transform.position = Vector3.MoveTowards(firstChild.transform.position, EndFirst.transform.position, Time.deltaTime);
            secondChild.transform.position = Vector3.MoveTowards(secondChild.transform.position, EndSecond.transform.position, Time.deltaTime);
            thirdChild.transform.position = Vector3.MoveTowards(thirdChild.transform.position, EndThird.transform.position, Time.deltaTime);
            fourthChild.transform.position = Vector3.MoveTowards(fourthChild.transform.position, EndFourth.transform.position, Time.deltaTime);
            fifthChild.transform.position = Vector3.MoveTowards(fifthChild.transform.position, EndFifth.transform.position, Time.deltaTime);
            sixthChild.transform.position = Vector3.MoveTowards(sixthChild.transform.position, EndSixth.transform.position, Time.deltaTime);
        }

        if (goToBlink)
        {
            Player.transform.position = Vector3.MoveTowards(Player.transform.position, Blink.transform.position, 1.5f * Time.deltaTime);
        }

        if (goNewPlayer)
        {
            newPlayer.transform.position = Vector3.MoveTowards(newPlayer.transform.position, Player.transform.position, Time.deltaTime);
        }

        if(doneCoroutine)
        {
            Player.transform.Translate(Vector2.up * 2 * Time.deltaTime);
        }
    }
}
