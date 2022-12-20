using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonScroll : MonoBehaviour
{
    [SerializeField] GameObject[] buttons;
    [SerializeField] buttonSelector _buttonSelector;
    [SerializeField] GameObject scrollBar;
    int _buttonCounter = 1;
    int _buttonCounterPre = 1;
    int spaceNum = 0;
    int selectPos = 0;
    int scrollMargin = 3;
    int scrollLevel = 0;
    float margin;
    float standardY;
    float maxScroll;
    [SerializeField] int startPoint = 5;
    [SerializeField] Vector3 barPosStart = new Vector3(0, 120, 0);
    [SerializeField] Vector3 barPosEnd = new Vector3(0, -120, 0);

    // Start is called before the first frame update
    void Start()
    {
        margin = Mathf.Abs(buttons[0].transform.position.y - buttons[1].transform.position.y);
        standardY = buttons[0].transform.position.y;
        maxScroll = -buttons.Length + scrollMargin + 1;
    }

    // Update is called once per frame
    void Update()
    {
        _buttonCounter = _buttonSelector.buttonCounter;
        if (_buttonCounter > 0 && _buttonCounter <= buttons.Length)
        {
            //上下移動で値を変更
            if (_buttonCounter < _buttonCounterPre)
            {
                selectPos += 1;
                spaceNum += 1;
            }
            else if (_buttonCounter > _buttonCounterPre)
            {
                selectPos -= 1;
                spaceNum -= 1;
            }
            
            //移動値を設定
            if (spaceNum > 0)
            {
                spaceNum = 0;
                scrollLevel += 1;
            }
            else if (spaceNum < -scrollMargin)
            {
                spaceNum = -scrollMargin;
                scrollLevel -= 1;
            }
            //端で補正
            if (_buttonCounter == 1)
            {
                spaceNum = 0;
                selectPos = 0;
                scrollLevel = 0;
            }
            else if (_buttonCounter == buttons.Length)
            {
                spaceNum = -scrollMargin;
                selectPos = -buttons.Length + 1;
                scrollLevel = -buttons.Length + scrollMargin + 1;
            }
            if (_buttonCounter != _buttonCounterPre)
            {
                for (int i = 0; i < buttons.Length; i++)
                {
                    Vector3 buttonPos = new Vector3(buttons[i].transform.position.x, standardY + margin * (Mathf.Abs(scrollLevel)-i), buttons[i].transform.position.z);
                    buttons[i].transform.position = buttonPos;
                }
            }
        }
        bar();
        _buttonCounterPre = _buttonCounter;

    }
    void scroll()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            Vector3 buttonPos = new Vector3(buttons[i].transform.position.x, standardY - margin * i, buttons[i].transform.position.z);
            buttons[i].transform.position = buttonPos;
        }
    }
    void bar()
    {
        scrollBar.transform.localPosition = Vector3.Lerp(barPosStart, barPosEnd, scrollLevel / maxScroll);
    }
}
