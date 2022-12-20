using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class special : MonoBehaviour
{
    float timeCharge = 0f;
    int spCount = 0;
    public int _abicount = 0;
    int abicount_pre = 0;
    int _spCount,_spCount_pre;
    int useSpecial = 1;
    bool R1 = false;
    public bool overR1 = false;
    bool juziLcheck, juziRcheck = false;
    bool juziLcheckC, juziRcheckC = false;
    bool sp1, sp2, sp3, sp4, sp5 = false;
    float juziH = 0.0f;
    public float maxCharge = 3f;

    public Image special_bar;
    public Image icon1, icon2, icon3, icon4, icon5, icon6;
    public Image icon2_1, icon2_2, icon2_3, icon2_4, icon2_5, icon2_6;
    [SerializeField] Image[] lanceList;
    //hp回復,状態異常回復,能力up,攻撃,蘇生,加速,ジャンプ
    public Image[] abi;
    public moveTPS _moveTPS;
    List<Image> iconList = new List<Image>();
    List<Image> icon2List = new List<Image>();
    List<Image> abiList = new List<Image>();
    bool changeAbiCheck = true;
    bool abiAvailableCheck = false;

    // Start is called before the first frame update
    void Start()
    {
        iconList.Add(icon1);
        iconList.Add(icon2);
        iconList.Add(icon3);
        iconList.Add(icon4);
        iconList.Add(icon5);
        iconList.Add(icon6);
        icon2List.Add(icon2_1);
        icon2List.Add(icon2_2);
        icon2List.Add(icon2_3);
        icon2List.Add(icon2_4);
        icon2List.Add(icon2_5);
        icon2List.Add(icon2_6);

        for(int i = 0; i < selectBallSet.selectAbiList.Length; i++)
        {
            abiList.Add(abi[selectBallSet.selectAbiList[i]]);
            if (i == 0)
            {
                abiList[i].enabled = true;
            }
            else
            {
                abiList[i].enabled = false;
            }
        }
        StartCoroutine(flashIcon());
        _spCount = _moveTPS.spCount;
        _spCount_pre = _spCount;
    }

    // Update is called once per frame
    void Update()
    {
        _spCount = _moveTPS.spCount;
        if (spCount <= iconList.Count)
        {
            SPcounter();
        }
        spCount = _moveTPS.spCount;
        //SPconsum();
        avirityChange();
        _spCount_pre = _spCount;
    }
    private void LateUpdate()
    {
        
    }

    void SPcounter()
    {
        //時間経過でspゲージ増加、最大で1つ溜まる
        float _special_lerp = _moveTPS.special_lerp;
        special_bar.fillAmount = Mathf.Lerp(0, 1, _special_lerp);
        //溜まった分のアイコン色管理
        for (int i = 0; iconList.Count > i; i++)
        {
            if (spCount > i)
            {
                iconList[i].GetComponent<Image>().color = new Color(0.5f, 1f, 0f);
            }
            else
            {
                iconList[i].GetComponent<Image>().color = new Color(0f, 0f, 0f);
            }
            if (_moveTPS.lanceCount > i)
            {
                lanceList[i].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            }
            else
            {
                lanceList[i].GetComponent<Image>().color = new Color32(0, 0, 0, 255);
            }
        }
    }

    void SPconsum()
    {
        R1 = Input.GetButtonDown("Fire_R1");
        if (R1 || overR1)
        {
            float _nowHP = _moveTPS.nowHP;
            float _maxHP = _moveTPS.maxHP;
            bool _nowCondition = _moveTPS.nowCondition;
            bool _sleep = _moveTPS.sleep;
            //1つ目のアイテムでspが溜まっており、HPの条件を満たすとき
            if (_abicount == 0 && spCount >= useSpecial && _nowHP > 0 && _maxHP > _nowHP && !_sleep)
            {
                _moveTPS.spCount -= useSpecial;
                sp1 = true;
            }
            //2つ目のアイテムでspが溜まっており、状態異常になっているとき
            else if (_abicount == 1 && spCount >= useSpecial && _nowCondition)
            {
                _moveTPS.spCount -= useSpecial;
                sp2 = true;
            }
            //ランダムステータスアップ
            else if (_abicount == 2 && spCount >= useSpecial && _nowHP > 0 && !_sleep)
            {
                _moveTPS.spCount -= useSpecial;
                sp3 = true;
            }
            //玉放出
            else if (_abicount == 3 && spCount >= useSpecial && _nowHP > 0 && !_sleep)
            {
                _moveTPS.spCount -= useSpecial;
                sp4 = true;
            }
            //蘇生
            else if (_abicount == 4 && spCount >= useSpecial && _nowHP <= 0)
            {
                _moveTPS.spCount -= useSpecial;
                sp5 = true;
            }
            R1 = false;
        }
    }

    void avirityChange()
    {
        if ((_abicount != abicount_pre) || (_spCount != _spCount_pre))
        {
            changeAbiCheck = true;
            abiAvailableCheck = false;
        }
        //前後のabi画像を非表示
        if (_abicount == abiList.Count-1)
        {
            abiList[_abicount - 1].enabled = false;
            abiList[0].enabled = false;
            
        }
        else if (_abicount == 0)
        {
            abiList[_abicount + 1].enabled = false;
            abiList[abiList.Count - 1].enabled = false;
        }
        else
        {
            abiList[_abicount + 1].enabled = false;
            abiList[_abicount - 1].enabled = false;
        }
        //使うspの塗り替え
        if (changeAbiCheck)
        {
            changeAbiCheck = false;
            for (int i = 0; icon2List.Count > i; i++)
            {
                if (_moveTPS.useSpecial > i)
                {
                    icon2List[i].GetComponent<Image>().color = new Color(0.25f, 0.5f, 0f);
                }
                else
                {
                    icon2List[i].GetComponent<Image>().color = new Color(0f, 0f, 0f);
                }
            }
        }
        //アイコンが暗く使用可能な時明るくする
        if (_moveTPS.spCount >= _moveTPS.useSpecial && !abiAvailableCheck)
        {
            abiAvailableCheck = true;
            for (int i = 0; i < _moveTPS.useSpecial; i++)
            {
                icon2List[i].GetComponent<Image>().color = new Color(0.5f, 1f, 0f);
            }
        }
        abiList[_abicount].enabled = true;
        //前フレームのabicountを保存しておく
        abicount_pre = _abicount;
    }
    IEnumerator flashIcon()
    {
        //遷移のタイミング秒
        float[] flashTiming = new float[] { 3f, 0.15f, 0.15f, 0.15f };
        //trueのタイミングで光らせる
        bool[] flashChange = new bool[] { false, true, false, true, false };
        int timingNow = 0; 
        while (true)
        {
            //使用可能なポイントがたまっているとき点滅させて使用可能をわかりやすく
            if(_moveTPS.spCount >= _moveTPS.useSpecial)
            {
                //光るタイミングの時
                if (flashChange[timingNow])
                {
                    for (int i = 0; i < _moveTPS.useSpecial; i++)
                    {
                        icon2List[i].GetComponent<Image>().color = new Color(0.9f, 1f, 0.75f);
                    }
                }
                else
                {
                    for (int i = 0; i < _moveTPS.useSpecial; i++)
                    {
                        icon2List[i].GetComponent<Image>().color = new Color(0.5f, 1f, 0f);
                    }
                }
                
                //光らせる時間を管理
                yield return new WaitForSeconds(flashTiming[timingNow]);
                timingNow += 1;
                //超えたらリセット
                if (timingNow >= flashTiming.Length)
                {
                    timingNow = 0;
                }
            }
            //使用不可の時何もしない
            else
            {
                yield return null;
            }
        }
    }

    public bool sp1Check()
    {
        bool tasikame = false;
        if (sp1)
        {
            tasikame = true;
            sp1 = false;
        }
        return tasikame;
    }
    public bool sp2Check()
    {
        bool tasikame = false;
        if (sp2)
        {
            tasikame = true;
            sp2 = false;
        }
        return tasikame;
    }
    public bool sp3Check()
    {
        bool tasikame = false;
        if (sp3)
        {
            tasikame = true;
            sp3 = false;
        }
        return tasikame;
    }
    public bool sp4Check()
    {
        bool tasikame = false;
        if (sp4)
        {
            tasikame = true;
            sp4 = false;
        }
        return tasikame;
    }
    public bool sp5Check()
    {
        bool tasikame = false;
        if (sp5)
        {
            tasikame = true;
            sp5 = false;
        }
        return tasikame;
    }
}
