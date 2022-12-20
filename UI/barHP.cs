using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class barHP : MonoBehaviour
{
    //このスクリプトいらんかも
    public GameObject mainBall;
    float _nowHP;
    float _maxHP;

    // Update is called once per frame
    void Update()
    {
        _nowHP = mainBall.GetComponent<moveTPS>().nowHP;
        _maxHP = mainBall.GetComponent<moveTPS>().maxHP;
        float hp_lerp = _nowHP / _maxHP;
        GetComponent<Image>().color = new Color(0.7f * (1f - hp_lerp), 0.7f * hp_lerp, 0.2f);
    }
}
