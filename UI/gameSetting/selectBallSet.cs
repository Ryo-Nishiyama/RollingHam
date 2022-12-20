using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class selectBallSet : MonoBehaviour
{
    public frameLine _frameLine;

    public GameObject mainBall;
    public Button gameStart,returnButton;
    [SerializeField] Sprite[] startButtons;
    [SerializeField] GameObject startText;
    [SerializeField] ChangeScene _ChangeScene;
    Vector3 textPos;

    public Image[] skillFrame;
    public Image[] ability;
    public Image[] back;
    public Image[] randAbility;
    public Image buttonGuard;
    public Image[] ballGauge;
    float[,] gaugePow = new float[,] { { 0.2f, 0.2f, 0.8f, 0.5f, 0.75f, 0.1f, 1f },
                                       { 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f },
                                       { 1.0f, 0.8f, 0.3f, 0.3f, 0.4f, 0.75f, 0.3f },
                                       { 0.5f, 1f, 1f, 1f, 0.3f, 1f, 0.05f},
                                       { 0.75f, 0.8f, 0.5f, 0.8f, 0.8f, 0f, 0.6f },
                                       { 0.6f, 0.65f, 0.65f, 0.05f, 0.5f, 0.6f, 0.5f },
                                       { 0.4f, 0.8f, 0.7f, 1.0f, 0.05f, 0.1f, 1.0f },
                                       { 0.1f, 0.8f, 0.85f, 0.5f, 0.75f, 0.1f, 1.0f }};

    public Image[] skilPoint;
    int[] usePoint = new int[] { 1, 2, 3, 2, 6, 2, 2, 0 };
    Color32 skilColor = new Color32(128, 255, 0, 255);
    public TextMeshProUGUI[] mainDescription;
    public TextMeshProUGUI[] subDescription;

    Renderer _Renderer;
    [SerializeField] Material[] ballMaterials;
    string[] ballNames = new string[] {"軽いボール","普通のボール","重いボール","チャージボール",
                                       "基礎高ボール","滑るボール","直線ボール","超軽いボール"};

    AudioSource audioSource;
    [SerializeField] AudioClip a_select,a_cursor,a_cancel,a_start,a_impossible;

    int ballNum = 1;
    int abiNum = 0;
    int abiSelectCounter = 0;
    public bool abiMaxCheck = false;
    bool key_up, key_down, key_right, key_left = false;
    Color32[] ballColors = new Color32[] { new Color32(0, 255, 255, 40), new Color32(0, 255, 0, 40),
                                           new Color32(100, 30, 0, 40), new Color32(150, 0, 255, 40),
                                           new Color32(255, 255, 0, 40), new Color32(255, 0, 50, 40),
                                           new Color32(0, 0, 0, 40), new Color32(255, 255, 255, 40) };

    static public int[] selectAbiList = new int[] { 0, 2, 6 };
    static public int[][] selectAbiBotList = new int[][] { new int[] { 0, 1, 2 }, new int[] { 3, 4, 5 }, new int[] { 5, 6, 0 } };
    static public int selectHam = 1;
    static public int selectHamInitial = 1;
    static public int[] selectBotHam = new int[] {4, 3, 7 };
    static public int[] selectBotHamInitial = new int[] { 4, 3, 7 };
    
    public bool[] selectAbiCheck;
    bool[] randAbiCheck = new bool[] { false, false };

    // Start is called before the first frame update
    void Start()
    {

        _Renderer = mainBall.GetComponent<Renderer>();
        _Renderer.material.EnableKeyword("_EMISSION");
        textPos = startText.transform.position;
        audioSource = this.GetComponent<AudioSource>();
        //選択済みスキルをチェックする配列をスキル個分作成
        selectAbiCheck = new bool[ability.Length];
        for (int i = 0; i < ballGauge.Length; i++)
        {
            ballGauge[i].fillAmount = gaugePow[1, i];
        }
        //Botのボール,スキルをランダムに選択
        for (int i = 0; i < selectBotHam.Length; i++)
        {
            {
                if (raceSet.raceMode)
                {
                    //5種類のボール
                    selectBotHam[i] = Random.Range(0, 8);
                }
                else
                {
                    selectBotHam[i] = Random.Range(0, 3);
                }
                selectBotHamInitial[i] = selectBotHam[i];
            }
            for(int j = 0; j < selectAbiBotList[i].Length; j++)
            {
                int randAbi = Random.Range(0, 7);
                while (randAbi == selectAbiBotList[i][0] || randAbi == selectAbiBotList[i][1] || randAbi == selectAbiBotList[i][2])
                {
                    randAbi = Random.Range(0, 7);
                }
                selectAbiBotList[i][j] = randAbi;
            }
        }
        fillPoint();
    }

    // Update is called once per frame
    void Update()
    {
        //テキストの位置を元に戻す
        startText.transform.position = textPos;
        getKey();
        if (_frameLine.selectCheck)
        {
            switch (_frameLine.framePos)
            {
                case 0:
                    selectBall();
                    break;
                case 1:
                    selectAbi();
                    break;
            }
        }
        else
        {
            //選択画面を離れた時選択箇所の色を戻す
            if (!selectAbiCheck[abiNum])
            {
                skillFrame[abiNum].GetComponent<Image>().color = new Color32(120, 120, 120, 255);
            }
        }
        if (_frameLine.framePos == 2)
        {
            playStart();
        }
        else
        {
            if (abiMaxCheck)
            {
                gameStart.GetComponent<Image>().sprite = startButtons[2];
            }
            else
            {
                gameStart.GetComponent<Image>().sprite = startButtons[0];
            }
            
        }
        if (_frameLine.framePos == 3)
        {
            returnButton.GetComponent<Image>().color = new Color32(182, 255, 255, 255);
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Fire_2"))
            {
                if (raceSet.raceMode && SceneManager.GetActiveScene().name == "selectBall")
                {
                    Debug.Log("押した");
                    _ChangeScene.stageSelect_load();
                    _ChangeScene.SE_cancel();
                    _ChangeScene.fadeOutNoTime();
                }
                else
                {
                    returnButton.onClick.Invoke();
                }
            }
        }
        else
        {
            returnButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }
    void getKey()
    {
        key_up= Input.GetKeyDown(KeyCode.UpArrow);
        key_down = Input.GetKeyDown(KeyCode.DownArrow);
        key_right = Input.GetKeyDown(KeyCode.RightArrow);
        key_left = Input.GetKeyDown(KeyCode.LeftArrow);
    }
    void selectBall()
    {
        if (_frameLine.juzi_right || key_right)
        {
            ballNum += 1;
            audioSource.PlayOneShot(a_cursor);
            if (raceSet.raceMode)
            {
                if (ballNum > 7)
                {
                    ballNum = 0;
                }
            }
            else
            {
                if (ballNum > 2)
                {
                    ballNum = 0;
                }
            }
            
        }
        else if (_frameLine.juzi_left || key_left)
        {
            ballNum -= 1;
            audioSource.PlayOneShot(a_cursor);
            if (raceSet.raceMode)
            {
                if (ballNum < 0)
                {
                    ballNum = 7;
                }
            }
            else
            {
                if (ballNum < 0)
                {
                    ballNum = 2;
                }
            }
        }
        selectHam = ballNum;
        selectHamInitial = ballNum;
        switch (ballNum)
        {
            case 0:
                setBallColor(0);
                break;
            case 1:
                setBallColor(1);
                break;
            case 2:
                setBallColor(2);
                break;
            case 3:
                setBallColor(3);
                break;
            case 4:
                setBallColor(4);
                break;
            case 5:
                setBallColor(5);
                break;
            case 6:
                setBallColor(6);
                break;
            case 7:
                setBallColor(7);
                break;

        }
    }
    void setBallColor(int Num)
    {
        //ボール名変更
        mainDescription[0].text = ballNames[Num];
        //色変更
        _Renderer.material = ballMaterials[Num];
        //ステータスバー変更
        for (int i = 0; i < ballGauge.Length; i++)
        {
            ballGauge[i].fillAmount = gaugePow[Num, i];
        }
    }
    void selectAbi()
    {
        //前に選択していた場所の色を初期化
        if (!selectAbiCheck[abiNum])
        {
            skillFrame[abiNum].GetComponent<Image>().color = new Color32(120, 120, 120, 255);
        }
        if (_frameLine.juzi_right || key_right)
        {
            audioSource.PlayOneShot(a_cursor);
            abiNum += 1;
            if (abiNum > 7)
            {
                abiNum = 0;
            }
        }
        else if (_frameLine.juzi_left || key_left)
        {
            audioSource.PlayOneShot(a_cursor);
            abiNum -= 1;
            if (abiNum < 0)
            {
                abiNum = 7;
            }
        }
        else if (_frameLine.juzi_up)
        {
            audioSource.PlayOneShot(a_cursor);
            abiNum -= 4;
            if (abiNum < 0)
            {
                abiNum += 8;
            }
        }
        else if (_frameLine.juzi_down)
        {
            audioSource.PlayOneShot(a_cursor);
            abiNum += 4;
            if (abiNum >= 8)
            {
                abiNum -= 8;
            }
        }
        fillPoint();

        switch (abiNum)
        {
            case 0:
                mainDescription[1].text = "にんじん";
                subDescription[1].text = "必要ポイント：\n最大HPの2割を回復します。";
                break;
            case 1:
                mainDescription[1].text = "赤にんじん";
                subDescription[1].text = "必要ポイント：\n状態異常を直します。";
                break;
            case 2:
                mainDescription[1].text = "ひまわりの種";
                subDescription[1].text = "必要ポイント：\nランダムでステータスを\n一段階上昇させます。";
                break;
            case 3:
                mainDescription[1].text = "シールド";
                subDescription[1].text = "必要ポイント：\n15秒の間\n飛び道具から身を守ります。";
                break;
            case 4:
                mainDescription[1].text = "復活";
                subDescription[1].text = "必要ポイント：\nHPが0になったとき時間経過を\n待たずに復活できます。";
                break;
            case 5:
                mainDescription[1].text = "加速";
                subDescription[1].text = "必要ポイント：\n最大チャージと同じ加速を\n得ることができます。";
                break;
            case 6:
                mainDescription[1].text = "ジャンプ";
                subDescription[1].text = "必要ポイント：\n地上にいるとき\nジャンプをすることができます。";
                break;
            case 7:
                mainDescription[1].text = "ランダム";
                subDescription[1].text = "必要ポイント:\nランダムでスキルを決ます。\nスキルに迷って決められないときにどうぞ。";
                break;
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Fire_2"))
        {
            //選べる個数を制限
            if (!selectAbiCheck[abiNum] && !abiMaxCheck)
            {
                if (abiNum == 7)
                {
                    if (selectAbiCheck[abiNum])
                    {
                        randSelectReturn();
                    }
                    else
                    {
                        audioSource.PlayOneShot(a_select);
                        switch (abiSelectCounter)
                        {
                            case 0:
                                selectAbiList[0] = 7;
                                selectAbiList[1] = 7;
                                randAbiCheck[0] = true;
                                randAbiCheck[1] = true;
                                randAbility[0].enabled = true;
                                randAbility[1].enabled = true;
                                break;
                            case 1:
                                selectAbiList[1] = 7;
                                randAbiCheck[0] = true;
                                randAbility[0].enabled = true;
                                break;
                        }
                        selectAbiList[abiSelectCounter] = abiNum;
                        abiSelectCounter = 2;
                    }
                }
                if (selectAbiCheck[abiNum])
                {
                    abiSelectCounter -= 1;
                }
                selectAbiCheck[abiNum] = !selectAbiCheck[abiNum];

                skillFrame[abiNum].GetComponent<Image>().color = new Color32(255, 255, 0, 255);
                selectAbiList[abiSelectCounter] = abiNum;
                abiSelectCounter += 1;
                audioSource.PlayOneShot(a_select);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Backspace) || Input.GetButtonDown("Fire_3"))
        {
            switch(selectAbiCheck.Count(x => x == true))
            {
                case 1:
                    if (selectAbiList[0] == 7)
                    {
                        randSelectReturn();
                        skillFrame[7].GetComponent<Image>().color = new Color32(120, 120, 120, 255);
                        selectAbiCheck[7] = !selectAbiCheck[7];
                        abiSelectCounter -= 1;
                    }
                    else
                    {
                        abiSelectReturn(selectAbiList[0]);
                    }
                    break;
                case 2:
                    if (selectAbiList[1] == 7)
                    {
                        randSelectReturn();
                        skillFrame[7].GetComponent<Image>().color = new Color32(120, 120, 120, 255);
                        selectAbiCheck[7] = !selectAbiCheck[7];
                        abiSelectCounter -= 1;
                    }
                    else
                    {
                        abiSelectReturn(selectAbiList[1]);
                    }
                    break;
                case 3:
                    abiSelectReturn(selectAbiList[2]);
                    abiMaxCheck = false;
                    break;
            }
        }
        if (selectAbiCheck.Count(x => x == true) + randAbiCheck.Count(x => x == true) >= 3)
        {
            abiMaxCheck = true;
            //ボタンクリックを開放
            buttonGuard.enabled = false;
        }
        else
        {
            abiMaxCheck = false;
            buttonGuard.enabled = true;
        }
        //選択中の場所の色を変える
        if (!selectAbiCheck[abiNum])
        {
            skillFrame[abiNum].GetComponent<Image>().color = new Color32(0, 255, 255, 255);
        }
        //スキルを選択し終えたとき暗く薄くする
        if (abiMaxCheck)
        {
            for(int i = 0; i < ability.Length; i++)
            {
                if (!selectAbiCheck[i])
                {
                    back[i].GetComponent<Image>().color = new Color32(100, 100, 100, 255);
                    ability[i].GetComponent<Image>().color = new Color32(255, 255, 255, 100);
                }
            }
        }
        else
        {
            for (int i = 0; i < ability.Length; i++)
            {
                if (!selectAbiCheck[i])
                {
                    back[i].GetComponent<Image>().color = new Color32(207, 207, 207, 255);
                    ability[i].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                }
            }
        }
    }
    void fillPoint()
    {
        for (int i = 0; i < skilPoint.Length; i++)
        {
            if (usePoint[abiNum] <= i)
            {
                skilPoint[i].color = new Color32(0, 0, 0, 255);
            }
            else
            {
                skilPoint[i].color = skilColor;
            }
        }
    }
    void abiSelectReturn(int selNum)
    {
        audioSource.PlayOneShot(a_cancel);
        skillFrame[selNum].GetComponent<Image>().color = new Color32(120, 120, 120, 255);
        abiSelectCounter -= 1;
        selectAbiCheck[selNum] = !selectAbiCheck[selNum];
    }
    void randSelectReturn()
    {
        audioSource.PlayOneShot(a_cancel);
        abiSelectCounter -= randAbiCheck.Count(x => x == true);
        randAbiCheck[0] = false;
        randAbiCheck[1] = false;
        randAbility[0].enabled = false;
        randAbility[1].enabled = false;
    }
    void playStart()
    {
        if (abiMaxCheck)
        {
            gameStart.GetComponent<Image>().sprite = startButtons[3];
            if (Input.GetKey(KeyCode.Return) || Input.GetButton("Fire_2"))
            {
                gameStart.GetComponent<Image>().sprite = startButtons[4];
                startText.transform.position = new Vector3(textPos.x - 13, textPos.y - 15, textPos.z);
            }
            if (Input.GetKeyUp(KeyCode.Return) || Input.GetButtonUp("Fire_2"))
            {
                audioSource.PlayOneShot(a_start);
                for (int i = 0; selectAbiList.Length > i; i++)
                {
                    if (selectAbiList[i] == 7)
                    {
                        int randAbi = Random.Range(0, 7);
                        while (randAbi == selectAbiList[0] || randAbi == selectAbiList[1] || randAbi == selectAbiList[2])
                        {
                            randAbi = Random.Range(0, 7);
                        }
                        selectAbiList[i] = randAbi;
                    }
                }
                if (raceSet.raceMode)
                {
                    switch (selectStage.selectRaceNum)
                    {
                        case 1:
                            _ChangeScene.race_load();
                            break;
                        case 2:
                            _ChangeScene.race2_load();
                            break;
                    }
                    
                }
                else
                {
                    _ChangeScene.startGame_load();
                }
                _ChangeScene.fadeOut();
                _ChangeScene.SE_start();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Fire_2"))
            {
                audioSource.PlayOneShot(a_impossible);
            }
            gameStart.GetComponent<Image>().sprite = startButtons[1];
        }
        
    }
}
