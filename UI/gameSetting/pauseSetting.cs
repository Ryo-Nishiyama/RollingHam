using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class pauseSetting : MonoBehaviour
{
    GameObject[] childObj;
    [SerializeField] gamePause _gamePause;
    [SerializeField] bool returnCheck = true;
    AudioSource _audioSource;
    [SerializeField] AudioClip a_select, a_cursor;
    int selectNum = 0;
    int selectNum_pre = 0;
    [SerializeField] Sprite[] buttonSprites;
    GameObject[] buttonTexts;
    bool enterCheck = false;
    [SerializeField] bool lastRace = false;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        childObj = new GameObject[this.transform.childCount];
        buttonTexts = new GameObject[this.transform.childCount];
        for (int i = 0; i < childObj.Length; i++)
        {
            childObj[i] = this.transform.GetChild(i).gameObject;
            buttonTexts[i] = childObj[i].transform.GetChild(0).gameObject;
        }
        //一番上を選択状態にしておく
        childObj[0].GetComponent<Image>().sprite = buttonSprites[1];
        if (raceSet.raceMode)
        {
            lastRace = false;
        }
        if (lastRace)
        {
            //探索後のレースでは最初からを無効化
            childObj[1].SetActive(false);
            childObj[0].GetComponent<RectTransform>().localPosition = new Vector3(childObj[0].GetComponent<RectTransform>().localPosition.x,
                                                                                  childObj[0].GetComponent<RectTransform>().localPosition.y - 85,
                                                                                  childObj[0].GetComponent<RectTransform>().localPosition.z);
            childObj[2].GetComponent<RectTransform>().localPosition = new Vector3(childObj[2].GetComponent<RectTransform>().localPosition.x,
                                                                                  childObj[2].GetComponent<RectTransform>().localPosition.y + 85,
                                                                                  childObj[2].GetComponent<RectTransform>().localPosition.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //前フレームの位置を保存
        selectNum_pre = selectNum;
        getKey();
        //位置が変わったとき画像を変更
        if (selectNum != selectNum_pre)
        {
            changeButton();
        }
        clickButton();
    }
    void getKey()
    {
        //位置が変わったときstripeの位置を初期化
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (lastRace)
            {
                selectNum -= 1;
            }
            selectNum -= 1;
            _audioSource.PlayOneShot(a_cursor);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (lastRace)
            {
                selectNum += 1;
            }
            selectNum += 1;
            _audioSource.PlayOneShot(a_cursor);
        }
        if (selectNum < 0)
        {
            selectNum = childObj.Length - 1;
        }
        else if (selectNum >= childObj.Length)
        {
            selectNum = 0;
        }
    }
    void changeButton()
    {
        childObj[selectNum_pre].GetComponent<Image>().sprite = buttonSprites[0];
        childObj[selectNum].GetComponent<Image>().sprite = buttonSprites[1];
        buttonTexts[selectNum_pre].GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        enterCheck = false;
    }
    void clickButton()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            childObj[selectNum].GetComponent<Image>().sprite = buttonSprites[2];
            buttonTexts[selectNum].GetComponent<RectTransform>().localPosition = new Vector3(-20, -20, 0);
            enterCheck = true;
        }
        if (Input.GetKeyUp(KeyCode.Return) && enterCheck)
        {
            switch (selectNum)
            {
                case 0:
                    _gamePause.Pause();
                    Initialize();
                    break;
                case 1:
                    //ボールの種類を戻す
                    if (!lastRace)
                    {
                        selectBallSet.selectHam = selectBallSet.selectHamInitial;
                        selectBallSet.selectBotHam = selectBallSet.selectBotHamInitial;
                    }
                    
                    break;
                case 2:
                    break;
            }
            childObj[selectNum].GetComponent<Button>().onClick.Invoke();
            
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Initialize();
        }
    }
    void Initialize()
    {
        //ボタンクリック後初期状態に戻す
        for (int i = 0; i < childObj.Length; i++)
        {
            childObj[i].GetComponent<Image>().sprite = buttonSprites[0];
            buttonTexts[i].GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        }
        childObj[0].GetComponent<Image>().sprite = buttonSprites[1];
        selectNum = 0;
        selectNum_pre = 0;
    }
}
