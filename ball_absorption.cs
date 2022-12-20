using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball_absorption : MonoBehaviour
{
    public GameObject mainBall;
    ParticleSystem particleSet;
    float particleBall;
    float particleBallRate=0f;
    float _nowHP;
    // Start is called before the first frame update
    void Start()
    {
        particleSet = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        absorptionRate();
    }
    void absorptionRate()
    {
        var _emission = particleSet.emission;
        particleBall = mainBall.GetComponent<moveTPS>().color_lerp_set;
        _nowHP = mainBall.GetComponent<moveTPS>().nowHP;
        particleBallRate += Time.deltaTime*40f;
        if (particleBall > 0f && particleBall<0.9f && _nowHP > 0.0f)
        {
            if (particleBallRate < 30f)
            {
                _emission.rateOverTime = particleBallRate;
            }
            else
            {
                _emission.rateOverTime = 30f;
            }
        }
        
        else
        {
            _emission.rateOverTime = 0f;
            particleBallRate = 0f;
        }
    }
}
