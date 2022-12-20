using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class title_reset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //cpu�����Z�b�g
        CPUSet.cpuCount = 3;
        //timescale�����Z�b�g
        Time.timeScale = 1;
        //���[���ݒ�����Z�b�g
        timeSet.gameTimer = 420;
        eventSet.eventFrecency = 1;
        itemSet.itemAmount = 1;
        //�ݒ�����Z�b�g
        raceSet.raceMode = false;
        //�����ݒ�����Z�b�g
        GameFinish.EnhanceCount = new int[] { 0, 0, 0, 0, 0 };
        //�g�p�{�[�������Z�b�g
        for (int i = 0; i < selectBallSet.selectBotHam.Length; i++)
        {
            selectBallSet.selectBotHam[i] = Random.Range(0, 8);
        }
        //��Ԃ̃{�[���ɃZ�b�g
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
