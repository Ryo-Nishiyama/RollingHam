using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class nowHP : MonoBehaviour
{
    public GameObject mainBall;
    float _nowHP;
    public TextMeshProUGUI cardNameText;

    // Update is called once per frame
    void Update()
    {
        _nowHP = mainBall.GetComponent<moveTPS>().nowHP;   
        cardNameText.text = _nowHP.ToString();
    }
}
