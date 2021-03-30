using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineLegame : MonoBehaviour
{

    private GameObject player;
    public GameObject person;

    private LineRenderer myLine;
    public GameObject ParticleLegameRotto;

    private GameObject midPoint;

    private void Start()
    {
        myLine = GetComponent<LineRenderer>();

        player = GameObject.FindGameObjectWithTag("Player");
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

        float posX = ParticleLegameRotto.transform.position.x;
        float posY = ParticleLegameRotto.transform.position.y;
        posX = player.transform.position.x + (person.transform.position.x - player.transform.position.x) / 2;
        posY = player.transform.position.y + (person.transform.position.y - player.transform.position.y) / 2;
        Vector3 midPoint = new Vector3(posX, posY, 0);
        ParticleLegameRotto.transform.position = midPoint;

        Vector3 dir = person.transform.position - player.transform.position;

        
        ParticleLegameRotto.transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
        ParticleLegameRotto.transform.Rotate(0, 90, 0);

        ParticleLegameRotto.GetComponent<ParticleSystem>().Play();

    }
}
