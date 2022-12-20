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
    //hp��,��Ԉُ��,�\��up,�U��,�h��,����,�W�����v
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
        //���Ԍo�߂�sp�Q�[�W�����A�ő��1���܂�
        float _special_lerp = _moveTPS.special_lerp;
        special_bar.fillAmount = Mathf.Lerp(0, 1, _special_lerp);
        //���܂������̃A�C�R���F�Ǘ�
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
            //1�ڂ̃A�C�e����sp�����܂��Ă���AHP�̏����𖞂����Ƃ�
            if (_abicount == 0 && spCount >= useSpecial && _nowHP > 0 && _maxHP > _nowHP && !_sleep)
            {
                _moveTPS.spCount -= useSpecial;
                sp1 = true;
            }
            //2�ڂ̃A�C�e����sp�����܂��Ă���A��Ԉُ�ɂȂ��Ă���Ƃ�
            else if (_abicount == 1 && spCount >= useSpecial && _nowCondition)
            {
                _moveTPS.spCount -= useSpecial;
                sp2 = true;
            }
            //�����_���X�e�[�^�X�A�b�v
            else if (_abicount == 2 && spCount >= useSpecial && _nowHP > 0 && !_sleep)
            {
                _moveTPS.spCount -= useSpecial;
                sp3 = true;
            }
            //�ʕ��o
            else if (_abicount == 3 && spCount >= useSpecial && _nowHP > 0 && !_sleep)
            {
                _moveTPS.spCount -= useSpecial;
                sp4 = true;
            }
            //�h��
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
        //�O���abi�摜���\��
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
        //�g��sp�̓h��ւ�
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
        //�A�C�R�����Â��g�p�\�Ȏ����邭����
        if (_moveTPS.spCount >= _moveTPS.useSpecial && !abiAvailableCheck)
        {
            abiAvailableCheck = true;
            for (int i = 0; i < _moveTPS.useSpecial; i++)
            {
                icon2List[i].GetComponent<Image>().color = new Color(0.5f, 1f, 0f);
            }
        }
        abiList[_abicount].enabled = true;
        //�O�t���[����abicount��ۑ����Ă���
        abicount_pre = _abicount;
    }
    IEnumerator flashIcon()
    {
        //�J�ڂ̃^�C�~���O�b
        float[] flashTiming = new float[] { 3f, 0.15f, 0.15f, 0.15f };
        //true�̃^�C�~���O�Ō��点��
        bool[] flashChange = new bool[] { false, true, false, true, false };
        int timingNow = 0; 
        while (true)
        {
            //�g�p�\�ȃ|�C���g�����܂��Ă���Ƃ��_�ł����Ďg�p�\���킩��₷��
            if(_moveTPS.spCount >= _moveTPS.useSpecial)
            {
                //����^�C�~���O�̎�
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
                
                //���点�鎞�Ԃ��Ǘ�
                yield return new WaitForSeconds(flashTiming[timingNow]);
                timingNow += 1;
                //�������烊�Z�b�g
                if (timingNow >= flashTiming.Length)
                {
                    timingNow = 0;
                }
            }
            //�g�p�s�̎��������Ȃ�
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
