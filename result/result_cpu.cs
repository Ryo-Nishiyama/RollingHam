using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class result_cpu : MonoBehaviour
{
    [SerializeField] GameObject[] resultSetCPU;
    [SerializeField] List<GameObject> counter_1;
    [SerializeField] List<GameObject> counter_2;
    [SerializeField] List<GameObject> counter_3;
    [SerializeField] List<GameObject> ability_1;
    [SerializeField] List<GameObject> ability_2;
    [SerializeField] List<GameObject> ability_3;
    [SerializeField] GameObject[] BotBall;
    [SerializeField] GameObject[] BackStripe;
    [SerializeField] GameObject[] rankingImg;
    [SerializeField] Material[] ball_material;
    List<List<GameObject>> counterList = new List<List<GameObject>>();
    List<List<GameObject>> abilityList = new List<List<GameObject>>();
    [SerializeField] GameObject[] NumCounter;
    int[][] _EnhanceBotCount;
    int[] _NumBotCount;
    int cpuCount;

    // Start is called before the first frame update
    void Start()
    {
        counterList.Add(counter_1);
        counterList.Add(counter_2);
        counterList.Add(counter_3);
        abilityList.Add(ability_1);
        abilityList.Add(ability_2);
        abilityList.Add(ability_3);
        _EnhanceBotCount = GameFinish.EnhanceBotCount;
        _NumBotCount = GameFinish.NumBotCount;
        if (raceSet.raceMode)
        {
            cpuCount = raceSet.levelList[6];
        }
        else
        {
            cpuCount = CPUSet.cpuCount;
        }
        for(int i = 0; i < resultSetCPU.Length; i++)
        {
            //カウント以上のボールを非表示
            if (i+1 > cpuCount)
            {
                if (rankingImg[i])
                {
                    rankingImg[i].SetActive(false);
                }
                resultSetCPU[i].SetActive(false);
                BackStripe[i].SetActive(true);
            }
            //表示ボールのステータス書き換え
            else
            {
                counterChange(i);
                setAbility(i);
                setBall(i);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void counterChange(int Num)
    {
        //ステータスの数だけ回す
        for(int i = 0; i < 5; i++)
        {
            //強化状態の書き換え
            Image countImg = counterList[Num][i].transform.Find("count").GetComponent<Image>();
            TextMeshProUGUI countText = counterList[Num][i].transform.Find("counter").GetComponent<TextMeshProUGUI>();
            if (raceSet.raceMode)
            {
                countImg.fillAmount = (float)raceSet.levelList[1] / 20;
                countText.text = "*"+raceSet.levelList[1].ToString();
            }
            else
            {
                countImg.fillAmount = (float)GameFinish.EnhanceBotCount[Num][i] / 20;
                countText.text = "*"+GameFinish.EnhanceBotCount[Num][i].ToString();
            }
        }
        Image itemNumImg = NumCounter[Num].transform.Find("item").GetComponent<Image>();
        TextMeshProUGUI itemNumText = NumCounter[Num].transform.Find("counter").GetComponent<TextMeshProUGUI>();
        itemNumText.text = "*"+GameFinish.NumBotCount[Num].ToString();
    }
    void setAbility(int Num)
    {
        for (int i = 0; i < selectBallSet.selectAbiList.Length; i++)
        {
            abilityList[Num][selectBallSet.selectAbiBotList[Num][i]].SetActive(true);
            Vector3 abilityPos = abilityList[Num][selectBallSet.selectAbiBotList[Num][i]].transform.localPosition;
            abilityList[Num][selectBallSet.selectAbiBotList[Num][i]].transform.localPosition = new Vector3(abilityPos.x+ i * 200, abilityPos.y, abilityPos.z);
        }
            
    }
    void setBall(int Num)
    {
        //選択中の色を代入
        BotBall[Num].GetComponent<Renderer>().material = ball_material[selectBallSet.selectBotHam[Num]];
    }
}
