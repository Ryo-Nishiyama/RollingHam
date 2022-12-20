using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    GameObject SE;
    AsyncOperation async;
    [SerializeField] GameObject loadUI;
    [SerializeField] GameObject loadGauge;
    [SerializeField] Slider slider;
    [SerializeField] GameObject tips;
    CanvasGroup _CanvasGroup;
    string[] sceneName = new string[] { "gameSetting", "selectBall", "game", "gameOption",
                                        "rainEffectScene", "game_result", "race", "race_result",
                                        "race_select", "race2","stage_select","tips","credit" };
    static public int sceneNum = 0;
    float loadUItransparent;
    float loadUItime;
    bool fadeOutCheck = false;
    bool oneFade = false;
    AudioSource audioSource;
    public AudioClip a_cancel, a_start, a_select;

    private void Start()
    {
        _CanvasGroup = tips.GetComponent<CanvasGroup>();
        oneFade = false;
        SE = GameObject.Find("changeSceneSE");
        audioSource = GetComponent<AudioSource>();
        
    }
    private void Update()
    {
        if (fadeOutCheck)
        {
            fadeOut();
        }
    }
    public void NextScene()
    {
        loadGauge.SetActive(true);
        //コルーチン開始
        StartCoroutine("LoadData");
        loadUItransparent = 0;
        loadUItime = 0;
    }
    public void startGame()
    {
        sceneNum = 0;
    }
    public void selectBall()
    {
        sceneNum = 1;
    }
    public void startGame_load()
    {
        sceneNum = 2;
    }
    public void SettingLoad()
    {
        sceneNum = 3;
    }
    public void returnTitle()
    {
        sceneNum = 4;
    }
    public void result_load()
    {
        sceneNum = 5;
    }
    public void race_load()
    {
        sceneNum = 6;
    }
    public void raceResult_load()
    {
        sceneNum = 7;
    }
    public void raceSelect_load()
    {
        sceneNum = 8;
    }
    public void race2_load()
    {
        sceneNum = 9;
    }
    public void stageSelect_load()
    {
        sceneNum = 10;
    }
    public void tips_load()
    {
        sceneNum = 11;
    }
    public void credit_load()
    {
        sceneNum = 12;
    }
    public void SE_start()
    {
        //シーン遷移でのSEの消滅を防ぐため別オブジェクトで鳴らす
        if (SE != null)
        {
            SE.GetComponent<SE_menu>().startCheck = true;
        }
        else
        {
            audioSource.PlayOneShot(a_start);
        }
    }
    public void SE_cancel()
    {
        if (SE != null)
        {
            SE.GetComponent<SE_menu>().cancelCheck = true;
        }
        else
        {
            audioSource.PlayOneShot(a_cancel);
        }
        
    }
    public void SE_select()
    {
        audioSource.PlayOneShot(a_select);
    }
    public void fadeOut()
    {
        fadeOutCheck = true;
        loadUI.SetActive(true);
        //0.25秒でフェードアウト完了
        if (loadUItime < 0.25f)
        {
            //timeScale0でも動く
            loadUItime += Time.unscaledDeltaTime;
            loadUItransparent = Mathf.Lerp(0, 255, loadUItime / 0.25f);
            loadUI.GetComponent<Image>().color = new Color32(255, 255, 255, (byte)loadUItransparent);
            _CanvasGroup.alpha = ((float)loadUItransparent) / 255;
        }
        else if(!oneFade)
        {
            //フェード完了後遷移開始
            NextScene();
            fadeOutCheck=false;
            //複数回処理を防止
            oneFade = true;
        }
    }
    //フェードの画像を挟まずすぐに移行する
    public void fadeOutNoTime()
    {
        fadeOutCheck = true;
        if (!oneFade)
        {
            NextScene();
            fadeOutCheck = false;
            oneFade = true;
        }
    }
    IEnumerator LoadData()
    {
        async = SceneManager.LoadSceneAsync(sceneName[sceneNum]);
        //NowLoadingのゲージを変化
        while (!async.isDone)
        {
            var progressVal = Mathf.Clamp01(async.progress / 0.9f);
            slider.value = progressVal;
            yield return null;
        }
    }
}
