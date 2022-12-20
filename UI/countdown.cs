using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class countdown : MonoBehaviour
{
    public Image circleImg;
    public GameTime _GameTime;
    public gameTimer _gameTimer;
    public TextMeshProUGUI countDownTxt;
    public float realTime;
    [SerializeField] GameFinish _GameFinish;
    [SerializeField] goalCount _goalCount;
    [SerializeField] Image startImg;
    [SerializeField] Image finishImg;
    [SerializeField] Image[] backImgs;
    [SerializeField] Image[] circleImgs;

    AudioSource _AudioSource;
    [SerializeField] AudioClip a_start;
    [SerializeField] AudioClip a_finish;
    [SerializeField] AudioClip a_countdown;

    float startTime;
    string sceneName;
    static public bool startGameCheck = false;
    bool startCheck = false;
    bool[] countdownStartCheck = new bool[] { false, false, false, false };
    bool[] countdownFinishCheck = new bool[] { false, false, false,false };

    [SerializeField] bool debug;
    // Start is called before the first frame update
    void Start()
    {
        _AudioSource = GetComponent<AudioSource>();
        Time.timeScale=1;
        //マシンスペックで変わる時間を保存+始まりに余裕を持たせる
        startTime = Time.realtimeSinceStartup+1;
        sceneName = SceneManager.GetActiveScene().name;
        finishImg.enabled = false;
        startGameCheck = false;
        if (debug)
        {
            startGameCheck = true;
            circleImg.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //ゲーム開始カウント
        realTime = Time.realtimeSinceStartup - startTime;
        //テキストを消すまでの間動かす
        if (!startCheck)
        {
            startCountDown();
        }
        
        //ゲーム終了を表示
        if (sceneName == "game")
        {
            //ゲーム終了カウント
            float remainingTime = _GameTime.gameTimer - _GameTime.timeCounter;
            if (remainingTime < 3 && remainingTime >= 0)
            {
                
                if (remainingTime < 1)
                {
                    if (!countdownFinishCheck[0])
                    {
                        countdownFinishCheck[0] = true;
                        countDownTxt.text = "1";
                        _AudioSource.PlayOneShot(a_countdown);
                    }
                }
                else if (remainingTime < 2)
                {
                    if (!countdownFinishCheck[1])
                    {
                        countdownFinishCheck[1] = true;
                        countDownTxt.text = "2";
                        _AudioSource.PlayOneShot(a_countdown);
                    }
                }
                else if (remainingTime < 3)
                {
                    if (!countdownFinishCheck[2])
                    {
                        for (int i = 0; i < circleImgs.Length; i++)
                        {
                            circleImgs[i].enabled = true;
                        }
                        countdownFinishCheck[2] = true;
                        countDownTxt.text = "3";
                        _AudioSource.PlayOneShot(a_countdown);
                    }
                }
                for (int i = 0; i < circleImgs.Length; i++)
                {
                    circleImgs[i].fillAmount = remainingTime - Mathf.Floor(remainingTime);
                }
            }
            if (_GameFinish.finishCheck && !countdownFinishCheck[3])
            {
                for (int i = 0; i < circleImgs.Length; i++)
                {
                    circleImgs[i].enabled = false;
                }
                countdownFinishCheck[3] = true;
                finishImg.enabled = true;
                _AudioSource.PlayOneShot(a_finish);
            }
        }
        if (sceneName == "race"|| sceneName == "race2")
        {
            if (_goalCount.goal)
            {
                finishImg.enabled = true;
            }
        }
    }
    void startCountDown()
    {
        if (realTime < 0)
        {
            countDownTxt.text = "";
            circleImg.enabled = false;
        }
        else if (realTime < 1)
        {
            if (!countdownStartCheck[0])
            {
                for (int i = 0; i < circleImgs.Length; i++)
                {
                    circleImgs[i].enabled = true;
                }
                countdownStartCheck[0] = true;
                backImgs[1].fillClockwise = true;
                countDownTxt.text = "3";
                _AudioSource.PlayOneShot(a_countdown);
            }
            backImgs[0].fillAmount = 1 - realTime % 1;
            backImgs[1].fillAmount = realTime % 1;
        }
        else if (realTime < 2)
        {
            if (!countdownStartCheck[1])
            {
                countdownStartCheck[1] = true;
                backImgs[0].enabled = false;
                backImgs[1].fillClockwise = false;
                backImgs[2].fillClockwise = true;
                countDownTxt.text = "2";
                _AudioSource.PlayOneShot(a_countdown);
            }
            backImgs[1].fillAmount = 1 - realTime % 1;
            backImgs[2].fillAmount = realTime % 1;
        }
        else if (realTime < 3)
        {
            if (!countdownStartCheck[2])
            {
                countdownStartCheck[2] = true;
                backImgs[1].enabled = false;
                backImgs[2].fillClockwise = false;
                countDownTxt.text = "1";
                _AudioSource.PlayOneShot(a_countdown);
            }
            backImgs[2].fillAmount = 1 - realTime % 1;
        }
        else if (realTime < 4)
        {
            if (!countdownStartCheck[3])
            {
                for (int i = 0; i < circleImgs.Length; i++)
                {
                    circleImgs[i].enabled = false;
                }
                backImgs[2].enabled = false;
                countdownStartCheck[3] = true;
                //カウント終了後ゲームスタート
                countDownTxt.text = "";
                startImg.enabled = true;
                startGameCheck = true;
                _AudioSource.PlayOneShot(a_start);
            }
        }
        else if (countdownStartCheck[0] && countdownStartCheck[1] && countdownStartCheck[2] && countdownStartCheck[3])
        {
            //画像を非表示にする
            startImg.enabled = false;
            startCheck = true;
        }
    }
}
