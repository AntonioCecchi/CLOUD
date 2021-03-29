using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineLegame : MonoBehaviour
{

    public GameObject player;
    public GameObject person;

    private LineRenderer myLine;

    private void Start()
    {
        myLine = GetComponent<LineRenderer>();
    }

    public void drawLine()
    {
        myLine.enabled = true;

        Vector3 myPos = new Vector3(person.transform.position.x, person.transform.position.y, 0);
        Vector3 playerPos = new Vector3(player.transform.position.x, player.transform.position.y, 0);

        myLine.SetPosition(0, myPos);
        myLine.SetPosition(1, playerPos);
    }

    public void deleteLine()
    {
        myLine.enabled = false;
    }
}
