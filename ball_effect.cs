using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball_effect : MonoBehaviour
{
    public GameObject mainBall;
    ParticleSystem particleSet;
    float particleBall;
    float particleBallRate = 0f;
    float _nowHP;
    bool moduleEnabled = false;

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
        particleBall = mainBall.GetComponent<moveTPS>().color_lerp_set;
        _nowHP = mainBall.GetComponent<moveTPS>().nowHP;
        var _emission = particleSet.emission;
        if (particleBall > 0.6f && _nowHP > 0.0f)
        {
            moduleEnabled = true;
        }

        else
        {
            moduleEnabled = false;
        }
        _emission.enabled = moduleEnabled;
    }
}
