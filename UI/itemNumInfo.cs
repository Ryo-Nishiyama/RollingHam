using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemNumInfo : MonoBehaviour
{
    public moveTPS _moveTPS;
    float sign = 0;

    // Update is called once per frame
    void Update()
    {
        sign = Mathf.Sin(Time.time * 6) / 2;
        //âìÇ≥Ç…ÇÊÇ¡Çƒì_ñ≈ÇÃé¸ä˙ÇïœçX
        if (_moveTPS.detectionItemNum3)
        {
            GetComponent<Image>().color = new Color(1, 1, 1, sign + 0.5f);
        }
        else if (_moveTPS.detectionItemNum2)
        {
            if (sign + 0.25f < 0)
            {
                GetComponent<Image>().color = new Color(1, 1, 1, 0);
            }
            else
            {
                GetComponent<Image>().color = new Color(1, 1, 1, sign + 0.25f);
            }
        }
        else if (_moveTPS.detectionItemNum)
        {
            if (sign < 0)
            {
                GetComponent<Image>().color = new Color(1, 1, 1, 0);
            }
            else
            {
                GetComponent<Image>().color = new Color(1, 1, 1, sign);
            }
        }
        else
        {
            GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }
    }
}
