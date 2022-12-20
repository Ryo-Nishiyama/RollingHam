using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeedCheck : MonoBehaviour
{
    public GameObject mainBall;
    float _nowSpeed;
    public TextMeshProUGUI cardNameText;

    // Update is called once per frame
    void Update()
    {
        int keta = 2;
        _nowSpeed = mainBall.GetComponent<moveTPS>().nowSpeed * keta;
        float _nowSpeedHozo = Mathf.Floor(_nowSpeed) / keta;
        string _nowSpeedStr = ((int)_nowSpeedHozo).ToString("000"/*6åÖÉ[ÉçñÑÇﬂ*/);
        _nowSpeedStr += (_nowSpeedHozo - ((int)_nowSpeedHozo)).ToString("F2"/*è¨êîì_à»â∫2åÖ*/).TrimStart('0'/*êÊì™ÇÃÉ[ÉççÌèú*/);
        cardNameText.text = _nowSpeedStr.Substring(0,3);
        
    }
}
