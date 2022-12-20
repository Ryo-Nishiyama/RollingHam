using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class title_reset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //cpu数リセット
        CPUSet.cpuCount = 3;
        //timescaleをリセット
        Time.timeScale = 1;
        //ルール設定をリセット
        timeSet.gameTimer = 420;
        eventSet.eventFrecency = 1;
        itemSet.itemAmount = 1;
        //設定をリセット
        raceSet.raceMode = false;
        //強化設定をリセット
        GameFinish.EnhanceCount = new int[] { 0, 0, 0, 0, 0 };
        //使用ボールをリセット
        for (int i = 0; i < selectBallSet.selectBotHam.Length; i++)
        {
            selectBallSet.selectBotHam[i] = Random.Range(0, 8);
        }
        //一番のボールにセット
        selectBallSet.selectHam = 1;
        raceSet.levelList = new int[] { 0, 0, 0, 0, 0, 0, 3 };
        for (int i=0;i< GameFinish.EnhanceCount.Length; i++)
        {
            GameFinish.EnhanceCount[i] = 0;
        }
        for (int i = 0; i < GameFinish.EnhanceBotCount.Length; i++)
        {
            for (int j = 0; j < GameFinish.EnhanceBotCount[0].Length; j++)
            {
                GameFinish.EnhanceBotCount[i][j] = 0;
            }
        }
        for (int i = 0; i < GameFinish.NumBotCount.Length; i++)
        {
            GameFinish.NumBotCount[i] = 0;
        }
        GameFinish.NumCount = 0;
    }
}
