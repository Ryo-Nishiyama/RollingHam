using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class itemCount : MonoBehaviour
{
    public Image count1, count2, count3, count4, count5, item;
    public Image countBackground;
    public Image[] countTemp;
    [SerializeField] Image[] countConstant;
    public Image countback1, countback2, countback3, countback4, countback5;
    public GameObject MiniMap;
    public TextMeshProUGUI NumCounter
    int countMax = 20;
    bool L1 = false;
    bool L1_q = false;
    [SerializeField] bool field = true;
    int Hide = 0;

    // Update is called once per frame
    void Update()
    {
        if (field)
        {
            L1 = Input.GetButtonDown("Fire_L1");
            L1_q = Input.GetKeyDown(KeyCode.Q);
        }
        if (Hide==0)
        {
            //ステータスアップを表示
            MiniMap.SetActive(false);
            count1.enabled = true;
            count2.enabled = true;
            count3.enabled = true;
            count4.enabled = true;
            count5.enabled = true;
            countback1.enabled = true;
            countback2.enabled = true;
            countback3.enabled = true;
            countback4.enabled = true;
            countback5.enabled = true;
            item.enabled = true;
            NumCounter.enabled = true;
            countBackground.enabled = true;
            for (int i = 0; i < countTemp.Length; i++)
            {
                countTemp[i].enabled = true;
            }
            for (int i = 0; i < countConstant.Length; i++)
            {
                countConstant[i].enabled = true;
            }
            if (L1 || L1_q)
            {
                Hide = 1;
            }
        }
        else if(Hide == 1)
        {
            //ミニマップを表示
            MiniMap.SetActive(true);
            count1.enabled = false;
            count2.enabled = false;
            count3.enabled = false;
            count4.enabled = false;
            count5.enabled = false;
            countback1.enabled = false;
            countback2.enabled = false;
            countback3.enabled = false;
            countback4.enabled = false;
            countback5.enabled = false;
            item.enabled = false;
            NumCounter.enabled = false;
            countBackground.enabled = false;
            for (int i = 0; i < countTemp.Length; i++)
            {
                countTemp[i].enabled = false;
            }
            for (int i = 0; i < countConstant.Length; i++)
            {
                countConstant[i].enabled = false;
            }
            if (L1 || L1_q)
            {
                Hide = 2;
            }
        }
        else
        {
            //全て
            //非表示
            MiniMap.SetActive(false);
            if (L1 || L1_q)
            {
                Hide = 0;
            }
        }
    }
}
