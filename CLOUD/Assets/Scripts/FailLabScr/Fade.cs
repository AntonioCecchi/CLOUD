using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    private SpriteRenderer spriteR;
    private Animator myAnimator;

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            ReloadScene();
        }
    }


    public void PlayerDeath()
    {

        myAnimator.SetTrigger("playerIsDead");
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }

}
