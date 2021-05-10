using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDebug : MonoBehaviour
{
    public GameObject Player;
    public  TMP_Text yRangeText;
    public  TMP_Text initialSwipeSpeedText;
    public  TMP_Text swipeSpeedText;
    public  TMP_Text yRangeDoneText;


    void Start()
    {
        float YRange = Player.GetComponent<Player_Physic>().ySwipeRange;
        float initialSwipeSpeed = Player.GetComponent<Player_Physic>().initialSwipeMovementSpeed;
        

        yRangeText.text = YRange.ToString();
        initialSwipeSpeedText.text = initialSwipeSpeed.ToString();
    }

    private void Update()
    {
        float swipeSpeed = Player.GetComponent<Player_Physic>().swipeMovementSpeed;
        swipeSpeedText.text = swipeSpeed.ToString();

        float yStart = Player.GetComponent<Player_Physic>().startSwipePosition.y;
        float yEnd = Player.GetComponent<Player_Physic>().endSwipePosition.y;

        float yRangeDone = yEnd - Mathf.Abs(yStart);

        yRangeDoneText.text = yRangeDone.ToString();
        
    }
}
