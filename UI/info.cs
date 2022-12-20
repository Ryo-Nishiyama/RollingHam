using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class info : MonoBehaviour
{
    [SerializeField] GameObject info_mainObj, info_backObj;
    [SerializeField] Image info_main, info_back;
    [SerializeField] float popSpeed = 2;
    [SerializeField] float popDiff = 0.25f;
    public TextMeshProUGUI info_text;
    public lighting _lighting;
    Vector3 infoActiveSize;
    Vector3 infoNotActiveSize = new Vector3(0.01f, 0.01f, 0.01f);
    float popTime = 0;
    float rotateChange = -1;
    bool startCheck = false;

    string[] infoText = new string[]
    {
        "",
        "霧が 発生中",
        "チャージが 暴走中",
        "自然に 回復するぞ！",
        "巨大な岩が\nどこかに出現",
        "アイテムが\n大量発生中！",
        "アイテムに\n偏り 発生中",
        "プレイヤーの\n大きさが 変わった！",
        "夜に なった"
    };

    // Start is called before the first frame update
    void Start()
    {
        infoActiveSize = info_main.transform.localScale;
        info_main = info_mainObj.GetComponent<Image>();
        info_back = info_backObj.GetComponent<Image>();
        info_main.enabled = false;
        info_back.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_lighting.isEvent)
        {
            eventStartPop();
            eventText();
        }
        else
        {
            eventEndPop();
            
        }
    }

    void eventStartPop()
    {
        info_back.enabled = true;
        if (popTime < 1+popDiff)
        {
            popTime += Time.deltaTime * popSpeed;
        }
        if (popTime <= 1)
        {
            info_backObj.transform.localScale = Vector3.Lerp(infoNotActiveSize, infoActiveSize, popTime);
        }
        //backが最大サイズになったとき
        if (popTime > 1 && !startCheck)
        {
            //backを揺らす
            startCheck = true;
            StartCoroutine(backMove());
        }
        if (popTime >= popDiff)
        {
            info_main.enabled = true;
            info_text.enabled = true;
            info_mainObj.transform.localScale = Vector3.Lerp(infoNotActiveSize, infoActiveSize, popTime - popDiff);
        }


    }
    void eventEndPop()
    {
        if (popTime >= 0)
        {
            startCheck = false;
            popTime -= Time.deltaTime * popSpeed;
            info_mainObj.transform.localScale = Vector3.Lerp(infoNotActiveSize, infoActiveSize, popTime - popDiff);
            info_backObj.transform.localScale = Vector3.Lerp(infoNotActiveSize, infoActiveSize, popTime);
        }
        //backの縮小が始まるタイミングで終了
        if (popTime < 1 && startCheck)
        {
            startCheck = false;
            //傾きを初期化
            info_backObj.transform.eulerAngles = new Vector3(0, 0, 0);
            info_backObj.transform.localScale = infoActiveSize;
        }
        if (popTime < popDiff)
        {
            info_main.enabled = false;
            info_text.enabled = false;
            if (popTime < 0f)
            {
                info_back.enabled = false;
                popTime = 0;
                
            }
        }
    }
    void eventText()
    {
        info_text.text = infoText[_lighting.randEventCheck];
    }

    IEnumerator backMove()
    {
        while (startCheck)
        {
            float randSize = Random.Range(infoActiveSize.x, infoActiveSize.x * 1.05f);
            rotateChange *= -1;
            info_backObj.transform.eulerAngles = new Vector3(0, 0, Random.Range(0.5f, 1.25f) * rotateChange);
            info_backObj.transform.localScale = new Vector3(randSize, randSize, 1);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
