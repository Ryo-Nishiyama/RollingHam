using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class raceSet : MonoBehaviour
{
    [SerializeField] buttonSelector _buttonSelector;
    public TextMeshProUGUI[] textList;
    static public int[] levelList = new int[] { 0, 0, 0, 0, 0, 0, 3 };
    static public bool raceMode = false;
    string[] spText = new string[] { "0", "1", "2", "3", "4", "5" };
    string[] enhanceText = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20" };
    string[] cpuAmountText = new string[] { "0", "1", "2", "3" };
    string[] cpuLevelText = new string[] { "弱い", "ふつう", "強い", "とても強い" };

    bool juziCheck_right, juziCheck_left, juzi_right, juzi_left = false;
    float juziV = 0;

    AudioSource audioSource;
    [SerializeField] AudioClip a_select;

    // Start is called before the first frame update
    void Start()
    {
        //レースモードを起動する
        raceMode = true;
        audioSource = GetComponent<AudioSource>();
        for(int i = 0; i < GameFinish.EnhanceCount.Length; i++)
        {
            textList[i].text = GameFinish.EnhanceCountInitial[i].ToString();
            Debug.Log(textList[i].text);
        }
        textList[5].text = GameFinish.NumCount.ToString();
        textList[6].text = CPUSet.cpuCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        keyCheck();
        setting();
        for (int i = 0; i < GameFinish.EnhanceBotCount.Length; i++)
        {
            for (int j = 0; j < GameFinish.EnhanceBotCount[0].Length; j++)
            {
                GameFinish.EnhanceBotCount[i][j] = levelList[j];
                GameFinish.EnhanceBotCountInitial[i][j] = levelList[j];
            }
            GameFinish.NumBotCount[i] = levelList[5];
        }
        for (int i = 0; i < GameFinish.EnhanceBotCount[0].Length; i++)
        {
            GameFinish.EnhanceCountInitial[i] = levelList[i];
        }
        GameFinish.NumCount = levelList[5];

    }
    void keyCheck()
    {
        juziV = Input.GetAxis("Horizontal D-Pad");
        juzi_left = false;
        juzi_right = false;
        if (juziV > 0.5f && !juziCheck_right)
        {
            juzi_right = true;
            juziCheck_right = true;
        }
        else if (juziV <= 0.5f)
        {
            juziCheck_right = false;
        }
        if (juziV < -0.5f && !juziCheck_left)
        {
            juzi_left = true;
            juziCheck_left = true;
        }
        else if (juziV >= -0.5f)
        {
            juziCheck_left = false;
        }
    }
    void setting()
    {
        switch (_buttonSelector.buttonCounter)
        {
            case 1:
                if (juzi_left || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    audioSource.PlayOneShot(a_select);
                    enhanceDOWN(1);
                }
                else if (juzi_right || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    audioSource.PlayOneShot(a_select);
                    enhanceUP(1);
                }
                break;
            case 2:
                if (juzi_left || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    audioSource.PlayOneShot(a_select);
                    enhanceDOWN(2);
                }
                else if (juzi_right || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    audioSource.PlayOneShot(a_select);
                    enhanceUP(2);
                }
                break;
            case 3:
                if (juzi_left || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    audioSource.PlayOneShot(a_select);
                    enhanceDOWN(3);
                }
                else if (juzi_right || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    audioSource.PlayOneShot(a_select);
                    enhanceUP(3);
                }
                break;
            case 4:
                if (juzi_left || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    audioSource.PlayOneShot(a_select);
                    enhanceDOWN(4);
                }
                else if (juzi_right || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    audioSource.PlayOneShot(a_select);
                    enhanceUP(4);
                }
                break;
            case 5:
                if (juzi_left || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    audioSource.PlayOneShot(a_select);
                    enhanceDOWN(5);
                }
                else if (juzi_right || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    audioSource.PlayOneShot(a_select);
                    enhanceUP(5);
                }
                break;
            case 6:
                if (juzi_left || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    audioSource.PlayOneShot(a_select);
                    spDOWN();
                }
                else if (juzi_right || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    audioSource.PlayOneShot(a_select);
                    spUP();
                }
                break;
            case 7:
                if (juzi_left || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    audioSource.PlayOneShot(a_select);
                    cpuAmountDOWN();
                }
                else if (juzi_right || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    audioSource.PlayOneShot(a_select);
                    cpuAmountUP();
                }
                //cpu数を同期
                CPUSet.cpuCount = levelList[6];
                break;
        }
    }
    public void enhanceUP(int num)
    {
        num -= 1;
        if (levelList[num] < 20)
        {
            levelList[num] += 1;
        }
        else
        {
            levelList[num] = 0;
        }
        textList[num].text = enhanceText[levelList[num]];
    }
    public void enhanceDOWN(int num)
    {
        num -= 1;
        if (levelList[num] > 0)
        {
            levelList[num] -= 1;
        }
        else
        {
            levelList[num] = 20;
        }
        textList[num].text = enhanceText[levelList[num]];
    }
    public void spUP()
    {
        if (levelList[5] < 5)
        {
            levelList[5] += 1;
        }
        else
        {
            levelList[5] = 0;
        }
        textList[5].text = spText[levelList[5]];
    }
    public void spDOWN()
    {
        if (levelList[5] > 0)
        {
            levelList[5] -= 1;
        }
        else
        {
            levelList[5] = 5;
        }
        textList[5].text = spText[levelList[5]];
    }
    public void cpuAmountUP()
    {
        if (levelList[6] < 3)
        {
            levelList[6] += 1;
        }
        else
        {
            levelList[6] = 0;
        }
        textList[6].text = cpuAmountText[levelList[6]];
    }
    public void cpuAmountDOWN()
    {
        if (levelList[6] > 0)
        {
            levelList[6] -= 1;
        }
        else
        {
            levelList[6] = 3;
        }
        textList[6].text = cpuAmountText[levelList[6]];
    }
    public void cpuLevelUP()
    {
        if (levelList[3] < 3)
        {
            levelList[3] += 1;
        }
        else
        {
            levelList[3] = 0;
        }
        textList[3].text = cpuLevelText[levelList[3]];
    }
    public void cpuLevelDOWN()
    {
        if (levelList[3] > 0)
        {
            levelList[3] -= 1;
        }
        else
        {
            levelList[3] = 3;
        }
        textList[3].text = cpuLevelText[levelList[3]];
    }
}
