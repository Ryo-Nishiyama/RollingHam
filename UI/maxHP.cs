using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class maxHP : MonoBehaviour
{
    public GameObject mainBall;
    float _maxHP;
    public TextMeshProUGUI cardNameText;

    // Update is called once per frame
    void Update()
    {
        _maxHP = mainBall.GetComponent<moveTPS>().maxHP;
        cardNameText.text = _maxHP.ToString();
    }
}
