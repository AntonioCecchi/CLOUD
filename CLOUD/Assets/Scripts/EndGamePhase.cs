using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGamePhase : MonoBehaviour
{
    private GameObject Player;
    private GameObject ChildrenManager;
    private GameObject EndPositions;
    private GameObject[] ChildrenManagerChildren;
    public GameObject Blink;

    [HideInInspector]
    public GameObject newPlayer;

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
        Player.transform.position = new Vector2(Blink.transform.position.x, Player.transform.position.y);


        //posiziona i figli a semicerchio
        firstChild.transform.position = EndFirst.transform.position;
        secondChild.transform.position = EndSecond.transform.position;
        thirdChild.transform.position = EndThird.transform.position;
        fourthChild.transform.position = EndFourth.transform.position;
        fifthChild.transform.position = EndFifth.transform.position;
        sixthChild.transform.position = EndSixth.transform.position;

        yield return new WaitForSeconds(1f);

        //stacca i figli dal player
        ChildrenManagerChildren = ChildrenManager.GetComponent<PlayerChildrenManager>().PlayerCMChildren;

        foreach (GameObject child in ChildrenManagerChildren)
        {
            if (child.transform.childCount > 0)
            {
                child.transform.GetChild(0).parent = null;
            }
        }

        Player.transform.position = Blink.transform.position;
    }
}
