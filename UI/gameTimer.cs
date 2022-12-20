using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameTimer : MonoBehaviour
{
    public bool debug = true;
    public bool gameStart = false;
    public bool pause = false;
    float timeCounter = 0;
    public countdown _countdown;
    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    void Start()
    {
        if (!debug)
        {
            Time.timeScale = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool escButton= Input.GetKeyDown(KeyCode.Escape);
        bool optionButton = Input.GetButtonDown("Fire_Option");
        //カウントダウンが終わったら始める
        if (_countdown.realTime > 3 && gameStart == false)
        {
            gameStart = true;
            Time.timeScale = 1;
        }
        if ((optionButton || escButton) && gameStart)
        {
            Pause();
        }
    }

    void Pause()
    {
        if (!pause)
        {
            Time.timeScale = 0;
            pause = true;
        }
        else
        {
            Time.timeScale = 1;
            pause = false;
        }
    }

}
