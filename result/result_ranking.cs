using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class result_ranking : MonoBehaviour
{
    [SerializeField] GameObject mainBall;
    [SerializeField] GameObject[] mainBallBot;
    int activeBallNum = 0;
    List<float> rankingTime = new List<float>() { 6000, 6000, 6000, 6000 };
    List<int> rankingRand = new List<int>() { 1, 2, 3 };
    static public List<int> ranking = new List<int>(){ 0, 1, 2, 3 };
    static public float firstTime = 72.123456f;

    // Start is called before the first frame update
    void Start()
    {

        for(int i = 0; i < mainBallBot.Length; i++)
        {
            if (mainBallBot[i].activeSelf)
            {
                activeBallNum += 1;
                //Botが未ゴールの時の順位をばらけさせる
                int rand = Random.Range(0, rankingRand.Count);
                mainBallBot[i].GetComponent<goalCount>().goalTime = rankingRand[rand] * 0.0001f;
                //使用した数字を削除
                rankingRand.RemoveAt(rand);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (mainBall.GetComponent<goalCount>().goal)
        {
            rankingTime[0] = mainBall.GetComponent<goalCount>().goalTime;
            for (int i = 0; i < mainBallBot.Length; i++)
            {
                if (mainBallBot[i].activeSelf)
                {
                    rankingTime[i + 1] = mainBallBot[i].GetComponent<goalCount>().goalTime;
                }
            }
            List<float> rankingTime2 = new List<float>(rankingTime);
            rankingTime2.Sort();
            for (int i = 0; i < ranking.Count; i++)
            {
                //登録されていないプレイヤーの場合終了
                if (rankingTime[i] == 6000)
                {
                    break;
                }
                for (int j = 0; j < ranking.Count; j++)
                {
                    //順位情報を保存
                    if (rankingTime[i] == rankingTime2[j])
                    {
                        ranking[i] = j;
                        break;
                    }
                }
            }
            firstTime = rankingTime.Min();
        }
    }
}
