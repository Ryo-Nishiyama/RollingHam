using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bg_modeSelect : MonoBehaviour
{
    [SerializeField] Image back;
    [SerializeField] Sprite[] backs;
    [SerializeField] Image[] polkaCeil;
    [SerializeField] Image[] polkafloor;
    [SerializeField] buttonSelector _buttonSelector;

    Color32[] ceilColors = new Color32[] { new Color32(255, 212, 173, 255), 
                                           new Color32(255, 203, 204, 255), new Color32(229, 203, 255, 255),
                                           new Color32(206, 224, 255, 255), new Color32(200, 200, 200, 255) };
    Color32[] floorColors = new Color32[] { new Color32(255, 237, 194, 255),
                                            new Color32(255, 230, 233, 255), new Color32(247, 229, 255, 255),
                                            new Color32(204, 255, 255, 255), new Color32(220, 220, 220, 255) };

    int _buttonCounter = 0;

    // Update is called once per frame
    void Update()
    {
        if(_buttonCounter!= _buttonSelector.buttonCounter)
        {
            _buttonCounter = _buttonSelector.buttonCounter;
            //選択位置が変わったとき実行
            changeColor(_buttonCounter);
        }
    }
    void changeColor(int num)
    {
        //背景画像差し替え
        back.sprite = backs[num];
        for (int i = 0; i < polkaCeil.Length; i++)
        {
            //ドット色変更
            polkaCeil[i].color = ceilColors[num];
            polkafloor[i].color = floorColors[num];
        }
    }
}
