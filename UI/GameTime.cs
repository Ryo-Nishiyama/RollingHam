using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTime : MonoBehaviour
{
    public TextMeshProUGUI GameTimetxt;
    public TextMeshProUGUI GameTimetxtDeciaml;
    public float gameTimer=420.0f;
    public bool gameFinish = false;
    public bool debug = true;
    public float timeCounter = 0f;
    int minit, sec;
    float secDeciaml;
    string timeTextD, timeTextF;
    // Start is called before the first frame update
    void Start()
    {
        if (!debug)
        {
            gameTimer = timeSet.getTimer();
        }
        //�������\��
        minit = (int)Mathf.Floor(gameTimer / 60);
        sec = (int)Mathf.Floor((gameTimer) % 60);
        //D2��2���Œ艻
        timeTextD = minit.ToString("D2") + ":" + sec.ToString("D2");
        //�����_�ȉ��\��
        timeTextF = ".00";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (countdown.startGameCheck)
        {
            setTimerTime();
        }
        //�^�C�}�[�I��
        if (timeCounter >= gameTimer)
        {
            gameFinish = true;
            timeTextD = "00:00";
            timeTextF = (".00");
        }
        GameTimetxt.text = timeTextD;
        GameTimetxtDeciaml.text = timeTextF;
    }
    void setTimerTime()
    {
        //�������\��
        timeCounter += Time.deltaTime;
        minit = (int)Mathf.Ceil(gameTimer / 60) - (int)Mathf.Floor(timeCounter / 60) - 1;
        sec = (int)Mathf.Floor((gameTimer - timeCounter) % 60);
        //D2��2���Œ艻
        timeTextD = minit.ToString("D2") + ":" + sec.ToString("D2");
        //�����_�ȉ��\��
        secDeciaml = Mathf.Abs((gameTimer - timeCounter) % 60 - Mathf.Floor(gameTimer - timeCounter) % 60);
        timeTextF = (secDeciaml.ToString() + "0000").Substring(1, 3);
    }
}
