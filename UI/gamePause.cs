using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamePause : MonoBehaviour
{
    public GameObject _gamePause;
    
    public bool debug = true;
    public bool gameStart = false;
    static public bool pause = false;
    float timeCounter = 0;
    public countdown _countdown;

    //pauseSetting��escape��D�悳���邽��LateUpdate
    // Update is called once per frame
    void LateUpdate()
    {
        bool escButton = Input.GetKeyDown(KeyCode.Escape);
        bool optionButton = Input.GetButtonDown("Fire_Option");
        //�J�E���g�_�E�����I�������n�߂�
        if ((_countdown.realTime > 3 && gameStart == false)||debug)
        {
            gameStart = true;
        }
        if ((optionButton || escButton) && gameStart)
        {
            Pause();
        }
    }
    public void Pause()
    {
        if (!pause)
        {
            Time.timeScale = 0;
            pause = true;
            _gamePause.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pause = false;
            _gamePause.SetActive(false);
        }
    }
}
