using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainAttack : MonoBehaviour
{
    public GameObject attackObj;
    public GameObject mainBall;
    [SerializeField] GameObject fire;
    [SerializeField] AudioClip ice_break;
    public special special;
    public moveTPS _moveTPS;
    [SerializeField] autoPlayer _autoPlayer;
    public GameObject attackHit;
    GameObject _attackObj;
    GameObject player;
    GameObject enhanceParticle;
    
    List<GameObject> _attackObjList = new List<GameObject>();

    bool key,key_bot = false;
    bool special_key = false;

    public int itemEnhance, itemEnhance2, itemEnhance3, itemEnhance4, itemEnhance5, itemEnhanceAll = 0;
    public int[] tempEnhance = new int[] { 0, 0, 0, 0, 0, 0 };
    bool elseEnter = false;
    bool ball_enhance1, ball_enhance2, ball_enhance3, ball_enhance4, ball_enhance5, ball_enhanceAll;
    public bool tell_enhance1, tell_enhance2, tell_enhance3, tell_enhance4, tell_enhance5, tell_enhanceAll;
    public bool[] tell_temp = new bool[] { false, false, false, false, false, false };
    public int kaeruman = 10;
    bool tempCheck = false;

    Vector3 mainVel;

    AudioSource _AudioSource;
    [SerializeField] AudioClip a_shot;

    // Start is called before the first frame update
    void Start()
    {
        _AudioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        AttackObjgenerator();
    }

    void AttackObjgenerator()
    {
        if (!_moveTPS.BotMode)
        {
            key = Input.GetKeyDown(KeyCode.B);
        }
        else
        {
            key_bot = _autoPlayer.attackBot;
            _autoPlayer.attackBot = false;
        }

        if ((key || special_key || key_bot) && _moveTPS.lanceCount > 0 && !_moveTPS.sleep && _moveTPS.nowHP > 0)
        {
            _moveTPS.lanceCount -= 1;
            _AudioSource.PlayOneShot(a_shot);
            GameObject fire_child = Instantiate(fire, this.transform.position, Quaternion.Euler(fire.transform.eulerAngles));
            fire_child.transform.parent = mainBall.transform;
            mainVel = transform.parent.GetComponent<Rigidbody>().velocity;
            Quaternion quata_mainBall = Quaternion.Euler(mainBall.transform.eulerAngles+attackObj.transform.eulerAngles/*+_UDdir*/);
            //新しく生成されたオブジェクトが入る
            _attackObjList.Add(Instantiate(attackObj, this.transform.position, quata_mainBall));
            //mainBal分加速させて衝突を防ぐ
            _attackObjList[_attackObjList.Count-1].GetComponent<Rigidbody>().velocity += mainVel;
        }
        if (_attackObjList.Count > 0)
        {
            for (int i = 0; i < _attackObjList.Count; i++)
            {
                itemEnhance += _attackObjList[i].GetComponent<mainAttackControl>().itemEnhance;
                itemEnhance2 += _attackObjList[i].GetComponent<mainAttackControl>().itemEnhance2;
                itemEnhance3 += _attackObjList[i].GetComponent<mainAttackControl>().itemEnhance3;
                itemEnhance4 += _attackObjList[i].GetComponent<mainAttackControl>().itemEnhance4;
                itemEnhance5 += _attackObjList[i].GetComponent<mainAttackControl>().itemEnhance5;
                itemEnhanceAll += _attackObjList[i].GetComponent<mainAttackControl>().itemEnhanceAll;
                elseEnter = _attackObjList[i].GetComponent<mainAttackControl>().elseEnter;
                for(int j=0; j < tell_temp.Length; j++)
                {
                    tempEnhance[j] += _attackObjList[i].GetComponent<mainAttackControl>().tempEnhance[j];
                    if (tempEnhance[j] >= 1)
                    {
                        tempEnhance[j] -= 1;
                        attackDestroy(i);
                        i -= 1;
                        tell_temp[j] = true;
                        tempCheck = true;
                        break;
                    }
                }
                if (tempCheck)
                {
                    tempCheck = false;
                    break;
                }
                if (itemEnhance >= 1)
                {
                    itemEnhance -= 1;
                    attackDestroy(i);
                    i -= 1;
                    tell_enhance1 = true;
                }
                else if (itemEnhance2 >= 1)
                {
                    itemEnhance2 -= 1;
                    attackDestroy(i);
                    i -= 1;
                    tell_enhance2 = true;
                }
                else if (itemEnhance3 >= 1)
                {
                    itemEnhance3 -= 1;
                    attackDestroy(i);
                    i -= 1;
                    tell_enhance3 = true;
                }
                else if (itemEnhance4 >= 1)
                {
                    itemEnhance4 -= 1;
                    attackDestroy(i);
                    i -= 1;
                    tell_enhance4 = true;
                }
                else if (itemEnhance5 >= 1)
                {
                    itemEnhance5 -= 1;
                    attackDestroy(i);
                    i -= 1;
                    tell_enhance5 = true;
                }
                else if (itemEnhanceAll >= 1)
                {
                    itemEnhanceAll -= 1;
                    attackDestroy(i);
                    i -= 1;
                    tell_enhanceAll = true;
                }
                
                else if (_attackObjList[i].GetComponent<mainAttackControl>().isItemDamageEnter == true || _attackObjList[i].GetComponent<mainAttackControl>().deathCount >= 3.0f || _attackObjList[i].GetComponent<mainAttackControl>().isEnhanceRockEnter==true)
                {
                    attackDestroy(i);
                    i -= 1;
                }
                else if (elseEnter)
                {
                    elseEnter = false;
                    attackDestroy(i);
                    i -= 1;
                }
            }
        }
    }

    void attackDestroy(int num)
    {
        //距離に応じて音を減衰
        float volumeDist = 50 - Vector3.Distance(_attackObjList[num].transform.position, gameObject.transform.position);
        if (volumeDist > 0)
        {
            float nowVolume = Mathf.Lerp(0.0f, 0.4f, volumeDist / 50);
            _AudioSource.volume = nowVolume;
            _AudioSource.PlayOneShot(ice_break);
        }
        //hit時エフェクトを生成
        Instantiate(attackHit, _attackObjList[num].transform.position, Quaternion.Euler(attackHit.transform.eulerAngles));
        Destroy(_attackObjList[num]);
        _attackObjList.RemoveAt(num);
    }
}
