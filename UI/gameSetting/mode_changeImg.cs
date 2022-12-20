using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mode_changeImg : MonoBehaviour
{
    [SerializeField] buttonSelector _buttonSelector;
    //各々二枚以上必要
    [SerializeField] List<Image> trainingImgs;
    [SerializeField] List<Image> raceImgs;
    [SerializeField] List<Image> operationImgs;
    [SerializeField] List<Image> creditImgs;
    [SerializeField] List<Image> closeImgs;
    List<List<Image>> allImg = new List<List<Image>>();

    int buttonCounterPre = 0;
    bool endCoroutine = false;

    float changeSpeed = 1f;
    float changeTime = 0;
    int nowImgNum = 0;

    Coroutine _changeWaitImag;

    // Start is called before the first frame update
    void Start()
    {
        //画像をひとまとめにする
        allImg.Add(trainingImgs);
        allImg.Add(raceImgs);
        allImg.Add(operationImgs);
        allImg.Add(creditImgs);
        allImg.Add(closeImgs);
        buttonCounterPre = _buttonSelector.buttonCounter;
        _changeWaitImag = StartCoroutine(changeWaitImg(_buttonSelector.buttonCounter));
    }

    // Update is called once per frame
    void Update()
    {
        if (endCoroutine)
        {
            endCoroutine = false;
        }
        if (buttonCounterPre != _buttonSelector.buttonCounter)
        {
            changeButton();
            //古いコルーチンを止める
            StopCoroutine(_changeWaitImag);
            _changeWaitImag = StartCoroutine(changeWaitImg(_buttonSelector.buttonCounter));

        }

    }
    private void LateUpdate()
    {
        buttonCounterPre = _buttonSelector.buttonCounter;
    }

    IEnumerator changeWaitImg(int counter)
    {
        while (counter == _buttonSelector.buttonCounter)
        {
            yield return new WaitForSeconds(4);
            nowImgNum += 1;
            changeTime = 0;
            if (nowImgNum >= allImg[_buttonSelector.buttonCounter].Count)
            {
                nowImgNum = 0;
            }
            
            StartCoroutine(changeImg(nowImgNum, _buttonSelector.buttonCounter));
        }
        
    }
    IEnumerator changeImg(int nowNum,int Num)
    {
        
        //画像のクロスディゾルブ
        while (changeTime < changeSpeed && !endCoroutine)
        {
            changeTime += Time.deltaTime;
            float nowAlpha = changeTime / changeSpeed;
            //古い画像を薄く
            if (nowNum == 0)
            {
                allImg[Num][allImg[Num].Count-1].color = new Color32(255, 255, 255, (byte)((1 - nowAlpha) * 255));
            }
            else
            {
                allImg[Num][nowNum-1].color = new Color32(255, 255, 255, (byte)((1 - nowAlpha) * 255));
            }
            //新しい画像を濃く
            allImg[Num][nowNum].color = new Color32(255, 255, 255, (byte)((nowAlpha) * 255));
            
            yield return null;
        }

    }
    void changeButton()
    {
        //ボタンが変わったとき画像も切り替えてリセット,alpha書き換え
        changeTime = 0;
        nowImgNum = 0;
        for(int i = 0; i < allImg[buttonCounterPre].Count; i++)
        {
            allImg[buttonCounterPre][i].color = new Color32(255, 255, 255, 0);
        }
        //次に映す画像を255にする
        allImg[_buttonSelector.buttonCounter][nowImgNum].color = new Color32(255, 255, 255, 255);
        endCoroutine = true;
    }
}
