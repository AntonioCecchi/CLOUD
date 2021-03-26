using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpFx : MonoBehaviour
{
    public float timerDestroy;


    void Update()
    {
        timerDestroy -= Time.deltaTime;
        
        if(timerDestroy <= 0)
        {
            Destroy(gameObject);
        }
    }
}
