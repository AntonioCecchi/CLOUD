using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChildrenManager : MonoBehaviour
{
    public GameObject[] PlayerCMChildren;

    private GameObject first;
    private GameObject second;
    private GameObject third;
    private GameObject fourth;
    private GameObject fifth;
    private GameObject sixth;
    //private GameObject seventh;
    //private GameObject eighth;

    [Space(10)]
    public GameObject Player;
    public GameObject GroundCheck;

    [HideInInspector]
    public Transform[] firstChildChildren;
    [HideInInspector]
    public Transform[] secondChildChildren;
    [HideInInspector]
    public Transform[] thirdChildChildren;
    [HideInInspector]
    public Transform[] fourthChildChildren;
    [HideInInspector]
    public Transform[] fifthChildChildren;
    [HideInInspector]
    public Transform[] sixthChildChildren;
    [HideInInspector]
    //public Transform[] seventhChildChildren;
    //[HideInInspector]
    //public Transform[] eighthChildChildren;


    private void Start()
    {
        first = PlayerCMChildren[0];
        second = PlayerCMChildren[1];
        third = PlayerCMChildren[2];
        fourth = PlayerCMChildren[3];
        fifth = PlayerCMChildren[4];
        sixth = PlayerCMChildren[5];
        //seventh = PlayerCMChildren[6];
        //eighth = PlayerCMChildren[7];
    }

    private void Update()
    {
        
        if(first.transform.childCount == 0 && second.transform.childCount == 0 && third.transform.childCount == 0 && fourth.transform.childCount == 0 && fifth.transform.childCount == 0 && sixth.transform.childCount == 0/* && seventh.transform.childCount == 0 && eighth.transform.childCount == 0*/) //se non ho figli attaccati a me (persone)
        {
            GroundCheck.SetActive(true);
        }
        else
        {
            GroundCheck.SetActive(false);
        }


    }

}
