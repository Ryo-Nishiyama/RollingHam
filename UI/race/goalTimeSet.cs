using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class goalTimeSet : MonoBehaviour
{
    public TextMeshProUGUI GameTimetxt;
    public TextMeshProUGUI GameTimetxtDeciaml;
    public goalCount _goalCount;

    public float gameTimer = 420.0f;
    public bool gameFinish = false;
    public bool debug = true;
    public float timeCounter = 0f;
    int minit, sec;
    float secDeciaml;
    string timeTextD, timeTextF;

    // Start is called before the first frame update
    void Start()
    {
        timeTextD = "00:00";
        timeTextF = ".00";
    }

    // Update is called once per frame
    void Update()
    {
        if (countdown.startGameCheck)
        {
            timeCounter = _goalCount.goalTime;

            minit = (int)Mathf.Floor(timeCounter / 60);
            sec = (int)Mathf.Floor(timeCounter % 60);
            //D2で2桁固定化
            timeTextD = minit.ToString("D2") + ":" + sec.ToString("D2");
            //小数点以下表示
            secDeciaml = Mathf.Abs(timeCounter) % 60 - Mathf.Floor(timeCounter % 60);
            timeTextF = (secDeciaml.ToString() + "0000").Substring(1, 3);
        }

        //タイマー終了
        if (_goalCount.goal)
        {
            gameFinish = true;
        }
        GameTimetxt.text = timeTextD;
        GameTimetxtDeciaml.text = timeTextF;
    }
}
