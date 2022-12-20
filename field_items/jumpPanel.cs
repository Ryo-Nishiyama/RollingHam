using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpPanel : MonoBehaviour
{
    ParticleSystem tornado;
    float startTimer = 0;
    AudioClip jump_wind;
    AudioSource[] wind;
    bool start = false;
    bool finish=false;
    float finishTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        jump_wind = (AudioClip)Resources.Load("SE\\jump_wind");
        wind = GetComponents<AudioSource>();
        for(int i = 0; i < wind.Length; i++)
        {
            wind[i].clip = jump_wind;
        }
        
        wind[1].time = 0.5f;
        this.tag = "Ground";
        GetComponent<CapsuleCollider>().enabled = false;
        tornado = transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        tornado.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        startPanel();
    }
    void startPanel()
    {
        startTimer += Time.deltaTime;
        //ランダムで起動
        if (startTimer > 5 && Random.Range(0, 100) == 0 && this.tag != "jumpPanel")
        {
            this.tag = "jumpPanel";
            GetComponent<CapsuleCollider>().enabled = true;
            tornado.Play();
            for (int i = 0; i < wind.Length; i++)
            {
                wind[i].Play();
            }
            start = true;
            wind[1].time = 0.5f;
        }
        if (start)
        {
            finishTimer += Time.deltaTime;
            for (int i = 0; i < wind.Length; i++)
            {
                wind[i].volume=finishTimer;
            }
            if (finishTimer > 1)
            {
                finishTimer = 1;
                start = false;
            }
        }
        //一定時間+ランダムで終了、サイクルを戻す
        if(startTimer > 10 && Random.Range(0, 100) == 0)
        {
            this.tag = "Ground";
            GetComponent<CapsuleCollider>().enabled = false;
            startTimer = 0;
            tornado.Stop();
            finish = true;
        }
        if (finish)
        {
            finishTimer -= Time.deltaTime;
            for (int i = 0; i < wind.Length; i++)
            {
                wind[i].volume = finishTimer;
            }
            if (finishTimer < 0)
            {
                wind[0].Stop();
                wind[1].Stop();
                finish = false;
            }
        }
    }
}
