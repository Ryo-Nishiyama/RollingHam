using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class enhanceCount_result : MonoBehaviour
{
    public TextMeshProUGUI enhanceCounter1, enhanceCounter2, enhanceCounter3, enhanceCounter4, enhanceCounter5;
    [SerializeField] Image[] enhanceGauge;

    // Start is called before the first frame update
    void Start()
    {
        enhanceCounter1.text = "*" + GameFinish.EnhanceCount[0].ToString();
        enhanceCounter2.text = "*" + GameFinish.EnhanceCount[1].ToString();
        enhanceCounter3.text = "*" + GameFinish.EnhanceCount[2].ToString();
        enhanceCounter4.text = "*" + GameFinish.EnhanceCount[3].ToString();
        enhanceCounter5.text = "*" + GameFinish.EnhanceCount[4].ToString();
        for(int i = 0; i < enhanceGauge.Length; i++)
        {
            enhanceGauge[i].fillAmount = (float)GameFinish.EnhanceCount[i] / 20;
        }
    }
}
