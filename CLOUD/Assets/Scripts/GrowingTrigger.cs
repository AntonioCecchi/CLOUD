using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingTrigger : MonoBehaviour
{

    public Transform[] Steps;
    private Sprite[] growingSpritesSteps;

    [Space(10)]
    public GameObject playerGraphics;
    public GameObject playerBorder;

    private int triggerCount;

    private void Start()
    {
        //growingSpritesSteps = playerGraphics.GetComponent<PlayerGraphics>().growingSprites;
    }


    void Update()
    {
        switch(triggerCount)
        {
            case 1:
                transform.position = Steps[0].transform.position;
                playerBorder.GetComponent<SpriteRenderer>().sprite = growingSpritesSteps[0];
                break;
            case 2:
                transform.position = Steps[1].transform.position;
                playerBorder.GetComponent<SpriteRenderer>().sprite = growingSpritesSteps[1];
                break;
            case 3:
                transform.position = Steps[2].transform.position;
                playerBorder.GetComponent<SpriteRenderer>().sprite = growingSpritesSteps[2];
                break;
            case 4:
                transform.position = Steps[3].transform.position;
                playerBorder.GetComponent<SpriteRenderer>().sprite = growingSpritesSteps[3];
                break;
            case 5:
                transform.position = Steps[4].transform.position;
                playerBorder.GetComponent<SpriteRenderer>().sprite = growingSpritesSteps[4];
                break;
            case 6:
                transform.position = Steps[5].transform.position;
                playerBorder.GetComponent<SpriteRenderer>().sprite = growingSpritesSteps[5];
                break;
            case 7:
                transform.position = Steps[6].transform.position;
                playerBorder.GetComponent<SpriteRenderer>().sprite = growingSpritesSteps[6];
                break;
            case 8:
                transform.position = Steps[7].transform.position;
                playerBorder.GetComponent<SpriteRenderer>().sprite = growingSpritesSteps[7];
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            triggerCount++;
        }
    } 
}
