using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE_menu : MonoBehaviour
{
    GameObject SE;
    public AudioSource _AudioSource;
    [SerializeField] AudioClip a_start;
    [SerializeField] AudioClip a_cancel;
    public bool startCheck, cancelCheck;
    int _sceneNum;
    private void Start()
    {
        _AudioSource = GetComponent<AudioSource>();
        SE = GameObject.Find("changeSceneSE");
        //既にBGMオブジェクトがあるとき破棄する
        if (SE != null && (SE != this.gameObject))
        {
            Destroy(this.gameObject);
        }

        _sceneNum = ChangeScene.sceneNum;
        DontDestroyOnLoad(this.gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        if (cancelCheck)
        {
            cancelCheck = false;
            _AudioSource.PlayOneShot(a_cancel);
        }
        if (startCheck)
        {
            startCheck = false;
            _AudioSource.PlayOneShot(a_start);
        }
    }
    public void playOneShotSE(AudioClip SE)
    {
        _AudioSource.PlayOneShot(SE);
        
    }
}
