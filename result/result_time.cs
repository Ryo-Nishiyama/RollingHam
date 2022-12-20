using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class result_time : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI GameTimer;
    [SerializeField] TextMeshProUGUI GameTimerDecimal;
    float _firstTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        _firstTime = result_ranking.firstTime;
        int minit = (int)Mathf.Floor(_firstTime / 60);
        int sec = (int)Mathf.Floor(_firstTime % 60);
        //D2Ç≈2åÖå≈íËâª
        string timeTextD = minit.ToString("D2") + ":" + sec.ToString("D2");
        //è¨êîì_à»â∫ï\é¶
        float secDeciaml = Mathf.Abs(_firstTime) % 60 - Mathf.Floor(_firstTime % 60);
        string timeTextF = (secDeciaml.ToString() + "0000").Substring(1, 3);

        GameTimer.text = timeTextD;
        GameTimerDecimal.text = timeTextF;
    }
}
