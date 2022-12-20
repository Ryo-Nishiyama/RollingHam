using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFinish : MonoBehaviour
{
    AudioSource _AudioSource;
    public GameTime _GameTime;
    public moveTPS _moveTPS;
    [SerializeField] GameObject[] _mainBallBot;
    [SerializeField] ChangeScene _ChangeScene;
    [SerializeField] goalCount _goalCount;
    [SerializeField] AudioClip a_finish;
    public static int[] EnhanceCount = new int[] { 0, 0, 0, 0, 0 };
    static public int[] EnhanceCountInitial = new int[] { 0, 0, 0, 0, 0 };
    public static int[][] EnhanceBotCount = new int[][] { new int[] { 0, 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0, 0 } };
    static public int[][] EnhanceBotCountInitial = new int[][] { new int[] { 0, 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0, 0 } };
    public static int NumCount = 0;
    public static int[] NumBotCount = new int[] { 0, 0, 0 };
    float finishStop = 0;
    public bool finishCheck=false;
    bool finishColCheck = false;
    string SceneName;

    // Start is called before the first frame update
    void Start()
    {
        
        _AudioSource = GetComponent<AudioSource>();
        SceneName = SceneManager.GetActiveScene().name;
        if (SceneName== "rainEffectScene")
        {
            EnhanceCount = new int[] { 0, 0, 0, 0, 0 };
            EnhanceBotCount = new int[][] { new int[] { 0, 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0, 0 } };
            NumCount = 0;
            NumBotCount = new int[] { 0, 0, 0 };
        }
        if ((SceneName == "race" || SceneName == "race2")&&raceSet.raceMode)
        {
            for(int i = 0; i < EnhanceCount.Length; i++)
            {
                EnhanceCount[i] = EnhanceCountInitial[i];
            }
            for(int i = 0; i < EnhanceBotCount.Length; i++)
            {
                for (int j = 0; j < EnhanceBotCount[0].Length; j++)
                {
                    EnhanceBotCount[i][j] = EnhanceBotCountInitial[i][j];
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //複数玉があるとき対策で強化数を保存
        if (!finishCheck || SceneName == "game" || SceneName == "race" || SceneName == "race2")
        {
            //ゲーム終了時に値の更新を終了して初期化を防ぐ
            EnhanceCount[0] = _moveTPS.itemEnhanceCount[0];
            EnhanceCount[1] = _moveTPS.itemEnhanceCount[1];
            EnhanceCount[2] = _moveTPS.itemEnhanceCount[2];
            EnhanceCount[3] = _moveTPS.itemEnhanceCount[3];
            EnhanceCount[4] = _moveTPS.itemEnhanceCount[4];
            NumCount = _moveTPS.itemNumCount;
            for(int i = 0; i < _mainBallBot.Length; i++)
            {
                if (_mainBallBot[i].activeSelf)
                {
                    for(int j = 0; j < EnhanceCount.Length; j++)
                    {
                        EnhanceBotCount[i][j] = _mainBallBot[i].GetComponent<moveTPS>().itemEnhanceCount[j];
                    }
                    NumBotCount[i] = _mainBallBot[i].GetComponent<moveTPS>().itemNumCount;
                }
            }
        }
        if (_GameTime.gameFinish && !finishColCheck)
        {
            StartCoroutine(loadFieldResult());
        }
        else if (_goalCount.goal && !finishColCheck)
        {
            
            StartCoroutine(loadRaceResult());
        }
    }
    IEnumerator loadFieldResult() 
    {
        finishColCheck = true;
        finishCheck = true;
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(2);
        _moveTPS.enabled = false;
        finishCheck = false;
        Debug.Log("change");
        _ChangeScene.result_load();
        _ChangeScene.NextScene();
        _ChangeScene.fadeOutNoTime();
    }
    IEnumerator loadRaceResult()
    {
        finishColCheck = true;
        finishCheck = true;
        _AudioSource.PlayOneShot(a_finish);
        yield return new WaitForSeconds(2);
        finishCheck = false;
        _ChangeScene.raceResult_load();
        _ChangeScene.fadeOutNoTime();
    }
    private void Awake()
    {
        //fpsを60に固定
        Application.targetFrameRate = 60;
    }
}
