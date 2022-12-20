using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE_running : MonoBehaviour
{
    [SerializeField] AudioSource audioSource_running;
    [SerializeField] moveTPS _moveTPS;

    bool playCheck = true;

    // Update is called once per frame
    void Update()
    {
        //ゲーム終了、ポーズ時音停止
        if (Time.timeScale == 0 || gamePause.pause)
        {
            audioSource_running.Pause();
            playCheck = false;
        }
        else
        {
            playSE();
        }
    }
    void playSE()
    {
        float audioPitch = Mathf.Lerp(0, 1, _moveTPS.nowSpeed * 0.7f / _moveTPS.limitedSpeed) * 2f;
        if (_moveTPS.sleep||_moveTPS.nowHP<=0)
        {
            //睡眠状態のとき,やられたとき速度0
            audioPitch = 0;
        }
        audioSource_running.pitch = audioPitch;
        if (audioPitch <= 0.01f)
        {
            audioSource_running.Pause();
            playCheck = false;
        }
        else if (!playCheck)
        {
            audioSource_running.Play();
            playCheck = true;
        }
        if (audioSource_running.time > 1.6)
        {
            audioSource_running.time = 0.1f;
            audioSource_running.Play();
        }
    }
    IEnumerator runningSE()
    {
        while (true)
        {
            yield return new WaitForSeconds(4);
            
            audioSource_running.time = 0.76f;
            audioSource_running.Play();
        }
        
    }
}
