using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Security.Cryptography;

public class playerEffect : MonoBehaviour
{
    [SerializeField] GameObject landing;
    [SerializeField] GameObject[] abnormality;
    [SerializeField] GroundCheck _GroundCheck;
    [SerializeField] waterCheck _waterCheck;
    moveTPS _moveTPS;

    bool isGround_pre=false;

    Vector3[] conditionPos = new Vector3[] { new Vector3(-0.15f, 0, -0.45f), new Vector3(0.25f, 0.27f, -0.5f),
                                        new Vector3(0, 0.3f, 0.5f), new Vector3(-0.25f, 0.3f, -0.15f),new Vector3(0.4f,-0.25f,-0.1f) };
    int conditionNum = 3;
    int[] conditionTurn = new int[] { 0, 1, 2, 3, 4 };
    bool[] abnormalityStart = new bool[] { false, false, false, false, false, false, false };
    GameObject e_particle;

    //�������W�������o��
    [SerializeField] int accelLineMaxEmission = 100;
    [SerializeField] int accelLineMinEmission = 30;
    Color32 accelLineMaxCplor = new Color32(200, 200, 200, 100);
    Color32 accelLineMinCplor = new Color32(200, 200, 200, 50);
    [SerializeField] ParticleSystem eff_accelLine;
    ParticleSystem.MainModule _eff_accelLineMain;
    ParticleSystem.EmissionModule _eff_accelLineEmission;
    bool accelLinePlay_flag = false;

    bool _BotMode;

    // Start is called before the first frame update
    void Start()
    {
        _moveTPS = GetComponent<moveTPS>();
        _BotMode = _moveTPS.BotMode;

        //�W�����G�t�F�N�g�̏���
        if (!_BotMode)
        {
            _eff_accelLineMain = eff_accelLine.main;
            _eff_accelLineEmission = eff_accelLine.emission;
            //�ŏ��l�ɃZ�b�g
            eff_accelLine.startColor = accelLineMinCplor;
            _eff_accelLineEmission.rateOverTime = accelLineMinEmission;

            eff_accelLine.Stop();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        Landing();
        //burn�J�n����1����s
        if (!abnormalityStart[0] && _moveTPS.burn)
        {
            StartCoroutine(conditionEffect(0));
            abnormalityStart[0] = true;
        }
        else if (abnormalityStart[0] && !_moveTPS.burn)
        {
            abnormalityStart[0] = false;
        }
        if (!abnormalityStart[1] && _moveTPS.frostbite)
        {
            StartCoroutine(conditionEffect(1));
            abnormalityStart[1] = true;
        }
        else if (abnormalityStart[1] && !_moveTPS.frostbite)
        {
            abnormalityStart[1] = false;
        }
        if (!abnormalityStart[2] && _moveTPS.paralysis)
        {
            conditionEffect_one(2, transform.position + abnormality[2].transform.position);
        }
        else if (abnormalityStart[2] && !_moveTPS.paralysis)
        {
            Destroy(e_particle);
            abnormalityStart[2] = false;
        }
        if (!abnormalityStart[3] && _moveTPS.sleep)
        {
            if (!_moveTPS.nightmare)
            {
                conditionEffect_one(3, transform.position + abnormality[3].transform.position);
            }
            else
            {
                conditionEffect_one(4, transform.position + abnormality[4].transform.position);
                abnormalityStart[3] = true;
            }
        }
        else if (abnormalityStart[3] && !_moveTPS.sleep)
        {
            e_particle.GetComponent<ParticleSystem>().loop = false;
            abnormalityStart[3] = false;
            abnormalityStart[4] = false;
        }
        if (!abnormalityStart[5] && _moveTPS.poison)
        {
            conditionEffect_one(5, transform.transform.position);
            abnormalityStart[5] = true;
        }
        else if (abnormalityStart[5] && !_moveTPS.poison)
        {
            e_particle.GetComponent<ParticleSystem>().loop = false;
            abnormalityStart[5] = false;
        }
        if (!abnormalityStart[6] && _moveTPS.reverseOp)
        {
            conditionEffect_one(6, transform.transform.position);
            abnormalityStart[6] = true;
        }
        else if (abnormalityStart[6] && !_moveTPS.reverseOp)
        {
            Debug.Log("naotta");
            e_particle.GetComponent<ParticleSystem>().loop = false;
            abnormalityStart[6] = false;
        }
        if (!_BotMode)
        {
            AccelLine(_moveTPS.limitedSpeed, _moveTPS.nowSpeed);
        }
    }
    void Landing()
    {
        if (_GroundCheck.IsGround() && !isGround_pre && !_waterCheck.IsWater())
        {
            Instantiate(landing, new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z), Quaternion.Euler(landing.transform.localEulerAngles));
        }
        isGround_pre = _GroundCheck.IsGround();
    }
    /// <summary>
    /// �W�����̃G�t�F�N�g
    /// </summary>
    /// <param name="_limitedSpeed">���݂̏�����x</param>
    /// <param name="_nowSpeed">���݂̑��x</param>
    void AccelLine(float _limitedSpeed, float _nowSpeed)
    {
        //�Đ����Ă��炸�����𖞂����Ƃ�
        if (!accelLinePlay_flag && (_nowSpeed >= 30))
        {
            eff_accelLine.Play();
            accelLinePlay_flag = true;
        }
        else if(accelLinePlay_flag && (_nowSpeed < 30))
        {
            eff_accelLine.Stop();
            accelLinePlay_flag = false;
        }
        if (accelLinePlay_flag)
        {
            //min30,max130�ŃG�t�F�N�g��ω�������
            float accelRatio = (_nowSpeed - 30) / 100;
            eff_accelLine.startColor = Color32.Lerp(accelLineMinCplor, accelLineMaxCplor, accelRatio);
            _eff_accelLineEmission.rateOverTime = Mathf.Lerp(accelLineMinEmission, accelLineMaxEmission, accelRatio);
        }
        
    }

    IEnumerator conditionEffect(int num)
    {
        bool conditionType = true; ;
        
        
        //�X�Ƌ��p�ɂ���
        while (conditionType)
        {
            if (conditionNum >= conditionPos.Length)
            {
                //�R���鏇�Ԃ��V���b�t��
                for (var i = conditionTurn.Length - 1; i > 0; --i)
                {
                    // 0�ȏ�i�ȉ��̃����_���Ȑ������擾
                    // Random.Range�̍ő�l�͑�2���������Ȃ̂ŁA+1���邱�Ƃɒ���
                    var j = Random.Range(0, i + 1);

                    // i�Ԗڂ�j�Ԗڂ̗v�f����������
                    var tmp = conditionTurn[i];
                    conditionTurn[i] = conditionTurn[j];
                    conditionTurn[j] = tmp;
                }
                conditionNum = 0;
            }
            GameObject e_burn = Instantiate(abnormality[num], this.transform.localPosition + conditionPos[conditionTurn[conditionNum]], Quaternion.Euler(abnormality[num].transform.localEulerAngles));
            e_burn.transform.parent = this.gameObject.transform;
            conditionNum += 1;
            yield return new WaitForSeconds(0.3f);
            switch (num)
            {
                case 0:
                    conditionType = _moveTPS.burn;
                    break;
                case 1:
                    conditionType = _moveTPS.frostbite;
                    break;
            }
        }
    }

    void conditionEffect_one(int num, Vector3 pos)
    {
        e_particle = Instantiate(abnormality[num], pos, Quaternion.Euler(abnormality[num].transform.localEulerAngles));
        e_particle.transform.parent = this.gameObject.transform;
        abnormalityStart[num] = true;
    }
}
