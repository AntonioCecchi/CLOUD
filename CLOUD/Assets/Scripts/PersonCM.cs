using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonCM : MonoBehaviour
{
    public GameObject[] PersonCMChildren;

    private GameObject personMiddleChild;
    private GameObject personLeftChild;
    private GameObject personRightChild;

    [HideInInspector]
    public Transform[] personMiddleChildChildren;
    [HideInInspector]
    public Transform[] personLeftChildChildren;
    [HideInInspector]
    public Transform[] personRightChildChildren;


    private void Start()
    {
        personMiddleChild = PersonCMChildren[2];
        personLeftChild = PersonCMChildren[1];
        personRightChild = PersonCMChildren[0];
    }

    private void Update()
    {
        //controlla quanti figli hanno i miei figli = quante persone sono legate ai miei 3 anchor points
        personMiddleChildChildren = personMiddleChild.GetComponentsInChildren<Transform>();
        personLeftChildChildren = personLeftChild.GetComponentsInChildren<Transform>();
        personRightChildChildren = personRightChild.GetComponentsInChildren<Transform>();

    }



}
