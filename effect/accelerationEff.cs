using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class accelerationEff : MonoBehaviour
{
    [SerializeField] moveTPS _moveTPS;
    [SerializeField] GameObject mainBall;
    Rigidbody rb;
    [SerializeField] GameObject[] accelParticleObj;
    ParticleSystem[] accelParticles;

    bool chargeParticleCheck = false;
    bool accelParticleCheck = false;
    ParticleSystem.MainModule[] accelParticles_main;
    ParticleSystem.EmissionModule[] accelParticles_emission;
    private void OnDestroy()
    {
        _moveTPS = null;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = mainBall.GetComponent<Rigidbody>();
        accelParticles = new ParticleSystem[accelParticleObj.Length];
        accelParticles_main = new ParticleSystem.MainModule[accelParticleObj.Length];
        accelParticles_emission = new ParticleSystem.EmissionModule[accelParticleObj.Length];
        for (int i = 0; i < accelParticleObj.Length; i++)
        {
            accelParticles[i] = accelParticleObj[i].GetComponent<ParticleSystem>();
            accelParticles_main[i] = accelParticles[i].main;
            accelParticles_emission[i] = accelParticles[i].emission;
            accelParticles[i].Stop();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        changeRotate();
        chargeEffChange();
        accelEffChange();
    }
    void changeRotate()
    {
        float nowRotate_x = Mathf.Lerp(-90, 90, _moveTPS.UDdir.y / 2 + 0.5f);
        //チャージ落下中は落下加速に向きを固定
        if(_moveTPS.R2 || _moveTPS.R2_e || _moveTPS._R2Bot)
        {
            nowRotate_x = -90;
        }
        for(int i = 0; i < accelParticleObj.Length; i++)
        {
            accelParticleObj[i].transform.localEulerAngles = new Vector3(nowRotate_x, 180, 0);
        }
    }
    void chargeEffChange()
    {
        //ある程度チャージ加速力があるとき再生
        if (_moveTPS.addPower > _moveTPS.maxCharge * 0.3f && _moveTPS.nowHP>0)
        {
            if (!chargeParticleCheck)
            {
                accelParticles[0].Play();
                chargeParticleCheck = true;
            }
            //チャージ量に合わせて放出量を減少
            float rateOverTime = Mathf.Lerp(10.0f, 60.0f, _moveTPS.addPower/ _moveTPS.maxCharge);
            accelParticles_emission[0].rateOverTime = rateOverTime;
        }
        //加速が弱まったら停止
        else if(chargeParticleCheck)
        {
            accelParticles[0].Stop();
            chargeParticleCheck = false;
        }
    }
    void accelEffChange()
    {
        if (rb.velocity.magnitude > 25)
        {
            if (!accelParticleCheck)
            {
                accelParticles[1].Play();
                accelParticleCheck = true;
            }
            float startSpeed = Mathf.Lerp(7.5f, 15.0f, (rb.velocity.magnitude - 25) / 30);
            float rateOverTime = Mathf.Lerp(20.0f, 60.0f, (rb.velocity.magnitude - 25) / 30);
            accelParticles_main[1].startSpeed = startSpeed;
            accelParticles_emission[1].rateOverTime = rateOverTime;

        }
        else if (accelParticleCheck)
        {
            accelParticles[1].Stop();
            accelParticleCheck = false;
        }
    }
    float vectorComposition()
    {
        float PlaneVec = Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.z);
        float HeighVec = Mathf.Abs(rb.velocity.y);
        //速度からベクトルを合成して角度を求める
        float radian = Mathf.Sqrt(Mathf.Pow(PlaneVec, 2) + Mathf.Pow(HeighVec, 2) + 2 * (PlaneVec * HeighVec * Mathf.Cos(90)));
        return radian;
    }
}
