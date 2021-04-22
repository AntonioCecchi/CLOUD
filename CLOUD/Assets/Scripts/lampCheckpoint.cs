using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lampCheckpoint : MonoBehaviour
{

    public GameObject lampOn;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            lampOn.SetActive(true);
        }
    }
}
