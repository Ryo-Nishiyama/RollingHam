using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class titleDescription : MonoBehaviour
{
    [SerializeField] buttonSelector _buttonSelector;
    public TextMeshProUGUI description;
    string[] textList = new string[] { "ハムスターを育てて\nレースの優勝を目指します", "好きな設定で\nレースを遊ぶことができます",
                                       "ゲームの遊び方や\nまめちしきについて\n様々な情報を見ることが\nできます。",
                                       "ゲームに使用した\n様々な情報が\n記載されています。", "ゲームを終了します。" };

    // Update is called once per frame
    void Update()
    {
        description.text = textList[_buttonSelector.buttonCounter];
    }
}
